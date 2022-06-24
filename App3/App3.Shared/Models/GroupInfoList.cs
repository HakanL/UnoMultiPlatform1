using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Models
{
    public class GroupInfoList : List<object>
    {
        public GroupInfoList(IEnumerable<object> items) : base(items) { }

        public object Key { get; set; }

        public string Title { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
