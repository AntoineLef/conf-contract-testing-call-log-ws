using System;
using ContractTesting.Domain.contact;

namespace ContractTesting.Services
{
    public class CallLogAssembler
    {
        private readonly ContactAssembler contactAssembler = new ContactAssembler();
        public CallLogDto Create(CallLog callLog)
        {
            CallLogDto callLogDto = new CallLogDto
            {
                Id = callLog.Id,
                Caller = contactAssembler.Create(callLog.Caller),
                Date = callLog.Date,
                DurationInSeconds = callLog.DurationInSeconds
            };

            return callLogDto;
        }
    }
}