<%@ Page Title="Weight Loss Contest Information" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Flab2Fab.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <style>
            /* Exciting Heading Style */
            .exciting-heading {
                color: #120b40; /* Change to your desired heading color */
                font-size: 24px; /* Adjust the font size as needed */
            }

            /* Entry Fee Style */
            .entry-fee {
                color: #095222; /* Change to your desired color for the entry fee */
                font-weight: bold;
            }

            /* Venmo Style */
            .venmo {
                color: #1E90FF; /* Change to your desired Venmo link color */
            }

            /* Note Style */
            .note {
                font-style: italic;
                color: #888; /* Change to your desired color for notes */
            }

            /* CTA Buttons Style */
            .cta-buttons {
                text-align: center;
                margin-top: 20px; /* Add margin as needed */
            }

            /* Contest Rules Style */
            .contest-rules {
                font-weight: bold;
                color: #27AE60; /* Change to your desired color for contest rules */
            }

            /* End Date Style */
            .end-date {
                font-style: italic;
                color: #888; /* Change to your desired color for the end date */
            }

            /* Earnings List Style */
            .earnings-list {
                list-style-type: square; /* Use a different list style if desired */
                margin-left: 20px; /* Add margin as needed */
                color: #FF5733; /* Change to your desired color for earnings list items */
            }

            /* Earnings Style */
            .earnings {
                font-weight: bold;
                color: #27AE60; /* Change to your desired color for earnings */
            }

            /* Max Earnings Style */
            .max-earnings {
                color: #FF5733; /* Change to your desired color for max earnings */
            }

            /* Winner Takes All Style */
            .winner-takes-all {
                font-style: italic;
                color: #1E90FF; /* Change to your desired color for winner takes all */
            }
        </style>

        <main>
            <section>
                <h2 class="exciting-heading">Entering the Contest</h2>
                <div class="entry-info">
                    <p class="payment-info">Entry Fee: <span class="entry-fee">$100</span></p>
                    <p>Payment should be sent to Apy Zubaidha on Venmo (<span class="venmo">@Zubaidha-R</span>).</p>
                </div>
                <p>
                    Ready to join the challenge? &nbsp;&nbsp;
                    <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register as a new user</asp:HyperLink>
                </p>
                <p class="weight-note">Please provide at least one additional weight on the <a href="TrackWeight.aspx">TrackWeight</a> page to appear on the leaderboard.</p>
                <p class="note">Note: We kicked off on 08/30/2023. Make sure to weigh in and show your commitment!</p>
                <p class="end-date">End of competition date: TBD</p>

            </section>

            <section>
                <h2 class="exciting-heading">Contest Rules & Earnings</h2>
                <p>The rules are simple:</p>
                <ul>
                    <li>Lose 0.5% = <span class="earnings">$5.00</span></li>
                    <li>Lose 2.5% = <span class="earnings">$25.00</span></li>
                    <li>Lose 5% = <span class="earnings">$50.00</span></li>
                    <li>Lose 10% = <span class="earnings">$100.00</span></li>
                </ul>
                <p>Maximum earnings: <span class="earnings">$100.00</span></p>
                <p class="winner-takes-all"><b>HOWEVER:</b> At the end of the competition, the person with the highest percentage of weight lost is the overall winner and takes home the remaining pot money &#x1F4B0;!</p>
            </section>
        </main>
    </div>
</asp:Content>

