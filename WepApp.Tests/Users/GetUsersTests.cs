using WepApp.Domain.Entities;
using WepApp.Tests.Base;

namespace WepApp.Tests.StepDefinitions.Users
{
    [UsesVerify]
    public class GetUsersTests : IntegrationTestContext
    {
        [Fact]
        public async Task GetUsersList_ReturnListOfUsers()
        {
            // arrange
            await addUsers();

            TestContext.HttpMethod = "GET";
            TestContext.Url = $"Users/GetUsers";

            // act
            await CallApiAsync();
            var apiResponseJson = await TestContext.ResponseMessage.Content.ReadAsStringAsync();

            // assert
            VerifierSettings.ScrubInlineGuids();
            await VerifyJson(apiResponseJson).UseDirectory("VerifiedFiles");
        }

        private async Task addUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Id = Guid.NewGuid(),
                    FullName = "mahdi radi",
                    Gender = "male",
                    UserName = "user2"
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    FullName = "fati",
                    Gender = "female",
                    UserName = "user3"
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    FullName = "james",
                    Gender = "male",
                    UserName = "user1"
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    FullName = "vort",
                    Gender = "",
                    UserName = "user4"
                }
            };

            foreach (var user in users)
            {
                TestFixture.Application.DbContext.Users.Add(user);
            }

            await TestFixture.Application.DbContext.SaveChangesAsync();
        }

    }
}
