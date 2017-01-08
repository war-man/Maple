﻿using MvvmScarletToolkit;
using System.ComponentModel;

namespace InsireBot
{
    /// <summary>
    /// handles data related to data logic and data flow throughout the application
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BusinessViewModelBase<T> : ViewModelBase<T> where T : IIsSelected, ISequence, IIdentifier, INotifyPropertyChanged
    {
        protected readonly IDataService _dataService;

        public BusinessViewModelBase(IDataService dataService) : base()
        {
            _dataService = dataService;
        }
    }
}