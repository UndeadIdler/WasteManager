<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WasteAmount.aspx.cs" Inherits="WasteManagement.Content.State.WasteAmount" %>

<%@ Register TagPrefix="Zero" Namespace="HighchartsNET" Assembly="HighchartsNET" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta http-equiv="refresh" content="60">

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div >
    <asp:Button runat="server" ID="refresh" Text="刷新" OnClick="Page_Load" />
    <Zero:HighCharts runat="server" ID="highcharts1" Title="全年情况" Height="285px"/>
    <Zero:HighCharts runat="server" ID="highcharts2" Title="当日情况" Height="285px"/>
    </div>
    </form>
</body>
</html>
