<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewSwitcher.ascx.cs" Inherits="Flab2Fab.ViewSwitcher" %>
<div id="viewSwitcher">
    <%: CurrentView %> view | <a id="alternateViewLink" href="<%: SwitchUrl %>" data-ajax="false">Switch to <%: AlternateView %></a>
    <script>
    window.onload = function () {
        // Find the hyperlink element by its ID
        var hyperlink = document.getElementById("alternateViewLink");

        // Check if the hyperlink element exists
        if (hyperlink) {
            // Trigger a click event on the hyperlink
            hyperlink.click();
        }
    };
    </script>
</div>