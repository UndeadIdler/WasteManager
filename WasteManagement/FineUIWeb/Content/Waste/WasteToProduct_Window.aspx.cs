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
    public partial class WasteToProduct_Window : PageBase
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
                if (drop_Pond.SelectedIndex == 0)
                {
                    Entity.Pond entity = DAL.Pond.GetPond(int.Parse(drop_Pond.SelectedValue));
                    txt_WasteName.Text = entity.WasteName;
                }
                List<Entity.Waste> items = DAL.Waste.GetPartWasteEx(2);
                string WasteName1 = "";
                if (items.Count > 0)
                {
                    WasteName1 = items[0].WasteName;
                }
                //List<Entity.Waste> items2 = DAL.Waste.GetPartWasteEx(3);
                //string WasteName2 = "";
                //if (items2.Count > 0)
                //{
                //    WasteName2= items2[0].WasteName;
                //}
                List<Entity.Pond> items3 = DAL.Pond.GetAllPondEx2(2);
                string Pond = "";
                if (items3.Count > 0)
                {
                    Pond = items3[0].Name;
                }

                string deleteScript = GetDeleteScript();
                JObject defaultObj = new JObject();
                defaultObj.Add("DetailID", 9999);
                defaultObj.Add("WasteName", WasteName1);
                defaultObj.Add("Amount", 10);
                defaultObj.Add("Name", Pond);
                defaultObj.Add("Delete", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(Icon.Delete)));

                // 在第一行新增一条数据
                btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);

                
                // 删除选中行按钮
                btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！") + deleteScript;
                //BindGrid();


                //string deleteScript2 = GetDeleteScriptEx();
                //JObject defaultObj2 = new JObject();
                //defaultObj2.Add("FWID", 9999);
                //defaultObj2.Add("WasteName2", WasteName2);
                //defaultObj2.Add("Result2", 10);
                //defaultObj2.Add("Delete2", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript2, IconHelper.GetResolvedIconUrl(Icon.Delete)));

                //// 在第一行新增一条数据
                //btnNew2.OnClientClick = Grid2.GetAddNewRecordReference(defaultObj2, AppendToEnd);


                //// 删除选中行按钮
                //btnDelete2.OnClientClick = Grid2.GetNoSelectionAlertReference("请至少选择一项！") + deleteScript2;


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
            DataTable table = DAL.ProductDetail.GetProductDetail(int.Parse(sGuid));
            Grid1.DataSource = table;
            Grid1.DataBind();


            //DataTable table2 = DAL.FinalWaste.GetFinalWasteEx(int.Parse(sGuid));
            //Grid2.DataSource = table2;
            //Grid2.DataBind();
        }



        protected void Page_Init(object sender, EventArgs e)
        {
            List<Entity.Waste> items = DAL.Waste.GetPartWasteEx(2);
            //this.ddlGrade.DataSource = grade;
            //this.ddlGrade.DataTextField = "Name";
            //this.ddlGrade.DataValueField = "ID";
            //this.ddlGrade.DataBind();
            for (int i = 0; i < items.Count; i++)
            {
                FineUI.ListItem li = new FineUI.ListItem();
                Entity.Waste item = items[i];
                li.Text = item.WasteName;
                li.Value = item.WasteName;
                this.drop_Product.Items.Insert(i, li);
            }


            //List<Entity.Waste> items2 = DAL.Waste.GetPartWasteEx(3);
            //for (int i = 0; i < items2.Count; i++)
            //{
            //    FineUI.ListItem li = new FineUI.ListItem();
            //    Entity.Waste item = items2[i];
            //    li.Text = item.WasteName;
            //    li.Value = item.WasteName;
            //    this.drop_NewWaste.Items.Insert(i, li);
            //}

            //List<Entity.Pond> items3 = DAL.Pond.GetAllPondEx();
            List<Entity.Pond> items3 = DAL.Pond.GetAllPondEx2(2);
            for (int i = 0; i < items3.Count; i++)
            {
                FineUI.ListItem li = new FineUI.ListItem();
                Entity.Pond item = items3[i];
                li.Text = item.Name;
                li.Value = item.Name;
                this.drop_Pond2.Items.Insert(i, li);
            }

            //DataTable table2 = DAL.Pond.GetAllPond();
            DataTable table2 = DAL.Pond.GetPartPond(1);
            drop_Pond.DataSource = table2;
            drop_Pond.DataTextField = "Name";
            drop_Pond.DataValueField = "PondID";
            drop_Pond.DataBind();
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


        //protected void Grid2_PreDataBound(object sender, EventArgs e)
        //{
        //    // 设置LinkButtonField的点击客户端事件
        //    LinkButtonField deleteField = Grid2.FindColumn("Delete2") as LinkButtonField;
        //    deleteField.OnClientClick = GetDeleteScriptEx();
        //}

        //// 删除选中行的脚本
        //private string GetDeleteScriptEx()
        //{
        //    return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid2.GetDeleteSelectedReference(), String.Empty);
        //}


        #region 加载数据
        private void LoadData(string thisGuid)
        {
            if (thisGuid != string.Empty)
            {
                //BindGrid();
                Entity.WasteToProduct entity = DAL.WasteToProduct.GetWasteToProduct(int.Parse(thisGuid));
                if (!string.IsNullOrEmpty(entity.DateTime.ToString()))
                {
                    FDate.SelectedDate = entity.DateTime;
                }
                drop_Pond.SelectedValue = entity.FromPondID.ToString();
                txt_WasteName.Text = DAL.Pond.GetPond(entity.FromPondID).WasteName;
                NB_Amount.Text = entity.FromAmount.ToString();
                hf_HandelManID.Text = entity.HanderManID.ToString();
                hf_ReceiverID.Text = entity.ReceiverID.ToString();
                txt_HandleMan.Text = DAL.User.GetUserByID(entity.HanderManID).RealName;
                if (entity.ReceiverID != 0)
                {
                    txt_Receiver.Text = DAL.User.GetUserByID(entity.ReceiverID).RealName;
                }
                if (entity.Status == 2)
                {
                    FDate.Readonly = true;
                    drop_Pond.Readonly = true;
                    txt_WasteName.Readonly = true;
                    NB_Amount.Readonly = true;
                    txt_HandleMan.Readonly = true;
                    txt_Receiver.Readonly = true;
                    Grid1.Enabled = false;
                    //Grid2.Enabled = false;
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
            int RID = DAL.User.GetUserID(txt_HandleMan.Text.Trim(), 4);
            if (RID == 0)
            {
                msg += "请选择正确的处置人！";
            }
            else
            {
                hf_HandelManID.Text = RID.ToString();
            }
            if (!string.IsNullOrEmpty(txt_Receiver.Text.Trim()))
            {
                int RID2 = DAL.User.GetUserID(txt_Receiver.Text.Trim(), 2);
                if (RID2 == 0)
                {
                    msg += "请选择正确的签收人！";
                }
                else
                {
                    hf_ReceiverID.Text = RID2.ToString();
                }
            }
            decimal PlanAmount = decimal.Parse(NB_Amount.Text.Trim());
            decimal Old = 0;
            if (!string.IsNullOrEmpty(sGuid))
            {
                Entity.WasteToProduct ws = DAL.WasteToProduct.GetWasteToProduct(int.Parse(sGuid));
                Old = ws.FromAmount;
            }
            Entity.Pond pond = DAL.Pond.GetPond(int.Parse(drop_Pond.SelectedValue.ToString()));
            decimal capacity = pond.Capacity;
            decimal used = pond.Used;
            if (used + PlanAmount - Old > capacity)
            {
                msg += "罐池的剩余量不足,罐池的剩余量为" + (capacity - used).ToString() + "!";
            }
            #region 判断是否有重复项,暂时去掉
            //判断是否有重复项
            List<string> All3 = new List<string>();
            Hashtable ht3 = new Hashtable();
            Dictionary<int, Dictionary<string, object>> modifiedDict3 = Grid1.GetModifiedDict();
            foreach (int rowIndex in modifiedDict3.Keys)
            {
                All3.Add(Grid1.DataKeys[rowIndex][1].ToString());
            }
            List<Dictionary<string, object>> newAddedList3 = Grid1.GetNewAddedList();
            for (int i = 0; i < newAddedList3.Count; i++)
            {
                All3.Add(newAddedList3[i]["WasteName"].ToString());
            }
            List<int> deletedRows3 = Grid1.GetDeletedList();
            foreach (int rowIndex in deletedRows3)
            {
                All3.Remove(Grid1.DataKeys[rowIndex][1].ToString());
            }
            for (int i = 0; i < All3.Count; i++)
            {
                if (ht3.ContainsKey(All3[i]))
                {
                    ht3[All3[i]] = int.Parse(ht3[All3[i]].ToString()) + 1;
                }
                else
                {
                    ht3.Add(All3[i], 1);
                }
            }
            IDictionaryEnumerator ie3 = ht3.GetEnumerator();
            while (ie3.MoveNext())
            {
                if (int.Parse(ie3.Value.ToString()) > 1)
                {
                    msg += "成品中出现重复项" + ie3.Key + "!";
                }
                //Console.WriteLine(ie.Key.ToString() + "记录条数：" + ie.Value);
            }
            #endregion

            #region 判断是否有重复的罐池，并判断污染物和罐池是否对应
            List<string> All = new List<string>();
            Hashtable ht = new Hashtable();
            Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
            foreach (int rowIndex in modifiedDict.Keys)
            {
                All.Add(Grid1.DataKeys[rowIndex][3].ToString());
                Entity.Pond ponda = DAL.Pond.GetPondByName(Grid1.DataKeys[rowIndex][3].ToString());
                if (Grid1.DataKeys[rowIndex][1].ToString() != ponda.WasteName)
                {
                    msg += Grid1.DataKeys[rowIndex][1].ToString() + "不能存入" + Grid1.DataKeys[rowIndex][3].ToString() + "!";
                }
            }
            List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();
            for (int i = 0; i < newAddedList.Count; i++)
            {
                All.Add(newAddedList[i]["Name"].ToString());
                Entity.Pond ponda = DAL.Pond.GetPondByName(newAddedList[i]["Name"].ToString());
                if (newAddedList[i]["WasteName"].ToString() != ponda.WasteName)
                {
                    msg += newAddedList[i]["WasteName"].ToString() + "不能存入" + newAddedList[i]["Name"].ToString() + "!";
                }
            }
            List<int> deletedRows = Grid1.GetDeletedList();
            foreach (int rowIndex in deletedRows)
            {
                All.Remove(Grid1.DataKeys[rowIndex][3].ToString());
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
                    msg += "成品中出现重复罐池"+ie.Key.ToString()+"!";
                }
                //Console.WriteLine(ie.Key.ToString() + "记录条数：" + ie.Value);
            }
            #endregion

            #region 判断危废是否重复
            //判断是否有重复项
            //List<string> All2 = new List<string>();
            //Hashtable ht2 = new Hashtable();
            //Dictionary<int, Dictionary<string, object>> modifiedDict2 = Grid2.GetModifiedDict();
            //foreach (int rowIndex in modifiedDict2.Keys)
            //{
            //    All2.Add(Grid2.DataKeys[rowIndex][1].ToString());
            //}
            //List<Dictionary<string, object>> newAddedList2 = Grid2.GetNewAddedList();
            //for (int i = 0; i < newAddedList2.Count; i++)
            //{
            //    All2.Add(newAddedList2[i]["WasteName2"].ToString());
            //}
            //List<int> deletedRows2 = Grid2.GetDeletedList();
            //foreach (int rowIndex in deletedRows2)
            //{
            //    All2.Remove(Grid2.DataKeys[rowIndex][1].ToString());
            //}
            //for (int i = 0; i < All2.Count; i++)
            //{
            //    if (ht2.ContainsKey(All2[i]))
            //    {
            //        ht2[All2[i]] = int.Parse(ht2[All2[i]].ToString()) + 1;
            //    }
            //    else
            //    {
            //        ht2.Add(All2[i], 1);
            //    }
            //}
            //IDictionaryEnumerator ie2 = ht2.GetEnumerator();
            //while (ie2.MoveNext())
            //{
            //    if (int.Parse(ie2.Value.ToString()) > 1)
            //    {
            //        msg += "危废中出现重复项"+ie2.Key+"!";
            //    }
            //    //Console.WriteLine(ie.Key.ToString() + "记录条数：" + ie.Value);
            //}

            #endregion

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
                    Entity.WasteToProduct entity = new Entity.WasteToProduct();
                    entity.CreateDate = DateTime.Now;
                    entity.UpdateDate = DateTime.Now;
                    entity.CreateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                    entity.UpdateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                    entity.DateTime = FDate.SelectedDate;
                    entity.FromPondID = int.Parse(drop_Pond.SelectedValue);
                    entity.FromAmount = decimal.Parse(NB_Amount.Text);

                    entity.FromWasteCode = DAL.Pond.GetPond(int.Parse( drop_Pond.SelectedValue)).Stores;
                    entity.HanderManID = int.Parse(hf_HandelManID.Text);
                    if (!string.IsNullOrEmpty(txt_Receiver.Text.Trim()))
                    {
                        entity.ReceiverID = int.Parse(hf_ReceiverID.Text);
                    }
                    else
                    {
                        entity.ReceiverID = 0;
                    }

                    entity.Status = 1;

                    if (string.IsNullOrEmpty(sGuid))
                    {
                        //Add
                        List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();
                        List<Entity.ProductDetail> Adds = new List<Entity.ProductDetail>();
                        for (int i = 0; i < newAddedList.Count; i++)
                        {
                            Entity.ProductDetail add = new Entity.ProductDetail();
                            add.Name = newAddedList[i]["Name"].ToString();
                            add.PondID = DAL.Pond.GetPondByName(add.Name).PondID;
                            add.Amount = decimal.Parse(newAddedList[i]["Amount"].ToString());
                            add.ItemCode = DAL.Waste.GetWasteCodeByName(newAddedList[i]["WasteName"].ToString());
                            Adds.Add(add);
                        }

                        //List<Dictionary<string, object>> newAddedList2 = Grid2.GetNewAddedList();
                        //List<Entity.FinalWaste> Adds2 = new List<Entity.FinalWaste>();
                        //for (int i = 0; i < newAddedList2.Count; i++)
                        //{
                        //    Entity.FinalWaste add = new Entity.FinalWaste();
                        //    add.Result = decimal.Parse(newAddedList2[i]["Result2"].ToString());
                        //    add.ItemCode = DAL.Waste.GetWasteCodeByName(newAddedList2[i]["WasteName2"].ToString());
                        //    Adds2.Add(add);
                        //}

                        iReturn = DAL.WasteToProduct.AddWasteToProductEntity(entity, Adds);
                        
                    }
                    else
                    { 
                        //Update
                        entity.DealID = int.Parse(sGuid);
                        List<int> deletedRows = Grid1.GetDeletedList();
                        List<Entity.ProductDetail> Deletes = new List<Entity.ProductDetail>();
                        foreach (int rowIndex in deletedRows)
                        {
                            Entity.ProductDetail delete = new Entity.ProductDetail();
                            delete.DetailID = Convert.ToInt32(Grid1.DataKeys[rowIndex][1]);
                            Deletes.Add(delete);
                        }


                        Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
                        List<Entity.ProductDetail> Updates = new List<Entity.ProductDetail>();
                        foreach (int rowIndex in modifiedDict.Keys)
                        {
                            Entity.ProductDetail update = new Entity.ProductDetail();
                            update.DetailID = Convert.ToInt32(Grid1.DataKeys[rowIndex][1]);
                            update.ItemCode = DAL.Waste.GetWasteCodeByName(Grid1.DataKeys[rowIndex][2].ToString());
                            update.Amount = decimal.Parse(Grid1.DataKeys[rowIndex][3].ToString());
                            update.PondID = 1;
                            update.Status = 1;
                            Updates.Add(update);
                        }

                        List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();
                        List<Entity.ProductDetail> Adds = new List<Entity.ProductDetail>();
                        for (int i = 0; i < newAddedList.Count; i++)
                        {
                            Entity.ProductDetail add = new Entity.ProductDetail();
                            add.Name = newAddedList[i]["Name"].ToString();
                            add.PondID = DAL.Pond.GetPondByName(add.Name).PondID;
                            add.Amount = decimal.Parse(newAddedList[i]["Amount"].ToString());
                            add.ItemCode = DAL.Waste.GetWasteCodeByName(newAddedList[i]["WasteName"].ToString());
                            Adds.Add(add);
                        }


                        //List<int> deletedRows2 = Grid2.GetDeletedList();
                        //List<Entity.FinalWaste> Deletes2 = new List<Entity.FinalWaste>();
                        //foreach (int rowIndex in deletedRows2)
                        //{
                        //    Entity.FinalWaste delete = new Entity.FinalWaste();
                        //    delete.FWID = Convert.ToInt32(Grid2.DataKeys[rowIndex][1]);
                        //    Deletes2.Add(delete);
                        //}


                        //Dictionary<int, Dictionary<string, object>> modifiedDict2 = Grid2.GetModifiedDict();
                        //List<Entity.FinalWaste> Updates2 = new List<Entity.FinalWaste>();
                        //foreach (int rowIndex in modifiedDict2.Keys)
                        //{
                        //    Entity.FinalWaste update = new Entity.FinalWaste();
                        //    update.FWID = Convert.ToInt32(Grid2.DataKeys[rowIndex][1]);
                        //    update.ItemCode = DAL.Waste.GetWasteCodeByName(Grid2.DataKeys[rowIndex][2].ToString());
                        //    update.Result = decimal.Parse(Grid2.DataKeys[rowIndex][3].ToString());
                        //    update.Status = 1;
                        //    Updates2.Add(update);
                        //}

                        //List<Dictionary<string, object>> newAddedList2 = Grid2.GetNewAddedList();
                        //List<Entity.FinalWaste> Adds2 = new List<Entity.FinalWaste>();
                        //for (int i = 0; i < newAddedList2.Count; i++)
                        //{
                        //    Entity.FinalWaste add = new Entity.FinalWaste();
                        //    add.Result = decimal.Parse(newAddedList2[i]["Result2"].ToString());
                        //    add.ItemCode = DAL.Waste.GetWasteCodeByName(newAddedList2[i]["WasteName2"].ToString());
                        //    Adds2.Add(add);
                        //}
                        iReturn = DAL.WasteToProduct.UpdateWasteToProductEntity(entity, Adds, Updates, Deletes);
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

        protected void drop_Pond_Change(object sender, EventArgs e)
        {
            // 设置LinkButtonField的点击客户端事件
            if (drop_Pond.SelectedIndex > -1)
            {
                Entity.Pond entity = DAL.Pond.GetPond(int.Parse(drop_Pond.SelectedValue));
                txt_WasteName.Text = entity.WasteName;
            }
        }

    }
}
