using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using FineUI;

namespace WasteManagement.Content.Basic
{
    public partial class Car : PageBase
    {
        private DataBasic dataBasic = new DataBasic();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["Cookies"] == null) Response.Redirect("../Login.aspx");
                BindUserGrid();
                BindGrid1();
                //InitRadio();
                CheckUserRole();
            }

        }

        private void CheckUserRole()
        {
            string pageName = this.Request.Url.Segments[this.Request.Url.Segments.Length - 1].ToString();
            //if (!blPageWrite(pageName, Request.Cookies["Cookies"].Values["UserGuid"].ToString()))
            //{
            //    btnAddUser.Enabled = false;
            //    btnDelUser.Enabled = false;
            //    btnSaveTree1.Enabled = false;
            //    btnSaveTree2.Enabled = false;
            //}
        }

        #region Bind

        private void BindUserGrid()
        {
            DataTable dt = DAL.CarNumber.GetAllCarNumberEx();
            GridUser.DataSource = dt;
            GridUser.DataBind();
        }

        private void BindGrid1()
        {
            DataTable dt = DAL.Driver.GetAllDriverEx();
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }
       

        #endregion

        #region Events
        /// <summary>
        /// 删除车辆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelUser_Click(object sender, EventArgs e)
        {
            //string sIDStr = string.Empty;
            int selectedCount = GridUser.SelectedRowIndexArray.Length;
            if (selectedCount == 0)
            {
                Alert.ShowInTop("请选择一项纪录！", MessageBoxIcon.Warning);
                return;
            }


            int rowIndex = GridUser.SelectedRowIndexArray[0];
            object[] dataKeys = GridUser.DataKeys[rowIndex];
            int iReturn = DAL.CarNumber.DeleteCarNumber(int.Parse(HttpUtility.UrlEncode(dataKeys[0].ToString())));

            if (iReturn == 1)
            {
                Alert.ShowInTop(" 删除成功！", MessageBoxIcon.Information);
                BindUserGrid();
            }
            else
            {
                Alert.ShowInTop(" 删除失败！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 删除驾驶员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelRole_Click(object sender, EventArgs e)
        {
            int selectedCount = Grid1.SelectedRowIndexArray.Length;
            if (selectedCount == 0)
            {
                Alert.ShowInTop("请选择一项纪录！", MessageBoxIcon.Warning);
                return;
            }

            object[] keys = Grid1.DataKeys[Grid1.SelectedRowIndex];
            int BSuccess = DAL.Driver.DeleteDriver(int.Parse(HttpUtility.UrlEncode(keys[0].ToString())));
            if (BSuccess == 1)
            {
                Alert.ShowInTop(" 删除成功！", MessageBoxIcon.Information);
                BindGrid1();
            }
            else
            {
                Alert.ShowInTop(" 删除失败！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 添加车辆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            string openUrl = String.Format("Car_Window.aspx?ID={0}", "");
            PageContext.RegisterStartupScript(Window1.GetShowReference(openUrl, "新增车辆"));
        }


        /// <summary>
        /// 更新车辆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdUser_Click(object sender, EventArgs e)
        {
            string sIDStr = string.Empty;
            int selectedCount = GridUser.SelectedRowIndexArray.Length;
            if (selectedCount == 0)
            {
                Alert.ShowInTop("请选择一项纪录！", MessageBoxIcon.Warning);
                return;
            }

            int rowIndex = GridUser.SelectedRowIndexArray[0];
            object[] dataKeys = GridUser.DataKeys[rowIndex];
            sIDStr = HttpUtility.UrlEncode(dataKeys[0].ToString());

            string openUrl = String.Format("Car_Window.aspx?ID={0}", sIDStr);
            PageContext.RegisterStartupScript(Window1.GetShowReference(openUrl, "修改车辆"));
        }

        /// <summary>
        /// 添加驾驶员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddRole_Click(object sender, EventArgs e)
        {
            string openUrl = String.Format("Driver_Window.aspx?id={0}", "");
            PageContext.RegisterStartupScript(Window2.GetShowReference(openUrl, "新增驾驶员"));
        }

        /// <summary>
        /// 更新驾驶员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdRole_Click(object sender, EventArgs e)
        {
            string sIDStr = string.Empty;
            int selectedCount = Grid1.SelectedRowIndexArray.Length;
            if (selectedCount == 0)
            {
                Alert.ShowInTop("请选择一项纪录！", MessageBoxIcon.Warning);
                return;
            }
            object[] keys = Grid1.DataKeys[Grid1.SelectedRowIndex];
            string openUrl = String.Format("Driver_Window.aspx?ID={0}", HttpUtility.UrlEncode(keys[0].ToString()));
            PageContext.RegisterStartupScript(Window2.GetShowReference(openUrl, "修改驾驶员"));
        }



        #endregion

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindUserGrid();
        }

        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid1();
        }

    }
}