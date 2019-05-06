<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopDocumentsFromDocumentLibraryUserControl.ascx.cs"
    Inherits="RSG.Inside.KMTTemplateWebParts.TopDocumentsFromDocumentLibrary.TopDocumentsFromDocumentLibraryUserControl" %>
<table style="width: 300px; height: 250px; vertical-align: top;">
    <tr>
        <td valign="top">
            <asp:Label ID="lblNoDocumentsExists" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            <asp:Repeater ID="rptLinks" runat="server">
                <HeaderTemplate>
                    <table style="border: 0px solid #df5015; vertical-align: top;" cellpadding="0">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr valign="top" style="height: 0px;">
                        <td>
                            >>&nbsp;
                        </td>
                        <td>
                            <asp:HyperLink ID="HyperLink1" NavigateUrl='<%#Eval("FileURL") %>' runat="server">                
                <%#Eval("FileName")%>
                            </asp:HyperLink>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </td>
    </tr>
</table>
<%--<asp:GridView ID="Gv1" runat="server">
</asp:GridView>--%>
