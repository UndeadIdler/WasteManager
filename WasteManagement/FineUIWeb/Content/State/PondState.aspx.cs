using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WasteManagement.Content.State
{
    public partial class PondState : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //上面部分只是模拟数据
            //DataTable dt = new DataTable();
            //dt.Columns.Add("a");
            //dt.Columns.Add("b");
            //DataRow dr = dt.NewRow();
            //dr[0] = "2013/1";
            //dr[1] = "50";
            //dt.Rows.Add(dr);
            //DataRow dr1 = dt.NewRow();
            //dr1[0] = "2013/2";
            //dr1[1] = "150";
            //dt.Rows.Add(dr1);


            //DataTable dt = DAL.Pond.GetAllPond2();
            List<Entity.Pond> pond = DAL.Pond.GetAllPondEx();
            Dictionary<object, object> dic = new Dictionary<object, object>();
            Dictionary<object, object> dic1 = new Dictionary<object, object>();

            for (int i = 0; i < pond.Count; i++)
            {
                dic.Add(pond[i].Name, pond[i].Capacity);
                dic1.Add(pond[i].Name, pond[i].Used);
            }


            //图表只需这部分代码
            highcharts1.Type = HighchartsNET.ChartType.Column;
            highcharts1.SeriesList = new List<HighchartsNET.ChartsSeries> { 
            new HighchartsNET.ChartsSeries { SeriesName = "库存", SeriesData = dic1},
            new HighchartsNET.ChartsSeries { SeriesName = "容量", SeriesData = dic }
            };
            //highcharts1.DataKey = "Name";
            //highcharts1.DataValue = "Capacity";
            highcharts1.YAxis = "量(m³)";//Y轴的值;
            highcharts1.Tooltip = "valueSuffix: 'm³'";
            //highcharts1.DataName = "WasteName";
            highcharts1.Legend = true;//是否显示标示，默认为false
//            highcharts1.PlotOptions = @"column: {
//                allowPointSelect: true,
//                cursor: 'pointer',
//                dataLabels: {
//                    enabled: true,
//                    color: '#000000',
//                    connectorColor: '#000000',
//                    format: '{point.value}'
//                }
//            }";
            highcharts1.PlotOptions = @"
                    column:{
                        dataLabels:{
                            enabled:true, 
                            style:{
                                color:'#0000FF'
                            }
                        }
                    }";

            //highcharts1.PlotOptions = @"{column:{dataLabels:{enabled:true,style:{color:'#D7DEE9'}}}}";

            //highcharts1.DataSource = dt;
            //highcharts1.DataBind();
        }


        //protected void Click(object sender, EventArgs e)
        //{
        //    BindGrid();
        //}


    }
}