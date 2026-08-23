using BeautySalonBooking.Maui.Common.Interfaces;

namespace BeautySalonBooking.Maui.Common.Services;

public class ViewService : IViewService
{
    private readonly IServiceProvider _serviceProvider;

    public ViewService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public View GetView<TView>()
    {
        return _serviceProvider.GetRequiredService<TView>()
            as View
            ?? throw new InvalidOperationException(
                $"{typeof(TView).Name} must inherit from View.");
    }
}