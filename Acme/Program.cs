using Acme.CLI;
using Acme.Models;
using Database.Dao;
using ServiceStack;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
builder.Services.AddMvc(options => options.EnableEndpointRouting = false).AddRazorRuntimeCompilation();
#else
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
#endif

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}
else
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();
app.UseServiceStack(new AppHost());
app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controller=Home}/{action=Index}/{id?}");
});

var dao = new DeviceDao();
Device device = new Device("Device 1");
device.LastIp = "127.0.0.1";

device = dao.Write(device);
device = dao.Write(device);

//TODO Find a way to use lambda to query instead or in addition to BSON
Func<Device, bool> stm = x => x.Name.StartsWith("x");
var found = dao.Read("$.Name = 'Device 1'");
var founds = dao.ReadAll();


var prog = new Srcpy();
prog.Run();

Thread.Sleep(10000);
prog.Cancel();

app.Run();
