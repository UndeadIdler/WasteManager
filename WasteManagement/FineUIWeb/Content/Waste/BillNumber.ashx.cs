using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DAL;

namespace WasteManagement.Content.Waste
{
    /// <summary>
    /// BillNumber 的摘要说明
    /// </summary>
    public class BillNumber : IHttpHandler
    {
        //private static  List<string> BillNumbers = DAL.Analysis.GetBillNumbers();

        public void ProcessRequest(HttpContext context)
        {
            //System.Threading.Thread.Sleep(2000);
            List<string> BillNumbers = DAL.Analysis.GetBillNumbers();

            String term = context.Request.QueryString["term"];
            if (!String.IsNullOrEmpty(term))
            {
                term = term.ToLower();

                JArray ja = new JArray();
                foreach (string lang in BillNumbers)
                {
                    if (lang.ToLower().Contains(term))
                    {
                        ja.Add(lang);
                    }
                }


                context.Response.ContentType = "text/plain";
                context.Response.Write(ja.ToString());
            }

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