using am.BL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace WebService.v._1.Controllers
{
    public class PicorController : ApiController
    {
        // GET: Order picture
        public byte[] Get(int id)
        {
            string picFile = "D:\\Projects\\Frut\\DB\\ppp\\" + id + ".png";
            if (File.Exists(picFile))
            {
                byte[] bs = File.ReadAllBytes(picFile);
                return bs;
            }
            else
                return new byte[0];
        }
    }
}
