namespace BeautySalonBooking.Application.Common.Interfaces;

public interface IValidationService
{
    Task ValidateAsync<T>(T instance, CancellationToken cancellationToken = default);
}