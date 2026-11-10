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
    public class RequestTypeService : IRequestTypeService
    {
        private readonly IRequestTypeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RequestTypeService(IRequestTypeRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateAsync(CreateRequestTypeDto dto, CancellationToken ct = default)
        {
            if (await _repository.GetByCodeAsync(dto.Code, ct) != null)
                throw new Exception("کد وارد شده تکراری است.");

            var entity = new RequestType(dto.Title, dto.Code, dto.Description, dto.IsPaymentRequired);
            await _repository.AddAsync(entity, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task<List<RequestTypeResponseDto>> GetAllAsync(CancellationToken ct = default)
        {
            var list = await _repository.GetAllAsync(ct);
            return list.Select(x => new RequestTypeResponseDto
            {
                Id = x.Id,
                Title = x.Title,
                Code = x.Code,
                Description = x.Description,
                IsPaymentRequired = x.IsPaymentRequired
            }).ToList();
        }
    }
}
