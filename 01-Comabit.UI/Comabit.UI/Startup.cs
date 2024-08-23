// <copyright file="Startup.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit
{
    using Comabit.BL.Communication;
    using Comabit.BL.Company;
    using Comabit.BL.ElasticSearch;
    using Comabit.BL.File;
    using Comabit.BL.Geo;
    using Comabit.BL.Identity;
    using Comabit.BL.Inquiry;
    using Comabit.BL.Log;
    using Comabit.BL.Mail.DTO;
    using Comabit.BL.Match;
    using Comabit.BL.Message;
    using Comabit.BL.Porfolio;
    using Comabit.DL;
    using Comabit.DL.DBDal.Services;
    using Comabit.DL.Interfaces;
    using Comabit.DL.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;
    using StackExchange.Profiling.Storage;
    using System;
    using System.Globalization;
    using System.Reflection;
    using Microsoft.AspNetCore.SignalR;
    using Comabit.UI.SignalR;
    using Microsoft.Azure.SignalR;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
                //.AddUserStore<ApplicationDbContext>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                // enables immediate logout, after updating the user's stat.
                // options.ValidationInterval = TimeSpan.Zero;
                // options.OnRefreshingPrincipal;
            });

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IPortfolioService, PortfolioService>();
            services.AddScoped<IGeoService, GeoService>();
            services.AddScoped<IInquiryService, InquiryService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IElasticSearchService, ElasticSearchService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IMailTemplateService, MailTemplateService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IInquirySellerExclusionService, InquirySellerExclusionService>();

            services.AddScoped<SignInManager<ApplicationUser>, ApplicationSignInManager>();
            services.AddScoped<PortfolioManager>();
            services.AddScoped<CompanyManager>();
            services.AddScoped<SellerCompanyManager>();
            services.AddScoped<GeoManager>();
            services.AddScoped<GeoImportManager>();
            services.AddScoped<UserManager>();
            services.AddScoped<InquiryManager>();
            services.AddScoped<MatchManager>();
            services.AddScoped<ElasticSearchManager>();
            services.AddScoped<FileManager>();
            services.AddScoped<CommunicationManager>();
            services.AddScoped<MessageManager>();
            services.AddScoped<LogManager>();

            services.AddAutoMapper(Assembly.GetAssembly(typeof(Startup)));
            services.AddAutoMapper(Assembly.GetAssembly(typeof(Comabit.BL.Startup)));

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;
            }).AddRazorRuntimeCompilation();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            // Note .AddMiniProfiler() returns a IMiniProfilerBuilder for easy intellisense
            services.AddMiniProfiler(options =>
            {
                // All of this is optional. You can simply call .AddMiniProfiler() for all defaults

                // (Optional) Path to use for profiler URLs, default is /mini-profiler-resources
                options.RouteBasePath = "/profiler";

                // (Optional) Control storage
                // (default is 30 minutes in MemoryCacheStorage)
                // Note: MiniProfiler will not work if a SizeLimit is set on MemoryCache!
                //   See: https://github.com/MiniProfiler/dotnet/issues/501 for details
                (options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(60);

                // (Optional) Control which SQL formatter to use, InlineFormatter is the default
                options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();

                // (Optional) To control authorization, you can use the Func<HttpRequest, bool> options:
                // (default is everyone can access profilers)
                //options.ResultsAuthorize = request => MyGetUserFunction(request).CanSeeMiniProfiler;
                //options.ResultsListAuthorize = request => MyGetUserFunction(request).CanSeeMiniProfiler;
                // Or, there are async versions available:
                //options.ResultsAuthorizeAsync = async request => (await MyGetUserFunctionAsync(request)).CanSeeMiniProfiler;
                //options.ResultsAuthorizeListAsync = async request => (await MyGetUserFunctionAsync(request)).CanSeeMiniProfilerLists;

                // (Optional)  To control which requests are profiled, use the Func<HttpRequest, bool> option:
                // (default is everything should be profiled)
                //options.ShouldProfile = request => MyShouldThisBeProfiledFunction(request);
                //options.ShouldProfile = request => request.HttpContext.User.IsInRole("Admin");
                options.ShouldProfile = request => request.Host.Host == "localhost";

                // (Optional) Profiles are stored under a user ID, function to get it:
                // (default is null, since above methods don't use it by default)
                //options.UserIdProvider = request => MyGetUserIdFunction(request);

                // (Optional) Swap out the entire profiler provider, if you want
                // (default handles async and works fine for almost all applications)
                //options.ProfilerProvider = new MyProfilerProvider();

                // (Optional) You can disable "Connection Open()", "Connection Close()" (and async variant) tracking.
                // (defaults to true, and connection opening/closing is tracked)
                options.TrackConnectionOpenClose = true;

                // (Optional) Use something other than the "light" color scheme.
                // (defaults to "light")
                options.ColorScheme = StackExchange.Profiling.ColorScheme.Auto;

                // The below are newer options, available in .NET Core 3.0 and above:

                // (Optional) You can disable MVC filter profiling
                // (defaults to true, and filters are profiled)
                options.EnableMvcFilterProfiling = true;
                // ...or only save filters that take over a certain millisecond duration (including their children)
                // (defaults to null, and all filters are profiled)
                // options.MvcFilterMinimumSaveMs = 1.0m;

                // (Optional) You can disable MVC view profiling
                // (defaults to true, and views are profiled)
                options.EnableMvcViewProfiling = true;
                // ...or only save views that take over a certain millisecond duration (including their children)
                // (defaults to null, and all views are profiled)
                // options.MvcViewMinimumSaveMs = 1.0m;

                // (Optional) listen to any errors that occur within MiniProfiler itself
                // options.OnInternalError = e => MyExceptionLogger(e);

                // (Optional - not recommended) You can enable a heavy debug mode with stacks and tooltips when using memory storage
                // It has a lot of overhead vs. normal profiling and should only be used with that in mind
                // (defaults to false, debug/heavy mode is off)
                //options.EnableDebugMode = true;
            })
            .AddEntityFramework();

            services.AddSignalR()
                    //.AddAzureSignalR(options =>
                    //{
                    //    options.ConnectionCount = 2;
                    //    // options.AccessTokenLifetime = TimeSpan.FromDays(1);
                    //    // options.ClaimsProvider = context => context.User.Claims;

                    //    //options.GracefulShutdown.Mode = GracefulShutdownMode.WaitForClientsClose;
                    //    //options.GracefulShutdown.Timeout = TimeSpan.FromSeconds(5);
                    //})
                    ;

            services.AddSingleton<IUserIdProvider, CompanyIdProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MessageHub>("/message");
            });

            app.UseMiniProfiler();

            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Account",
                    pattern: "account/{action=Index}/{id?}",
                    defaults: new { Area = "Authentication", Controller = "Account" });
                endpoints.MapControllerRoute(
                    name: "Buyer",
                    pattern: "{area:exists}/{controller=Buyer}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // App_Data off your project root folder
            string baseDir = env.ContentRootPath; // Alternative for Public Web-Content: //string baseDir = env.WebRootPath;
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(baseDir, "App_Data"));

            var cultureInfo = new CultureInfo("de-DE");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseDefaultFiles();
            app.UseStaticFiles();
            //app.UseAzureSignalR(routes =>
            //{
            //    routes.MapHub<MessageHub>("/message");
            //});
        }
    }
}