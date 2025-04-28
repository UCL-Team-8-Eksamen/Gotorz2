using Gotorz2;
using Gotorz2.Client.Services;
using Gotorz2.Components;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpClient<TravelApiService>();


//Register TravelApiService
builder.Services.AddScoped<TravelApiService>();

//Login
builder.Services.AddControllers();

builder.Services.AddTransient<IUserDatabase, UserDatabase>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidAudience = "domain.com", // NOTE: ENTER DOMAIN HERE
        ValidateIssuer = true,
        ValidIssuer = "domain.com", // NOTE: ENTER DOMAIN HERE
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("524C1F22-6115-4E16-9B6A-3FBF185308F2")) // NOTE: THIS SHOULD BE A SECRET KEY NOT TO BE SHARED; A GUID IS RECOMMENDED, DO NOT REUSE THIS GUID
    };
});
// Login

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();


app.UseAuthentication(); //Login

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Gotorz2.Client._Imports).Assembly);

//Login
app.MapControllers();
app.UseAuthorization();

app.Run();