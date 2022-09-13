using ContractTesting.Domain.contact;

namespace ContractTesting.Services
{
    public class CallLogDto
    {
        public string Id { get; set; }
        public ContactDto Caller { get; set; }
        public string Date { get; set; }
        public int DurationInSeconds { get; set; }
    }
}