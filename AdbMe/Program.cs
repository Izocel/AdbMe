using AdbMe.CLI;
using ServiceStack;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: false);


#if DEBUG
builder.Services.AddMvc(options => options.EnableEndpointRouting = false).AddRazorRuntimeCompilation();
#else
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
#endif

var app = builder.Build();

// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHsts();
app.UseStaticFiles();
app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseServiceStack(new AppHost());
app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controller=Home}/{action=Index}/{id?}");
});

new PlugAndPlayPs();

app.Run();
