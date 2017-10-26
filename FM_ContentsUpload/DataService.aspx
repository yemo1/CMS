<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DataService.aspx.cs" Inherits="FM_ContentsUpload.DataService" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $(".notification-close-success").click(function () {
                $(".notification-box-success").fadeOut("slow"); return false;
            });

            $(".notification-close-warning").click(function () {
                $(".notification-box-warning").fadeOut("slow"); return false;
            });

            $(".notification-close-error").click(function () {
                $(".notification-box-error").fadeOut("slow"); return false;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="success" class="notification-box notification-box-success" runat="server">
        <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
        <asp:HyperLink ID="hpkClose" CssClass="notification-close notification-close-success" runat="server">X</asp:HyperLink>
    </div>
    <span class="GreenText">Data Alert Service</span> 
    <div class="myforms" runat="server" id="services">
        <asp:Label ID="lblService" runat="server" Text="Select  Service" CssClass="labels" >Select a service<span>*</span></asp:Label>
        <asp:DropDownList ID="ddlService" runat="server" CssClass="myText" 
            AppendDataBoundItems="True" >
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="reqService" runat="server" ErrorMessage="Select a service" 
            InitialValue="--Select a service--" ControlToValidate="ddlService" ValidationGroup="Error"><span class="failureNotification">*</span></asp:RequiredFieldValidator>
    </div>
    <div class="myforms">
        <asp:Label ID="lblBrowse" runat="server" Text="Browse File" CssClass="labels" >Browse File<span>*</span></asp:Label>
        <asp:FileUpload ID="fuUpload" runat="server" CssClass="myText" />
    </div>
    <%--<div class="myforms">
        <asp:Label ID="lblTitle" runat="server" Text="Title" CssClass="labels"></asp:Label>
        <asp:TextBox ID="txtTitle" CssClass="myText" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ReqTitle" runat="server" ErrorMessage="[Enter tile]" ControlToValidate="txtTitle" Display="Dynamic" ValidationGroup="Error" CssClass="text-danger small"><span class="failureNotification">*</span></asp:RequiredFieldValidator>
    </div>
    <div class="myforms">
        <asp:Label ID="lblLink" runat="server" Text="Link" CssClass="labels"></asp:Label>
        <asp:TextBox ID="txtLink" CssClass="myText" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ReqLink" runat="server" ErrorMessage="[Enter link]" ControlToValidate="txtLink" Display="Dynamic" ValidationGroup="Error" CssClass="text-danger small"><span class="failureNotification">*</span></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegLink" runat="server" ErrorMessage="Enter a valid Url" Display="Dynamic" ValidationGroup="Error" ControlToValidate="txtLink" CssClass="text-danger small" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"><span class="failureNotification">*</span></asp:RegularExpressionValidator>
    </div>
    <div class="myforms">
        <asp:Label ID="lblImage" runat="server" Text="Image Link" CssClass="labels"></asp:Label>
        <asp:TextBox ID="txtImage" CssClass="myText" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ReqImage" runat="server" ErrorMessage="[Enter Image url]" ControlToValidate="txtImage" Display="Dynamic" ValidationGroup="Error" CssClass="text-danger small"><span class="failureNotification">*</span></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegImage" runat="server" ErrorMessage="[Enter a valid Url]" ControlToValidate="txtImage" Display="Dynamic" ValidationGroup="Error" CssClass="text-danger small" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"><span class="failureNotification">*</span></asp:RegularExpressionValidator>
    </div>
    <div class="myforms">
        <asp:Label ID="lblDescription" runat="server" Text="Description" CssClass="labels"></asp:Label>
        <CKEditor:CKEditorControl ID="CKEditor1" runat="server" CssClass="myText" >

        </CKEditor:CKEditorControl>
    </div>--%>
    <div>
        <asp:Button ID="btnUpload" runat="server" Text="Upload" ValidationGroup="Error" 
             CssClass="orange" OnClick="btnUpload_Click" />
    </div>
</asp:Content>
