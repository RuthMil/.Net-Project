using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class HostingUnit
    {
        public long HostingUnitKey { get; set; }
        public Host Owner { get; set; }
        public string HostingUnitName { get; set; }
        public bool[,] Diary { get; set; }
        private string hostingDays()
        {
            DateTime tempDate = new DateTime(2019, 1, 1);
            string counter = "";
            for (int i = 0; i < 365; i++)
            {
                if (Diary[tempDate.Month - 1, tempDate.Day - 1])
                {
                    if (!Diary[tempDate.AddDays(-1).Month - 1, tempDate.AddDays(-1).Day - 1]
                        || (tempDate.Day == 1 && tempDate.Month == 1))
                        counter += "Enrty date: " + tempDate.Day.ToString() + '/' + tempDate.Month.ToString() + '\n';
                    if (!Diary[tempDate.AddDays(1).Month - 1, tempDate.AddDays(1).Day - 1]
                        || (tempDate.Day == 31 && tempDate.Month == 12))
                        counter += "Release date: " + tempDate.AddDays(1).Day.ToString() + '/' + tempDate.AddDays(1).Month.ToString() + '\n';
                }
                tempDate = tempDate.AddDays(1);
            }
            return counter;
        }
        public override string ToString()
        {
            return "Hosting Unit ID: " + HostingUnitKey.ToString() + " Hosting Unit Name: " + HostingUnitName + " Owner: " +
                Owner.ToString() + " Busy Days: " + hostingDays(); 
        }
    }
}
