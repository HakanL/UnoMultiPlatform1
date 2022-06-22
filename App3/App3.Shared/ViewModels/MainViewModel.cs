using App3.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace App3.ViewModels
{
    public class MainViewModel
    {
        protected IIoManager IoManager { get; }

        public MainViewModel(IIoManager ioManager)
        {
            IoManager = ioManager;
        }

        public string Message { get => "Hello from our MainViewModel"; }
    }
}
