<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KMTAdmin_SCRequestUserControl.ascx.cs"
    Inherits="RSG.Inside.KMTAdmin.KMTAdmin_SCRequest.KMTAdmin_SCRequestUserControl" %>
<SharePoint:CssRegistration ID="CssRegistration2" runat="server" Name="/_layouts/RSG.Inside.KMTAdmin/JS/JQuery1.9.1/jquery-ui.css">
</SharePoint:CssRegistration>
<script type="text/javascript" src="/_layouts/RSG.Inside.KMTAdmin/JS/JQuery1.9.1/jquery-1.9.1.js"></script>
<script type="text/javascript" src="/_layouts/RSG.Inside.KMTAdmin/JS/JQuery1.9.1/jquery-ui.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("#<%= txtLawsonDivisionNo.ClientID %>").keyup(function () {
            var sRootURL = $("#<%= hdnSiteRoot.ClientID %>").val();
            var fURL = sRootURL + $(this).val();
            $("#<%= txtSiteURL.ClientID %>").val(fURL);
        });
    });
</script>
<asp:Panel Width="100%" ID="pnlRequest" runat="server">
    <table width="1000px">
        <tr>
            <td width="1%">
                &nbsp;
            </td>
            <td width="98%">
                <h1>
                    <asp:Label ID="lblWebPartHeading" Text="" runat="server"></asp:Label></h1>
            </td>
            <td width="1%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td width="49%">
                            <table width="100%">
                                <tr>
                                    <td width="30%">
                                        <asp:Label ID="lblSiteName" Text="Site Name : " runat="server"></asp:Label>
                                    </td>
                                    <td width="50%">
                                        <asp:TextBox ID="txtSiteName" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                    <td width="20%">
                                        <asp:RequiredFieldValidator ID="rfvSiteName" runat="server" ErrorMessage="*Required"
                                            ControlToValidate="txtSiteName" ValidationGroup="Grp1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSiteDescription" Text="Site Description :" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSiteDescription" runat="server" Height="50px" TextMode="MultiLine"
                                            Width="250px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblLawsonDivisionNumber" Text="LawsonDivision Number :" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLawsonDivisionNo" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvLawsonDivisionNo" runat="server" ErrorMessage="*Required"
                                            ControlToValidate="txtLawsonDivisionNo" ValidationGroup="Grp1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSiteURL" Text="Site URL :" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSiteURL" runat="server" Enabled="False" Width="250px"></asp:TextBox>
                                        <asp:HiddenField ID="hdnSiteRoot" Value="" runat="server" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="2%">
                            &nbsp;
                        </td>
                        <td width="49%">
                            <table width="100%">
                                <tr>
                                    <td width="30%">
                                        <asp:Label ID="lblZipCode" Text="Zip Code :" runat="server"></asp:Label>
                                    </td>
                                    <td width="50%">
                                        <asp:TextBox ID="txtZipCode" Enabled="false" runat="server" Height="75px" TextMode="MultiLine"
                                            Width="250px"></asp:TextBox><br />
                                        <asp:LinkButton ID="lnkbtnGetZipCodes" runat="server" OnClick="lnkbtnGetZipCodes_Click">Get Zipcodes</asp:LinkButton>
                                    </td>
                                    <td width="20%">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPrimarySiteAdmin" Text="Primary Site Admin :" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPrimaryOwner" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvPrimaryOwner" runat="server" ErrorMessage="*Required"
                                            ControlToValidate="txtPrimaryOwner" ValidationGroup="Grp1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSecondarySiteAdmin" Text="Secondary Site Admin :" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSecondayOwner" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvSecondayOwner" runat="server" ErrorMessage="*Required"
                                            ControlToValidate="txtSecondayOwner" ValidationGroup="Grp1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblError" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnSubmitRequest" runat="server" Text="Submit" OnClick="btnSubmitRequest_Click"
                                ValidationGroup="Grp1" />
                            <asp:Button ID="btnCreateSiteCol" runat="server" Text="Create Site Collection" Visible="false"
                                OnClick="btnCreateSiteCol_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div runat="server" id="showProgress" style="display: none">
                                <table>
                                    <tr>
                                        <td>
                                            Submitting request...
                                        </td>
                                        <td>
                                            <img alt="wait" src="/_layouts/RSG.Inside.KMTAdmin/Images/wait.gif" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Label runat="server" ID="lblNoResults" ForeColor="Red" Text="No results found...!"
    Visible="false"></asp:Label>