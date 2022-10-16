using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace WepApp.Tests.Base
{
    public class IntegrationTestContext
    {
        public UserTestContext TestContext { get; }
        public TestFixture TestFixture { get; }

        public IntegrationTestContext()
        {
            TestContext = new UserTestContext();
            TestFixture = new TestFixture();
        }

        public async Task CallApiAsync()
        {
            if (TestContext.HttpMethod == "GET")
            {
                TestFixture.Client.DefaultRequestHeaders.Accept.Clear();
                TestFixture.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //TestFixture.Client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                TestContext.ResponseMessage = await TestFixture.Client.GetAsync(TestContext.Url);
            }
            else if (TestContext.HttpMethod == "PUT")
            {
                var bodyJson = JsonConvert.SerializeObject(TestContext.RequestBody);
                HttpContent httpContent = new StringContent(bodyJson, System.Text.Encoding.UTF8, "application/json");

                TestContext.ResponseMessage = await TestFixture.Client.PutAsync(TestContext.Url, httpContent);
            }
            else if (TestContext.HttpMethod == "DELETE")
            {
                TestContext.ResponseMessage = await TestFixture.Client.DeleteAsync(TestContext.Url);
            }
            else if (TestContext.HttpMethod == "POST")
            {
                var bodyJson = JsonConvert.SerializeObject(TestContext.RequestBody);
                HttpContent httpContent = new StringContent(bodyJson, System.Text.Encoding.UTF8, "application/json");

                TestContext.ResponseMessage = await TestFixture.Client.PostAsync(TestContext.Url, httpContent);
            }
        }

        public async Task ThenStatusCodeIsAsync(int statusCode)
        {
            var content = await TestContext.ResponseMessage.Content.ReadAsStringAsync();

            ((int)TestContext.ResponseMessage.StatusCode).Should().Be(statusCode);
        }

        public async void ThenExceptionThrownContainMessage(string exceptionMessage)
        {
            if (TestContext.ResponseMessage.IsSuccessStatusCode)
            {
                exceptionMessage.Should().BeNullOrEmpty();
            }
            else
            {
                var content = await TestContext.ResponseMessage.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(exceptionMessage))
                {
                    content.Should().BeNullOrEmpty();
                }
                else
                {
                    content.Should().ContainAny(exceptionMessage);
                }
            }

        }
    }
}
