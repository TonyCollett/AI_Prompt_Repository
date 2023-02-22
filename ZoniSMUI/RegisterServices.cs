using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using MudBlazor.Services;
using ZoniSMUI.Services;

namespace ZoniSMUI;

public static class RegisterServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor().AddMicrosoftIdentityConsentHandler();
        builder.Services.AddMudServices();
        builder.Services.AddMemoryCache();
        builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();

        builder.Services.AddScoped<NotificationService>();
        builder.Services.AddHostedService<AutoCloseService>();

        builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy =>
            {
                policy.RequireClaim("jobTitle", "Admin");
            });
        });

        builder.Services.AddSingleton<IDbConnection, DbConnection>();
        builder.Services.AddTransient<ICategoryData, MongoCategoryData>();
        builder.Services.AddTransient<IStatusData, MongoStatusData>();
        builder.Services.AddTransient<IUserData, MongoUserData>();
        builder.Services.AddTransient<ITicketData, MongoTicketData>();
        builder.Services.AddTransient<ICommentData, MongoCommentData>();
        builder.Services.AddTransient<ISettingsData, MongoSettingsData>();
        builder.Services.AddTransient<INotificationData, MongoNotificationData>();
    }
}
