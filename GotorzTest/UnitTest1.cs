using Bunit;
using Xunit;
using Gotorz.Components.Pages;
using Gotorz.Components.Account.Pages;
using Microsoft.AspNetCore.Components;
using Gotorz.Client.Pages;
using Microsoft.Extensions.DependencyInjection;
using Bunit.TestDoubles;
using Gotorz.Components;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gotorz.Data;

namespace TestProject1
{
	public class UnitTest1
	{
		[Fact]
		public void Clicking_Button_Should_Navigate_To_FlightSearch()
		{
			// Arrange
			using var ctx = new TestContext();
			var mockNav = ctx.Services.GetRequiredService<NavigationManager>();
			var component = ctx.RenderComponent<Home>();

            // Act
            component.Find("button").Click();

            // Assert
            Assert.Equal("http://localhost/flightsearch", mockNav.Uri);
        }

		[Fact]
		public void Should_Show_Page_When_Authorized()
		{

			var roleManagerMock = new Mock<RoleManager<IdentityRole>>(
				new Mock<IRoleStore<IdentityRole>>().Object,
				null, null, null, null);
			var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
			var userManagerMock = new Mock<UserManager<ApplicationUser>>(
				userStoreMock.Object, null, null, null, null, null, null, null, null);


			using var ctx = new TestContext();
			ctx.Services.AddSingleton(roleManagerMock.Object);
			ctx.Services.AddSingleton(userManagerMock.Object);
			var authContext = ctx.AddTestAuthorization();
			authContext.SetAuthorized("Admin", AuthorizationState.Unauthorized);

			// Act
			var cut = ctx.RenderComponent<AdminDashboard>();

			// Assert
			Assert.Contains("Only users with the Admin role can see this page.", cut.Markup);
		}
	}
}