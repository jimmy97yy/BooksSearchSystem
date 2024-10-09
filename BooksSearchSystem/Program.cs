using Microsoft.EntityFrameworkCore; // �ޥ� Entity Framework Core
using BooksSearchSystem.Data; // �ޥ� ApplicationDbContext �Ҧb���R�W�Ŷ�

var builder = WebApplication.CreateBuilder(args);

// �]�m��Ʈw�s���r��A�q appsettings.json ��Ū��
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ���U ApplicationDbContext ��A�Ȯe���A�ϥ� SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// �K�[ MVC ���
builder.Services.AddControllersWithViews();

var app = builder.Build();

// �t�m HTTP �ШD�޹D
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // �w�]�� HSTS �Ȭ� 30 �ѡA�z�i��Ʊ�b�Ͳ����Ҥ���惡��
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
