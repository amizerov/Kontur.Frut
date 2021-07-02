using am.BL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

            var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());
            //access form data  
            NameValueCollection formData = provider.FormData;
            //access files  
            IList<HttpContent> files = provider.Files;

            HttpContent file1 = files[0];
            //var thisFileName = file1.Headers.ContentDisposition.FileName.Trim('\"');

            ////-------------------------------------For testing----------------------------------  
            //to append any text in filename.  
            var thisFileName = file1.Headers.ContentDisposition.FileName.Trim('\"') + DateTime.Now.ToString("yyyyMMddHHmmssfff"); //ToDo: Uncomment this after UAT as per Jeeevan  

            List<string> tempFileName = thisFileName.Split('.').ToList();  
            int counter = 0;  
            foreach (var f in tempFileName)  
            {  
                if (counter == 0)  
                    thisFileName = f;  

                if (counter > 0)  
                {  
                    thisFileName = thisFileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + f;  
                }  
                counter++;  
            }  

            ////-------------------------------------For testing----------------------------------  

            string filename = String.Empty;
            Stream input = await file1.ReadAsStreamAsync();
            string directoryName = String.Empty;
            string URL = String.Empty;
            string tempDocUrl = WebConfigurationManager.AppSettings["DocsUrl"];

            if (formData["ClientDocs"] == "ClientDocs")
            {
                var path = HttpRuntime.AppDomainAppPath;
                directoryName = System.IO.Path.Combine(path, "ClientDocument");
                filename = System.IO.Path.Combine(directoryName, thisFileName);

                //Deletion exists file  
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }

                string DocsPath = tempDocUrl + "/" + "ClientDocument" + "/";
                URL = DocsPath + thisFileName;

            }


            //Directory.CreateDirectory(@directoryName);  
            using (Stream file = File.OpenWrite(filename))
            {
                input.CopyTo(file);
                //close file  
                file.Close();
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("DocsUrl", URL);
            return response;
        }

    }
}

