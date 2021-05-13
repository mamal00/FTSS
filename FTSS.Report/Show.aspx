<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="FTSS.Report.Show" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     
    <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
       	 <div style="text-align: right; direction: rtl; overflow: scroll;"> 
			<rsweb:ReportViewer ID="ReportViewer1" runat="server" KeepSessionAlive="true"
                Width="99%" ExportContentDisposition="AlwaysAttachment" Height="450px"
                PromptAreaCollapsed="True" SizeToReportContent="true">
                   <LocalReport EnableExternalImages="True" EnableHyperlinks="True">
                </LocalReport>
			</rsweb:ReportViewer>
        </div>
         <asp:Label runat="server" ID="lblError"></asp:Label>
    </form>
</body>
</html>
