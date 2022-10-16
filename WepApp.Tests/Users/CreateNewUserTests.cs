using WepApp.Tests.Base;

namespace WepApp.Tests.StepDefinitions.Users
{
    public class CreateNewUserTests : IntegrationTestContext
    {
        [Theory]
        [InlineData("","mahdi")]
        [InlineData("mahdi", "")]
        [InlineData("", "")]
        public async Task CreateNewUser_ReturnError(string username, string fullname)
        {
            // arrange
            TestContext.HttpMethod = "POST";
            TestContext.Url = "Users/CreateNewUser";

            // act
            TestContext.RequestBody = new Requests.CreateUserRequest()
            {
                UserName = username,
                FullName = fullname
            };
            await CallApiAsync();

            // assert
            TestContext.ResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.InternalServerError);
        }

        [Theory]
        [InlineData("a", "a")]
        [InlineData("asdasd", "asdasda")]
        [InlineData("45345345", "342342")]
        public async Task CreateNewUser_ReturnOk(string username, string fullname)
        {
            // arrange
            TestContext.HttpMethod = "POST";
            TestContext.Url = "Users/CreateNewUser";

            // act
            TestContext.RequestBody = new Requests.CreateUserRequest()
            {
                UserName = username,
                FullName = fullname
            };
            await CallApiAsync();

            // assert
            TestContext.ResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }


    }
}
