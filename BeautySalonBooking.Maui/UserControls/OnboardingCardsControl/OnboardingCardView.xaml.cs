namespace BeautySalonBooking.Maui.UserControls.OnboardingCard;

public class OnboardingCardData
{
    public OnboardingCardData(
        int id,
        string mainImageBoxSource,
        string circularImageSource,
        string title,
        string description)
    {
        Id = id;
        MainImageBoxSource = mainImageBoxSource;
        CircularImageSource = circularImageSource;
        Title = title;
        Description = description;
    }

    public int Id { get; set; }
    public string MainImageBoxSource { get; set; }
    public string CircularImageSource { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}

public partial class OnboardingCardView : ContentView
{
    private readonly List<OnboardingCardData> _data;
    private int _selected;
    private CancellationTokenSource? _rotationCts;

    public event EventHandler? OnboardingCompleted;

    public OnboardingCardView()
    {
        InitializeComponent();

        _data =
        [
            new OnboardingCardData(
                0,
                "onboarding_chair.png",
                "beauty_chair.png",
                "همه چیز برای زیبایی شما",
                "از پیدا کردن بهترین سالن ها تا مدیریت \n وقت های شما، همه در یکجا"),

            new OnboardingCardData(
                1,
                "onboarding_product.png",
                "calneder.png",
                "رزرو آسان و سریع",
                "با چند کلیک ساده نوبت دلخواه خود را\n از بین بهترین سالن ها رزرو کنید"),

            new OnboardingCardData(
                2,
                "onboarding_mobile.png",
                "message.png",
                "بررسی و انتخاب هوشمند",
                "با مشاهده نظرات کابران و تصاویر واقعی\n بهترین انتخاب را داشته باشید"),
        ];

        _selected = 0;

        ShowCard(_selected);

        Loaded += OnboardingCardView_Loaded;
        Unloaded += OnboardingCardView_Unloaded;
    }

    private void OnboardingCardView_Loaded(
        object? sender,
        EventArgs e)
    {
        StartRotation();
    }

    private void OnboardingCardView_Unloaded(
        object? sender,
        EventArgs e)
    {
        StopRotation();
    }

    public void StartRotation()
    {
        // جلوگیری از اجرای چند حلقه هم‌زمان
        if (_rotationCts is not null)
            return;

        _rotationCts = new CancellationTokenSource();

        _ = RotateCardsAsync(_rotationCts.Token);
    }

    public void StopRotation()
    {
        var cts = _rotationCts;

        if (cts is null)
            return;

        _rotationCts = null;

        cts.Cancel();
        cts.Dispose();
    }
    public async Task RotateCardsAsync(CancellationToken token)
    {
        try
        {
            while (true)
            {
                // مدت نمایش هر کارت
                await Task.Delay(3000, token);

                _selected = (_selected + 1) % _data.Count;

                ShowCard(_selected);
            }
        }
        catch (OperationCanceledException)
        {
            // توقف عادی چرخش کارت‌ها
        }
    }

    private void ShowCard(int index)
    {
        var card = _data[index];

        imgMainBox.Source = card.MainImageBoxSource;
        imgCircularIcon.ImageSource = card.CircularImageSource;

        textContent.Title = card.Title;
        textContent.Description = card.Description;

        var activeColor = Color.FromArgb("#F59DB4");
        var inactiveColor = Color.FromArgb("#D9D9D9");

        Indicator_1.Color =
            index == 0 ? activeColor : inactiveColor;

        Indicator_2.Color =
            index == 1 ? activeColor : inactiveColor;

        Indicator_3.Color =
            index == 2 ? activeColor : inactiveColor;

        imgCircularIcon.ImageMargin = 20;
    }



    private void SkipButton_Clicked(object sender, EventArgs e)
    {
        StopRotation();

        OnboardingCompleted?.Invoke(this, EventArgs.Empty);
    }
}