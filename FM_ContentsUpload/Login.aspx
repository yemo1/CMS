<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FM_ContentsUpload.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="formStyle">
            <asp:Login ID="log" runat="server" 
                InstructionText="Enter your credentials to login" 
                TextBoxStyle-CssClass="textEffect">
            <ValidatorTextStyle ForeColor="Red"/>
            <TitleTextStyle CssClass="loginTitle" />
            <LoginButtonStyle CssClass="orange" />
            </asp:Login>
        </div>
    </form>
</body>
</html>
