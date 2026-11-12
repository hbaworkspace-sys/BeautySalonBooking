using BeautySalonBooking.Domain.RequestAggregate.Enums;
using BeautySalonBooking.Domain.RequestAggregate.Repositories;
using RequestEntity = BeautySalonBooking.Domain.RequestAggregate.Entities.Request;
using PersonRequestDetailEntity = BeautySalonBooking.Domain.RequestAggregate.Entities.PersonRequestDetail;
using RequestDocumentEntity = BeautySalonBooking.Domain.RequestAggregate.Entities.RequestDocument;
using BeautySalonBooking.Application.Request.DTOs;
using BeautySalonBooking.Domain.Base.UnitOfWork;
using BeautySalonBooking.Application.Request.Interfaces;
using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Domain.BranchAggregate.Repositories;
using BeautySalonBooking.Domain.OrganizationAggregate.Repositories;
using BeautySalonBooking.Domain.RequestAggregate.Entities;
using BeautySalonBooking.Domain.OrganizationAggregate.Entities;
using BeautySalonBooking.Contracts.Branch.BranchService.Enums;
using BeautySalonBooking.Domain.Base.Enums;
using BeautySalonBooking.Domain.BranchAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Repositories;
using BeautySalonBooking.Domain.PersonAggregate.Repositories;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Entities;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using BeautySalonBooking.Domain.Identity.UserAggregate.Enums;
using BeautySalonBooking.Domain.Repositories;
using BeautySalonBooking.Domain.ContactAggregate.Entities;

namespace BeautySalonBooking.Application.Requests.Services
{

    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IRequestTypeRepository _requestTypeRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IRequestRequiredDocumentRepository _requiredDocumentRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IOrganizationDetailRepository _organizationDetailRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IBranchMemberRepository _branchMemberRepository;
        private readonly IBranchRoleRepository _branchRoleRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RequestService(
            IRequestRepository requestRepository,
            IRequestTypeRepository requestTypeRepository,
            IUserRepository userRepository,
            IOrganizationDetailRepository organizationDetailRepository,
            IDocumentTypeRepository documentTypeRepository,
            IOrganizationRepository organizationRepository,
            IBranchRepository branchRepository,
            IBranchRoleRepository branchRoleRepository,
            IPersonRepository personRepository,
            IBranchMemberRepository branchMemberRepository,
            IRoleRepository roleRepository,
            IUserRoleRepository userRoleRepository,
            IRequestRequiredDocumentRepository requiredDocumentRepository,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _requestRepository = requestRepository;
            _requestTypeRepository = requestTypeRepository;
            _documentTypeRepository = documentTypeRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _requiredDocumentRepository = requiredDocumentRepository;
            _organizationRepository = organizationRepository;
            _branchRepository = branchRepository;
            _organizationDetailRepository = organizationDetailRepository;
            _branchMemberRepository = branchMemberRepository;
            _branchRoleRepository = branchRoleRepository;
            _personRepository = personRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<long> CreateFullRequestAsync(CreateFullRequestDto dto, CancellationToken ct = default)
        {
            var currentUserId = _currentUserService.GetCurrentUserId();
            if (!currentUserId.HasValue)
                throw new UnauthorizedAccessException("کاربر لاگین نکرده است.");

            var requestType = await _requestTypeRepository.GetByIdAsync(dto.RequestTypeId, ct);
            if (requestType == null) throw new Exception("نوع درخواست یافت نشد.");

            // 👇 ========== اعتبارسنجی مدارک اجباری ==========
            var requiredDocuments = await _requiredDocumentRepository
                .GetByRequestTypeIdAsync(dto.RequestTypeId, ct);

            var uploadedDocumentTypeIds = dto.Documents
                .Select(x => x.DocumentTypeId)
                .Distinct()
                .ToList();

            var missingRequiredDocuments = requiredDocuments
                .Where(rd => rd.IsRequired && !uploadedDocumentTypeIds.Contains(rd.DocumentTypeId))
                .Select(rd => rd.DocumentType.Title) // یا rd.DocumentTypeId
                .ToList();

            if (missingRequiredDocuments.Any())
            {
                var missingList = string.Join("، ", missingRequiredDocuments);
                throw new Exception($"وارد نمودن مدارک زیر اجباری است: {missingList}");
            }
            // 👆 ============================================

            var trackingCode = "REQ-" + Guid.NewGuid().ToString("N")[..6].ToUpper();
            var request = new RequestEntity(
                currentUserId.Value,
                dto.RequestTypeId,
                trackingCode,
                dto.InitialStatus,
                DateTime.UtcNow
            );

            // دیتیل شخص
            if (!string.IsNullOrEmpty(dto.Address) || !string.IsNullOrEmpty(dto.LicenseNo))
            {
                var personDetail = new PersonRequestDetailEntity(
                    dto.Address, dto.CodePosti, dto.LicenseNo, dto.Biography, dto.ExperienceYears,
                    null, null, null, null, null
                );
                request.SetPersonRequestData(personDetail);
            }

            // مدارک (Base64)
            foreach (var docUpload in dto.Documents)
            {
                byte[] fileBytes;
                try
                {
                    var base64Data = docUpload.Base64Content.Contains(",")
                        ? docUpload.Base64Content.Split(',')[1]
                        : docUpload.Base64Content;
                    fileBytes = Convert.FromBase64String(base64Data);
                }
                catch (FormatException)
                {
                    throw new Exception($"فرمت Base64 برای فایل {docUpload.FileName} نامعتبر است.");
                }

                var requestDoc = new RequestDocumentEntity(
                    docUpload.DocumentTypeId,
                    docUpload.FileName,
                    docUpload.ContentType,
                    fileBytes
                );
                request.AddDocument(requestDoc);
            }

            request.AddInitialHistory(currentUserId.Value, dto.Description);

            await _requestRepository.AddAsync(request, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return request.Id;
        }

        public async Task ChangeStatusAsync(ChangeRequestStatusDto dto, CancellationToken ct = default)
        {
            // 👇 گرفتن شناسه کاربر جاری (کسی که وضعیت را تغییر می‌دهد)
            var currentUserId = _currentUserService.GetCurrentUserId();
            if (!currentUserId.HasValue)
                throw new UnauthorizedAccessException("کاربر لاگین نکرده است.");

            var request = await _requestRepository.GetByIdWithIncludesAsync(dto.RequestId, ct, x => x.Histories);
            if (request == null) throw new Exception("درخواست یافت نشد.");

            // 👇 تغییر وضعیت و ثبت تاریخچه
            request.ChangeStatus(dto.NewStatus, currentUserId.Value, dto.Action, dto.Description);

            _requestRepository.Update(request);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task<List<RequestListDto>> GetAllAsync(CancellationToken ct = default)
        {
            var list = await _requestRepository.GetAllAsync(ct);
            return list.Select(x => new RequestListDto
            {
                Id = x.Id,
                TrackingCode = x.TrackingCode,
                RequestTypeTitle = x.RequestType?.Title ?? "نامشخص",
                Status = x.Status.ToString(),
                SubmittedAt = x.SubmittedAt,
                DocumentsCount = x.Documents.Count
            }).ToList();
        }


        public async Task<long> CreateOrganizationUpgradeRequestAsync(CreateOrganizationUpgradeRequestDto dto, CancellationToken ct = default)
        {
            try
            {
                var currentUserId = _currentUserService.GetCurrentUserId();
                if (!currentUserId.HasValue)
                    throw new UnauthorizedAccessException("کاربر لاگین نکرده است.");

                // پیدا کردن RequestType با کد مشخص
                var requestType = await _requestTypeRepository.GetByCodeAsync("ORG_UPGRADE", ct);
                if (requestType == null)
                    throw new Exception("نوع درخواست ارتقا به سالن تعریف نشده است.");

                // اعتبارسنجی مدارک اجباری
                var requiredDocuments = await _requiredDocumentRepository.GetByRequestTypeIdAsync(requestType.Id, ct);
                var uploadedIds = dto.Documents.Select(x => x.DocumentTypeId).Distinct().ToList();
                var missing = requiredDocuments
                    .Where(rd => rd.IsRequired && !uploadedIds.Contains(rd.DocumentTypeId))
                    .Select(rd => rd.DocumentType.Title)
                    .ToList();

                if (missing.Any())
                    throw new Exception($"وارد نمودن مدارک زیر اجباری است: {string.Join("، ", missing)}");

                // ساخت درخواست
                var trackingCode = "ORG-" + Guid.NewGuid().ToString("N")[..6].ToUpper();
                var request = new RequestEntity(
                    currentUserId.Value,
                    requestType.Id,
                    trackingCode,
                    RequestStatus.Submitted,
                    DateTime.UtcNow
                );

                // پر کردن OrganizationRequestDetail
                var orgDetail = new OrganizationRequestDetail(

                    organizationName: dto.OrganizationName,
                    organizationDescription: dto.OrganizationDescription,
                    phoneNumber: dto.PhoneNumber,
                    mobileNumber: dto.MobileNumber,
                    website: dto.Website,
                    instagram: dto.Instagram,
                    email: dto.Email,
                    whatsapp: dto.Whatsapp,
                    branchName: dto.BranchName,
                    branchDescription: dto.BranchDescription,
                    regionId: dto.RegionId,
                    address: dto.Address,
                    postalCode: dto.PostalCode,
                    licenseNumber: dto.LicenseNumber,
                    licenseIssuedDate: dto.LicenseIssuedDate,
                    licenseExpireDate: dto.LicenseExpireDate,
                    slogan: dto.Slogan,
                    additionalInfo: dto.AdditionalInfo
                );
                request.SetOrganizationRequestData(orgDetail);

                // اضافه کردن مدارک (Base64)
                foreach (var doc in dto.Documents)
                {
                    byte[] fileBytes;
                    try
                    {
                        var base64Data = doc.Base64Content.Contains(",")
                            ? doc.Base64Content.Split(',')[1]
                            : doc.Base64Content;
                        fileBytes = Convert.FromBase64String(base64Data);
                    }
                    catch (FormatException)
                    {
                        throw new Exception($"فرمت Base64 برای فایل {doc.FileName} نامعتبر است.");
                    }

                    var requestDoc = new RequestDocumentEntity(
                        doc.DocumentTypeId,
                        doc.FileName,
                        doc.ContentType,
                        fileBytes
                    );
                    request.AddDocument(requestDoc);
                }

                request.AddInitialHistory(currentUserId.Value, "درخواست ارتقا به سالن ثبت شد.");

                await _requestRepository.AddAsync(request, ct);
                await _unitOfWork.SaveChangesAsync(ct);

                return request.Id;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task ApproveOrganizationUpgradeRequestAsync(ApproveOrganizationUpgradeRequestDto dto, CancellationToken ct = default)
        {
            await _unitOfWork.BeginTransactionAsync(ct);
            try
            {
                var currentUserId = _currentUserService.GetCurrentUserId();
                if (!currentUserId.HasValue)
                    throw new UnauthorizedAccessException("کاربر لاگین نکرده است.");

                // 1. گرفتن درخواست به همراه دیتیل و مدارک
                var request = await _requestRepository.GetByIdWithIncludesAsync(
                    dto.RequestId, ct,
                    x => x.OrganizationRequestData,
                    x => x.Documents
                );
                var clientUser = await _userRepository.GetByIdAsync(request.UserId);

                if (request == null) throw new Exception("درخواست یافت نشد.");
                if (request.Status == RequestStatus.Approved) throw new Exception("این درخواست قبلاً تایید شده است.");
                if (request.OrganizationRequestData == null) throw new Exception("اطلاعات ارگان در درخواست وجود ندارد.");

                var orgData = request.OrganizationRequestData;

                // 2. ساخت Organization
                var organization = new BeautySalonBooking.Domain.OrganizationAggregate.Entities.Organization(
                    title: orgData.OrganizationName!,
                    licenseNumber: orgData.LicenseNumber!,
                    type: Domain.OrganizationAggregate.Enums.OrganizationType.BeautySalon,
                    slogan: orgData.Slogan
                );

                // 3. اضافه کردن مالک
                var owner = new OrganizationOwner(request.UserId, isPrimary: true);
                organization.AddOwner(owner);

                // 4. ذخیره Organization (تا Id تولید شود)
                await _organizationRepository.AddAsync(organization, ct);
                await _unitOfWork.SaveChangesAsync(ct);

                // 5. ساخت OrganizationDetail
                var orgDetail = new OrganizationDetail(
                    description: orgData.OrganizationDescription,
                    website: orgData.Website,
                    instagram: orgData.Instagram,
                    email: orgData.Email,
                    whatsapp: orgData.Whatsapp,
                    fromDate: DateTime.UtcNow,
                    toDate: DateTime.UtcNow.AddYears(1)
                );
                orgDetail.SetOrganizationId(organization.Id);
                await _organizationDetailRepository.AddAsync(orgDetail, ct);
                await _unitOfWork.SaveChangesAsync(ct);

                // 6. ساخت Branch
                var branch = new BeautySalonBooking.Domain.BranchAggregate.Entities.Branch(
                    organizationId: organization.Id,
                    title: orgData.BranchName ?? orgData.OrganizationName!,
                    description: orgData.BranchDescription
                );
                await _branchRepository.AddAsync(branch, ct);
                await _unitOfWork.SaveChangesAsync(ct);

                // 👇 7. انتقال مدارک پیوست از RequestDocument به OrganizationMedia
                foreach (var doc in request.Documents)
                {
                    var media = new BeautySalonBooking.Domain.OrganizationAggregate.Entities.OrganizationMedia(
                        fileName: doc.FileName,
                        contentType: doc.ContentType,
                        content: doc.Content,
                        type: MediaType.Main,
                        displayOrder: 0
                    );
                    organization.AddMedia(media);
                }
                await _unitOfWork.SaveChangesAsync(ct);

                // 👇 8. ثبت مالک به عنوان BranchMember
                var person = await _personRepository.GetByUserIdAsync(request.UserId, ct);
                if (person == null)
                    throw new Exception("اطلاعات شخصی کاربر یافت نشد.");

                var ownerRole = await _branchRoleRepository.GetByCodeAsync("BRANCH_MANAGER", ct);
                if (ownerRole == null)
                    throw new Exception("نقش مالک (OWNER) در سیستم تعریف نشده است.");

                var branchMember = new BranchMember(
                    branchId: branch.Id,
                personId: person.Id,
                    branchRoleId: ownerRole.Id,
                    startDate: DateOnly.FromDateTime(DateTime.UtcNow)
                );
                await _branchMemberRepository.AddAsync(branchMember, ct);
                await _unitOfWork.SaveChangesAsync(ct);

                // 9. تغییر وضعیت درخواست
                request.Approve(currentUserId.Value, dto.ReviewDescription);
                await _unitOfWork.SaveChangesAsync(ct);

                var user =
              User.Create(
                  person.Id,
                  person.NationalCode, person.NationalCode,
                  AuthenticationMode.Sms);

                var mobile = PhoneNumber.Create(clientUser.PhoneNumbers.FirstOrDefault().Number, PhoneNumberType.Mobile, true);

                user.AddPhoneNumber(mobile);

                await _userRepository.AddAsync(user, ct);

                await _unitOfWork.SaveChangesAsync(ct);

                var role = await _roleRepository.GetByCodeAsync(Domain.Identity.RoleAggregate.Enums.RoleCode.OrganizationManager, ct);

                var userRole =
                    UserRole.Create(
                        user,
                        role);

                await _userRoleRepository.AddAsync(userRole, ct);

                await _unitOfWork.CommitAsync(ct);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync(ct);
                throw;
            }

        }
    }
}


