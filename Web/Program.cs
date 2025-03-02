using Application.Email.Service;
using Data.EF;
using Application.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.SeedWorks;
using Data.SeedWorks;
using Application.DTO.Categorys;
using Application.DTO.Products;
using Application.DTO.Adventisements;
using Application.DTO.Orders;
using Application.DTO.OrderDetails;
using Application.DTO.ProductImages;
using Application.DTO.SizeProducts;
using Application.DTO.TotalRevenues;
using Application.DTO.Transactions;
using Application.DTO.VariantsProducts;
using Application.DTO.Carts;

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
builder.Services.AddIdentity<User,Role>(
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
// Configure the Application Cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    // If the LoginPath isn't set, ASP.NET Core defaults the path to /Account/Login.
    options.LoginPath = "/Account/Login"; // Set your login path here
    // If the AccessDenied isn't set, ASP.NET Core defaults the path to /Account/AccessDenied
    options.AccessDeniedPath = "/Account/AccessDenied"; // Set your access denied path here
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
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
