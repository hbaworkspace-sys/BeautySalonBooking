using BeautySalonBooking.Contracts.Appointment.Enums;
using BeautySalonBooking.Maui.Common.Enums;
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

        control.OrganizationLabel.Text = newValue as string;
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

        control.ServicesLabel.Text = newValue as string;
    }

    #endregion


    #region Date
    public string? Date
    {
        get => (string?)GetValue(DateProperty);
        set => SetValue(DateProperty, value);
    }

    public static readonly BindableProperty DateProperty =
        BindableProperty.Create(
            nameof(Date),
            typeof(string),
            typeof(AppointmentCardView),
            default(string),
            propertyChanged: OnDateChanged);

    private static void OnDateChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not AppointmentCardView control)
            return;

        control.DateLabel.Text = newValue as string;
    }

    #endregion


    #region Time

    public string? Time
    {
        get => (string?)GetValue(TimeProperty);
        set => SetValue(TimeProperty, value);
    }

    public static readonly BindableProperty TimeProperty =
        BindableProperty.Create(
            nameof(Time),
            typeof(string),
            typeof(AppointmentCardView),
            default(string),
            propertyChanged: OnTimeChanged);

    private static void OnTimeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not AppointmentCardView control)
            return;

        control.TimeLabel.Text = newValue as string;
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

        control.OrganizationMediaView.ImageSource =
            newValue as ImageSource;
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
            case AppointmentStatus.Confirmed:

                StatusLabel.Text = "تایید شده";
                StatusLabel.TextColor =
                    (Color)Resources["AppointmentSuccessTextColor"];

                StatusBorder.BackgroundColor =
                    (Color)Resources["AppointmentSuccessBackgroundColor"];

                break;


            case AppointmentStatus.Pending:

                StatusLabel.Text = "در انتظار تایید";
                StatusLabel.TextColor =
                    (Color)Resources["AppointmentPendingTextColor"];

                StatusBorder.BackgroundColor =
                    (Color)Resources["AppointmentPendingBackgroundColor"];

                break;

            case AppointmentStatus.Completed:

                break;

            case AppointmentStatus.Cancelled:

                break;

            case AppointmentStatus.Rejected:

                break;

            case AppointmentStatus.NoShow:

                break;
        }
    }
    #endregion
}