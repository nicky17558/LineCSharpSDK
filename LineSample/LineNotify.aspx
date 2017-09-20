<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineNotify.aspx.cs" Inherits="LineSample.LineNotify" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="Button1" runat="server" Text="Send" OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" Text="AskToken" OnClick="Button2_Click" />
            <asp:Button ID="Button3" runat="server" Text="Revoke" OnClick="Button3_Click" />
        </div>
    </form>
</body>
</html>
