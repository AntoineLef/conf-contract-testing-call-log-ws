using System;
using System.Threading.Tasks;

namespace ContractTesting.Domain.contact
{
    public interface IContactRepository
    {
        Task<Contact> GetContactAsync(string callerId);
    }
}