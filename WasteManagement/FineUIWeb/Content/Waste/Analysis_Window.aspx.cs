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

namespace WasteManagement.Content.Waste
{
    public partial class Analysis_Window : PageBase
    {
        private DataBasic dataBasic = new DataBasic();
        // private string sGuid =string.Empty;
        private string sGuid//所选择操作列记录对应的id
        {
            get { return (string)ViewState["sGuid"]; }
            set { ViewState["sGuid"] = value; }
        }


        //private string[] Codes
        //{
        //    get { return (string[])ViewState["Codes"]; }
        //    set { ViewState["Codes"] = value; }
        //}

        //private string[] NewCodes
        //{
        //    get { return (string[])ViewState["NewCodes"]; }
        //    set { ViewState["NewCodes"] = value; }
        //}
        private int RowNum = 0;
        private bool AppendToEnd = false;


        protected void Page_Load(object sender, EventArgs e)
        {
            PageBase pb = new PageBase();
            if (Request.Cookies["Cookies"] == null) Response.Redirect("../../Login.aspx");
            if (!IsPostBack)
            {
                FineUI.DropDownList drop = new FineUI.DropDownList();
                List<Entity.AnalysisItem> items = DAL.AnalysisItem.GetAnalysisItemList(1);
                //foreach (Entity.AnalysisItem item in items)
                //{
                //    FineUI.ListItem li = new FineUI.ListItem();
                //    li.Text = item.ItemName;
                //    li.Value = item.ItemCode;
                //    drop.Items.Add(li);
                //}
                string Code="";
                if(items.Count>0)
                {
                    Code=items[0].ItemName;
                }
                string deleteScript = GetDeleteScript();
                JObject defaultObj = new JObject();
                //defaultObj.Add("Name", "新用户");
                //defaultObj.Add("Gender", "1");
                defaultObj.Add("ResultID", 999999);
                defaultObj.Add("ItemName", Code);
                defaultObj.Add("Result", 9.1234);
                //List<Entity.EmisstionItem> itemss2 = DAL.SewageWarrantItem.GetAllSewageWarrantItemEx2(1);
                //foreach (Entity.EmisstionItem item in itemss2)
                //{
                //    defaultObj.Add(item.ItemName, "0");
                //}

                //defaultObj.Add("EntranceDate", "2016-09-01");
                //defaultObj.Add("AtSchool", false);
                //defaultObj.Add("ProductInfo", "产品产量");
                defaultObj.Add("Delete", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(Icon.Delete)));

                // 在第一行新增一条数据
                btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);

                // 重置表格
                //btnReset.OnClientClick = Confirm.GetShowReference("确定要重置表格数据？", String.Empty, Grid1.GetRejectChangesReference(), String.Empty);


                // 删除选中行按钮
                btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！") + deleteScript;
                //BindGrid();


                if (Request.QueryString["id"] != null)
                {
                    sGuid = Request.QueryString["id"].ToString().Trim();
                    LoadData(sGuid);
                    BindGrid();
                }
                //BindGrid();
            }
        }

        private void BindGrid()
        {

            //DataTable table = Code.EnterpriseEmission.getdt(sGuid, "0", "");
            DataTable table = DAL.AnalysisResult.GetAnalysisResult(txt_BillNumber.Text.Trim());
            Grid1.DataSource = table;
            Grid1.DataBind();
            //List<Entity.AnalysisResult> results = DAL.AnalysisResult.GetAnalysisResultEx(txt_BillNumber.Text.Trim());
            //Codes = new string[results.Count];
            //for (int i = 0; i < results.Count; i++)
            //{
            //    Codes[i] = results[i].ItemCode;
            //}
        }
        
        

        protected void Page_Init(object sender, EventArgs e)
        {
            List<Entity.AnalysisItem> items = DAL.AnalysisItem.GetAnalysisItemList(1);
            //this.ddlGrade.DataSource = grade;
            //this.ddlGrade.DataTextField = "Name";
            //this.ddlGrade.DataValueField = "ID";
            //this.ddlGrade.DataBind();
            for (int i = 0; i < items.Count; i++)
            {
                FineUI.ListItem li = new FineUI.ListItem();
                Entity.AnalysisItem item = items[i];
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
                Entity.Analysis entity = DAL.Analysis.GetAnalysisByID(int.Parse(sGuid));
                txt_BillNumber.Text = entity.BillNumber;
                txt_Analysis.Text = entity.RealName;
                hf_AnalysisID.Text = entity.AnalysisManID.ToString();
                if (!string.IsNullOrEmpty(entity.DateTime.ToString()))
                {
                    Date.SelectedDate = entity.DateTime;
                }
                //BindGrid();
                if (entity.Status == 2)
                {
                    txt_BillNumber.Readonly = true;
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

            //if (txt_qymc.Text.Trim() == "") msg += "请输入企业名称！";
            //if (drp_sd.SelectedValue.ToString() == "3304") msg += "请选择企业所属区域！";
            //if (drop_status.SelectedValue.ToString() == "0") msg += "请选择企业生产状态！";
            //if (drop_industry.SelectedValue.ToString() == "0") msg += "请选择企业所属行业！";
            //if (sGuid == string.Empty || sGuid == null)
            //{
            //    string checkstr = "select * from Enterprise where Name='" + txt_qymc.Text.Trim() + "'";
            //    DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
            //    if (dscheck != null)
            //        if (dscheck.Tables[0].Rows.Count > 0)
            //        {
            //            msg += "该企业已存在！";
            //        }
            //}
            //else
            //{
            //    string checkstr = "select * from Enterprise where Name='" + txt_qymc.Text.Trim() + "' and EnterpriseID!='" + sGuid + "'";
            //    DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
            //    if (dscheck != null)
            //        if (dscheck.Tables[0].Rows.Count > 0)
            //        {
            //            msg += "单位全称不能重复！";
            //        }

            //}
            int WSID = 0;
            if (!string.IsNullOrEmpty(txt_BillNumber.Text.Trim()))
            {
                WSID = DAL.WasteStorage.GetWasteStorageIDByBillNumber(txt_BillNumber.Text);
                if (WSID == 0)
                {
                    msg += "请选择正确的联单编号！";
                }
                else
                {
                    DataTable dt = DAL.Analysis.GetAnalysis(txt_BillNumber.Text, "", "", -2);
                    if (!string.IsNullOrEmpty(sGuid))
                    {
                        if (dt.Rows[0]["AnalysisID"].ToString()!=sGuid)
                        {
                            msg += "此联单编号已存在分析信息！";
                        }
                    }
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            msg += "此联单编号已存在分析信息！";
                        }
                    }
                }
                //else
                //{
                //    hf_PlanID.Text = PlanID.ToString();
                //}
            }

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
                    msg += "分析项中出现重复项!";
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
                    Entity.Analysis entity = new Entity.Analysis();
                    entity.BillNumber = txt_BillNumber.Text.Trim();
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


                        //List<Entity.AnalysisResult> results = new List<Entity.AnalysisResult>();
                        //foreach (GridRow gr in Grid1.Rows)
                        //{
                        //    Entity.AnalysisResult result = new Entity.AnalysisResult();
                        //    result.ItemCode = DAL.AnalysisItem.GetItemCodeByName(gr.DataKeys[2].ToString());
                        //    result.Result = decimal.Parse(gr.DataKeys[3].ToString());
                        //    results.Add(result);
                        //}
                        List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();
                        List<Entity.AnalysisResult> Adds = new List<Entity.AnalysisResult>();
                        for (int i = 0; i < newAddedList.Count; i++)
                        {
                            Entity.AnalysisResult add = new Entity.AnalysisResult();
                            add.Result = decimal.Parse(newAddedList[i]["Result"].ToString());
                            add.ItemCode = DAL.AnalysisItem.GetItemCodeByName(newAddedList[i]["ItemName"].ToString());
                            Adds.Add(add);
                        }
                        iReturn = DAL.Analysis.AddAnalysisEntity(entity, Adds);

                        //iReturn = DAL.Analysis.AddAnalysisEntity(entity, results);
                    }
                    else
                    {
                        //Update
                        entity.AnalysisID = int.Parse(sGuid);
                        List<int> deletedRows = Grid1.GetDeletedList();
                        List<Entity.AnalysisResult> Deletes = new List<Entity.AnalysisResult>();
                        foreach (int rowIndex in deletedRows)
                        {
                            Entity.AnalysisResult delete = new Entity.AnalysisResult();
                            delete.ResultID = Convert.ToInt32(Grid1.DataKeys[rowIndex][0]);
                            Deletes.Add(delete);
                        }


                        Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
                        List<Entity.AnalysisResult> Updates = new List<Entity.AnalysisResult>();
                        foreach (int rowIndex in modifiedDict.Keys)
                        {
                            Entity.AnalysisResult update = new Entity.AnalysisResult();
                            update.ResultID = Convert.ToInt32(Grid1.DataKeys[rowIndex][0]);
                            update.ItemCode = DAL.AnalysisItem.GetItemCodeByName(Grid1.DataKeys[rowIndex][1].ToString());
                            update.Result = decimal.Parse(Grid1.DataKeys[rowIndex][2].ToString());
                            Updates.Add(update);
                        }

                        List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();
                        List<Entity.AnalysisResult> Adds = new List<Entity.AnalysisResult>();
                        for (int i = 0; i < newAddedList.Count; i++)
                        {
                            Entity.AnalysisResult add = new Entity.AnalysisResult();
                            add.Result = decimal.Parse(newAddedList[i]["Result"].ToString());
                            add.ItemCode = DAL.AnalysisItem.GetItemCodeByName(newAddedList[i]["ItemName"].ToString());
                            Adds.Add(add);
                        }
                    iReturn = DAL.Analysis.UpdateAnalysisEntity(entity, Adds, Deletes, Updates);
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
