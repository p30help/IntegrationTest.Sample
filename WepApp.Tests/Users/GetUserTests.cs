using WepApp.Domain.Entities;
using WepApp.Tests.Base;

namespace WepApp.Tests.StepDefinitions.Users
{
    [UsesVerify]
    public class GetUserTests : IntegrationTestContext
    {

        [Fact]
        public async Task GetUserById_ReturnUser()
        {
            // arrange
            var userId = Guid.NewGuid();
            await addUser(userId);

            TestContext.HttpMethod = "GET";
            TestContext.Url = $"Users/GetUser/{userId}";

            // act
            await CallApiAsync();
            var content = await TestContext.ResponseMessage.Content.ReadAsStringAsync();

            // assert
            await VerifyJson(content).UseDirectory("VerifiedFiles");
        }

        [Fact]
        public async Task GetUserByUsername_ReturnUser()
        {
            // arrange
            var userId = Guid.NewGuid();
            await addUser(userId);

            TestContext.HttpMethod = "GET";
            TestContext.Url = $"Users/GetUserByUsername/mahdiradi";

            // act
            await CallApiAsync();
            var content = await TestContext.ResponseMessage.Content.ReadAsStringAsync();

            // assert
            await VerifyJson(content).UseDirectory("VerifiedFiles");
        }


        private async Task addUser(Guid userId)
        {
            var users = new List<User>()
            {
                new User()
                {
                    Id = userId,
                    FullName = "mahdi radi",
                    Gender = "male",
                    UserName = "mahdiradi"
                },
            };

            foreach (var user in users)
            {
                TestFixture.Application.DbContext.Users.Add(user);
            }

            await TestFixture.Application.DbContext.SaveChangesAsync();
        }


    }
}
