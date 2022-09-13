using System;
using ContractTesting.Domain.contact;

namespace ContractTesting.Services
{

    public class CallLog
    {
        public string Id { get; set; }
        public  string CallerId { get; set; }
        public Contact Caller { get; set; }
        public string Date { get; set; }
        public int DurationInSeconds { get; set; }

    }
}