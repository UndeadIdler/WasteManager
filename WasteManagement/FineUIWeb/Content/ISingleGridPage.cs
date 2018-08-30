using System;
using System.Collections.Generic;
using System.Text;
using FineUI;

namespace WasteManagement
{
    public interface ISingleGridPage
    {
        /// <summary>
        /// 主表格实例
        /// </summary>
        Grid Grid { get; }

        /// <summary>
        /// 重新绑定表格
        /// </summary>
        void BindGrid();

        /// <summary>
        /// 删除选中的行数据
        /// </summary>
        void DeleteSelectedRows();

        /// <summary>
        /// 编辑选中的行数据
        /// </summary>
        //void EditSelectedRows();

        /// <summary>
        /// 获取新增地址
        /// </summary>
        /// <returns></returns>
        string GetNewUrl();

        /// <summary>
        /// 获取编辑地址
        /// </summary>
        /// <returns></returns>
        string GetEditUrl();
    }
}

