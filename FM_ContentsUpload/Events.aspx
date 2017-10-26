<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="FM_ContentsUpload.Events" %>
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

            $("input[id$='txtDate']").datetimepicker({
                format: 'd/m/Y H:i'
            });

            $('input[id$=imgDate]').click(function (e) {
                e.preventDefault();
                $("input[id$='txtDate']").datetimepicker('show');
            });
        });
        //$(function () {

        //});

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="success" class="notification-box notification-box-success" runat="server">
        <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
        <asp:HyperLink ID="hpkClose" CssClass="notification-close notification-close-success"
            runat="server">X</asp:HyperLink>
    </div>
    <div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="failureNotification" ShowMessageBox="true" ValidationGroup="submit" DisplayMode="BulletList" ShowSummary="false"/>
    </div>
    <span class="GreenText">Register an event</span>
    <div id="Div3" class="myforms" runat="server">
        <asp:Label ID="Label6" runat="server" Text="" CssClass="labels">Team A<span>*</span></asp:Label>
        <asp:TextBox ID="txtTeamA" runat="server" CssClass="myText"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Team A" Display="Dynamic"
            ControlToValidate="txtTeamA" ValidationGroup="submit"><span class="failureNotification">*</span></asp:RequiredFieldValidator>
    </div>
    <div id="Div1" class="myforms" runat="server">
        <asp:Label ID="Label1" runat="server" Text="" CssClass="labels">Team B<span>*</span></asp:Label>
        <asp:TextBox ID="txtTeamB" runat="server" CssClass="myText"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Team B" Display="Dynamic"
            ControlToValidate="txtTeamB" ValidationGroup="submit"><span class="failureNotification">*</span></asp:RequiredFieldValidator>
    </div>
    <div id="Div2" class="myforms" runat="server">
        <asp:Label ID="Label2" runat="server" Text="" CssClass="labels">Event Code<span>*</span></asp:Label>
        <asp:TextBox ID="txtCode" runat="server" CssClass="myText"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter event code" Display="Dynamic"
            ControlToValidate="txtCode" ValidationGroup="submit"><span class="failureNotification">*</span></asp:RequiredFieldValidator>
    </div>
    <div id="Div4" class="myforms" runat="server">
        <asp:Label ID="Label3" runat="server" Text="" CssClass="labels">Kickoff Time<span>*</span></asp:Label>
        <asp:TextBox ID="txtDate" runat="server" CssClass="myText"></asp:TextBox>
        <asp:ImageButton ID="imgDate" runat="server" ImageUrl="~/Styles/Images/Calendar_scheduleHS.png" CausesValidation="False" />
        <asp:CompareValidator ID="validDate" runat="server" ErrorMessage="Date must be greater than to equal  current date."
            ValidationGroup="CompulsoryFields" ForeColor="Red" ToolTip="Date must be graeter than or equal to current date"
            Type="Date" ControlToValidate="txtDate" Operator="GreaterThanEqual" Display="Dynamic">*</asp:CompareValidator>
        <asp:RequiredFieldValidator ID="reqDate" runat="server" ErrorMessage="Please select a kickoff time" Display="Dynamic"
            ControlToValidate="txtDate" ForeColor="Red" ValidationGroup="submit">*</asp:RequiredFieldValidator><br />
    </div>
    <div>
        <asp:Button ID="btnCreate" runat="server" Text="Create Event" ValidationGroup="submit" 
             CssClass="orange" OnClick="btnCreate_Click" />
    </div>
</asp:Content>
