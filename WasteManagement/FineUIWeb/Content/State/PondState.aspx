<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PondState.aspx.cs" Inherits="WasteManagement.Content.State.PondState" %>

<%@ Register TagPrefix="Zero" Namespace="HighchartsNET" Assembly="HighchartsNET" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

<meta http-equiv="refresh" content="60">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<asp:Button runat="server" ID="refresh" Text="刷新" OnClick="Page_Load" />
    <Zero:HighCharts runat="server" ID="highcharts1" Title="罐池使用情况"/>
    </div>
    </form>
</body>
</html>
