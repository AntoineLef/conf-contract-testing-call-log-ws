using ContractTesting.Domain.contact;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContractTesting.Infra
{
    internal class ContactRestClient : IContactRepository
    {

        private HttpClient client = new HttpClient();
        private string endpoint;

        public ContactRestClient(string contactWsUrl)
        {
            this.endpoint = contactWsUrl;
            Configure(client);
        }

        public async Task<Contact> GetContactAsync(string callerId)
        {
            HttpResponseMessage response = await client.GetAsync($"{endpoint}/contacts/{callerId}");
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            using var contentStream =
               await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<Contact>(contentStream);
        }


        private void Configure(HttpClient client)
        {
            client.BaseAddress = new Uri(endpoint);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}