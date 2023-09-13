<%@ Page Title="Track Weight" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TrackWeight.aspx.cs" Inherits="Flab2Fab.TrackWeight" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <div class="container mt-4">
        <h3>Your Recorded Weights</h3>
        <h4>Starting Weight:
            <asp:Label runat="server" ID="lblStartingWeightPlaceholder" />
            lbs</h4>
        <div class="table-responsive">
            <asp:GridView ID="userWeights" runat="server" CssClass="table table-bordered table-striped">
            </asp:GridView>
        </div>
    </div>
    <div class="row">
        <div class="col-md-8">
            <section id="WeightForm">
                <div class="form-horizontal">
                    <hr />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="TxtWeight" CssClass="col-md-2 control-label">Weight</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="TxtWeight" CssClass="form-control" TextMode="SingleLine" onchange="validateDecimal(this);" />
                            <asp:Label runat="server">lbs</asp:Label>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtWeight"
                                CssClass="text-danger" ErrorMessage="Please enter your weight" />
                        </div>
                        <script type="text/javascript">
                            function validateDecimal(input) {
                                var value = input.value;

                                // Regular expression to match a valid positive decimal number less than 300.00
                                var regex = /^(?!300(\.00)?$)([0-2]?\d{0,2}(\.\d{1,2})?)?$/;

                                if (!regex.test(value)) {
                                    var errorMessage = `${value} is not a valid weight. Weights must not exceed 300.00. Here's an example of a valid format 150.56`;

                                    // Set the error message in the data-content attribute
                                    $(input).attr("data-content", errorMessage);

                                    // Initialize the popover
                                    $(input).popover({
                                        trigger: "manual", // Manually trigger the popover
                                    });

                                    // Show the popover
                                    $(input).popover("show");
                                    // Disable the submit button
                                    disableSubmitButton();

                                    // Clear the input
                                    input.value = "";

                                    // Focus on the input for the user to correct the value
                                    $(input).focus();
                                } else {
                                    // If a valid value is entered, hide the popover
                                    $(input).popover("hide");
                                    enableSubmitButton();

                                }

                                function disableSubmitButton() {
                                    // Disable the submit button
                                    $("#<%= btnSubmit.ClientID %>").prop("disabled", true);
                                }

                                function enableSubmitButton() {
                                    // Enable the submit button
                                    $("#<%= btnSubmit.ClientID %>").prop("disabled", false);
                                }
                            }
                        </script>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button id="btnSubmit" runat="server" OnClick="SaveWeight" Text="Submit" CssClass="btn btn-default" />
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
