using Autokereskedes_BS.Components;
using Autokereskedes_BS.Components.Account;
using Autokereskedes_BS.Data;
using Autokereskedes_BS.Seed;
using Autokereskedes_BS.Services;
using Autokereskedes_BS.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

internal class Program
{

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<IdentityUserAccessor>();
        builder.Services.AddScoped<IdentityRedirectManager>();
        builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
        builder.Services.AddBlazorBootstrap();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        })
            .AddIdentityCookies();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddIdentityCore<ApplicationUser>(
            options => {
                options.SignIn.RequireConfirmedAccount = true;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(builder.Configuration.GetSection("Lockout").GetValue<int>("LockoutTimeSpanMinutes"));
                options.Lockout.MaxFailedAccessAttempts = builder.Configuration.GetSection("Lockout").GetValue<int>("MaxFailedAccessAttempts");

                options.Password.RequireDigit = builder.Configuration.GetSection("PasswordRules").GetValue<bool>("RequireDigit");
                options.Password.RequiredLength = builder.Configuration.GetSection("PasswordRules").GetValue<int>("RequiredLength");
                options.Password.RequireNonAlphanumeric = builder.Configuration.GetSection("PasswordRules").GetValue<bool>("RequireNonAlphanumeric");
                options.Password.RequireUppercase = builder.Configuration.GetSection("PasswordRules").GetValue<bool>("RequireUppercase");
                options.Password.RequireLowercase = builder.Configuration.GetSection("PasswordRules").GetValue<bool>("RequireLowercase");
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
        builder.Services.AddScoped<ICarServices, CarServices>();
        builder.Services.AddScoped<ExtUserService>();

        var app = builder.Build();


        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }

        var AutoSeed = builder.Configuration.GetValue<bool>("AutoSeed");
        if (AutoSeed)
        {
            CreateAndUpdateDatabase(app);
            AddBaseUser(app);
            AddBaseRoles(app);
            AddBaseUserInRoles(app);
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.MapAdditionalIdentityEndpoints();

        app.Run();
    }

    private static void AddBaseUserInRoles(WebApplication app)
    {
        var services = app.Services.CreateScope().ServiceProvider;
        Thread.Sleep(2000);
        var rm = services.GetService<RoleManager<IdentityRole>>();
        var um = services.GetService<UserManager<ApplicationUser>>();
        if (rm.Roles.Any() && um.Users.Any())
        {
            foreach(var uir in Seeds.BaseRolesForUsers())
            {
                var user = um.Users.FirstOrDefault(x => x.UserName == uir.Item1);
                if(user != null)
                    um.AddToRoleAsync(user, uir.Item2);
            }
        }
    }

    private static void AddBaseRoles(WebApplication app)
    {
        var services = app.Services.CreateScope().ServiceProvider;
        var rm = services.GetService<RoleManager<IdentityRole>>();
        if (!rm.Roles.Any())
        {
            try
            {
                foreach(var role in Seeds.getBaseRoles())
                {
                    rm.CreateAsync(role).GetAwaiter().GetResult();
                }
            }catch (Exception ex)
            {
                Console.WriteLine("Hiba a Role hozzáadásánál!");
            }
        }
    }

    private static void AddBaseUser(WebApplication app)
    {
        var services = app.Services.CreateScope().ServiceProvider;
        var um = services.GetService<UserManager<ApplicationUser>>();
        if (!um.Users.Any())
        {
            try
            {
                foreach (var u in Seeds.getBaseUsers())
                {
                    ApplicationUser user = Activator.CreateInstance<ApplicationUser>();
                    user.UserName = u.Item1.UserName;
                    user.FullName = u.Item1.FullName;
                    user.Email = u.Item1.Email;
                    user.Active = u.Item1.Active;
                    user.EmailConfirmed = u.Item1.EmailConfirmed;
                    um.CreateAsync(user, u.Item2);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba");
            }
        }
    }

    private static void CreateAndUpdateDatabase(WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hiba a migráció közben.");
        }

    }

    
}