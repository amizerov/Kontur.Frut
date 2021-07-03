using am.BL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using WebService.v._1.Models;

namespace WebService.v._1.Controllers
{
    public class NewOplaController : ApiController
    {
        // POST api/values
        public JArray Post([FromBody] CNewOplata o)
        {
            DataTable dt = G.db_select($@"NewOplata 
                {o.nomerp},
                {o.posred}, 
                {o.summap}, 
                {o.firmap},
                {o.contra},
                {o.naznac},
                {o.procen},
                '{o.datepp}',
                '{o.usr}'
            ");
            JArray res = JsonHelper.ToJson(dt);
            return res;
        }

        /// <summary>  
        /// Upload Order Image  
        /// </summary>        
        /// <returns></returns>  
        [HttpPost]
        [Route("api/NewOpla/ImgUpload")]
        public async Task<HttpResponseMessage> ImgUpload()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Write image on disk
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (MultipartFileData file in provider.FileData)
                {
                    string f1 = file.Headers.ContentDisposition.FileName.Replace("\"", "");
                    string f2 = file.LocalFileName;
                    G.WriteLog(f1 + " / " + f2);
                    File.Copy(f2, "D:\\Projects\\Frut\\DB\\ppp\\" + f1);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                G.WriteLog("Error ImgUpload: " + e.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

    }
}

