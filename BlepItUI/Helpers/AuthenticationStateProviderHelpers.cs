using Microsoft.AspNetCore.Components.Authorization;

namespace BlepItUI.Helpers;

public static class AuthenticationStateProviderHelpers
{
    public static async Task<User> GetUserFromAuthAsync(this AuthenticationStateProvider provider, IUserData userData)
    {
        var authState = await provider.GetAuthenticationStateAsync();
        string objectId = authState.User.Claims.FirstOrDefault(c => c.Type.Contains("objectidentifier"))?.Value;
        if (objectId is null)
        {
            return null;
        }
        return await userData.GetUserFromAuthentication(objectId);
    }
}