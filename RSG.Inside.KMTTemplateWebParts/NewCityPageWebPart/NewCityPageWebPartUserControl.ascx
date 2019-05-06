<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewCityPageWebPartUserControl.ascx.cs"
    Inherits="RSG.Inside.KMTTemplateWebParts.NewCityPageWebPart.NewCityPageWebPartUserControl" %>
<table>
    <tr>
        <td>
            Select State :<br />
            <asp:DropDownList ID="ddlState" runat="server" Width="200px">
            </asp:DropDownList>
        </td>        
    </tr>
    <tr>
        <td>
            City Page Name :<br />
            <asp:TextBox ID="txtCityPageName" runat="server" Text="" Width="200px"></asp:TextBox><br />
            Note: Enter multiple filesnames with ; seperated
        </td>
    </tr>
    <tr>
        <td>
            Document Library :<br />
            <asp:DropDownList ID="ddlDocLibrary" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnCreatePage" runat="server" Text="Create Page" OnClick="btnCreatePage_Click" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr><td>
    <asp:Label ID="lblError" runat="server"></asp:Label>
    </td></tr>
</table>
