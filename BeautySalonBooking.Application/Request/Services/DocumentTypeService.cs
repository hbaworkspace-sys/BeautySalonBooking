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
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IDocumentTypeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DocumentTypeService(IDocumentTypeRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateAsync(CreateDocumentTypeDto dto, CancellationToken ct = default)
        {
            if (await _repository.GetByCodeAsync(dto.Code, ct) != null)
                throw new Exception("کد وارد شده تکراری است.");

            var entity = new DocumentType(dto.Title, dto.Code, dto.Description);
            await _repository.AddAsync(entity, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task UpdateAsync(UpdateDocumentTypeDto dto, CancellationToken ct = default)
        {
            var entity = await _repository.GetByIdAsync(dto.Id, ct);
            if (entity == null) throw new Exception("نوع مدرک یافت نشد.");

            entity.Update(dto.Title, dto.Code, dto.Description);
            _repository.Update(entity);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repository.GetByIdAsync(id, ct);
            if (entity == null) throw new Exception("نوع مدرک یافت نشد.");

            _repository.Delete(entity);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task<List<DocumentTypeResponseDto>> GetAllAsync(CancellationToken ct = default)
        {
            var list = await _repository.GetAllAsync(ct);
            return list.Select(x => new DocumentTypeResponseDto
            {
                Id = x.Id,
                Title = x.Title,
                Code = x.Code,
                Description = x.Description
            }).ToList();
        }

    }
}
