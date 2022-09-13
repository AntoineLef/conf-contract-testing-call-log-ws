using System;
using System.Collections.Generic;

namespace ContractTesting.Services
{
    public interface ICallLogRepository
    {
        ICollection<CallLog> FindAll();

        void Save(CallLog callLog);
    
    }
}