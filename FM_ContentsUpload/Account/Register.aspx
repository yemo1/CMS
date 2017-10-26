<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="FM_ContentsUpload.Account.Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $(".notification-close-success").click(function () {
                $(".notification-box-success").fadeOut("slow"); return false;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
     <div id="success" class="notification-box notification-box-success" runat="server">
        <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
        <asp:HyperLink ID="hpkClose" CssClass="notification-close notification-close-success" runat="server">X</asp:HyperLink>
    </div>
     <div class="singup">
         <asp:CreateUserWizard ID="RegisterUser" runat="server" EnableViewState="false" OnCreatedUser="RegisterUser_CreatedUser" RequireEmail="false">
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>
            <asp:CreateUserWizardStep ID="RegisterUserWizardStep" runat="server">
                <ContentTemplate>
                    <p>
                        Passwords are required to be a minimum of <%= Membership.MinRequiredPasswordLength %> characters in length.
                    </p>
                    <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="RegisterUserValidationGroup"/>
                    
                    <div class="signUpHeader">
                       Account Information
                    </div>
                    <div class="myforms">
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" CssClass="labels">Username</asp:Label>
                        <asp:TextBox ID="UserName" runat="server"  CssClass="myText"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                                CssClass="failureNotification" ErrorMessage="Email address is compulsory to create an account." ToolTip="Email is required." 
                                ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="myforms">
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" CssClass="labels">Password:</asp:Label>
                        <asp:TextBox ID="Password" runat="server" CssClass="myText" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                                CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                                ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="myforms">
                        <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword" CssClass="labels">Confirm Password:</asp:Label>
                        <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="myText" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification" Display="Dynamic" 
                                ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired" runat="server" 
                                ToolTip="Confirm Password is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                                ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
                    </div>
                    <div class="signupButton">
                        <asp:Button ID="CreateUserButton" runat="server" CommandName="MoveNext" ValidationGroup="RegisterUserValidationGroup" Text="Create User" CssClass="orange"/>
                    </div>
                </ContentTemplate>
                <CustomNavigationTemplate>
                </CustomNavigationTemplate>
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep>
                <ContentTemplate>
                    <div style="margin:0 auto 0 auto">
                        <a href="../Default.aspx">Click here to continue</a>
                    </div>
                </ContentTemplate>
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
    </div>
</asp:Content>
