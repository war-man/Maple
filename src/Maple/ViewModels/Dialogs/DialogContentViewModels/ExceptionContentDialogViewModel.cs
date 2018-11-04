﻿using System;

using Maple.Core;

namespace Maple
{
    public class ExceptionContentDialogViewModel : ObservableObject
    {
        private Exception _exception;
        public Exception Exception
        {
            get { return _exception; }
            set { SetValue(ref _exception, value, OnChanged: () => OnPropertyChanged(nameof(ExceptionText))); }
        }

        public string ExceptionText => Exception?.ToString();
    }
}
