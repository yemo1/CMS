<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="FM_ContentsUpload.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="formStyle">
         <asp:Login ID="log" runat="server" InstructionText="Enter your credentials to login" TextBoxStyle-CssClass="textEffect">
            <ValidatorTextStyle ForeColor="Red"/>
            <TitleTextStyle CssClass="loginTitle" />
            <LoginButtonStyle CssClass="orange" />
         </asp:Login>
    </div>
</asp:Content>
