<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Campaign.aspx.cs" Inherits="FM_ContentsUpload.Campaign" %>
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

            $("input[id$='txtDate']").datepicker({
                changeMonth: true,
                dateFormat: 'dd/mm/yy',
                showOtherMonths: true,
                changeYear: true,
                yearRange: '1970:(new Date).getFullYear()',
                showOn: "button",
                buttonImage: "Styles/Images/Calendar_scheduleHS.png",
                buttonImageOnly: true,
                showWeek: true,
                showButtonPanel: true,
                firstDay: 1
            });

            $("input[id$='txtTime']").timepicker({
                'timeFormat': 'H:i:s', 'step': 60, 'minTime': '8:00am','maxTime': '6:00pm'
            });
            
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="success" class="notification-box notification-box-success" runat="server">
        <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
        <asp:HyperLink ID="hpkClose" CssClass="notification-close notification-close-success"
            runat="server">X</asp:HyperLink>
    </div>
    <span class="GreenText">Campaign</span>
    <div id="Div1" class="myforms" runat="server">
        <asp:Label ID="Label1" runat="server" Text="" CssClass="labels">Select a segment<span>*</span></asp:Label>
        <asp:DropDownList ID="ddlSegment" runat="server" CssClass="myText" AppendDataBoundItems="true">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select a segment"
            InitialValue="--Select a segment--" ControlToValidate="ddlSegment" ValidationGroup="submit"><span class="failureNotification">*</span></asp:RequiredFieldValidator>
    </div>
    <div class="myforms">
        <asp:Label ID="Label9" runat="server" Text="Select Service" CssClass="labels">Select service<span>*</span></asp:Label>
        <asp:DropDownList ID="ddlService" runat="server" CssClass="myText" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlService_SelectedIndexChanged">
        </asp:DropDownList>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Select a service"
            InitialValue="--Select a service--" ControlToValidate="ddlService" ValidationGroup="submit"><span class="failureNotification">*</span></asp:RequiredFieldValidator>
    </div>
    <div class="myforms" id="type" runat="server">
        <asp:Label ID="Label2" runat="server" Text="" CssClass="labels">Select shortcode<span>*</span></asp:Label>
        <asp:DropDownList ID="ddlShortcode" runat="server" CssClass="myText"
            AppendDataBoundItems="True">

        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="reqType" runat="server" ErrorMessage="Select shortcode"
            InitialValue="--Select shortcode--" ControlToValidate="ddlShortcode" ValidationGroup="submit"><span class="failureNotification">*</span></asp:RequiredFieldValidator>
    </div>
    <div class="myforms">
        <asp:Label ID="Label4" runat="server" Text="" CssClass="labels">Select a Bind<span>*</span></asp:Label>
        <asp:DropDownList ID="ddlBind" runat="server" CssClass="myText">
            
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select a bind"
            InitialValue="--Select Bind--" ControlToValidate="ddlBind" ValidationGroup="submit"><span class="failureNotification">*</span></asp:RequiredFieldValidator>
    </div>
    <div class="myforms" id="size" runat="server">
        <asp:Label ID="Label10" runat="server" Text="" CssClass="labels">Sample Size<span>*</span></asp:Label>
        <asp:TextBox ID="txtSize" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="reqSize" runat="server" ErrorMessage="Enter a target size" ControlToValidate="txtSize" CssClass="failureNotification" ValidationGroup="submit">*</asp:RequiredFieldValidator>
        <asp:CompareValidator ID="cmpSize" runat="server" ErrorMessage="Enter a numeric value" Type="Integer" Operator="DataTypeCheck"
            ToolTip="Enter a numeric value" ControlToValidate="txtSize" ValidationGroup="submit" CssClass="failureNotification">*</asp:CompareValidator>
    </div>
    <div id="targ" class="myforms" runat="server">
        <asp:Label ID="Label8" runat="server" Text="" CssClass="labels">Select a state<span>*</span></asp:Label>
        <asp:DropDownList ID="ddlState" runat="server" CssClass="myText" AppendDataBoundItems="True">
            <asp:ListItem>--Select a state--</asp:ListItem>
        </asp:DropDownList>
        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select a state"
            InitialValue="--Select a state--" ControlToValidate="ddlState" ValidationGroup="submit"><span class="failureNotification">*</span></asp:RequiredFieldValidator>--%>
    </div>
    
    <div class="myforms">
        <asp:Label ID="lblSchedule" runat="server" Text="Message" CssClass="labels">Schedule<span>*</span></asp:Label>
        <asp:TextBox ID="txtDate" runat="server" CssClass="myText"></asp:TextBox>
        <asp:CompareValidator ID="validDate" runat="server" ErrorMessage="Date must be greater than to equal  current date."
            ValidationGroup="CompulsoryFields" ForeColor="Red" ToolTip="Date must be graeter than or equal to current date"
            Type="Date" ControlToValidate="txtDate" Operator="GreaterThanEqual">*</asp:CompareValidator>
        <asp:RequiredFieldValidator ID="reqDate" runat="server" ErrorMessage="Please select a schedule date"
            ControlToValidate="txtDate" ForeColor="Red" ValidationGroup="submit">*</asp:RequiredFieldValidator><br />
    </div>
    <div class="myforms">
        <asp:Label ID="Label6" runat="server" Text="" CssClass="labels">Time<span>*</span></asp:Label>
        <asp:TextBox ID="txtTime" runat="server" CssClass="myText"></asp:TextBox>
        <asp:RequiredFieldValidator ID="reqTime" runat="server" ErrorMessage="Please select time"
            ControlToValidate="txtTime" ForeColor="Red" ValidationGroup="submit">*</asp:RequiredFieldValidator><br />
    </div>
     <div class="myforms">
        <asp:Label ID="lblMsg" runat="server" Text="Message" CssClass="labels">Message<span>*</span></asp:Label>
        <asp:TextBox ID="txtMessage" runat="server" CssClass="multiText" TextMode="MultiLine" Rows="8"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Provide the campaign message"
            ValidationGroup="submit" ControlToValidate="txtMessage">
            <span class="failureNotification">*</span>
        </asp:RequiredFieldValidator>
    </div>
    <span id="spn" style="margin: 0 10em 0 24.5em; color: Red; font-weight: bold"></span>
    <div style="margin-left:17em;">
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
            CssClass="orange" OnClick="btnSubmit_Click"  />
    </div>
</asp:Content>
