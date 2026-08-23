using System.Collections.ObjectModel;
using BeautySalonBooking.Maui.Common.Enums;
using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Appointment.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Appointment.ViewModels;

public partial class AppointmentsViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    public AppointmentsViewModel(
        INavigationService navigationService)
    {
        _navigationService = navigationService;

        ChangeAppointmentList(AppointmentListType.Upcoming);
    }

    public ObservableCollection<Appointment> AllAppointments { get; } = new()
    {
        new Appointment
        {
            Organization = "سالن زیبایی رز",
            Services = "کوتاهی مو، براشینگ",
            Date = "۲۵ مرداد ۱۴۰۵",
            Time = "۱۰:۳۰",
            OrganizationImage = "onboarding_mobile.png",
            Status = Status.Confirmed
        },

        new Appointment
        {
            Organization = "آرایشگاه نیلا",
            Services = "رنگ مو",
            Date = "۲۶ مرداد ۱۴۰۵",
            Time = "۱۴:۰۰",
            OrganizationImage = "onboarding_mobile.png",
            Status = Status.Pending
        },

        new Appointment
        {
            Organization = "سالن زیبایی آناهیتا",
            Services = "کراتین مو",
            Date = "۲۸ مرداد ۱۴۰۵",
            Time = "۱۶:۳۰",
            OrganizationImage = "onboarding_mobile.png",
            Status = Status.Confirmed
        },

        new Appointment
        {
            Organization = "سالن زیبایی ماه",
            Services = "میکاپ",
            Date = "۳۰ مرداد ۱۴۰۵",
            Time = "۱۸:۰۰",
            OrganizationImage = "onboarding_mobile.png",
            Status = Status.Pending
        },

        new Appointment
        {
            Organization = "آرایشگاه الیزه",
            Services = "کوتاهی مو",
            Date = "۱۸ مرداد ۱۴۰۵",
            Time = "۱۱:۰۰",
            OrganizationImage = "onboarding_mobile.png",
            Status = Status.Completed
        },

        new Appointment
        {
            Organization = "سالن زیبایی شاین",
            Services = "رنگ و مش",
            Date = "۱۵ مرداد ۱۴۰۵",
            Time = "۱۳:۳۰",
            OrganizationImage = "onboarding_mobile.png",
            Status = Status.Completed
        },

        new Appointment
        {
            Organization = "سالن زیبایی ویونا",
            Services = "پاکسازی پوست",
            Date = "۱۰ مرداد ۱۴۰۵",
            Time = "۱۷:۰۰",
            OrganizationImage = "onboarding_mobile.png",
            Status = Status.Completed
        },

        new Appointment
        {
            Organization = "آرایشگاه مروارید",
            Services = "میکاپ و شینیون",
            Date = "۵ مرداد ۱۴۰۵",
            Time = "۱۹:۰۰",
            OrganizationImage = "onboarding_mobile.png",
            Status = Status.Completed
        }
    };

    public ObservableCollection<Appointment> VisibleAppointments { get; } = new();

    [RelayCommand]
    private Task GoToServiceSelectionAsync()
    {
        return _navigationService.GoToServiceSelectionAsync();
    }

    [RelayCommand]
    private void ChangeAppointmentList(
        AppointmentListType listType)
    {
        switch (listType)
        {
            case AppointmentListType.Upcoming:

                SetVisibleAppointments(
                    AllAppointments.Where(x =>
                        x.Status == Status.Pending ||
                        x.Status == Status.Confirmed));

                break;

            case AppointmentListType.History:

                SetVisibleAppointments(
                    AllAppointments.Where(x =>
                        x.Status == Status.Completed));

                break;
        }
    }

    private void SetVisibleAppointments(
        IEnumerable<Appointment> appointments)
    {
        VisibleAppointments.Clear();

        foreach (var appointment in appointments)
        {
            VisibleAppointments.Add(appointment);
        }
    }

}
public class Appointment
{
    public string Organization { get; set; } = string.Empty;

    public string Services { get; set; } = string.Empty;

    public string Date { get; set; } = string.Empty;

    public string Time { get; set; } = string.Empty;

    public ImageSource? OrganizationImage { get; set; }

    public Status Status { get; set; }
}