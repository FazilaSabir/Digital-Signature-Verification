<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="VerifySubscription.aspx.cs"
    Inherits="EService.VerifySubscription" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="main_ind">
        <tr runat="server" id="rowVerificationResult">
            <td style="height: 36px">
                <asp:Label ID="lblUserInfo" runat="server" Text="Accessing this e-service requires a subcription fee to be paid at the e-service subscription office located at,"
                    Width="494px" Font-Size="Small"></asp:Label>
            </td>
        </tr>
        <tr runat="server" id="rowAddress">
            <td style="height: 17px">
                IT Division,<br />
                3rd Floor,<br />
                Caraval House,<br />
                Victoria.<br />
                Tel: 286640
            </td>
        </tr>
        <tr runat="server" id="rowSubsDetailsLabel">
            <td style="height: 30px">
                <asp:Label ID="Label1" runat="server" Text="Your online user subscription details are as follows"></asp:Label>
            </td>
        </tr>
        <tr runat="server" id="rowSubsStatus">
            <td style="height: 21px">
                Subscription Stauts&nbsp; :
                <asp:Label ID="lblSubsStatus" runat="server" Text="Pending"></asp:Label>
            </td>
        </tr>
        <tr runat="server" id="rowSubsDate">
            <td style="height: 30px">
                Subscribed Date &nbsp;&nbsp;&nbsp;&nbsp; :
                <asp:Label ID="lblSubsDate" runat="server" Text="12th Feb 2009"></asp:Label>
            </td>
        </tr>
        <tr runat="server" id="rowExpiryDate">
            <td style="height: 30px">
                Expiry Date &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;:
                <asp:Label ID="lblExpiryDate" runat="server" Text="12th dec 2009"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
