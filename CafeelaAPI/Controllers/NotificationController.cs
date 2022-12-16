using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace MohsinFoodAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificationController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        [Route("api/push/android")]
        public void PushNotification()
        {
            try
            {
                //var applicationID = "AAAArE69nn0:APA91bFqI80DpPrdb1s0lT8tf4xUfoigYvGVXQOlBAIq7tCB3224CjqOTyvxiP7L_eKN4uoRWsTVw0661yX4CooMBPgIsddZSMxpypJCKg6l5Q7xgHTGYDlqeVV-HTCn9ud94a5X5a2e";
                //var senderId = "740055424637"; 
                var applicationID = "AAAAdW_YYNU:APA91bHKMOSd6H-VUNDnanbzXURegrZA5S6dSDWQCDagwJh5v-g94hv1_i-j7iJNpD3loWZ-VdUpOM1okpuUFGzOzO7A7eEFpNz3lwiRukyDMRZYvB4DVZY39a2bSTYmtqgULvcb84OW";
                var senderId = "504387625173";
                string deviceId = "fVXI6Og7iJS899D-BXnsy0:APA91bGL5FDcbaVAeWGIFaHJBiXFpydnG5niaq3yT3rXlkaE3VDjpGAu8vi8qLwr1_PMs3DevyXw_QYacpwobqxjysuTu-tuF3yZkiMMAVo4VaMbjXSmwVBpz827m3kSFCwAjOqydPcj";
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = "test",
                        title = "teest",
                        icon = "myicon",
                        sound = "default"

                    }
                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
    }
}
