using Bunit;
using Xunit;
using Gotorz2.Components.Pages;
using Gotorz2.Client.Pages;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void HomeComponentRendersCorrectly()
        {
            // Arrange
            using var ctx = new TestContext();

            //Act
            var cut = ctx.RenderComponent<Home>();

            // Assert

            cut.MarkupMatches("<h1>Hello, world!</h1>");
        }
    }
}