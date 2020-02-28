namespace SpecFlowTests.Steps
{
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    [Binding]
    public class StepBindings
    {
        private readonly HttpClient client;

        private HttpResponseMessage lastHttpResponseMessage;

        public StepBindings()
        {
            this.client = new HttpClient();
        }

        [When("I send a GET request to '(.*)'")]
        public async Task WhenISendAGetRequestTo(string uri)
        {
            this.lastHttpResponseMessage = await this.client.GetAsync(uri).ConfigureAwait(false);
        }

        [When("I send a POST request to '(.*)'")]
        public async Task WhenISendAPOSTRequestTo(string uri)
        {
            this.lastHttpResponseMessage = await this.client.PostAsync(uri, null).ConfigureAwait(false);
        }

        [When("I send a POST request to '(.*)' with data in the request body")]
        public async Task WhenISendAPOSTRequestToWithDataInTheRequestBody(string uri, Table table)
        {
            var requestBody = new JObject();
            foreach (TableRow row in table.Rows)
            {
                requestBody.Add(row[0], row[1]);
            }

            var content = new StringContent(requestBody.ToString(Formatting.None), Encoding.UTF8, "application/json");

            this.lastHttpResponseMessage = await this.client.PostAsync(uri, content).ConfigureAwait(false);
        }

        [Then("I receive a (.*) response code")]
        public void ThenIReceiveAResponseCode(int expectedCode)
        {
            Assert.AreEqual((HttpStatusCode)expectedCode, this.lastHttpResponseMessage.StatusCode);
        }

        [Then("the response body contains the text '(.*)'")]
        public async Task ThenTheResponseBodyContainsTheText(string expectedContent)
        {
            string actualContent = await this.lastHttpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.AreEqual(expectedContent, actualContent);
        }
    }
}
