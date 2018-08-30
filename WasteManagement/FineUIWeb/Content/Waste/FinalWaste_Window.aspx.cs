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
    public partial class FinalWaste_Window : PageBase
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
                BindDropdownList();
                List<Entity.Waste> items2 = DAL.Waste.GetPartWasteEx(3);
                string WasteName2 = "";
                if (items2.Count > 0)
                {
                    WasteName2 = items2[0].WasteName;
                }

                //BindGrid();


                string deleteScript2 = GetDeleteScriptEx();
                JObject defaultObj2 = new JObject();
                defaultObj2.Add("FWID", 9999);
                defaultObj2.Add("WasteName2", WasteName2);
                defaultObj2.Add("Result2", 10);
                defaultObj2.Add("Delete2", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript2, IconHelper.GetResolvedIconUrl(Icon.Delete)));

                // 在第一行新增一条数据
                btnNew2.OnClientClick = Grid2.GetAddNewRecordReference(defaultObj2, AppendToEnd);


                // 删除选中行按钮
                btnDelete2.OnClientClick = Grid2.GetNoSelectionAlertReference("请至少选择一项！") + deleteScript2;


                if (Request.QueryString["id"] != null)
                {
                    sGuid = Request.QueryString["id"].ToString().Trim();
                    LoadData(sGuid);
                    BindGrid();
                }
                else
                {
                    int Number = DAL.FinalWasteLog.GetCount(DateTime.Now.Date) + 1;
                    txt_LogNumber.Text = GenerateLogNumberEx(DateTime.Now.Date, Number.ToString());
                }
                //BindGrid();
            }
        }

        private void BindGrid()
        {
            DataTable table2 = DAL.FinalWaste.GetFinalWasteEx(int.Parse(sGuid));
            Grid2.DataSource = table2;
            Grid2.DataBind();
        }

        private void BindDropdownList()
        {
            DataTable dt = DAL.User.GetUserNamesEx2("1,4");
            drop_Man.DataTextField = "RealName";
            drop_Man.DataValueField = "UserID";
            drop_Man.DataSource = dt;
            drop_Man.DataBind();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            List<Entity.Waste> items2 = DAL.Waste.GetPartWasteEx(3);
            for (int i = 0; i < items2.Count; i++)
            {
                FineUI.ListItem li = new FineUI.ListItem();
                Entity.Waste item = items2[i];
                li.Text = item.WasteName;
                li.Value = item.WasteName;
                this.drop_NewWaste.Items.Insert(i, li);
            }


            //DataTable table2 = DAL.Pond.GetAllPond();
        }


        protected void Grid2_PreDataBound(object sender, EventArgs e)
        {
            // 设置LinkButtonField的点击客户端事件
            LinkButtonField deleteField = Grid2.FindColumn("Delete2") as LinkButtonField;
            deleteField.OnClientClick = GetDeleteScriptEx();
        }

        // 删除选中行的脚本
        private string GetDeleteScriptEx()
        {
            return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid2.GetDeleteSelectedReference(), String.Empty);
        }


        #region 加载数据
        private void LoadData(string thisGuid)
        {
            if (thisGuid != string.Empty)
            {
                //BindGrid();
                Entity.FinalWasteLog entity = DAL.FinalWasteLog.GetFinalWasteLog(int.Parse(thisGuid));
                if (!string.IsNullOrEmpty(entity.DateTime.ToString()))
                {
                    FDate.SelectedDate = entity.DateTime;
                }
                drop_Man.SelectedValue = entity.UserID.ToString();
                txt_LogNumber.Text = entity.LogNumber.ToString();               
                if (entity.Status == 2)
                {
                    FDate.Readonly = true;
                    drop_Man.Readonly = true;
                    txt_LogNumber.Readonly = true;
                    Grid2.Enabled = false;
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

            #region 判断危废是否重复
            //判断是否有重复项
            List<string> All2 = new List<string>();
            Hashtable ht2 = new Hashtable();
            Dictionary<int, Dictionary<string, object>> modifiedDict2 = Grid2.GetModifiedDict();
            foreach (int rowIndex in modifiedDict2.Keys)
            {
                All2.Add(Grid2.DataKeys[rowIndex][1].ToString());
            }
            List<Dictionary<string, object>> newAddedList2 = Grid2.GetNewAddedList();
            for (int i = 0; i < newAddedList2.Count; i++)
            {
                All2.Add(newAddedList2[i]["WasteName2"].ToString());
            }
            List<int> deletedRows2 = Grid2.GetDeletedList();
            foreach (int rowIndex in deletedRows2)
            {
                All2.Remove(Grid2.DataKeys[rowIndex][1].ToString());
            }
            for (int i = 0; i < All2.Count; i++)
            {
                if (ht2.ContainsKey(All2[i]))
                {
                    ht2[All2[i]] = int.Parse(ht2[All2[i]].ToString()) + 1;
                }
                else
                {
                    ht2.Add(All2[i], 1);
                }
            }
            IDictionaryEnumerator ie2 = ht2.GetEnumerator();
            while (ie2.MoveNext())
            {
                if (int.Parse(ie2.Value.ToString()) > 1)
                {
                    msg += "危废中出现重复项" + ie2.Key + "!";
                }
                //Console.WriteLine(ie.Key.ToString() + "记录条数：" + ie.Value);
            }

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
                    Entity.FinalWasteLog entity = new Entity.FinalWasteLog();
                    entity.CreateDate = DateTime.Now;
                    entity.UpdateDate = DateTime.Now;
                    entity.CreateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                    entity.UpdateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                    entity.DateTime = FDate.SelectedDate;
                    entity.IYear = DateTime.Now.Year;
                    entity.Number = DAL.FinalWasteLog.GetMaxNumber(entity.IYear) + 1;
                    int Number = DAL.FinalWasteLog.GetCount(entity.DateTime) + 1;
                    entity.LogNumber = int.Parse(GenerateLogNumberEx(entity.DateTime.Value, Number.ToString()));
                    //entity.LogNumber = int.Parse(GenerateLogNumber(entity.IYear, entity.Number.ToString()));
                    entity.Status = 1;
                    entity.UserID = int.Parse(drop_Man.SelectedValue.Trim());
                    if (string.IsNullOrEmpty(sGuid))
                    {
                        //Add
                        List<Dictionary<string, object>> newAddedList2 = Grid2.GetNewAddedList();
                        List<Entity.FinalWaste> Adds2 = new List<Entity.FinalWaste>();
                        for (int i = 0; i < newAddedList2.Count; i++)
                        {
                            Entity.FinalWaste add = new Entity.FinalWaste();
                            add.Result = decimal.Parse(newAddedList2[i]["Result2"].ToString());
                            add.ItemCode = DAL.Waste.GetWasteCodeByName(newAddedList2[i]["WasteName2"].ToString());
                            Adds2.Add(add);
                        }

                        iReturn = DAL.FinalWasteLog.AddFinalWasteLogEntity(entity,  Adds2);

                    }
                    else
                    {
                        //Update
                        entity.LogID = int.Parse(sGuid);

                        List<int> deletedRows2 = Grid2.GetDeletedList();
                        List<Entity.FinalWaste> Deletes2 = new List<Entity.FinalWaste>();
                        foreach (int rowIndex in deletedRows2)
                        {
                            Entity.FinalWaste delete = new Entity.FinalWaste();
                            delete.FWID = Convert.ToInt32(Grid2.DataKeys[rowIndex][1]);
                            Deletes2.Add(delete);
                        }


                        Dictionary<int, Dictionary<string, object>> modifiedDict2 = Grid2.GetModifiedDict();
                        List<Entity.FinalWaste> Updates2 = new List<Entity.FinalWaste>();
                        foreach (int rowIndex in modifiedDict2.Keys)
                        {
                            Entity.FinalWaste update = new Entity.FinalWaste();
                            update.FWID = Convert.ToInt32(Grid2.DataKeys[rowIndex][1]);
                            update.ItemCode = DAL.Waste.GetWasteCodeByName(Grid2.DataKeys[rowIndex][2].ToString());
                            update.Result = decimal.Parse(Grid2.DataKeys[rowIndex][3].ToString());
                            update.Status = 1;
                            Updates2.Add(update);
                        }

                        List<Dictionary<string, object>> newAddedList2 = Grid2.GetNewAddedList();
                        List<Entity.FinalWaste> Adds2 = new List<Entity.FinalWaste>();
                        for (int i = 0; i < newAddedList2.Count; i++)
                        {
                            Entity.FinalWaste add = new Entity.FinalWaste();
                            add.Result = decimal.Parse(newAddedList2[i]["Result2"].ToString());
                            add.ItemCode = DAL.Waste.GetWasteCodeByName(newAddedList2[i]["WasteName2"].ToString());
                            Adds2.Add(add);
                        }
                        iReturn = DAL.FinalWasteLog.UpdateFinalWasteLogEntity(entity, Adds2, Updates2, Deletes2);
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

        private string GenerateLogNumber(int Year,string Number)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Year);
            for (int i = 0; i < 4 - Number.Length; i++)
            {
                sb.Append(0);
            }
            sb.Append(Number);
            return sb.ToString();
        }

        private string GenerateLogNumberEx(DateTime Date, string Number)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Date.Year);
            if (Date.Month < 10)
            {
                sb.Append(0);
            }
            sb.Append(Date.Month);
            if (Date.Day < 10)
            {
                sb.Append(0);
            }
            sb.Append(Date.Day);
            for (int i = 0; i < 2 - Number.Length; i++)
            {
                sb.Append(0);
            }
            sb.Append(Number);
            return sb.ToString();
        }

    }
}
