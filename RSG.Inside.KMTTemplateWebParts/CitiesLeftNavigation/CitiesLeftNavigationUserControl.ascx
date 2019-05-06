<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CitiesLeftNavigationUserControl.ascx.cs"
    Inherits="RSG.Inside.KMTTemplateWebParts.CitiesLeftNavigation.CitiesLeftNavigationUserControl" %>

<link rel="stylesheet" href="../../../../_layouts/15/RSG.Inside.KMTTemplateWebParts/JS/jquery-ui.css">
<script src="../../../../_layouts/15/RSG.Inside.KMTTemplateWebParts/JS/jquery-1.10.2.js"></script>
<script src="../../../../_layouts/15/RSG.Inside.KMTTemplateWebParts/JS/jquery-ui.js"></script>

<script type="text/javascript">

    function GetQueryStringParams(sParam) {

        var sPageURL = window.location.search.substring(1);

        var sURLVariables = sPageURL.split('&');

        for (var i = 0; i < sURLVariables.length; i++) {

            var sParameterName = sURLVariables[i].split('=');

            if (sParameterName[0] == sParam) {
                return sParameterName[1];
            }
        }
    }

    $(function () {

        $("#accordion").accordion({ collapsible: true, active: false });
        var index = 0;
        if (GetQueryStringParams('sindex').length > 0) {
            index = parseInt(GetQueryStringParams('sindex'));
        }
        else {
            index = 0;
        }

        $("#accordion").accordion("option", "active", index);
    });

</script>


<style type="text/css">
    .ui-accordion-content-active {
        height: auto !important;
    }
</style>
<%--<asp:Repeater ID="rptLinks" runat="server">
    <HeaderTemplate>
        <table style="border: 0px solid #df5015; width: 200px" cellpadding="0">
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td>
                <asp:HyperLink ID="HyperLink1" NavigateUrl='<%#Eval("NavigationLink") %>' runat="server">
                <div style="font-size:large; font-weight:bold;">
                <%#Eval("EntityName")%></div>
                </asp:HyperLink>
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>--%>
<asp:Label ID="lblCitiesNavigation" runat="server" Text=""></asp:Label>
<asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
