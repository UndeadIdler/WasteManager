using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Newtonsoft.Json.Linq;
using FineUI;
using DataAccess;
using System.Collections;

namespace WasteManagement.Content.State
{
    public partial class Monitor_Window : PageBase
    {
        private DataBasic dataBasic = new DataBasic();
        // private string sGuid =string.Empty;
        private string sGuid//所选择操作列记录对应的id
        {
            get { return (string)ViewState["sGuid"]; }
            set { ViewState["sGuid"] = value; }
        }

        private int RowNum = 0;
        private bool AppendToEnd = false;


        protected void Page_Load(object sender, EventArgs e)
        {
            PageBase pb = new PageBase();
            if (Request.Cookies["Cookies"] == null) Response.Redirect("../../Login.aspx");
            if (!IsPostBack)
            {
                FineUI.DropDownList drop = new FineUI.DropDownList();
                List<Entity.MonitorItem> items = DAL.MonitorItem.GetMonitorItemList(1);
                //foreach (Entity.MonitorItem item in items)
                //{
                //    FineUI.ListItem li = new FineUI.ListItem();
                //    li.Text = item.ItemName;
                //    li.Value = item.ItemCode;
                //    drop.Items.Add(li);
                //}
                string Code = "";
                if (items.Count > 0)
                {
                    Code = items[0].ItemName;
                }
                string deleteScript = GetDeleteScript();
                JObject defaultObj = new JObject();
                defaultObj.Add("ResultID", 999999);
                defaultObj.Add("ItemName", Code);
                defaultObj.Add("Result", 9.1234);
                defaultObj.Add("Delete", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(Icon.Delete)));

                // 在第一行新增一条数据
                btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);

                // 重置表格
                //btnReset.OnClientClick = Confirm.GetShowReference("确定要重置表格数据？", String.Empty, Grid1.GetRejectChangesReference(), String.Empty);


                // 删除选中行按钮
                btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！") + deleteScript;
                //BindGrid();

                BindDropdownList();

                if (Request.QueryString["id"] != null)
                {
                    sGuid = Request.QueryString["id"].ToString().Trim();
                    LoadData(sGuid);
                    BindGrid();
                }
                //BindGrid();
            }
        }

        private void BindDropdownList()
        {

            DataTable dt = DAL.Position.GetAllPositionEx();
            drop_PositionName.DataTextField = "Name";
            drop_PositionName.DataValueField = "ID";
            drop_PositionName.DataSource = dt;
            drop_PositionName.DataBind();
        }


        private void BindGrid()
        {

            //DataTable table = Code.EnterpriseEmission.getdt(sGuid, "0", "");
            DataTable table = DAL.MonitorResult.GetMonitorResult(int.Parse(sGuid));
            Grid1.DataSource = table;
            Grid1.DataBind();
        }



        protected void Page_Init(object sender, EventArgs e)
        {
            List<Entity.MonitorItem> items = DAL.MonitorItem.GetMonitorItemList(1);
            //this.ddlGrade.DataSource = grade;
            //this.ddlGrade.DataTextField = "Name";
            //this.ddlGrade.DataValueField = "ID";
            //this.ddlGrade.DataBind();
            for (int i = 0; i < items.Count; i++)
            {
                FineUI.ListItem li = new FineUI.ListItem();
                Entity.MonitorItem item = items[i];
                li.Text = item.ItemName;
                li.Value = item.ItemName;
                this.drop_Item.Items.Insert(i, li);
            }
        }


        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 设置LinkButtonField的点击客户端事件
            LinkButtonField deleteField = Grid1.FindColumn("Delete") as LinkButtonField;
            deleteField.OnClientClick = GetDeleteScript();
        }

        // 删除选中行的脚本
        private string GetDeleteScript()
        {
            return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid1.GetDeleteSelectedReference(), String.Empty);
        }


        #region 加载数据
        private void LoadData(string thisGuid)
        {
            if (thisGuid != string.Empty)
            {
                //BindGrid();
                Entity.Monitor entity = DAL.Monitor.GetMonitorByID(int.Parse(sGuid));
                drop_PositionName.SelectedValue = entity.PositionID.ToString();
                txt_Analysis.Text = entity.RealName;
                hf_AnalysisID.Text = entity.AnalysisManID.ToString();
                if (!string.IsNullOrEmpty(entity.DateTime.ToString()))
                {
                    Date.SelectedDate = entity.DateTime;
                }
                //BindGrid();
                if (entity.Status == 2)
                {
                    drop_PositionName.Readonly = true;
                    txt_Analysis.Readonly = true;
                    Date.Readonly = true;
                    Grid1.Enabled = false;
                    btn_save.Enabled = false;
                }
            }
        }
        #endregion

        #region 保存数据

        private void AllTxtreadOnly()//设置详细页面中所有的TextBox为只读
        {
            //txt_pwNO.Enabled = false;
            ;
        }
        private string checkInput()
        {
            string msg = "";

            int RID = DAL.User.GetUserID(txt_Analysis.Text.Trim(), 1);
            if (RID == 0)
            {
                msg += "请选择正确的分析人！";
            }
            else
            {
                hf_AnalysisID.Text = RID.ToString();
            }
            //判断是否有重复项
            List<string> All = new List<string>();
            Hashtable ht = new Hashtable();
            Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
            foreach (int rowIndex in modifiedDict.Keys)
            {
                All.Add(Grid1.DataKeys[rowIndex][1].ToString());
            }
            List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();
            for (int i = 0; i < newAddedList.Count; i++)
            {
                All.Add(newAddedList[i]["ItemName"].ToString());
            }
            List<int> deletedRows = Grid1.GetDeletedList();
            foreach (int rowIndex in deletedRows)
            {
                All.Remove(Grid1.DataKeys[rowIndex][1].ToString());
            }
            for (int i = 0; i < All.Count; i++)
            {
                if (ht.ContainsKey(All[i]))
                {
                    ht[All[i]] = int.Parse(ht[All[i]].ToString()) + 1;
                }
                else
                {
                    ht.Add(All[i], 1);
                }
            }
            IDictionaryEnumerator ie = ht.GetEnumerator();
            while (ie.MoveNext())
            {
                if (int.Parse(ie.Value.ToString()) > 1)
                {
                    msg += "监测项中出现重复项!";
                }
                //Console.WriteLine(ie.Key.ToString() + "记录条数：" + ie.Value);
            }

            //List<string> Add = new List<string>();
            //List<string> Add2 = new List<string>();
            //List<string> Update = new List<string>();



            //for (int i = 0; i < Add2.Count; i++)  //外循环是循环的次数
            //{
            //    for (int j = Add2.Count - 1; j > i; j--)  //内循环是 外循环一次比较的次数
            //    {

            //        if (Add2[i] == Add2[j])
            //        {
            //            Add2.RemoveAt(j);
            //        }

            //    }
            //}
            //if (Add.Count > Add2.Count)
            //{
            //    msg += "分析项中出现重复项!";
            //}

            //for (int i = 0; i < newAddedList.Count; i++)
            //{
            //    Add.Add(newAddedList[i]["ItemName"].ToString());
            //}
            //foreach (string s in Add)
            //{
            //    if (Update.Contains(s))
            //    {
            //        msg += "分析项中出现多个"+s+"!";
            //    }
            //}
            return msg;
        }




        protected void btn_save_Click(object sender, EventArgs e)
        {
            string msg = checkInput();
            if (msg != "")
            {
                Alert.Show(msg);
                return;
            }
            else
            {


                int iReturn = 0;
                try
                {
                    Entity.Monitor entity = new Entity.Monitor();
                    entity.PositionID = int.Parse(drop_PositionName.SelectedValue);
                    entity.CreateDate = DateTime.Now;
                    entity.UpdateDate = DateTime.Now;
                    entity.CreateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                    entity.UpdateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                    entity.DateTime = Date.SelectedDate;
                    entity.AnalysisManID = int.Parse(hf_AnalysisID.Text.Trim());
                    entity.Status = 1;

                    if (string.IsNullOrEmpty(sGuid))
                    {
                        //Add


                        //List<Entity.MonitorResult> results = new List<Entity.MonitorResult>();
                        //foreach (GridRow gr in Grid1.Rows)
                        //{
                        //    Entity.MonitorResult result = new Entity.MonitorResult();
                        //    result.ItemCode = DAL.MonitorItem.GetItemCodeByName(gr.DataKeys[2].ToString());
                        //    result.Result = decimal.Parse(gr.DataKeys[3].ToString());
                        //    results.Add(result);
                        //}
                        List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();
                        List<Entity.MonitorResult> Adds = new List<Entity.MonitorResult>();
                        for (int i = 0; i < newAddedList.Count; i++)
                        {
                            Entity.MonitorResult add = new Entity.MonitorResult();
                            add.Result = decimal.Parse(newAddedList[i]["Result"].ToString());
                            add.ItemCode = DAL.MonitorItem.GetItemCodeByName(newAddedList[i]["ItemName"].ToString());
                            Adds.Add(add);
                        }
                        iReturn = DAL.Monitor.AddMonitorEntity(entity, Adds);

                        //iReturn = DAL.Monitor.AddMonitorEntity(entity, results);
                    }
                    else
                    {
                        //Update
                        entity.MonitorID = int.Parse(sGuid);
                        List<int> deletedRows = Grid1.GetDeletedList();
                        List<Entity.MonitorResult> Deletes = new List<Entity.MonitorResult>();
                        foreach (int rowIndex in deletedRows)
                        {
                            Entity.MonitorResult delete = new Entity.MonitorResult();
                            delete.ResultID = Convert.ToInt32(Grid1.DataKeys[rowIndex][0]);
                            Deletes.Add(delete);
                        }


                        Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
                        List<Entity.MonitorResult> Updates = new List<Entity.MonitorResult>();
                        foreach (int rowIndex in modifiedDict.Keys)
                        {
                            Entity.MonitorResult update = new Entity.MonitorResult();
                            update.ResultID = Convert.ToInt32(Grid1.DataKeys[rowIndex][0]);
                            update.ItemCode = DAL.MonitorItem.GetItemCodeByName(Grid1.DataKeys[rowIndex][1].ToString());
                            update.Result = decimal.Parse(Grid1.DataKeys[rowIndex][2].ToString());
                            Updates.Add(update);
                        }

                        List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();
                        List<Entity.MonitorResult> Adds = new List<Entity.MonitorResult>();
                        for (int i = 0; i < newAddedList.Count; i++)
                        {
                            Entity.MonitorResult add = new Entity.MonitorResult();
                            add.Result = decimal.Parse(newAddedList[i]["Result"].ToString());
                            add.ItemCode = DAL.MonitorItem.GetItemCodeByName(newAddedList[i]["ItemName"].ToString());
                            Adds.Add(add);
                        }
                        iReturn = DAL.Monitor.UpdateMonitorEntity(entity, Adds, Deletes, Updates);
                    }


                }
                catch (Exception ex)
                {

                }
                finally
                {

                }
                if (iReturn == 1)
                {
                    Alert.ShowInTop(" 保存成功！", MessageBoxIcon.Information);
                    //            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }
                else
                {
                    Alert.ShowInTop(" 保存失败！", MessageBoxIcon.Warning);
                }


            }
        #endregion
        }



    }
}
