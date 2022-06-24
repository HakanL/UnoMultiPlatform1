using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Models
{
    public class NetworkAdapter
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IList<Address> Addresses { get; set; }
    }
}
