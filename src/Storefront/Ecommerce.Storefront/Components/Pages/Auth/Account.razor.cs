
using Ecommerce.Storefront.ApiClients.Identity.Contracts;

namespace Ecommerce.Storefront.Components.Pages.Auth;

public partial class Account
{
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private string _email = string.Empty;
    private string _phone = string.Empty;

    private bool _isSaving;
    private bool _saveSuccess;
    private string? _saveError;

    protected override async Task OnInitializedAsync()
    {
        var profile = await IdentityApiClient.GetProfileAsync();

        if (profile is null)
            return;

        _firstName = profile.FirstName;
        _lastName = profile.LastName;
        _email = profile.Email;
        _phone = profile.PhoneNumber;
    }

    private async Task SaveProfile()
    {
        if (_isSaving)
            return;

        _isSaving = true;
        _saveSuccess = false;
        _saveError = null;

        try
        {
            var request = new UpdateProfileRequest(
                _firstName,
                _lastName,
                _email,
                _phone);

            var success = await IdentityApiClient.UpdateProfileAsync(
                request);

            if (success)
            {
                _saveSuccess = true;
                return;
            }

            _saveError = "Unable to update your profile.";
        }
        catch
        {
            _saveError = "An error occurred while updating your profile.";
        }
        finally
        {
            _isSaving = false;
        }
    }

    private static string GetInitials(ClaimsPrincipal user)
    {
        var email = user.FindFirstValue(ClaimTypes.Email);

        return string.IsNullOrWhiteSpace(email)
            ? "U"
            : email[..1].ToUpperInvariant();
    }
}