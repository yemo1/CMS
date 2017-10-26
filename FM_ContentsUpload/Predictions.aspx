<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Predictions.aspx.cs" Inherits="FM_ContentsUpload.Predictions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <span class="GreenText">View Predictions</span>

    <div id="Div3" class="myforms" runat="server">
        <asp:Label ID="Label6" runat="server" Text="" CssClass="labels">Match<span>*</span></asp:Label>
        <asp:DropDownList ID="ddlCode" runat="server" CssClass="myText"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select a match" Display="Dynamic"
            ControlToValidate="ddlCode" ValidationGroup="submit" InitialValue="--Select a match--"><span class="failureNotification">*</span></asp:RequiredFieldValidator>
    </div>
    <div>
        <asp:Button ID="btnFind" runat="server" Text="Find Predictions" ValidationGroup="submit"
            CssClass="orange" OnClick="btnFind_Click" />
    </div>
       
    <div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="grvPredict" runat="server" Width="100%" AllowPaging="true" GridLines="Horizontal"
                    PageSize="30" AutoGenerateColumns="false" CssClass="DDGridView"
                    RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6" PagerStyle-CssClass="DDPager"
                    ShowFooter="true">
                    <Columns>
                        <asp:BoundField DataField="TeamA" HeaderText="Team A" SortExpression="TeamA" />
                        <asp:BoundField DataField="TeamB" HeaderText="Team B" SortExpression="TeamB" />
                        <asp:BoundField DataField="MSISDN" HeaderText="MSISDN" SortExpression="MSISDN" />
                        <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" />
                        <asp:BoundField DataField="RecTime" HeaderText="Prediction Time" SortExpression="RecTime"
                            DataFormatString="{0:dd-MM-yyyy hh:mm:ss}" />
                    </Columns>
                    <EmptyDataTemplate>
                        <center>There is no content for this category</center>
                    </EmptyDataTemplate>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
       
    </div>
</asp:Content>
