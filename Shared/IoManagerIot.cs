#if IOT
using App3.Contracts;
using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App3
{
    public class IoManagerIot : IIoManager
    {
        private Iot.Device.Mcp9808.Mcp9808 mcp9808;

        public IoManagerIot()
        {
        }

        public void Dispose()
        {
            this.mcp9808.Dispose();
        }

        public UnitsNet.Temperature? DoThing()
        {
            try
            {
                lock (this)
                {
                    if (this.mcp9808 == null)
                    {
                        var settings = new I2cConnectionSettings(22, Iot.Device.Mcp9808.Mcp9808.DefaultI2cAddress);
                        this.mcp9808 = new Iot.Device.Mcp9808.Mcp9808(I2cDevice.Create(settings));
                    }
                }

                return this.mcp9808.Temperature;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return null;
            }
        }
    }
}
#endif
