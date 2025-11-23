using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesManagement.Data;
using SalesManagement.Models.Entities;
using SalesManagement.Repositories.Implementations;
using SalesManagement.Repositories.Interfaces;
using SalesManagement.Services.Implementations;
using SalesManagement.Services.Interfaces;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SalesManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SalesManagementContext") ?? throw new InvalidOperationException("Connection string 'SalesManagementContext' not found.")));

builder.Services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
builder.Services.AddScoped<IGenericRepository<Customer>, GenericRepository<Customer>>();
builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
builder.Services.AddScoped<IGenericRepository<ProductType>, GenericRepository<ProductType>>();
builder.Services.AddScoped<IGenericRepository<Transaction>, GenericRepository<Transaction>>();
builder.Services.AddScoped<IGenericRepository<TransactionProduct>, GenericRepository<TransactionProduct>>();
builder.Services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();

builder.Services.AddScoped<IService<Category>, CategoryService>();
builder.Services.AddScoped<IService<Customer>, CustomerService>();
builder.Services.AddScoped<IService<Product>, ProductService>();
builder.Services.AddScoped<IService<User>, UserService>();
builder.Services.AddScoped<IService<Transaction>, TransactionService>();
builder.Services.AddScoped<IService<TransactionProduct>, TransactionProductService>();
builder.Services.AddScoped<IService<ProductType>, ProductTypeService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransactionProductService, TransactionProductService>();
builder.Services.AddScoped<IProductTypeService, ProductTypeService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
