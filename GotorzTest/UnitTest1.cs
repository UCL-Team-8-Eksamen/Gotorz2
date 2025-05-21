using Bunit;
using Xunit;
using Gotorz.Components.Pages;
using Microsoft.AspNetCore.Components;
using Gotorz.Client.Pages;
using Microsoft.Extensions.DependencyInjection;

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
	}
}