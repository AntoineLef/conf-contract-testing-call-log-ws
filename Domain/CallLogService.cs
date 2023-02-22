using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using ContractTesting.Controllers;
using System.Linq;
using ContractTesting.Domain.contact;

namespace ContractTesting.Services
{
    public class CallLogService {
    
        private readonly ICallLogRepository callLogRepository;
        private readonly CallLogAssembler callLogAssembler;
        private readonly IContactRepository contactRepository;

        public CallLogService(ICallLogRepository callLogRepository, CallLogAssembler callLogAssembler, IContactRepository contactRepository)
        {
            this.callLogRepository = callLogRepository;
            this.callLogAssembler = callLogAssembler;
            this.contactRepository = contactRepository;
        }

        public List<CallLogDto> FindAllCallLogs(ILogger _logger)
        {
            _logger.LogInformation("Get all call logs");
            List<CallLog> callLogs = callLogRepository.FindAll().ToList();

            callLogs.ForEach(delegate(CallLog callLog){
                string callerId = callLog.CallerId;
                Contact foundContact = contactRepository.GetContactAsync(callerId).Result;
                callLog.Caller = foundContact;
                _logger.LogInformation($"Fetching caller's contact info with Id: {callerId} for call log: {callLog.Id}");
            });

            List<CallLogDto> callLogDtos = new List<CallLogDto>();
            callLogs.ForEach(calllog => callLogDtos.Add(callLogAssembler.Create(calllog)));

            return callLogDtos;
        }

    }
}
