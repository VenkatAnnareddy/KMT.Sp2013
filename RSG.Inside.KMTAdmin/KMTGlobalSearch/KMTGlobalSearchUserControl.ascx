<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KMTGlobalSearchUserControl.ascx.cs"
    Inherits="RSG.Inside.KMTAdmin.KMTGlobalSearch.KMTGlobalSearchUserControl" %>
<table width="100%">
    <tr>
        <td align="center" valign="middle">
            <asp:RadioButtonList ID="optSearchOn" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Lawson Division Number" Value="1" Selected="True">By LawsonDivision Number</asp:ListItem>
                <asp:ListItem Text="Zip Code" Value="2">By ZipCode</asp:ListItem>
            </asp:RadioButtonList>
            <br />
            <asp:TextBox ID="txtSearchText" Text="" Width="350px" runat="server"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" OnClick="btnSearch_Click"
                ValidationGroup="Grp1" />
            <br />
            <asp:RequiredFieldValidator ID="rfvtxtSearchText" runat="server" ErrorMessage="* Search string required"
                ControlToValidate="txtSearchText" ValidationGroup="Grp1"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top">
            <asp:Repeater ID="rptrSites" runat="server">
                <HeaderTemplate>
                    <table style="border: 0px solid #df5015; width: 500px" cellpadding="0">
                        <tr>
                            <td width="5%">
                                &nbsp;
                            </td>
                            <td width="95%">
                                <h3>
                                    Search Results :</h3>
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Site Name :<asp:Label ID="lblSelSiteName" runat="server" Text='<%#Eval("SiteName") %>'
                                Font-Bold="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Link :
                            <asp:HyperLink ID="HyperLink1" Target="_blank" NavigateUrl='<%#Eval("LandingPageURL") %>'
                                runat="server"><%#Eval("SiteURL") %></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <hr />
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
<asp:Label runat="server" ID="lblNoResults" ForeColor="Red" Text="No results found...!"
    Visible="false"></asp:Label>
<%--<asp:GridView ID="gv1" runat="server">
</asp:GridView>
<asp:GridView ID="gv2" runat="server">
</asp:GridView>
<asp:GridView ID="gv3" runat="server">
</asp:GridView>--%>
