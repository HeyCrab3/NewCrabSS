using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NewCrabSS.WebHandler
{
    public partial class webhandler
    {
        public static string IGetReq(string url)
        {
            HttpClient httpClient = new HttpClient();
            var result = httpClient.GetAsync(url);
            string? content = result.Result.Content.ReadAsStringAsync().Result.ToString();
            // MessageBox.Show(content);
            if (content == null){
                return null;
            }
            return content;
        }
    }
}

