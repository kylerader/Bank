<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <% using (Html.BeginForm("Create","BankAccount"))
  {%>
    <h2>Welcome to Initech!  Please open an account.</h2>
    <h3>Please enter the details for the account you wish to open</h3>
    <table>
    <tr><td>First Name</td><td><input name="firstName" id="firstName" /></td></tr>
    <tr><td>Last Name</td><td><input name="lastName" id="lastName" /></td></tr>
    <tr><td>FICO Score</td><td><input name="ficoScore" id="ficoScore" /></td></tr>
    </table>
    <input type="submit" value="Create Account"/>
  <%
  }%> 
</asp:Content>
