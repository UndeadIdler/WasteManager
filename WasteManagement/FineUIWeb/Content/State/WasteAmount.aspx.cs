using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WasteManagement.Content.State
{
    public partial class WasteAmount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Entity.Waste> wastes = DAL.Waste.GetPartWasteEx(1);
            List<Entity.Waste> products = DAL.Waste.GetPartWasteEx(2);
            Dictionary<object, object> In = new Dictionary<object, object>();
            Dictionary<object, object> Out = new Dictionary<object, object>();
            Dictionary<object, object> NowIn = new Dictionary<object, object>();
            Dictionary<object, object> NowOut = new Dictionary<object, object>();
            foreach (Entity.Waste waste in wastes)
            {
                In.Add(waste.WasteName, DAL.WasteStorage.GetPartSumWasteStorage(waste.WasteName));
                Out.Add(waste.WasteName, DAL.WasteToProduct.GetPartSumWasteToProduct(waste.WasteName));
                NowIn.Add(waste.WasteName, DAL.WasteStorage.GetPartSumWasteStorageEx(waste.WasteName));
                NowOut.Add(waste.WasteName, DAL.WasteToProduct.GetPartSumWasteToProductEx(waste.WasteName));
            }
            foreach (Entity.Waste product in products)
            {
                In.Add(product.WasteName, DAL.ProductDetail.GetPartSumProductDetail(product.WasteName));
                Out.Add(product.WasteName, DAL.ProductOut.GetPartSumProductOut(product.WasteName));
                NowIn.Add(product.WasteName, DAL.ProductDetail.GetPartSumProductDetailEx(product.WasteName));
                NowOut.Add(product.WasteName, DAL.ProductOut.GetPartSumProductOutEx(product.WasteName));
            }







            #region 作废的
            //List<Entity.WasteStorage> ws = DAL.WasteStorage.GetSumWasteStorage();
            //Dictionary<object, object> dic = new Dictionary<object, object>();
            //List<Entity.WasteToProduct> wtp = DAL.WasteToProduct.GetSumWasteToProduct();
            //Dictionary<object, object> dic1 = new Dictionary<object, object>();
            //List<Entity.ProductDetail> pd = DAL.ProductDetail.GetSumProductDetail();
            //Dictionary<object, object> dic2 = new Dictionary<object, object>();
            //List<Entity.ProductOut> po = DAL.ProductOut.GetSumProductOut();
            //Dictionary<object, object> dic3 = new Dictionary<object, object>();
            ////List<Entity.Waste> wastes = DAL.Waste.GetPartWasteEx(1);
            ////List<Entity.Waste> products = DAL.Waste.GetPartWasteEx(2);
            //List<string> All = new List<string>();
            //foreach (Entity.Waste waste in wastes)
            //{
            //    All.Add(waste.WasteName);
            //}
            //foreach (Entity.Waste product in products)
            //{
            //    All.Add(product.WasteName);
            //}
            //for (int i = 0; i < All.Count; i++)
            //{
            //    //for (int j = 0; j < ws.Count; j++)
            //    //{
            //    //    if (dic.ContainsKey(All[i]))
            //    //    {
            //    //        dic.Add(ws[j].WasteName, ws[j].Amount);
            //    //    }
            //    //    else
            //    //    {
            //    //        dic.Add(All[i], 0);
            //    //    }
                    
            //    //}
            //    //if(ws.Contains())
            
            //}

            //List<object> Wastes = new List<object>();
            //for (int i = 0; i < ws.Count; i++)
            //{
            //    dic.Add(ws[i].WasteName, ws[i].Amount);
            //    Wastes.Add(ws[i].WasteName);
            //}
            //for (int i = 0; i < wtp.Count; i++)
            //{
            //    dic1.Add(wtp[i].FromWasteCode, wtp[i].FromAmount);
            //    if(!Wastes.Contains(wtp[i].FromWasteCode))
            //    {
            //        Wastes.Add(wtp[i].FromWasteCode);
            //    }
            //}
            //for (int i = 0; i < pd.Count; i++)
            //{
            //    dic2.Add(pd[i].Name, pd[i].Amount);
            //    Wastes.Add(pd[i].Name);
            //}
            //for (int i = 0; i < po.Count; i++)
            //{
            //    dic3.Add(po[i].WasteName, po[i].Amount);
            //    if(!Wastes.Contains(po[i].WasteName))
            //    {
            //        Wastes.Add(po[i].WasteName);
            //    }
            //}
            #endregion


            //图表只需这部分代码
            highcharts1.Type = HighchartsNET.ChartType.Column;
            highcharts1.SeriesList = new List<HighchartsNET.ChartsSeries> { 
            new HighchartsNET.ChartsSeries { SeriesName = "入库", SeriesData = In},
            new HighchartsNET.ChartsSeries { SeriesName = "出库", SeriesData = Out }
            };
            //highcharts1.SeriesList = new List<HighchartsNET.ChartsSeries> { 
            //new HighchartsNET.ChartsSeries { SeriesName = "废酸入库", SeriesData = dic},
            //new HighchartsNET.ChartsSeries { SeriesName = "废酸出库", SeriesData = dic1 },
            //new HighchartsNET.ChartsSeries { SeriesName = "成品入库", SeriesData = dic2},
            //new HighchartsNET.ChartsSeries { SeriesName = "成品出库", SeriesData = dic3 }
            //};
            //highcharts1.DataKey = "WasteName";
            //highcharts1.DataValue = "Total";

            highcharts1.YAxis = "量(m³)";//Y轴的值;
            highcharts1.Tooltip = "valueSuffix: 'm³'";
            highcharts1.Legend = true;//是否显示标示，默认为false
            //highcharts1.Height = @"50%";
            highcharts1.PlotOptions = @"
                    column:{
                        dataLabels:{
                            enabled:true, 
                            style:{
                                color:'#0000FF'
                            }
                        }
                    }";



            //highcharts1.XAxis = Wastes;
            //highcharts1.PlotOptions = "1111";
            //highcharts1.XAxis = new List<object> { 
            //    Wastes.GetEnumerator()
            //};

            //图表只需这部分代码
            highcharts2.Type = HighchartsNET.ChartType.Column;
            highcharts2.SeriesList = new List<HighchartsNET.ChartsSeries> { 
            new HighchartsNET.ChartsSeries { SeriesName = "入库", SeriesData = NowIn},
            new HighchartsNET.ChartsSeries { SeriesName = "出库", SeriesData = NowOut }
            };

            highcharts2.YAxis = "量(m³)";//Y轴的值;
            highcharts2.Tooltip = "valueSuffix: 'm³'";
            highcharts2.Legend = true;//是否显示标示，默认为false
            highcharts2.PlotOptions = @"
                    column:{
                        dataLabels:{
                            enabled:true, 
                            style:{
                                color:'#0000FF'
                            }
                        }
                    }";


        }
    }
}