using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.v._1.Models
{
    public class CUpdateOplataField
    {
        public int Oplata_ID { get; set; }
        public string FieldName { get; set; }
        public bool BoolVal { get; set; }
        public string StrVal { get; set; }
        public int IntVal { get; set; }
        public string usr { get; set; }
    }
    public class CPaied
    {
        public int Oplata_ID { get; set; }
        public bool IsPaied { get; set;  }
        public String usr { get; set; }
    }
    public class CRecieved
    {
        public int Oplata_ID { get; set; }
        public bool IsRecieved { get; set; }
        public String usr { get; set; }
    }
    public class CVidano
    {
        public int Oplata_ID { get; set; }
        public bool IsVidano { get; set; }
        public String usr { get; set; }
    }

    public class CNewOplata
    {
        public int nomerp { get; set; }
        public int posred { get; set; }
        public double summap { get; set; }
        public int firmap { get; set; }
        public int contra { get; set; }
        public int naznac { get; set; }
        public int procen { get; set; }
        public string datepp { get; set; }
        public string imagep { get; set; }
        public string usr { get; set; }
    }
}