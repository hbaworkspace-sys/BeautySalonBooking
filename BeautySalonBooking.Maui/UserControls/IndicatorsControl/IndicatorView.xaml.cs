using System.Collections.ObjectModel;

namespace BeautySalonBooking.Maui.UserControls.Indicator;

public partial class IndicatorView : ContentView
{
    public ObservableCollection<IndicatorItem> Indicators { get; } = new();
    public event EventHandler<IndicatorSelectedIndexChangedEventArgs>? SelectedIndexChanged;
    public IndicatorView()
    {
        InitializeComponent();
        BindingContext = this;
        BuildIndicators();
    }

    //Shape="Circle"
    //ActiveColor="HotPink"
    //InactiveColor="LightGray"
    //IndicatorWidth="12"
    //IndicatorHeight="12"
    //Spacing="6"
    //IsAnimated="True"
    //AutoPlay="False"

    public int Count
    {
        get => (int)GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }
    public static readonly BindableProperty CountProperty =
        BindableProperty.Create(nameof(Count), typeof(int), typeof(IndicatorView), 0,
            propertyChanged: OnIndicatorPropertyChanged);

    public int SelectedIndex
    {
        get => (int)GetValue(SelectedIndexProperty);
        set => SetValue(SelectedIndexProperty, value);
    }
    public static readonly BindableProperty SelectedIndexProperty =
        BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(IndicatorView), 0,
            propertyChanged: OnIndicatorPropertyChanged);

    public IndicatorSelectionMode SelectionMode
    {
        get => (IndicatorSelectionMode)GetValue(SelectionModeProperty);
        set => SetValue(SelectionModeProperty, value);
    }

    public static readonly BindableProperty SelectionModeProperty =
        BindableProperty.Create(nameof(SelectionMode), typeof(IndicatorSelectionMode), typeof(IndicatorView), IndicatorSelectionMode.Single,
            propertyChanged: OnIndicatorPropertyChanged);
    private static void OnIndicatorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (IndicatorView)bindable;
        control.BuildIndicators();
    }

    private static void OnSelectedIndexChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (IndicatorView)bindable;

        if (control.SelectedIndex < 0)
            control.SelectedIndex = 0;

        if (control.SelectedIndex >= control.Count)
            control.SelectedIndex = control.Count - 1;

        control.BuildIndicators();

        control.SelectedIndexChanged?.Invoke(
            control,
            new IndicatorSelectedIndexChangedEventArgs(
                (int)oldValue,
                (int)newValue));
    }

    public sealed class IndicatorSelectedIndexChangedEventArgs : EventArgs
    {
        public int OldIndex { get; }
        public int NewIndex { get; }
        public IndicatorSelectedIndexChangedEventArgs(int oldIndex, int newIndex)
        {
            OldIndex = oldIndex;
            NewIndex = newIndex;
        }
    }

    public IndicatorShape Shape
    {
        get => (IndicatorShape)GetValue(ShapeProperty);
        set => SetValue(ShapeProperty, value);
    }
    public static readonly BindableProperty ShapeProperty =
        BindableProperty.Create(nameof(Shape), typeof(IndicatorShape), typeof(IndicatorView), IndicatorShape.RoundedRectangle,
            propertyChanged: OnIndicatorPropertyChanged);

    //public IndicatorOrientation Orientation
    //{
    //    get => (IndicatorOrientation)GetValue(OrientationProperty);
    //    set => SetValue(OrientationProperty, value);
    //}
    //public static readonly BindableProperty OrientationProperty =
    //    BindableProperty.Create(nameof(Orientation), typeof(IndicatorOrientation), typeof(IndicatorView), IndicatorOrientation.Horizontal,
    //        propertyChanged: OnIndicatorPropertyChanged);

    public double IndicatorWidth
    {
        get => (double)GetValue(IndicatorWidthProperty);
        set => SetValue(IndicatorWidthProperty, value);
    }
    public static readonly BindableProperty IndicatorWidthProperty =
        BindableProperty.Create(nameof(IndicatorWidth), typeof(double), typeof(IndicatorView), 8d,
            propertyChanged: OnIndicatorPropertyChanged);

    public double IndicatorHeight
    {
        get => (double)GetValue(IndicatorHeightProperty);
        set => SetValue(IndicatorHeightProperty, value);
    }
    public static readonly BindableProperty IndicatorHeightProperty =
        BindableProperty.Create(nameof(IndicatorHeight), typeof(double), typeof(IndicatorView), 8d,
            propertyChanged: OnIndicatorPropertyChanged);

    public double ActiveWidth
    {
        get => (double)GetValue(ActiveWidthProperty);
        set => SetValue(ActiveWidthProperty, value);
    }
    public static readonly BindableProperty ActiveWidthProperty =
        BindableProperty.Create(nameof(ActiveWidth), typeof(double), typeof(IndicatorView), 20d,
        propertyChanged: OnIndicatorPropertyChanged);

    public double ActiveHeight
    {
        get => (double)GetValue(ActiveHeightProperty);
        set => SetValue(ActiveHeightProperty, value);
    }
    public static readonly BindableProperty ActiveHeightProperty =
        BindableProperty.Create(nameof(ActiveHeight), typeof(double), typeof(IndicatorView), 8d,
            propertyChanged: OnIndicatorPropertyChanged);

    public double Spacing
    {
        get => (double)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }
    public static readonly BindableProperty SpacingProperty =
        BindableProperty.Create(nameof(Spacing), typeof(double), typeof(IndicatorView), 6d,
            propertyChanged: OnIndicatorPropertyChanged);

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(IndicatorView), new CornerRadius(50),
            propertyChanged: OnIndicatorPropertyChanged);

    public Brush ActiveColor
    {
        get => (Brush)GetValue(ActiveColorProperty);
        set => SetValue(ActiveColorProperty, value);
    }
    public static readonly BindableProperty ActiveColorProperty =
        BindableProperty.Create(nameof(ActiveColor), typeof(Brush), typeof(IndicatorView), Brush.Black,
            propertyChanged: OnIndicatorPropertyChanged);

    public Brush InactiveColor
    {
        get => (Brush)GetValue(InactiveColorProperty);
        set => SetValue(InactiveColorProperty, value);
    }
    public static readonly BindableProperty InactiveColorProperty =
        BindableProperty.Create(nameof(InactiveColor), typeof(Brush), typeof(IndicatorView), Brush.LightGray,
            propertyChanged: OnIndicatorPropertyChanged);

    public Brush ActiveBorderColor
    {
        get => (Brush)GetValue(ActiveBorderColorProperty);
        set => SetValue(ActiveBorderColorProperty, value);
    }
    public static readonly BindableProperty ActiveBorderColorProperty =
        BindableProperty.Create(nameof(ActiveBorderColor), typeof(Brush), typeof(IndicatorView), Brush.Transparent,
            propertyChanged: OnIndicatorPropertyChanged);

    public Brush InactiveBorderColor
    {
        get => (Brush)GetValue(InactiveBorderColorProperty);
        set => SetValue(InactiveBorderColorProperty, value);
    }
    public static readonly BindableProperty InactiveBorderColorProperty =
        BindableProperty.Create(nameof(InactiveBorderColor), typeof(Brush), typeof(IndicatorView), Brush.Transparent,
            propertyChanged: OnIndicatorPropertyChanged);

    public double BorderThickness
    {
        get => (double)GetValue(BorderThicknessProperty);
        set => SetValue(BorderThicknessProperty, value);
    }
    public static readonly BindableProperty BorderThicknessProperty =
        BindableProperty.Create(nameof(BorderThickness), typeof(double), typeof(IndicatorView), 0d,
            propertyChanged: OnIndicatorPropertyChanged);






    public bool IsAnimated
    {
        get => (bool)GetValue(IsAnimatedProperty);
        set => SetValue(IsAnimatedProperty, value);
    }
    public static readonly BindableProperty IsAnimatedProperty =
        BindableProperty.Create(nameof(IsAnimated), typeof(bool), typeof(IndicatorView), true);


    public IndicatorAnimationMode AnimationMode
    {
        get => (IndicatorAnimationMode)GetValue(AnimationModeProperty);
        set => SetValue(AnimationModeProperty, value);
    }
    public static readonly BindableProperty AnimationModeProperty =
        BindableProperty.Create(nameof(AnimationMode), typeof(IndicatorAnimationMode), typeof(IndicatorView), IndicatorAnimationMode.FadeAndScale);

    public uint AnimationDuration
    {
        get => (uint)GetValue(AnimationDurationProperty);
        set => SetValue(AnimationDurationProperty, value);
    }
    public static readonly BindableProperty AnimationDurationProperty =
        BindableProperty.Create(nameof(AnimationDuration), typeof(uint), typeof(IndicatorView), 250u);


    public bool IsAutoPlayEnabled
    {
        get => (bool)GetValue(IsAutoPlayEnabledProperty);
        set => SetValue(IsAutoPlayEnabledProperty, value);
    }
    public static readonly BindableProperty IsAutoPlayEnabledProperty =
        BindableProperty.Create(nameof(IsAutoPlayEnabled), typeof(bool), typeof(IndicatorView), false);



    private void BuildIndicators()
    {
        if (Count <= 0)
            return;
        Indicators.Clear();
        for (int i = 0; i < Count; i++)
        {
            Indicators.Add(new IndicatorItem
            {
                Index = i,
                IsSelected = i == SelectedIndex,
                Width = 20,
                Height = 6,
                CornerRadius = new CornerRadius(50),
                Background =
                    i == SelectedIndex
                        ? Brush.Black
                        : Brush.LightGray
            });
        }
    }
}
public enum IndicatorShape : byte
{
    Circle = 0,
    Line = 1,
    RoundedRectangle = 2,
    Rectangle = 3
}
public enum IndicatorOrientation : byte
{
    Horizontal = 0,
    Vertical = 1
}
public enum IndicatorAnimationMode : byte
{
    None = 0,
    Fade = 1,
    Scale = 2,
    FadeAndScale = 3
}
public enum IndicatorSelectionMode : byte
{
    Single = 0,
    Progress = 1
}
public enum IndicatorAutoPlayMode : byte
{
    None = 0,
    Forward = 1,
    Reverse = 2,
    PingPong = 3
}
public enum IndicatorTapMode : byte
{
    Disabled = 0,
    Enabled = 1
}