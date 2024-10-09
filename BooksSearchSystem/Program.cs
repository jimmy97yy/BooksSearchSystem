using Microsoft.EntityFrameworkCore; // 引用 Entity Framework Core
using BooksSearchSystem.Data; // 引用 ApplicationDbContext 所在的命名空間

var builder = WebApplication.CreateBuilder(args);

// 設置資料庫連接字串，從 appsettings.json 中讀取
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 註冊 ApplicationDbContext 到服務容器，使用 SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 添加 MVC 支持
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 配置 HTTP 請求管道
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // 預設的 HSTS 值為 30 天，您可能希望在生產環境中更改此值
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
