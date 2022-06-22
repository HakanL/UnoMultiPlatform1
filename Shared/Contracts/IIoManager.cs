using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Contracts
{
    public interface IIoManager : IDisposable
    {
        UnitsNet.Temperature? DoThing();
    }
}
