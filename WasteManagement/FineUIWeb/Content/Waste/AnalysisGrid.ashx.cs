using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json.Linq;

namespace WasteManagement.Content.Waste
{
    /// <summary>
    /// AnalysisGrid 的摘要说明
    /// </summary>
    public class AnalysisGrid : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string analysisId = context.Request.QueryString["id"];
            int analysisIdInt = Convert.ToInt32(analysisId);

            JObject jo = new JObject();

            JArray ja = new JArray();

            Random rd = new Random();
            for (int i = 0; i < 3; i++)
            {
                JObject joItem = new JObject();

                if (i == 0)
                {
                    joItem.Add("type", "入学");
                }
                else if (i == 1)
                {
                    joItem.Add("type", "期中");
                }
                else if (i == 2)
                {
                    joItem.Add("type", "期末");
                }

                int randomMinValue = 80;
                int randomMaxValue = 100;
                if (analysisIdInt % 2 == 0)
                {
                    randomMinValue = 40;
                    randomMaxValue = 80;
                }
                joItem.Add("yuwen", rd.Next(randomMinValue, randomMaxValue));
                joItem.Add("shuxue", rd.Next(randomMinValue, randomMaxValue));
                joItem.Add("yingwen", rd.Next(randomMinValue, randomMaxValue));
                joItem.Add("wuli", rd.Next(randomMinValue, randomMaxValue));
                joItem.Add("huaxue", rd.Next(randomMinValue, randomMaxValue));

                ja.Add(joItem);
            }

            jo.Add("data", ja);
            jo.Add("total", ja.Count);

            context.Response.ContentType = "text/plain";
            context.Response.Write(jo.ToString(Newtonsoft.Json.Formatting.None));

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}