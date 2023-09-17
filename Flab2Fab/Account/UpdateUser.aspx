<%@ Page Title="Update Info" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateUser.aspx.cs" Inherits="Flab2Fab.Account.UpdateUser" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%: Title %></h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
<%--    <div class="form-row">
        <asp:Button runat="server" ID="btnEdit" Text="Edit" CssClass="btn btn-secondary" OnClientClick="return btnSubmitUpdates();" />
    </div>--%>
    <div class="form-row">
        <asp:Label runat="server" for="txtName"><b>Name</b></asp:Label>
        <asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName"
            CssClass="text-danger" Display="Dynamic" ErrorMessage="The Name field is required." />
        <button type="button" class="minimalist-button">Edit</button>
    </div>
    <div class="form-row">
        <asp:Label runat="server" for="txtPhone"><b>Phone</b></asp:Label>
        <asp:TextBox runat="server" ID="txtPhone" ClientIDMode="Static" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPhone"
            CssClass="text-danger" Display="Dynamic" ErrorMessage="The Phone field is required." />
        <button type="button" class="minimalist-button">Edit</button>
    </div>
    <div class="form-row">
        <asp:Label runat="server" for="chkAnonymous"><b>Anonymous User</b></asp:Label>
        <asp:CheckBox runat="server" ID="chkAnonymous" ClientIDMode="Static" />
        <button type="button" class="minimalist-button">Edit</button>
    </div>
    <div class="form-row">
        <asp:Label runat="server" for="chkPaid"><b>Paid Dues</b></asp:Label>
        <asp:CheckBox runat="server" ID="chkPaid" ClientIDMode="Static" />
        <button type="button" class="minimalist-button">Edit</button>
    </div>
    <div class="form-row">
        <asp:Button runat="server" ID="btnSubmitUpdates" Text="Update" CssClass="btn btn-primary" OnClientClick="return btnSubmitUpdates();" OnClick="BtnUpdate_Click" />
    </div>
    <style type="text/css">
        /* Define a common class for form rows to make styling uniform */
        .form-row {
            display: flow;
            justify-content: flex-start;
            align-items: center; /* Vertically center align elements */
            margin: 10px 0;
        }

            /* Style labels */
            .form-row span {
                font-weight: bold;
                margin-right: 2px; /* Increase the space between label and input */
                flex: 0; /* Allow labels to grow and occupy available space */
            }

        span {
            font-size: 14px;
        }

        /* Style inputs (textboxes and checkboxes) */
        .form-row input[type="text"],
        .form-row input[type="checkbox"] {
            font-size: 20px;
            border-radius: 5px;
            border: 1px solid rgba(169, 169, 169, 0.5);
            flex: 2; /* Allow inputs to grow and occupy available space */
            margin: 10px 0px 10px 3rem;
        }

        /* Style edit buttons */
        .form-row button.minimalist-button {
            background-color: rgba(169, 169, 169, 0.12);
            border: 1px solid #ccc;
            border-radius: 3px;
            padding: 3px 8px;
            font-size: 8px;
            cursor: pointer;
            flex: 0; /* Prevent buttons from growing; they will maintain their size */
            margin-left: 2px; /* Adjust the space between input and button */
        }

            .form-row button.minimalist-button text {
                background-color: rgba(169, 169, 169, 0.12);
                border: 1px solid #ccc;
                border-radius: 3px;
                padding: 3px 8px;
                font-size: 8px;
                cursor: pointer;
                flex: 0; /* Prevent buttons from growing; they will maintain their size */
                margin-left: 2px; /* Adjust the space between input and button */
            }


        input[type="text"]:focus, input[type="checkbox"]:focus {
            outline: none; /* Remove the default focus outline */
            border: 0.5px solid rgba(0, 137, 225, 0.9); /* Add a light blue border when focused */
            box-shadow: 0 0 10px rgba(0, 167, 225, 0.5); /* Add a glowing effect */
        }

        input[type="text"]:read-only,
        input[type="checkbox"]:read-only {
            background-color: rgba(204, 204, 204, 0.25); /* Adjust the alpha value to control darkness */
        }
    </style>
    <script type="text/javascript">

        function btnSubmitUpdates() {

            var chkAnonymous = $("#chkAnonymous");
            chkAnonymous.prop('disabled', false);

            var chkPaid = $("#chkPaid");
            chkPaid.prop('disabled', false);

            return true;
        }

        $(document).ready(function () {
            $("input[type='checkbox']").prop("disabled", true);
            $("input[type='text']").prop("readonly", true);

            $(".minimalist-button").on("click", function () {
                var formRow = $(this).closest(".form-row");
                var textField = formRow.find("input[type='text'], input[type='checkbox']");
                var isCheckbox = textField.is("input[type='checkbox']");

                if (isCheckbox) {
                    // For checkboxes, toggle the disabled property
                    textField.prop("disabled", function (i, value) {
                        return !value;
                    });
                    textField.focus();

                    textField.on("blur", function () {
                        $(this).prop("disabled", true);
                    });
                } else {
                    // For textboxes, toggle the readonly property
                    textField.prop("readonly", function (i, value) {
                        return !value;
                    });

                    // Set cursor position only if it's a textbox
                    textField.filter("input[type='text']").focus().each(function () {
                        this.setSelectionRange(this.value.length, this.value.length);
                    });

                    textField.on("blur", function () {
                        $(this).prop("readonly", true);
                    });
                }
            });
        });

    </script>
</asp:Content>
