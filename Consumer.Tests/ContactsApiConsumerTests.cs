using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ContractTesting.Domain.contact;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using PactNet;
using Xunit;

namespace Consumer.Tests
{
    public class ContactsApiConsumerTest
    {
        private readonly IPactBuilderV3 pactBuilder;

        public ContactsApiConsumerTest()
        {
            // Use default pact directory ..\..\pacts and default log
            // directory ..\..\logs
            var pact = Pact.V3("Calllogs API Consumer", "Calllogs API", new PactConfig {
                PactDir = @"..\pacts",
                DefaultJsonSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            });

            // Initialize Rust backend
            this.pactBuilder = pact.UsingNativeBackend();
        }

        [Fact]
        public async Task GetContact_WhenTheContactExists_ReturnsTheTelNumberOfContact()
        {
            // Arrange
            this.pactBuilder
                .UponReceiving("A GET request to retrieve the contact")
                    .Given("There is a contact with id '123'")
                    .WithRequest(HttpMethod.Get, "/contacts/123")
                    .WithHeader("Accept", "application/json")
                .WillRespond()
                    .WithStatus(HttpStatusCode.OK)
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithJsonBody( new
                    {
                        telephoneNumber = "4189550764"
                    });

            this.pactBuilder.Verify(async ctx =>
            {
                // 
                var client = new ContractTesting.Infra.ContactRestClient(ctx.MockServerUri.ToString());

                // 
                var contact = await client.GetContactAsync("123");

                // then
                Assert.Equal("4189550764", contact.telephoneNumber);
            });
        }
    }
}
