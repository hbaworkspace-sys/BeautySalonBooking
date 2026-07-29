using BeautySalonBooking.WebApp.Interfaces.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

public class AuthGuard : ComponentBase
{
    [Inject] protected ISessionManager SessionManager { get; set; }
    [Inject] protected NavigationManager NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var isValid = await SessionManager.ValidateCurrentSessionAsync();
        if (!isValid)
        {
            NavigationManager.NavigateTo("/", forceLoad: true);
        }
    }
}