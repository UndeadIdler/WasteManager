﻿using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DAL;

namespace WasteManagement.Content.Waste
{
    /// <summary>
    /// Consignor 的摘要说明
    /// </summary>
    public class Consignor : IHttpHandler
    {
        //private static  List<string> ConsignorNames = DAL.User.GetUserNames(3);

        //private static readonly List<string> ConsignorNames = DAL.User.GetUserNames(3);

        public void ProcessRequest(HttpContext context)
        {
            //System.Threading.Thread.Sleep(2000);
            List<string> ConsignorNames = DAL.User.GetUserNames(3);

            String term = context.Request.QueryString["term"];
            if (!String.IsNullOrEmpty(term))
            {
                term = term.ToLower();

                JArray ja = new JArray();
                foreach (string lang in ConsignorNames)
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