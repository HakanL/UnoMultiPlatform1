using App3.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App3
{
    public partial class IoManagerNone : IIoManager
    {
        public IoManagerNone()
        {
        }

        public void Dispose()
        {
        }

        public UnitsNet.Temperature? DoThing()
        {
            return null;
        }
    }
}
