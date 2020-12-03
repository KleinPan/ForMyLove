using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ForMyLove
{
    class HttpDownloadHelper
    {
        private static readonly HttpClient HttpClient;

        public static CancellationTokenSource source;

        static HttpDownloadHelper()
        {
            var handler = new SocketsHttpHandler() { };

            HttpClient = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(10) };

            source = new CancellationTokenSource();
        }

        public static async void DownloadLittleAsync(string downloadUrl, string filePath, string fileName)
        {

            try
            {
                var httpclient = HttpDownloadHelper.HttpClient;

                //url = url + "?";

                var response = httpclient.GetByteArrayAsync(downloadUrl).Result;

                if (File.Exists(filePath + "//" + fileName))
                {
                    File.Delete(filePath + "//" + fileName);
                }
                System.IO.FileStream fs;

                fs = new System.IO.FileStream(filePath + "//" + fileName, System.IO.FileMode.CreateNew);
                fs.Write(response, 0, response.Length);
                fs.Close();
                return;
            }
            catch (Exception)
            {

               
            }
         
        }

    }
}
