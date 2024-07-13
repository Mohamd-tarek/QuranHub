
namespace QuranHub.Web.Extensions;

public static class ProgramExtensions
{
    public static void AddCustomDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        var identityConnectionString = builder.Configuration.GetConnectionString("Identity");
        var videoConnectionString = builder.Configuration.GetConnectionString("Video");

        builder.Services.AddDbContext<QuranContext>(options =>
                        options.UseSqlServer(connectionString));

        builder.Services.AddDbContext<IdentityDataContext>(options =>
                        options.UseSqlServer(identityConnectionString));

    }

    public static void AddCustomIdentity(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<QuranHubUser, IdentityRole>()
                 .AddEntityFrameworkStores<IdentityDataContext>()
                 .AddDefaultTokenProviders();
    }
    public static void AddCustomAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication()
        .AddFacebook(opts =>
        {
            opts.AppId = builder.Configuration["Facebook:AppId"];
            opts.AppSecret = builder.Configuration["Facebook:AppSecret"];
        })
        .AddGoogle(opts =>
        {
            opts.ClientId = builder.Configuration["Google:ClientId"];
            opts.ClientSecret = builder.Configuration["Google:ClientSecret"];
        })
        //.AddTwitter(opts =>
        // {
        //     opts.ConsumerKey =  builder.Configuration["Twitter:ApiKey"];
        //     opts.ConsumerSecret =  builder.Configuration["Twitter:ApiSecret"];
        //     opts.RetrieveUserDetails = true;
        // });
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
        {
            opts.TokenValidationParameters.ValidateAudience = false;
            opts.TokenValidationParameters.ValidateIssuer = false;
            opts.TokenValidationParameters.IssuerSigningKey
             = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                  builder.Configuration["BearerTokens:Key"]));

            opts.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];

                    // If the request is for our hub...
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) &&
                        (path.StartsWithSegments("/NotificationHub")))
                    {
                        // Read the token out of the query string
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
        });
    }

    public static void AddCustomCaching(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDistributedSqlServerCache(options => 
        {
            options.ConnectionString = connectionString;
            options.SchemaName = "dbo";
            options.TableName = "SessionData";
        });

        builder.Services.AddSession(options =>
        {
            options.Cookie.Name = "QuranHub.Web.Session";
            options.IdleTimeout = TimeSpan.FromHours(72);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
    }
    public static void AddCustomCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder => {

                builder.WithOrigins("https://localhost:44425")
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
            });
        });
    }

    public static void AddCustomApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<AnalysisService>();

        builder.Services.AddTransient<IQuranRepository, QuranRepository>();

        builder.Services.AddTransient<IHadithRepository, HadithRepository>();

        builder.Services.AddTransient<IDocumentaryRepository, DocumentaryRepository>();

        builder.Services.AddTransient<IPostRepository, PostRepository>();

        builder.Services.AddTransient<INotificationRepository, NotificationRepository>();

        builder.Services.AddTransient<IFollowRepository, FollowRepository>();

        builder.Services.AddTransient<IPrivacySettingRepository, PrivacySettingRepository>();

        builder.Services.AddTransient<IHomeService, HomeService>();

        builder.Services.AddTransient<IProfileService, ProfileService>();

        builder.Services.AddTransient<IUserViewModelsFactory, UserViewModelsFactory>();

        builder.Services.AddTransient<IPostViewModelsFactory, PostViewModelsFactory>();

        builder.Services.AddTransient<IVideoInfoViewModelsFactory, VideoInfoViewModelsFactory>();

        builder.Services.AddTransient<INotificationViewModelsFactory, NotificationViewModelsFactory>();

        builder.Services.AddTransient<IViewModelsService, ViewModelsService>();

        builder.Services.AddScoped<TokenUrlEncoderService>();

        builder.Services.AddScoped<IEmailSender, SMTPEmailSenderUsingMailKit>();

        builder.Services.AddScoped<IEmailService, IdentityEmailService>();
    }
   

}
