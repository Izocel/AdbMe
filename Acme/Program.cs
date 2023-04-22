using Acme.Models;
using LiteDB;
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

// Open database (or create if doesn't exist)
using(var db = new LiteDatabase(@"MyData.db"))
{
    // Get device collection
    var col = db.GetCollection<Device>("devices");

    // Create your new device instance
    var device = new Device
    { 
        Name = "Device 1",
        LastIp = "192.168.0.228"
    };

    // Create unique index in Name field
    col.EnsureIndex(x => x.Name, true);

    // Insert new device document (Id will be auto-incremented)
    col.Insert(device);

    // Update a document inside a collection
    // device.Name = "Joana Doe";
    // col.Update(device);

    // Use LINQ to query documents (with no index)
    var results = col.Find(x => x.LastIp == "192.168.0.128");
    Console.WriteLine(results);
}

app.Run();
