using Application.DTO.Adventisements;
using Application.DTO.Carts;
using Application.DTO.Categorys;
using Application.DTO.OrderDetails;
using Application.DTO.Orders;
using Application.DTO.ProductImages;
using Application.DTO.Products;
using Application.DTO.SizeProducts;
using Application.DTO.TotalRevenues;
using Application.DTO.Transactions;
using Application.DTO.VariantsProducts;
using Application.Email.Service;
using Application.Entities;
using Application.SeedWorks;
using Data.EF;
using Data.SeedWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Configure Entity Framework Core
var connectionString = builder.Configuration.GetConnectionString("SQLServerIdentityConnection") ?? throw new InvalidOperationException("Connection string 'SQLServerIdentityConnection' not found.");
builder.Services.AddDbContext<WebDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Data")));

//Register Repositories 
builder.Services.AddScoped(typeof(IRepository<,>), typeof(RepositoryBase<,>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<SignInManager<User>, SignInManager<User>>();
builder.Services.AddScoped<UserManager<User>, UserManager<User>>();
builder.Services.AddScoped<RoleManager<Role>, RoleManager<Role>>();
//Register Mapper ModelDTO
builder.Services.AddAutoMapper(typeof(CreateUpdateCartRequest));
builder.Services.AddAutoMapper(typeof(CreateUpdateCateoryRequest));
builder.Services.AddAutoMapper(typeof(CreateUpdateProductRequest));
builder.Services.AddAutoMapper(typeof(CreateUpdateAdvertisementRequest));
builder.Services.AddAutoMapper(typeof(CreateUpdateOrderRequest));
builder.Services.AddAutoMapper(typeof(CreateUpdateOrderDetailRequest));
builder.Services.AddAutoMapper(typeof(CreateUpdateProductImageRequest));
builder.Services.AddAutoMapper(typeof(CreateUpdateSizeProductRequest));
builder.Services.AddAutoMapper(typeof(CreateUpdateTotalRevenueRequest));
builder.Services.AddAutoMapper(typeof(CreateUpdateTransactionRequest));
builder.Services.AddAutoMapper(typeof(CreateUpdateVariantsProductRequest));
//Configuration Identity Services
builder.Services.AddIdentity<User, Role>(
    options =>
    {
        // Password settings
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequiredUniqueChars = 0;

    })
    .AddEntityFrameworkStores<WebDbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@.";
});

// Configure token lifespan
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    // Set token lifespan to 2 hours
    options.TokenLifespan = TimeSpan.FromHours(2);
});
builder.Services.AddAuthentication()
.AddGoogle(options =>
{
    options.ClientId = "1068986385057-qdkl6fnokgqu8mojtrg9jdrqtdggv5lk.apps.googleusercontent.com";
    options.ClientSecret = "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";
    options.CallbackPath = "/signin-google";
    // Configure Other Options 
});
builder.Services.AddTransient<EmailSenderService>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("AdminAPI", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "API for Administrators",
        Description = "API for CMS core domain. This domain keeps track of campaigns, campaign rules, and campaign execution."
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("AdminAPI/swagger.json", "Admin API V1");
    });
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("AllowFrontendDev");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
