﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class Order
    {
        public long HostingUnitKey { get; set; }
        public long GuestRequestKey { get; set; }
        public long OrderKey { get; set; }
        public Enum_s.OrderStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime OrderDate { get; set; }
        public string GuestName { get; set; }
        public override string ToString()
        {
            return "Hosting Unit ID: " + HostingUnitKey.ToString() + " Hosting Request ID: " + GuestRequestKey.ToString() +
                " Order ID: " + OrderKey.ToString() + " Order Status: " + Status + " Order Creation Date: " +
                CreateDate.ToString("d") + " Offer Date Submission: " + OrderDate.ToString("d"); 
        }
    }
}
