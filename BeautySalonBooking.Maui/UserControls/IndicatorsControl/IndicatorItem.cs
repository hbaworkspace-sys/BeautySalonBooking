using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeautySalonBooking.Maui.UserControls.Indicator;

public sealed class IndicatorItem : INotifyPropertyChanged
{
    private Brush _background = Brush.Transparent;
    private Brush _stroke = Brush.Transparent;
    private double _width;
    private double _height;
    private double _strokeThickness;
    private Thickness _margin;
    private bool _isSelected;
    private int _index;

    public int Index
    {
        get => _index;
        set => SetProperty(ref _index, value);
    }

    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }

    public Brush Background
    {
        get => _background;
        set => SetProperty(ref _background, value);
    }

    public Brush Stroke
    {
        get => _stroke;
        set => SetProperty(ref _stroke, value);
    }

    public double Width
    {
        get => _width;
        set => SetProperty(ref _width, value);
    }

    public double Height
    {
        get => _height;
        set => SetProperty(ref _height, value);
    }

    private CornerRadius _cornerRadius;

    public CornerRadius CornerRadius
    {
        get => _cornerRadius;
        set => SetProperty(ref _cornerRadius, value);
    }
    public double StrokeThickness
    {
        get => _strokeThickness;
        set => SetProperty(ref _strokeThickness, value);
    }

    public Thickness Margin
    {
        get => _margin;
        set => SetProperty(ref _margin, value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetProperty<T>(
        ref T storage,
        T value,
        [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
            return false;

        storage = value;

        OnPropertyChanged(propertyName);

        return true;
    }
}