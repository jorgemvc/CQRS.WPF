﻿using GalaSoft.MvvmLight.Ioc;

namespace Try4Real.Client.Wpf.Business.ViewModels
{
    public class ViewModelLocator
    {
        public CustomerListViewModel CustomerListViewModel
        {
            get { return SimpleIoc.Default.GetInstance<CustomerListViewModel>(); }
        }
        public MainViewModel MainViewModel
        {
            get { return SimpleIoc.Default.GetInstance<MainViewModel>(); }
        }
        public CustomerDetailViewModel CustomerDetailViewModel
        {
            get { return SimpleIoc.Default.GetInstance<CustomerDetailViewModel>(); }
        }
    }
}