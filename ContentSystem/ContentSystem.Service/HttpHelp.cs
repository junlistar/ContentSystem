using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ContentSystem.Service
{
    public class HttpHelp
    {

        /// <summary>
        /// 获取接口返回json
        /// </summary>
        /// <param name="url">接口</param>
        /// <param name="token">token</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static string GetResponseJson(string url, Hashtable param) {
            //formData用于保存提交的信息
            string formData = "";
            foreach (DictionaryEntry de in param)
            {
                formData += de.Key.ToString() + "=" + de.Value.ToString() + "&";
            }
            if (formData.Length > 0)
            {
                formData = formData.Substring(0, formData.Length - 1); //去除最后一个 '&'
            }
            //把提交的信息转码（post提交必须转码）
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(formData);

            //开始创建请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";    //提交方式：post
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; SV1; .NET CLR 2.0.1124)";
            request.AllowAutoRedirect = true;
            request.KeepAlive = true;

            Stream newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);//将请求的信息写入request
            newStream.Close();


            //向服务器发送请求
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream2 = response.GetResponseStream();//获得回应的数据流
                                                          //将数据流转成 String
            return new StreamReader(stream2, System.Text.Encoding.UTF8).ReadToEnd();

        }
    }
}
