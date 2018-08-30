<%@ Page Title="" Language="C#" MasterPageFile="~/Content/SingleGridContracts.Master" AutoEventWireup="true" CodeBehind="StockOut.aspx.cs" Inherits="WasteManagement.Content.Report.StockOut" %>
<%@ MasterType VirtualPath="~/Content/SingleGridContracts.Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="mainCPH" runat="server">
   <f:Form ID="Form6" ShowBorder="true" ShowHeader="true" Title="条件查询" runat="server" Width="1120px" BodyPadding="5">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drop_Type" EmptyText="种类" LabelWidth="95px" Label="种类" ShowLabel="true" AutoPostBack="true"  
                        runat="server" Width="220px" OnSelectedIndexChanged="drop_TypeChanged">
                        <f:ListItem Text="废物" Value="1" Selected="true" />
                        <f:ListItem Text="成品" Value="2" />
                        </f:DropDownList>

                     <f:DatePicker runat="server" ID="DateStart" Width="220px" LabelWidth="95px" Label="日期大于" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                    <f:DatePicker runat="server" ID="DateEnd"  Width="220px" LabelWidth="95px" Label="日期小于" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                     <f:Label ID="Label1" runat="server" Width="220px" ></f:Label>


                     
                </Items>
            </f:FormRow>            
             <f:FormRow>
                 <Items>
                    <f:RadioButton runat="server" Width="220px" Label="分类依据：" LabelWidth="95px" ID="rb" Text="废物名称" Checked="true"></f:RadioButton>
                    <%--<f:TextBox runat="server" Width="220px" LabelWidth="95px" Label="分类依据：" ID="txt_ContractNumber" Readonly="true" Text="废物名称"></f:TextBox>--%>             
                    <f:CheckBox runat="server" Width="220px" LabelWidth="95"  Text="收货单位" ShowLabel="true" ID="cb_Enterprise"></f:CheckBox>
                     <f:Button ID="btn_save" Text="查询" CssClass="btn" ValidateForms="Form1"  OnClick="btn_Search_Click" ValidateMessageBox="false" runat="server">
                        </f:Button>
                        <f:Button ID="btn_Export" runat="server" Text="导出" OnClick="btn_Export_Click" EnableAjax="false" DisableControlBeforePostBack="false"></f:Button>
                 </Items>
             </f:FormRow>
        </Rows>
    </f:Form>
    <f:Grid ID="Grid1" EnableCollapse="false" PageSize="10" ShowBorder="true" ShowHeader="false"
        BoxFlex="1"
        AllowPaging="true" runat="server" EnableCheckBoxSelect="True" CheckBoxSelectOnly="true"
         IsDatabasePaging="true"  SortField="WasteName" OnSort="Grid1_Sort"
        AllowSorting="true"  SortDirection="ASC">
                   <Columns>
                <f:RowNumberField />

                       <%--<f:BoundField DataField="ProduceName" SortField="ProduceName" Width="220px" HeaderText="企业名称" />--%>
                       <f:BoundField DataField="WasteName" SortField="WasteName" Width="220px" HeaderText="废物名称" />
                <f:BoundField DataField="Total" SortField="Total" Width="280px" HeaderText="数量总计" />
            </Columns>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" runat="server">
            </f:Toolbar>
        </Toolbars>
    </f:Grid>

     <f:Grid ID="Grid2" EnableCollapse="false" PageSize="10" ShowBorder="true" ShowHeader="false"
        BoxFlex="1"
        AllowPaging="true" runat="server" EnableCheckBoxSelect="True" CheckBoxSelectOnly="true"
         IsDatabasePaging="true" SortField="Name"
        AllowSorting="true"  SortDirection="ASC">
                   <Columns>
                <f:RowNumberField />

                       <f:BoundField DataField="Name" SortField="Name" Width="220px" HeaderText="收货单位名称" />
                       <f:BoundField DataField="WasteName" SortField="WasteName" Width="220px" HeaderText="废物名称" />
                <f:BoundField DataField="Total" SortField="Total" Width="280px" HeaderText="数量总计" />
            </Columns>
        <Toolbars>
            <f:Toolbar ID="Toolbar2" runat="server">
            </f:Toolbar>
        </Toolbars>
    </f:Grid>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="headCPH">
    <meta name="sourcefiles" content="SingleGridContracts.Master;ISingleGridPage.cs" />
</asp:Content>
