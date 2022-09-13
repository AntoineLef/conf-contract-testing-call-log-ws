using ContractTesting.Services;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ContractTesting.Infra
{
    internal class CallLogRepositoryInMemory : ICallLogRepository
    {
        private readonly IDictionary<String, CallLog> callLogs = new Dictionary<String, CallLog> ();

        public ICollection<CallLog> FindAll()
        {
            return callLogs.Values;
        }

        public void Save(CallLog callLog)
        {
            callLogs.Add(callLog.Id, callLog);
        }
    }
}