namespace BeautySalonBooking.Maui.UserControls.OnboardingCard;

class OnboardingCardData
{
    public OnboardingCardData(int id, string mainImageBoxSource, string circularImageSource, string title, string description)
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
    List<OnboardingCardData> Data;
    int Selected;
    public event EventHandler? OnboardingCompleted;
    public OnboardingCardView()
    {
        InitializeComponent();
        Data =
        [
            new OnboardingCardData(0, "onboarding_chair.png", "beauty_chair.png", "همه چیز برای زیبایی شما", "از پیدا کردن بهترین سالن ها تا مدیریت \n وقت های شما، همه در یکجا"),
            new OnboardingCardData(1, "onboarding_product.png", "calneder.png", "رزرو آسان و سریع ", "با چند کلیک ساده نوبت دلخواه خود را\n از بین بهترین سالن ها رزرو کنید"),
            new OnboardingCardData(2, "onboarding_mobile.png", "message.png", "بررسی و انتخاب هوشمند", "با مشاهده نظرات کابران و تصاویر واقعی\n بهترین انتخاب را داشته باشید"),
        ];
        Selected = 0;
        SwitchOnboarding();
    }

    private void btnNext_Clicked(object sender, EventArgs e)
    {
        if (Selected == 2)
        {
            OnboardingCompleted?.Invoke(this, EventArgs.Empty);
            return;
        }
        Selected++;
        SwitchOnboarding();
    }
    private void btnBack_Clicked(object sender, EventArgs e)
    {
        if (Selected == 0)
        {
            return;
        }
        Selected--;
        SwitchOnboarding();
    }
    void SwitchOnboarding()
    {
        imgMainBox.Source = Data[Selected].MainImageBoxSource;
        imgCircularIcon.ImageSource = Data[Selected].CircularImageSource;
        lblTitle.Text = Data[Selected].Title;
        lblDescription.Text = Data[Selected].Description;

        switch (Selected)
        {
            case 0:
                btnBack.IsVisible = false;
                btnNext.IsVisible = true;
                Indicator_1.Color = Color.FromArgb("#F59DB4");
                Indicator_2.Color = Indicator_3.Color = Color.FromArgb("#D9D9D9");
                imgCircularIcon.InternalMargin = 20;

                break;

            case 1:
                btnBack.IsVisible = true;
                btnNext.IsVisible = true;
                Indicator_1.Color = Indicator_2.Color = Color.FromArgb("#F59DB4");
                Indicator_3.Color = Color.FromArgb("#D9D9D9");
                imgCircularIcon.InternalMargin = 30;
                break;

            case 2:
                btnBack.IsVisible = true;
                btnNext.IsVisible = true;
                Indicator_1.Color = Indicator_2.Color = Indicator_3.Color = Color.FromArgb("#F59DB4");
                imgCircularIcon.InternalMargin = 30;

                break;
        }
    }
}