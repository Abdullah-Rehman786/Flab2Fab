<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Flab2Fab.Account.Register" ClientIDMode="Static" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %></h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Create a new account</h4>
        <hr />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="TxtName" CssClass="col-md-2 control-label">Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="TxtName" TextMode="SingleLine" CssClass="form-control" OnTextChanged="TextBox1_TextChanged" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtName"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The Name field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="TxtPhone" CssClass="col-md-2 control-label">Phone</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="TxtPhone" TextMode="Phone" CssClass="form-control" OnTextChanged="TextBox1_TextChanged" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtPhone"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The Phone field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    CssClass="text-danger" ErrorMessage="The email field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="The password field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Confirm password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="RadioButtonListOptions" CssClass="col-md-2 control-label">Did you pay?</asp:Label>
            <div class="col-md-10">
                <asp:RadioButtonList ID="RadioButtonListOptions" runat="server" RepeatDirection="Vertical">
                    <asp:ListItem Text="Yes" Value="true" />
                    <asp:ListItem Text="Not yet" Value="false" />
                </asp:RadioButtonList>

                <asp:RequiredFieldValidator ID="RequiredFieldValidatorOptions" runat="server"
                    ControlToValidate="RadioButtonListOptions"
                    InitialValue=""
                    ErrorMessage="Please select an option"
                    Display="Dynamic"
                    CssClass="text-danger" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="TxtStartWeight" CssClass="col-md-2 control-label">Starting Weight</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="TxtStartWeight" CssClass="form-control" TextMode="SingleLine" />
                <asp:Label runat="server" CssClass="fieldDescriptor">lbs</asp:Label>
                <br />
                <asp:CheckBox ID="CheckNoStartWeight" Text=" Enter starting weight later" runat="server" ClientIDMode="Static" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="AnonymousButtonListOptions" CssClass="col-md-2 control-label">Hide weight from other users?</asp:Label>
            <div class="col-md-10">
                <asp:RadioButtonList ID="AnonymousButtonListOptions" runat="server" RepeatDirection="Vertical">
                    <asp:ListItem Text="Yes" Value="true" />
                    <asp:ListItem Text="No" Value="false" />
                </asp:RadioButtonList>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="AnonymousButtonListOptions"
                    InitialValue=""
                    ErrorMessage="Please select an option"
                    Display="Dynamic"
                    CssClass="text-danger" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            console.log("ready!");
            $("#CheckNoStartWeight").change(function () {
                console.log("changed");
                if (this.checked) {
                    $("#TxtStartWeight").prop("disabled", true);
                } else {
                    $("#TxtStartWeight").prop("disabled", false);
                }
            });
        });
    </script>
</asp:Content>
