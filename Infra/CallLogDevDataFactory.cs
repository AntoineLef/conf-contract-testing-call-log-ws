using ContractTesting.Services;
using System.Collections.Generic;

namespace ContractTesting.Infra
{
    public class CallLogDevDataFactory
    {
        public List<CallLog> CreateMockData()
        {
            List<CallLog> callLogs = new List<CallLog>();

            CallLog callLog1 = new CallLog
            {
                Id = "1",
                CallerId = "3",
                Date = "2016-07-31T16:45:00Z",
                DurationInSeconds = 65
            };
            callLogs.Add(callLog1);

            CallLog callLog2 = new CallLog
            {
                Id = "2",
                CallerId = "2",
                Date = "2016-06-31T15:29:00Z",
                DurationInSeconds = 99
            };
            callLogs.Add(callLog2);

            CallLog callLog3 = new CallLog
            {
                Id = "3",
                CallerId = "2",
                Date = "2016-07-30T08:32:33Z",
                DurationInSeconds = 22
            };
            callLogs.Add(callLog3);

            return callLogs;
        }
    }
}