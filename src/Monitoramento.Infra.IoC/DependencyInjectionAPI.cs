//using System;

//namespace Monitoramento.Infra.IoC
//{
//    public static class DependencyInjectionAPI
//    {
//        public static IServiceCollection AddInfraStructureAPI(this IServiceCollection services, IConfiguration configuration)
//        {
//            services.AddDbContext<ApplicationDbContext>(
//                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")
//                , b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

//            services.AddIdentity<ApplicationUser, IdentityRole>()
//                .AddEntityFrameworkStores<ApplicationDbContext>()
//                .AddDefaultTokenProviders();

//            services.AddScoped<ICategoryRepository, CategoryRepository>();
//            services.AddScoped<IProductRepository, ProductRepository>();
//            services.AddScoped<IProductService, ProductService>();
//            services.AddScoped<ICategoryService, CategoryService>();

//            services.AddScoped<IAuthenticate, AuthenticateService>();
//            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

//            services.AddAutoMapper(typeof(MappingProfile));

//            var myHandlers = AppDomain.CurrentDomain.Load("CV.Application");
//            services.AddMediatR(myHandlers);

//            return services;
//        }

//    }
//}
