﻿using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace CQRS.WPF.EndPoint.Contracts.Services
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        IEnumerable<CustomerListItem> GetCustomerListItems();

        [OperationContract]
        void CreateCustomer(string firstName, string lastName, DateTime birthDate, string email);
    }
}
