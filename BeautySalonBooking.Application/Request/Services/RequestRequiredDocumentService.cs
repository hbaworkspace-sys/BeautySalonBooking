using BeautySalonBooking.Application.Request.DTOs;
using BeautySalonBooking.Application.Request.Interfaces;
using BeautySalonBooking.Domain.Base.UnitOfWork;
using BeautySalonBooking.Domain.RequestAggregate.Entities;
using BeautySalonBooking.Domain.RequestAggregate.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Request.Services
{
    public class RequestRequiredDocumentService : IRequestRequiredDocumentService
    {
        private readonly IRequestRequiredDocumentRepository _repository;
        private readonly IRequestTypeRepository _requestTypeRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RequestRequiredDocumentService(
            IRequestRequiredDocumentRepository repository,
            IRequestTypeRepository requestTypeRepository,
            IDocumentTypeRepository documentTypeRepository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _requestTypeRepository = requestTypeRepository;
            _documentTypeRepository = documentTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateAsync(CreateRequestRequiredDocumentDto dto, CancellationToken ct = default)
        {
            // بررسی وجود نوع درخواست
            var requestType = await _requestTypeRepository.GetByIdAsync(dto.RequestTypeId, ct);
            if (requestType == null) throw new Exception("نوع درخواست یافت نشد.");

            // بررسی وجود نوع مدرک
            var documentType = await _documentTypeRepository.GetByIdAsync(dto.DocumentTypeId, ct);
            if (documentType == null) throw new Exception("نوع مدرک یافت نشد.");

            // بررسی تکراری نبودن
            var existing = await _repository.GetAsync(dto.RequestTypeId, dto.DocumentTypeId, ct);
            if (existing != null) throw new Exception("این مدرک قبلاً برای این نوع درخواست تعریف شده است.");

            var entity = new RequestRequiredDocument(dto.RequestTypeId, dto.DocumentTypeId, dto.IsRequired, dto.DisplayOrder);
            await _repository.AddAsync(entity, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task UpdateAsync(UpdateRequestRequiredDocumentDto dto, CancellationToken ct = default)
        {
            var entity = await _repository.GetByIdAsync(dto.Id, ct);
            if (entity == null) throw new Exception("رکورد یافت نشد.");

            entity.Update(dto.IsRequired, dto.DisplayOrder);
            _repository.Update(entity);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repository.GetByIdAsync(id, ct);
            if (entity == null) throw new Exception("رکورد یافت نشد.");

            _repository.Delete(entity);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task<List<RequestRequiredDocumentResponseDto>> GetByRequestTypeIdAsync(int requestTypeId, CancellationToken ct = default)
        {
            var list = await _repository.GetByRequestTypeIdAsync(requestTypeId, ct);
            return list.Select(x => new RequestRequiredDocumentResponseDto
            {
                Id = x.Id,
                RequestTypeId = x.RequestTypeId,
                RequestTypeTitle = x.RequestType?.Title ?? "",
                DocumentTypeId = x.DocumentTypeId,
                DocumentTypeTitle = x.DocumentType?.Title ?? "",
                IsRequired = x.IsRequired,
                DisplayOrder = x.DisplayOrder
            }).ToList();
        }
    }
}
