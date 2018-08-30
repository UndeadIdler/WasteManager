<%@ Page Title="" Language="C#" MasterPageFile="~/Content/SingleGridContracts.Master" AutoEventWireup="true" CodeBehind="Analysis.aspx.cs" Inherits="WasteManagement.Content.Waste.Analysis" %>
<%@ MasterType VirtualPath="~/Content/SingleGridContracts.Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="mainCPH" runat="server">
<%--        <meta name="sourcefiles" content="AnalysisGrid.ashx" />
    <link href="../../res/css/main.css" rel="stylesheet" type="text/css" />--%>
   <f:Form ID="Form6" ShowBorder="true" ShowHeader="true" Title="条件查询" runat="server" Width="1120px" BodyPadding="5">
        <Rows>                
             <f:FormRow>
                 <Items>
                    <f:TextBox runat="server" Width="220px" LabelWidth="95px" Label="联单号" ID="txt_BillNumber"></f:TextBox>
                     <f:DatePicker runat="server" ID="DateStart" Width="220px" LabelWidth="95px" Label="分析日期大于" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                    <f:DatePicker runat="server" ID="DateEnd"  Width="220px" LabelWidth="95px" Label="分析日期小于" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                     <f:DropDownList ID="drop_Analysis" EmptyText="分析人" LabelWidth="95px" Label="分析人" ShowLabel="true" AutoPostBack="true"  
                        runat="server" Width="220px"></f:DropDownList>
                     <f:Button ID="btn_save" Text="查询" CssClass="btn" ValidateForms="Form1"  OnClick="btn_Search_Click" ValidateMessageBox="false" runat="server">
                        </f:Button>
                 </Items>
             </f:FormRow>
        </Rows>
    </f:Form>
    <%--<f:PageManager ID="PageManager1" AutoSizePanelID="Grid1" runat="server" />--%>
    <f:Grid ID="Grid1" EnableCollapse="false" PageSize="10" ShowBorder="true" ShowHeader="false"
        BoxFlex="1"
        AllowPaging="true" runat="server" EnableCheckBoxSelect="True" CheckBoxSelectOnly="true" OnSort="Grid1_Sort"
        DataKeyNames="AnalysisID,BillNumber" IsDatabasePaging="true"  EnableCollapseEvent="true"
        AllowSorting="true" SortField="BillNumber" SortDirection="ASC">
                   <Columns>
                <f:RowNumberField />
                <f:BoundField DataField="AnalysisID" Hidden="true" />  
                <f:BoundField DataField="BillNumber" SortField="BillNumber" Width="80px" HeaderText="联单号" />
                       <f:BoundField DataField="DateTime" SortField="DateTime" Width="120px" HeaderText="日期" />
                       <f:BoundField DataField="RealName" SortField="RealName" Width="120px" HeaderText="分析人" />
                       <f:BoundField DataField="StatusName" SortField="StatusName" Width="80px" HeaderText="状态" />
                <f:GroupField ID="GF1" HeaderText="分析结果" TextAlign="Center">
                            <Columns>                
                            </Columns>
                        </f:GroupField>                             
            </Columns>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" runat="server">
                <Items>
                    <%--<f:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click"></f:Button>--%>
                    <f:Button ID="btn_Pass" runat="server" Text="审核通过" OnClick="btn_Pass_Click"></f:Button>
                    <f:Button ID="btn_Export" runat="server" Text="导出" OnClick="btn_Export_Click" EnableAjax="false" DisableControlBeforePostBack="false"></f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Grid>
    <%--<script src="../../res/js/jquery.min.js" type="text/javascript"></script>
    <script>

        var grid1 = '<%= Grid1.ClientID %>';

        F.ready(function () {
            var grid1Cmp = F(grid1);
            // 展开行扩展列事件
            grid1Cmp.view.on('expandbody', function (rowNode, record, expandRow) {
                var tplEl = Ext.get(expandRow).query('.f-grid-tpl')[0];
                if (!Ext.String.trim(tplEl.innerHTML)) {

                    var store = Ext.create('Ext.data.Store', {
                        fields: ['BillNumber', 'ItemName', 'Result'],
                        proxy: {
                            type: 'ajax',
                            url: 'AnalysisGrid.ashx?id=' + grid1Cmp.DataKeys[record.dataIndex][1],
                            reader: {
                                type: 'json',
                                root: 'data',
                                totalProperty: 'total'
                            }
                        },
                        autoLoad: true,
                        listeners: {
                            load: function () {
                                rowExpandersDoLayout();
                            }
                        }
                    });

                    Ext.create('Ext.grid.Panel', {
                        renderTo: tplEl,
                        header: false,
                        border: true,
                        draggable: false,
                        enableDragDrop: false,
                        enableColumnResize: false,
                        cls: 'gridinrowexpander',
                        store: store,
                        columns: [{
                            text: '联单号', dataIndex: 'BillNumber', sortable: false, menuDisabled: true, width: 80
                        }, {
                            text: '分析项目', dataIndex: 'ItemName', sortable: false, menuDisabled: true, width: 80
                        }, {
                            text: '结果', dataIndex: 'Result', sortable: false, menuDisabled: true, width: 80
                        }]
                    });
                }
            });

            // 折叠行扩展列事件
            grid1Cmp.view.on('collapsebody', function (rowNode, record, expandRow) {
                rowExpandersDoLayout();
            });

        });


        // 重新布局表格和行扩展列中的表格（解决出现纵向滚动条时的布局问题）
        function rowExpandersDoLayout() {
            var grid1Cmp = F(grid1);

            grid1Cmp.doLayout();

            $('.x-grid-row:not(.x-grid-row-collapsed) .gridinrowexpander').each(function () {
                var gridInside = F($(this).attr('id'));
                gridInside.doLayout();
            });
        }

    </script>--%>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="headCPH">
    <meta name="sourcefiles" content="SingleGridContracts.Master;ISingleGridPage.cs" />
    
</asp:Content>


