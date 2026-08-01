namespace BeautySalonBooking.Maui.UserControls.IndicatorsControl;

public partial class SimpleIndicator : ContentView
{
    public SimpleIndicator()
    {
        InitializeComponent();
    }
    public async Task StartAnimationAsync()
    {
        Reset();

        await Task.Delay(1200);
        Indicator_Border1.Background = Colors.PaleVioletRed;

        await Task.Delay(1200);
        Indicator_Border2.Background = Colors.PaleVioletRed;

        await Task.Delay(1200);
        Indicator_Border3.Background = Colors.PaleVioletRed;

        await Task.Delay(1200);
    }

    public void Reset()
    {
        Indicator_Border1.Background = Colors.LightGray;
        Indicator_Border2.Background = Colors.LightGray;
        Indicator_Border3.Background = Colors.LightGray;
    }
}