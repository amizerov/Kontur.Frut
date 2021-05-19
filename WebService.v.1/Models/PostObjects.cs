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
}