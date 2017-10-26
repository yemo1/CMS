<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewService.aspx.cs" Inherits="FM_ContentsUpload.NewService" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div id="success" class="notification-box notification-box-success" runat="server">
        <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
        <asp:HyperLink ID="hpkClose" CssClass="notification-close notification-close-success"
            runat="server">X</asp:HyperLink>
    </div>
    <div>
        <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="failureNotification" ShowMessageBox="true" ValidationGroup="submit" DisplayMode="BulletList" ShowSummary="false"/>
    </div>
    <span class="GreenText">Create a new Service</span>
    <div id="Div3" class="myforms" runat="server">
        <asp:Label ID="Label6" runat="server" Text="" CssClass="labels">Service Name<span>*</span></asp:Label>
        <asp:TextBox ID="txtService" runat="server" CssClass="myText"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="Enter a service name" Display="Dynamic"
            ControlToValidate="txtService" ValidationGroup="submit"><span class="failureNotification">*</span></asp:RequiredFieldValidator>
    </div>
     <div>
        <asp:Button ID="btnCreate" runat="server" Text="Create Service" ValidationGroup="submit" 
             CssClass="orange" OnClick="btnCreate_Click" />
    </div>
</asp:Content>
