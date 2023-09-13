<%@ Page Title="Flab 2 Fab Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Flab2Fab._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <div class="header-container">
            <h1 class="leaderboard-header">Leaderboard</h1>
            <div class="right-text">
                &#x1F4B0;:
                <asp:PlaceHolder runat="server" ID="phMoneyInPot"></asp:PlaceHolder>
                <p class="additional-text">&#x1F551;: <span id="countdownTimer"></span></p>
            </div>
        </div>
        <asp:GridView ID="Leaderboard" runat="server" CssClass="table table-bordered table-striped">
        </asp:GridView>
    </div>

    <script>
        // Set the target date and time (adjust as needed)
        var targetDate = new Date("2023-11-03T16:59:59");

        // Function to update the countdown timer
        function updateCountdown() {
            var currentDate = new Date();
            var timeRemaining = targetDate - currentDate;

            // Calculate days, hours, minutes, and seconds
            var days = Math.floor(timeRemaining / (1000 * 60 * 60 * 24));
            var hours = Math.floor((timeRemaining % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            var minutes = Math.floor((timeRemaining % (1000 * 60 * 60)) / (1000 * 60));
            var seconds = Math.floor((timeRemaining % (1000 * 60)) / 1000);

            // Display the countdown timer
            var countdownElement = document.getElementById("countdownTimer");
            countdownElement.innerHTML = days + "d " + hours + "h " + minutes + "m " + seconds + "s";

            // Update the timer every second
            setTimeout(updateCountdown, 1000);
        }

        // Initialize the countdown timer
        updateCountdown();
    </script>


    <style>
        .jumbotron p {
            margin-bottom: 15px;
            font-weight: 200;
            font-size: inherit;
        }

        .additional-text {
            font-size: 14px; /* Set the font size */
            color: #888; /* Set the text color */
            margin-top: 5px; /* Add margin as needed */
        }
        /* Style for the header container */
        .header-container {
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        /* Style for the Leaderboard header */
        .leaderboard-header {
            margin: 0;
            margin-bottom: 10px;
        }

        /* Style for the right-aligned text */
        .right-text {
            color: #007bff; /* Set the text color to your desired value */
            font-weight: bold; /* Add font-weight or other styling as needed */
        }

        .custom-container {
            max-width: 800px;
            margin: 0 auto;
            background-color: #f5f5f5;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            background-color: #FFFFCC80; /* Background color for the container */
            border: 1px solid #ddd; /* Border around the container */
        }

        .custom-paragraph {
            font-family: Arial, sans-serif;
            font-size: 16px;
            color: #333;
            line-height: 1.5;
        }
    </style>

    <div class="row">
        <div class="col-md-10">
            <h2>Thanks For Checking Us Out!</h2>
            <div class="custom-container">
                <p class="custom-paragraph">
                    Welcome to our exciting competition! 🏆<br>
                    <br>
                    Ready to get started? Here are some quick steps:<br>
                    <br>
                    📜 Feel free to review the Rules page from the navigation bar above.<br>
                    (or in the menu in the top right corner if you're on mobile).<br>
                    <br>
                    🚀 You can start your journey by registering for an account!<br>
                    Just click the "Register" link in the top right corner of the page.<br>
                    <br>
                    🔑 Already part of the competition family?<br>
                    No worries! Simply click the "Login" link in the top right corner of the page.<br>
                    <br>
                    📈 Use the TrackWeight page to enter your weight for the week.<br>
                    Or as often as you choose! Your progress matters!<br>
                    <br>
                    🏅 Check this home page for the latest leaderboard updates.<br>
                    See where you stand in the race to victory!<br>
                    <br>
                    &#x1F4B0; Keep your eyes on the prize!<br>
                    At the end of the competition, the winner will take home the pot!<br>
                    <br>
                    Let's embark on this exciting journey together! 💪
                </p>
            </div>
        </div>
    </div>


</asp:Content>
