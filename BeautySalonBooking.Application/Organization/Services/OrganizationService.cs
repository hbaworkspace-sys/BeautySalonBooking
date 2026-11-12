using BeautySalonBooking.Application.Organization.Interfaces;
using BeautySalonBooking.Contracts.Branch.BranchService.Enums;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Contracts.Organization.Dtos;
using BeautySalonBooking.Contracts.Organization.Enums;
using BeautySalonBooking.Contracts.Organization.Responses;
using BeautySalonBooking.Domain.OrganizationAggregate.Repositories;
using BeautySalonBooking.Domain.RequestAggregate.Enums;
using BeautySalonBooking.Domain.RequestAggregate.Repositories;

namespace BeautySalonBooking.Application.Organization.Services;

public sealed class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IRequestRepository _requestRepository;

    public OrganizationService(IRequestRepository requestRepository,
        IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
        _requestRepository = requestRepository;
    }

    public async Task<ApiResponse_New<GetOrganizationsResponse>>
        GetAlleAsync(
            CancellationToken cancellationToken)
    {
        var organizations =
            await _organizationRepository.GetAllAsync(
                cancellationToken);

        var response = new GetOrganizationsResponse
        {
            Organizations = organizations
                .Select(organization => new OrganizationDto
                {
                    Id = organization.Id,
                    Title = organization.Title,
                    Slogan = organization.Slogan,
                    Glyph = organization.Glyph,

                    Media = organization.Media
                        .Where(media => media.DisplayOrder == 1)
                        .Select(media => new OrganizationMediaDto
                        {
                            FileName = media.FileName,
                            ContentType = media.ContentType,
                            FileSize = media.FileSize,
                            Content = media.Content,
                            DisplayOrder = media.DisplayOrder
                        })
                        .ToList()
                })
                .ToList()
        };

        return new ApiResponse_New<GetOrganizationsResponse>
        {
            IsSuccess = true,
            Code = 200,
            Payload = response
        };
    }

    public async Task<OrganizationFullDetailDto?> GetFullDetailAsync(long organizationId, CancellationToken ct = default)
    {
        // 1. گرفتن سازمان با تمام جزئیات
        var organization = await _organizationRepository.GetFullDetailAsync(organizationId, ct);
        if (organization == null) return null;

        // 2. گرفتن درخواست‌های مرتبط (بر اساس TrackingCode یا UserId مالک)
        // چون Request به OrganizationId مستقیم وصل نیست، از طریق UserId مالک پیدا می‌کنیم
        var ownerUserIds = organization.Owners.Select(x => x.OwnerUserId).ToList();
        var requests = await _requestRepository.FindAsync(
            x => ownerUserIds.Contains(x.UserId), ct);

        // 3. ساخت DTO
        var dto = new OrganizationFullDetailDto
        {
            Id = organization.Id,
            Title = organization.Title,
            LicenseNumber = organization.LicenseNumber,
            Type = (OrganizationType)organization.Type,
            Slogan = organization.Slogan,
            Signature = organization.Signature,
            Glyph = organization.Glyph,
            CreatedAt = organization.CreatedAt,
        };

        // 3.1. Detail
        if (organization.Detail != null)
        {
            dto.Detail = new OrganizationDetailDto
            {
                Description = organization.Detail.Description,
                Website = organization.Detail.Website,
                Instagram = organization.Detail.Instagram,
                Email = organization.Detail.Email,
                Whatsapp = organization.Detail.Whatsapp,
                FromDate = organization.Detail.FromDate,
                ToDate = organization.Detail.ToDate
            };
        }

        // 3.2. Owners
        dto.Owners = organization.Owners.Select(o => new OrganizationOwnerDto
        {
            OwnerUserId = o.OwnerUserId,
            FirstName = o.OwnerUser?.Person?.FirstName,
            LastName = o.OwnerUser?.Person?.LastName,
            //PhoneNumber = o.OwnerUser?.Person?.PhoneNumbers?
            //    .FirstOrDefault(p => p.IsDefault)?.Number,
            IsPrimary = o.IsPrimary
        }).ToList();

        // 3.3. Media
        dto.Media = organization.Media.Select(m => new OrganMediaDto
        {
            Id = m.Id,
            FileName = m.FileName,
            ContentType = m.ContentType,
            FileSize = m.FileSize,
            Base64Content = Convert.ToBase64String(m.Content),
            Type = (MediaType)m.Type,
            DisplayOrder = m.DisplayOrder
        }).ToList();

        // 3.4. Branches
        dto.Branches = organization.Branches.Select(b => new BranchSummaryDto
        {
            Id = b.Id,
            Title = b.Title,
            Description = b.Description,
            Glyph = b.Glyph,
            MemberCount = b.Members.Count,
            ServiceCount = b.Services.Count,
            MediaCount = b.Media.Count,
            Members = b.Members.Select(m => new BranchMemberSummaryDto
            {
                Id = m.Id,
                PersonId = m.PersonId,
                FirstName = m.Person?.FirstName,
                LastName = m.Person?.LastName,
                // PhoneNumber = m.Person?.PhoneNumbers?
                //.FirstOrDefault(p => p.IsDefault)?.Number,
                BranchRoleTitle = m.BranchRole?.Title,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                ServiceCount = m.Services.Count
            }).ToList()
        }).ToList();

        // 3.5. Statistics
        dto.Statistics = new OrganizationStatisticsDto
        {
            TotalBranches = organization.Branches.Count,
            TotalMembers = organization.Branches.Sum(b => b.Members.Count),
            TotalServices = organization.Branches.Sum(b => b.Services.Count),
            TotalMedia = organization.Media.Count,
            TotalRequests = requests.Count(),
            PendingRequests = requests.Count(r => r.Status == RequestStatus.Submitted),
            ApprovedRequests = requests.Count(r => r.Status == RequestStatus.Approved),
            RejectedRequests = requests.Count(r => r.Status == RequestStatus.Rejected),
            TotalOrganizationDocuments = organization.Media.Count(m => m.Type == Domain.Base.Enums.MediaType.Main)
        };

        return dto;
    }




}