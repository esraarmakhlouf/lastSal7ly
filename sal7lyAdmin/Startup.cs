using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BL.Infrastructure;
using BL.Secuirty;
using BL.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model;
using Model.APIModels;
using Microsoft.AspNetCore.Internal;
using sal7lyAdmin.Services;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using static BL.SharedCS.Enumrations;
using sal7lyAdmin.Hubs;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Threading;


namespace sal7lyAdmin
{
    public class Startup
    {
        private static IServiceProvider _provider;
        public static UnitOfWork un;
        public static bool SendDataIsStarted = false;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<DBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ISecurity, Security>();

            services.AddSignalR();
            //services.AddTransient<IThreadFunction, ThreadFunction>();
            #region Configure Session
            services.AddDistributedMemoryCache();
            services.AddMvc().AddSessionStateTempDataProvider();
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSession(
                options =>
                {
                    options.Cookie.IsEssential = true;
                    options.Cookie.HttpOnly = true;
                    // options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.IdleTimeout = TimeSpan.FromHours(10);
                }
            );
            #endregion

            #region API Token Config

            services.Configure<TokenManagement>(Configuration.GetSection("tokenManagement"));
            var token = Configuration.GetSection("tokenManagement").Get<TokenManagement>();

            services.AddAuthentication(jwtBearerDefaults =>
            {
                jwtBearerDefaults.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                jwtBearerDefaults.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                    // ValidIssuer = token.Issuer,
                    // ValidAudience = token.Audience
                };
            });

            services.AddScoped<IAuthenticateService, TokenAuthenticationService>();
            services.AddScoped<IUserManagementService, UserManagementService>();

            #endregion
            #region "CORS"
            // include support for CORS
            // More often than not, we will want to specify that our API accepts requests coming from other origins (other domains). When issuing AJAX requests, browsers make preflights to check if a server accepts requests from the domain hosting the web app. If the response for these preflights don't contain at least the Access-Control-Allow-Origin header specifying that accepts requests from the original domain, browsers won't proceed with the real requests (to improve security).
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy-public",
                    builder => builder.AllowAnyOrigin()   //WithOrigins and define a specific origin to be allowed (e.g. https://mydomain.com)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                .Build());
            });

            //mvc service
            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .ConfigureApiBehaviorOptions(options =>
                {
                    //options.SuppressModelStateInvalidFilter = true;
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        // Get an instance of ILogger (see below) and log accordingly.
                        //var errors = ModelState.Values.Select(ent => ent.Errors.Select(err => err.ErrorMessage)).ToList();
                        var errors = context.ModelState.Select(ent => new { key = ent.Key, value = ent.Value.Errors.Select(err => (err.ErrorMessage == null || err.ErrorMessage == "") ? err.Exception.Message : err.ErrorMessage) });
                        var errors_list = new List<string>();
                        foreach (var sublist in errors)
                        {
                            foreach (var item in sublist.value)
                                errors_list.Add(sublist.key + ": " + item);
                        }

                        return new BadRequestObjectResult(new ApiResponseModel
                        {

                            Status = EN_ResponseStatus.Faild,
                            Message = "Model not valid",
                            Data = null,
                            Errors = errors_list.ToArray()
                        });
                    };
                });


            #endregion

            #region Razor

            services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();

            #endregion
            //services.AddControllers()
            //.AddNewtonsoftJson(options =>
            //{
            //    var dateConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter
            //    {
            //        DateTimeFormat = "HH':'mm':'ss"
            //    };

            //    options.SerializerSettings.Converters.Add(dateConverter);
            //    options.SerializerSettings.Culture = new CultureInfo("en-IE");
            //    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            //});
            #region "Swagger"  	    
            //Swagger API documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "sal7lyAdmin API", Version = "v1" });
                c.SwaggerDoc("v2", new Info { Title = "sal7lyAdmin API", Version = "v2" });
                c.OperationFilter<SwaggerFileOperationFilter>();
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            UpdateDatabase(app); //Run Migration

            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationHub>("/NotificationHub");
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            WebHelpers.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            app.UseRequestLocalization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSwagger();// Enable middleware to serve generated Swagger as a JSON endpoint.
                             // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
                             // specifying the Swagger JSON endpoint.
                             //Swagger API documentation


            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "sal7lyAdmin API V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "sal7lyAdmin API V2");
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages();
            //});
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });

            ThreadStart starter = delegate { startProcess(app); };
            Thread tr = new Thread(starter);
            tr.IsBackground = true;

            tr.Start();
        }

        #region Assign Service

        public void startProcess(IApplicationBuilder builder)
        {
            var aTimer = new System.Timers.Timer(60 * 3 * 1000); //one hour in milliseconds
            aTimer.Elapsed += delegate { AssignOrderToTechnical(builder); };
            aTimer.Start();

        }
        private void AssignOrderToTechnical(IApplicationBuilder builder)
        {

            //_provider = builder.ApplicationServices;
            var provider = builder.ApplicationServices;
            var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                using (var _uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>())
                {
                    var Orders = new HashSet<Order>();
                    //Orders still in Ordered action state
                    var OrdersNotAccepted = _uow.OrderRepository.GetAll().Include(ent => ent.OrderService).Where(ent => ent.OrderService != null && ent.OrderTrackActionId == (int)EN_OrderActions.ordered);
                    //Orders no accepted with assigned time less than halh hour
                    var datetime = DateTime.Now;
                    //Orders not accepted but to wait

                    var OrderToWait = _uow.OrderTechnicalAssignmentRepository.GetAll().Where(ent => (datetime - ent.CreationDate).TotalMinutes < 30 && ent.status == (int)EN_OrderTechnicalAssignmentStatus.waitToAnswer).Select(ent => ent.OrderId);
                    Orders = OrdersNotAccepted.Where(ent => !OrderToWait.Contains(ent.Id)).ToHashSet();
                    var technical = new Technical();
                    foreach (var Order in Orders)
                    {
                        technical = _uow.TechnicalsRepository.GetAll().Where(ent => ent.ServiceId == Order.OrderService.ServiceId).OrderBy(ent => ent.LastAssignTime).FirstOrDefault();
                        if (technical != null)
                        {
                            OrderTechnicalAssignment ors = new OrderTechnicalAssignment();
                            ors.OrderId = Order.Id;
                            ors.status = (int)EN_OrderTechnicalAssignmentStatus.waitToAnswer;
                            ors.TechnicalUserId = technical.UsersId;
                            technical.LastAssignTime = DateTime.Now;
                            //change technica last assign
                            _uow.TechnicalsRepository.Update(technical);
                            //change last technical assigned status
                            var last_assigned = _uow.OrderTechnicalAssignmentRepository.Get(ent => ent.OrderId == Order.Id && ent.status == (int)EN_OrderTechnicalAssignmentStatus.waitToAnswer);
                            if (last_assigned != null && last_assigned.status != (int)EN_OrderTechnicalAssignmentStatus.rejected)
                            {
                                last_assigned.status = (int)EN_OrderTechnicalAssignmentStatus.timedout;
                                _uow.OrderTechnicalAssignmentRepository.Update(last_assigned);
                                //Add notification to technical for not available order
                                Notification notification = new Notification();
                                notification.Text = "Order With Code: " + Order.Code + " Not Available yet";
                                notification.ToUSer = last_assigned.TechnicalUserId ?? 0;
                                notification.TypeOfUser = (int)EN_TypeUser.Technical;
                                _uow.NotificationRepository.Add(notification);
                            }
                            _uow.OrderTechnicalAssignmentRepository.Add(ors);
                            //Add notification to techncal to accept order
                            Notification notification2Accept = new Notification();
                            notification2Accept.Text = "A new Order Wait Your Accept With Code: " + Order.Code;
                            notification2Accept.ToUSer = technical.UsersId ?? 0;
                            notification2Accept.URl = AppSession.AppURL + "api/ApiOrder/AcceptOrder/" + Order.Id;
                            notification2Accept.TypeOfUser = (int)EN_TypeUser.Technical;
                            _uow.NotificationRepository.Add(notification2Accept);

                        }
                        else
                        {
                            //Add alert notification to admin with no technical available for service
                            Notification adminNotification = new Notification();
                            adminNotification.IsAlert = true;
                            adminNotification.Text = "No Technical Available for New Order woth Code: " + Order.Code;
                            adminNotification.URl = "/ServicesReport/Index";
                            adminNotification.TypeOfUser = (int)EN_TypeUser.Admin;
                            var isfind = _uow.NotificationRepository.GetMany(ent => ent.URl == adminNotification.URl).Any();
                            if (!isfind) { _uow.NotificationRepository.Add(adminNotification); }
                        }
                        _uow.Save();
                    }
                }
            }
        }
        #endregion
        public static void UpdateDatabase(IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<DBContext>();
            // db = context;
            //context.Database.Migrate();
            un = new UnitOfWork(context);
        }
    }
}
