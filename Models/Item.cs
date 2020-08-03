using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDatabaseFirstApproach.Models
{
    public class Item
    {
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}