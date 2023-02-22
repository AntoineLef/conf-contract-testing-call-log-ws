﻿using System;

namespace ContractTesting.Domain.contact
{
    internal class ContactAssembler
    {
        internal ContactDto Create(Contact contact)
        {
            ContactDto contactDto = new ContactDto
            {
                TelephoneNumber = contact.telephoneNumber,
                Address = contact.address,
                Name = contact.name,
            };

            return contactDto;
        }
    }
}