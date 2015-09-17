﻿using System;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using SimpleInjector;
using Try4Real.Client.Wpf.Business.Services;
using Try4Real.Client.Wpf.Business.ViewModels;

namespace Try4Real.Client.Wpf.Business
{
    public class Ioc
    {
        private static readonly Ioc _instance = new Ioc();
        public static Ioc Instance
        {
            get { return _instance; }
        }

        private readonly Container _container;

        public Ioc()
        {
            _container = new Container();
        }

        public void Init(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterService(_container);

            _container.RegisterSingleton<ICustomerListService, CustomerListService>();
            _container.RegisterSingleton<ICustomerDetailService, CustomerDetailService>();

            _container.RegisterSingleton<CustomerListViewModel>();
            _container.RegisterSingleton<MainViewModel>();
            _container.RegisterSingleton<CustomerDetailViewModel>();

            _container.RegisterSingleton(() => Messenger.Default);

            _container.RegisterSingleton(typeof(IViewModelFactory<>), typeof(ViewModelFactory<>));
        }

        public T GetInstance<T>() where T : class
        {
            return _container.GetInstance<T>();
        }
    }
}