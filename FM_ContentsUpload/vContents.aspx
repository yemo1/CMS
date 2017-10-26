<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vContents.aspx.cs" Inherits="FM_ContentsUpload.vContents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <span class="GreenText">Personal Information</span> 
    <div class="myforms">
        <asp:Label ID="Label9" runat="server" Text="Select Platform" CssClass="labels" >Select Platform<span>*</span></asp:Label>
        <asp:DropDownList ID="ddlPlatform" runat="server" CssClass="myText" 
             AutoPostBack="True" 
            onselectedindexchanged="ddlPlatform_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="reqPlatform" runat="server" ErrorMessage="Select a Platform" 
            InitialValue="--Select a platform--" ControlToValidate="ddlPlatform" ValidationGroup="submit"><span class="failureNotification">*</span></asp:RequiredFieldValidator>
    </div>
    <div class="myforms" runat="server" id="services">
        <asp:Label ID="Label1" runat="server" Text="Select  Service" CssClass="labels" >Select a service<span>*</span></asp:Label>
        <asp:DropDownList ID="ddlServices" runat="server" CssClass="myText" 
            AppendDataBoundItems="True" 
            onselectedindexchanged="ddlServices_SelectedIndexChanged" 
            AutoPostBack="True">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="reqService" runat="server" ErrorMessage="Select a service" 
            InitialValue="--Select a service--" ControlToValidate="ddlServices" ValidationGroup="submit"><span class="failureNotification">*</span></asp:RequiredFieldValidator>
    </div>
    <div>
        <asp:ImageButton ID="imgExport" runat="server" 
            ImageUrl="~/Styles/Images/ExcelImage.jpg" CausesValidation="False" 
            onclick="imgExport_Click" />
    </div>
    <div>
        <asp:GridView ID="grvContents" runat="server" Width="100%" AllowPaging="true" GridLines="Horizontal"
            PageSize="30" DataKeyNames="ID" AutoGenerateColumns="false"   
            CssClass="DDGridView" RowStyle-CssClass="td" HeaderStyle-CssClass="th" 
            CellPadding="6" PagerStyle-CssClass="DDPager" 
             ShowFooter="true" onrowdatabound="grvContents_RowDataBound" 
            onpageindexchanging="grvContents_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="Contents" HeaderText="Message" SortExpression="Contents"
                    ItemStyle-Width="450px" />
                <asp:BoundField DataField="Status" HeaderText="Sent" SortExpression="Status" />
                <asp:BoundField DataField="Published" HeaderText="Date uploaded" SortExpression="Published"
                    DataFormatString="{0:MMMM dd, yyyy}" />
            </Columns>
             <EmptyDataTemplate>
                <center>There is no content for this category</center>
             </EmptyDataTemplate>
        </asp:GridView>
        
        <asp:GridView ID="grvNames" runat="server" Width="100%" AllowPaging="true" GridLines="Horizontal"
            PageSize="30" DataKeyNames="ID" AutoGenerateColumns="false" CssClass="DDGridView"
            RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6" PagerStyle-CssClass="DDPager"
            ShowFooter="true" OnRowDataBound="grvContents_RowDataBound" OnPageIndexChanging="grvContents_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Arabic" HeaderText="Arabic" SortExpression="Arabic" />
                <asp:BoundField DataField="Meaning" HeaderText="Meaning" SortExpression="Meaning"
                    ItemStyle-Width="350px" />
                <asp:BoundField DataField="Published" HeaderText="Date uploaded" SortExpression="Published"
                    DataFormatString="{0:MMMM dd, yyyy}" />
            </Columns>
            <EmptyDataTemplate>
                <center>There is no content for this category</center>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
