<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="FM_ContentsUpload._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
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

        $(function () {
            var limit = 160;
            var $tb = $('textarea[id$=txtMessage]');
            $tb.keyup(function () {
                var len = $(this).val().length;
                if (len > limit) {
                    $(this).addClass('exceeded');
                    $('#spn').text(len - limit + " characters exceeded");
                }
                else {
                    $(this).removeClass('exceeded');
                    $('#spn').text(limit - len + " characters left");
                }
            });
            $('input[id$=btnSubmit]').click(function (e) {
                var len = $tb.val().length;
                if (len > limit) {
                    e.preventDefault();
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="success" class="notification-box notification-box-success" runat="server">
        <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
        <asp:HyperLink ID="hpkClose" CssClass="notification-close notification-close-success" runat="server">X</asp:HyperLink>
    </div>
    <span class="GreenText">Content Upload</span>

    <div class="myforms">
        <asp:Label ID="Label9" runat="server" Text="Select Platform" CssClass="labels">Select Platform<span>*</span></asp:Label>
        <asp:DropDownList ID="ddlPlatform" runat="server" CssClass="myText"
            AutoPostBack="True"
            OnSelectedIndexChanged="ddlPlatform_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="reqPlatform" runat="server" ErrorMessage="Select a Platform"
            InitialValue="--Select a platform--" ControlToValidate="ddlPlatform" ValidationGroup="submit"><span class="failureNotification">*</span></asp:RequiredFieldValidator>
    </div>
    <div class="myforms" id="type" runat="server">
        <asp:Label ID="Label2" runat="server" Text="Select content type" CssClass="labels">Select content type<span>*</span></asp:Label>
        <asp:DropDownList ID="ddlType" runat="server" CssClass="myText"
            AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
            AppendDataBoundItems="True">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="reqType" runat="server" ErrorMessage="Select content type"
            InitialValue="--Select content type--" ControlToValidate="ddlType" ValidationGroup="submit"><span class="failureNotification">*</span></asp:RequiredFieldValidator>
    </div>
    <div class="myforms" runat="server" id="services">
        <asp:Label ID="Label1" runat="server" Text="Select  Service" CssClass="labels">Select a service<span>*</span></asp:Label>
        <asp:DropDownList ID="ddlServices" runat="server" CssClass="myText"
            AppendDataBoundItems="True" AutoPostBack="true"
            OnSelectedIndexChanged="ddlServices_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="reqService" runat="server" ErrorMessage="Select a service"
            InitialValue="--Select a service--" ControlToValidate="ddlServices" ValidationGroup="submit"><span class="failureNotification">*</span></asp:RequiredFieldValidator>
    </div>
    <div id="Bulk" runat="server">
        <div class="myforms">
            <asp:Label ID="Label11" runat="server" Text="Browse File" CssClass="labels">Browse File<span>*</span></asp:Label>
            <asp:FileUpload ID="fuUpload" runat="server" CssClass="myText" />
        </div>
        <div>
            <asp:Button ID="btnUpload" runat="server" Text="Upload" ValidationGroup="submit"
                OnClick="btnUpload_Click" CssClass="orange" />
        </div>
        <p>
            <asp:Label ID="lblMessage" runat="server"
                Text="File size should not exceed 20MB" Font-Bold="True" ForeColor="Red"></asp:Label>
        </p>
    </div>

    <div id="Shortz" runat="server">
        <div class="myforms">
            <asp:Label ID="lblMsg" runat="server" Text="Message" CssClass="labels">Message<span>*</span></asp:Label>
            <asp:TextBox ID="txtMessage" runat="server" CssClass="multiText" TextMode="MultiLine" Rows="8"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Provide the campaign message"
                ValidationGroup="submit" ControlToValidate="txtMessage">
            <span class="failureNotification">*</span>
            </asp:RequiredFieldValidator>
        </div>
        <span id="spn" style="margin: 0 10em 0 24.5em; color: Red; font-weight: bold"></span>
        <div style="margin-left: 17em;">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                CssClass="orange" OnClick="btnSubmit_Click" />
        </div>
    </div>

</asp:Content>
