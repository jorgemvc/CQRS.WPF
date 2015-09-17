﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Try4Real.Client.Wpf.Business.Services;
using Try4Real.Client.Wpf.Business.ViewModels.Base;
using Try4Real.EndPoint.Contracts;

namespace Try4Real.Client.Wpf.Business.ViewModels
{
    public class CustomerListViewModel : LoadingViewModelBase, IViewModelTab
    {
        private readonly IMessenger _messenger;
        private readonly ICustomerListService _customerListService;

        public string Title { get { return "Customer list"; } }
        public bool CanClose { get { return false; } }
        public bool IsClosable { get { return false; } }
        public ObservableCollection<CustomerListItem> Customers
        {
            get { return GetNotifiableProperty<ObservableCollection<CustomerListItem>>("Customers"); }
            set { SetNotifiableProperty("Customers", value); }
        }
        public CustomerListItem SelectedCustomer
        {
            get { return GetNotifiableProperty<CustomerListItem>("SelectedCustomer"); }
            set { SetNotifiableProperty("SelectedCustomer", value); }
        }
        public ICommand CreateCustomerCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand DeleteCustomerCommand { get; private set; }
        public ICommand OpenCustomerDetailsCommand { get; private set; }

        // ----- Constructors
        public CustomerListViewModel(IMessenger messenger, ICustomerListService customerListService)
        {
            _messenger = messenger;
            _customerListService = customerListService;
            CreateCustomerCommand = new RelayCommand(CreateCustomer);
            RefreshCommand = new RelayCommand(RefreshCustomerList);
            DeleteCustomerCommand = new RelayCommand<CustomerListItem>(DeleteCustomer);
            OpenCustomerDetailsCommand = new RelayCommand<CustomerListItem>(OpenCustomerDetails);
        }

        // ----- Overrides
        public void Boot()
        {
            RefreshCustomerList();
        }

        // ----- Internal logics
        private void CreateCustomer()
        {
            var random = new Random((int) DateTime.Now.Ticks);
            
            _customerListService.CreateCustomer(
                "FirstName" + random.Next(0, 50),
                "LastName" + random.Next(0, 50),
                DateTime.Now.Subtract(TimeSpan.FromDays(random.Next(0, 50 * 365))),
                random.Next(100, 10000) + "@gmail.com");

            RefreshCustomerList();
        }
        private void DeleteCustomer(CustomerListItem customerListItem)
        {
            //_customerListService.DeleteCustomer(customerListItem.Id);
            Customers.Remove(customerListItem);
        }
        private void OpenCustomerDetails(CustomerListItem customerListItem)
        {
            _messenger.Send(new OpenCustomerDetailsMessage(customerListItem.Id));
        }
        private async void RefreshCustomerList()
        {
            var customers = await Async(() => _customerListService.GetCustomers());
            Customers = new ObservableCollection<CustomerListItem>(customers);
        }
    }
}