using System.Windows.Input;
using BeautySalonBooking.Contracts.Appointment.Enums;
using BeautySalonBooking.Maui.Common.Helpers;

namespace BeautySalonBooking.Maui.Components.Card;

public partial class AppointmentCardView : ContentView
{
    public AppointmentCardView()
    {
        InitializeComponent();
        UpdateStatus();
    }

    #region Organization

    public string? Organization
    {
        get => (string?)GetValue(OrganizationProperty);
        set => SetValue(OrganizationProperty, value);
    }

    public static readonly BindableProperty OrganizationProperty =
        BindableProperty.Create(
            nameof(Organization),
            typeof(string),
            typeof(AppointmentCardView),
            default(string),
            propertyChanged: OnOrganizationChanged);

    private static void OnOrganizationChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not AppointmentCardView control)
            return;

        control.UpdateOrganization();
    }

    private void UpdateOrganization()
    {
        OrganizationLabel.Text = Organization;
    }

    #endregion


    #region Branch

    public string? BranchByLocation
    {
        get => (string?)GetValue(BranchProperty);
        set => SetValue(BranchProperty, value);
    }

    public static readonly BindableProperty BranchProperty =
        BindableProperty.Create(
            nameof(BranchByLocation),
            typeof(string),
            typeof(AppointmentCardView),
            default(string),
            propertyChanged: OnBranchChanged);

    private static void OnBranchChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not AppointmentCardView control)
            return;

        control.UpdateBranch();
    }

    private void UpdateBranch()
    {
        BranchByLocationLabel.Text = BranchByLocation;
    }

    #endregion


    #region Services

    public string? Services
    {
        get => (string?)GetValue(ServicesProperty);
        set => SetValue(ServicesProperty, value);
    }

    public static readonly BindableProperty ServicesProperty =
        BindableProperty.Create(
            nameof(Services),
            typeof(string),
            typeof(AppointmentCardView),
            default(string),
            propertyChanged: OnServicesChanged);

    private static void OnServicesChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not AppointmentCardView control)
            return;

        control.UpdateServices();
    }

    private void UpdateServices()
    {
        ServicesLabel.Text = Services;
    }

    #endregion


    #region Stylist

    public string? Stylist
    {
        get => (string?)GetValue(StylistProperty);
        set => SetValue(StylistProperty, value);
    }

    public static readonly BindableProperty StylistProperty =
        BindableProperty.Create(
            nameof(Stylist),
            typeof(string),
            typeof(AppointmentCardView),
            default(string),
            propertyChanged: OnStylistChanged);

    private static void OnStylistChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not AppointmentCardView control)
            return;

        control.UpdateStylist();
    }

    private void UpdateStylist()
    {
        StylistLabel.Text = Stylist;
    }

    #endregion


    #region Date
    public DateOnly Date
    {
        get => (DateOnly)GetValue(DateProperty);
        set => SetValue(DateProperty, value);
    }

    public static readonly BindableProperty DateProperty =
        BindableProperty.Create(
            nameof(Date),
            typeof(DateOnly),
            typeof(AppointmentCardView),
            default(DateOnly),
            propertyChanged: OnDateChanged);

    private static void OnDateChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not AppointmentCardView control)
            return;

        control.UpdateDate();
    }

    private void UpdateDate()
    {
        DateLabel.Text =
            PersianDateHelper.FormatDate(Date);
    }
    #endregion


    #region Time

    public TimeOnly Time
    {
        get => (TimeOnly)GetValue(TimeProperty);
        set => SetValue(TimeProperty, value);
    }

    public static readonly BindableProperty TimeProperty =
        BindableProperty.Create(
            nameof(Time),
            typeof(TimeOnly),
            typeof(AppointmentCardView),
            default(TimeOnly),
            propertyChanged: OnTimeChanged);

    private static void OnTimeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not AppointmentCardView control)
            return;

        control.UpdateTime();
    }

    private void UpdateTime()
    {
        TimeLabel.Text =
            PersianDateHelper.FormatTime(
                Time);
    }

    #endregion


    #region Price

    public decimal Price
    {
        get => (decimal)GetValue(PriceProperty);
        set => SetValue(PriceProperty, value);
    }

    public static readonly BindableProperty PriceProperty =
        BindableProperty.Create(
            nameof(Price),
            typeof(decimal),
            typeof(AppointmentCardView),
            default(decimal),
            propertyChanged: OnPriceChanged);

    private static void OnPriceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not AppointmentCardView control)
            return;

        control.UpdatePrice();
    }

    private void UpdatePrice()
    {
        PriceLabel.Text =
            PersianDateHelper.FormatPrice(Price);
    }

    #endregion


    #region Organization Image

    public ImageSource? OrganizationImage
    {
        get => (ImageSource?)GetValue(OrganizationImageProperty);
        set => SetValue(OrganizationImageProperty, value);
    }

    public static readonly BindableProperty OrganizationImageProperty =
        BindableProperty.Create(
            nameof(OrganizationImage),
            typeof(ImageSource),
            typeof(AppointmentCardView),
            default(ImageSource),
            propertyChanged: OnOrganizationImageChanged);

    private static void OnOrganizationImageChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not AppointmentCardView control)
            return;

        control.UpdateOrganizationImage();
    }

    private void UpdateOrganizationImage()
    {
        OrganizationMediaView.ImageSource = OrganizationImage;
    }

    #endregion


    #region Stylist Image

    public ImageSource? StylistImage
    {
        get => (ImageSource?)GetValue(StylistImageProperty);
        set => SetValue(StylistImageProperty, value);
    }

    public static readonly BindableProperty StylistImageProperty =
        BindableProperty.Create(
            nameof(StylistImage),
            typeof(ImageSource),
            typeof(AppointmentCardView),
            default(ImageSource),
            propertyChanged: OnStylistImageChanged);

    private static void OnStylistImageChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not AppointmentCardView control)
            return;

        control.UpdateStylistImage();
    }

    private void UpdateStylistImage()
    {
        StylistMediaView.ImageSource = StylistImage;
    }

    #endregion


    #region Status

    public AppointmentStatus Status
    {
        get => (AppointmentStatus)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    public static readonly BindableProperty StatusProperty =
        BindableProperty.Create(
            nameof(Status),
            typeof(AppointmentStatus),
            typeof(AppointmentCardView),
            AppointmentStatus.Confirmed,
            propertyChanged: OnStatusChanged);

    private static void OnStatusChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not AppointmentCardView control)
            return;

        control.UpdateStatus();
    }

    private void UpdateStatus()
    {
        switch (Status)
        {
            case AppointmentStatus.Pending:

                StatusLabel.Text = "در انتظار تایید";
                StatusLabel.TextColor =
                    (Color)Resources["AppointmentPendingTextColor"];

                StatusBorder.BackgroundColor =
                    (Color)Resources["AppointmentPendingBackgroundColor"];

                break;


            case AppointmentStatus.Confirmed:

                StatusLabel.Text = "تایید شده";
                StatusLabel.TextColor =
                    (Color)Resources["AppointmentSuccessTextColor"];

                StatusBorder.BackgroundColor =
                    (Color)Resources["AppointmentSuccessBackgroundColor"];

                break;


            case AppointmentStatus.CheckedIn:

                StatusLabel.Text = "حضور ثبت شده";
                StatusLabel.TextColor =
                    (Color)Resources["AppointmentSuccessTextColor"];

                StatusBorder.BackgroundColor =
                    (Color)Resources["AppointmentSuccessBackgroundColor"];

                break;


            case AppointmentStatus.InProgress:

                StatusLabel.Text = "در حال انجام";
                StatusLabel.TextColor =
                    (Color)Resources["AppointmentPendingTextColor"];

                StatusBorder.BackgroundColor =
                    (Color)Resources["AppointmentPendingBackgroundColor"];

                break;


            case AppointmentStatus.Completed:

                StatusLabel.Text = "تکمیل شده";
                StatusLabel.TextColor =
                    (Color)Resources["AppointmentSuccessTextColor"];

                StatusBorder.BackgroundColor =
                    (Color)Resources["AppointmentSuccessBackgroundColor"];

                break;


            case AppointmentStatus.Cancelled:

                StatusLabel.Text = "لغو شده";
                StatusLabel.TextColor =
                    (Color)Resources["AppointmentPendingTextColor"];

                StatusBorder.BackgroundColor =
                    (Color)Resources["AppointmentPendingBackgroundColor"];

                break;


            case AppointmentStatus.Rejected:

                StatusLabel.Text = "رد شده";
                StatusLabel.TextColor =
                    (Color)Resources["AppointmentPendingTextColor"];

                StatusBorder.BackgroundColor =
                    (Color)Resources["AppointmentPendingBackgroundColor"];

                break;


            case AppointmentStatus.NoShow:

                StatusLabel.Text = "عدم حضور";
                StatusLabel.TextColor =
                    (Color)Resources["AppointmentPendingTextColor"];

                StatusBorder.BackgroundColor =
                    (Color)Resources["AppointmentPendingBackgroundColor"];

                break;
        }
    }

    #endregion


    #region Command

    public ICommand? Command
    {
        get => (ICommand?)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(AppointmentCardView),
            default(ICommand));


    public object? CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(AppointmentCardView),
            default(object));

    #endregion
}