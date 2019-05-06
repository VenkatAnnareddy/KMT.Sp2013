<%-- _lcid="1033" _version="14.0.4762" _dal="1" --%>
<%-- _LocalBinding --%>

<%@ Page Language="C#" MasterPageFile="~masterurl/default.master" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage,Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <SharePoint:ListItemProperty Property="BaseName" MaxLength="40" runat="server" />
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    <WebPartPages:WebPartZone runat="server" Title="loc:TitleBar" ID="TitleBar" AllowLayoutChange="false"
        AllowPersonalization="false" />
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderTitleAreaClass" runat="server">
    <style type="text/css">
        Div.ms-titleareaframe
        {
            height: 100%;
        }
        .ms-pagetitleareaframe table
        {
            background: none;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <meta name="GENERATOR" content="Microsoft SharePoint" />
    <meta name="ProgId" content="SharePoint.WebPartPage.Document" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="CollaborationServer" content="SharePoint Team Web Site" />
    <script type="text/javascript">
// <![CDATA[
        var navBarHelpOverrideKey = "WSSEndUser";
// ]]>
    </script>
    <SharePoint:UIVersionedContent ID="WebPartPageHideQLStyles" UIVersion="4" runat="server">
        <contenttemplate>
<style type="text/css">
    body #s4-leftpanel
    {
        display: none;
    }
    .s4-ca
    {
        margin-left: 0px;
    }
</style>
		</contenttemplate>
    </SharePoint:UIVersionedContent>
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderSearchArea" runat="server">
    <SharePoint:DelegateControl runat="server" ControlId="SmallSearchInputBox" />
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderLeftActions" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <SharePoint:ProjectProperty Property="Description" runat="server" />
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderBodyRightMargin" runat="server">
    <div height="100%" class="ms-pagemargin">
        <img src="/_layouts/images/blank.gif" width="10" height="1" alt="" /></div>
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageImage" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderNavSpacer" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderLeftNavBar" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table cellpadding="4" cellspacing="0" border="0" width="100%">
        <tr>
            <td id="_invisibleIfEmpty" name="_invisibleIfEmpty" colspan="3" valign="top" width="100%">
                <WebPartPages:WebPartZone runat="server" Title="loc:Header" ID="Header" FrameType="TitleBarOnly" />
            </td>
        </tr>
        <tr>
            <td id="_invisibleIfEmpty" name="_invisibleIfEmpty" valign="top" height="100%">
                <WebPartPages:WebPartZone runat="server" Title="loc:LeftColumn" ID="LeftColumn" FrameType="TitleBarOnly" />
            </td>
            <td id="_invisibleIfEmpty" name="_invisibleIfEmpty" valign="top" height="100%">
                <table cellpadding="4" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td colspan="2" id="Td1" name="_invisibleIfEmpty" valign="top" height="100%">
                            <WebPartPages:WebPartZone runat="server" Title="loc:MiddleColumn" ID="MiddleColumn"
                                FrameType="TitleBarOnly" />
                        </td>
                    </tr>
                    <tr>
                        <td id="_invisibleIfEmpty" name="_invisibleIfEmpty" valign="top" height="100%">
                            <WebPartPages:WebPartZone runat="server" Title="loc:Middle1Column" ID="Middle1Column"
                                FrameType="TitleBarOnly" />
                        </td>
                        <td id="_invisibleIfEmpty" name="_invisibleIfEmpty" valign="top" height="100%">
                            <WebPartPages:WebPartZone runat="server" Title="loc:Middle2Column" ID="Middle2Column"
                                FrameType="TitleBarOnly" />
                        </td>
                    </tr>                    
                </table>
            </td>
            <td id="_invisibleIfEmpty" name="_invisibleIfEmpty" valign="top" height="100%">
                <WebPartPages:WebPartZone runat="server" Title="loc:RightColumn" ID="RightColumn"
                    FrameType="TitleBarOnly" />
            </td>
        </tr>
        <tr>
            <td id="_invisibleIfEmpty" name="_invisibleIfEmpty" colspan="3" valign="top" width="100%">
                <WebPartPages:WebPartZone runat="server" Title="loc:Footer" ID="Footer" FrameType="TitleBarOnly" />
            </td>
        </tr>
        <script type="text/javascript" language="javascript">            if (typeof (MSOLayout_MakeInvisibleIfEmpty) == "function") { MSOLayout_MakeInvisibleIfEmpty(); }</script>
    </table>
</asp:Content>
