using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Administration;
using System.Linq;
using RSG.Inside.KMTAdmin.ServiceLocationRef;

namespace RSG.Inside.KMTAdmin.KMTAdmin_SCRequest
{
    public partial class KMTAdmin_SCRequestUserControl : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public KMTAdmin_SCRequest WebPart { get; set; }

        /// <summary>
        /// Page load event for Request Service class
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Event Args</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //LoadFormFieldValues();
                hdnSiteRoot.Value = GetListValue("KMTConfig", "Key", "RootURL", "Value", false);
                txtSiteURL.Text = hdnSiteRoot.Value;

                #region Label text from Web Part Properties

                this.WebPart = this.Parent as KMTAdmin_SCRequest;

                lblWebPartHeading.Text = WebPart.WebPartTitle.ToString();
                lblSiteName.Text = WebPart.SiteNameLabel;
                lblSiteDescription.Text = WebPart.SiteDescriptionLabel;
                lblLawsonDivisionNumber.Text = WebPart.LawsonDivisionNumberLabel;
                lblSiteURL.Text = WebPart.SiteURLLabel;
                lblZipCode.Text = WebPart.ZipCodeLabel;
                lblPrimarySiteAdmin.Text = WebPart.PrimarySiteAdminLabel;
                lblSecondarySiteAdmin.Text = WebPart.SecondarySiteAdminLabel;
                btnSubmitRequest.Text = WebPart.SubmitButtonLabel;

                #endregion

                try
                {
                    string requestersGroup = GetListValue("KMTConfig", "Key", "RequestersGroup", "Value", false);
                    if (CurrentUserExistsInGroup(requestersGroup))
                    {
                        pnlRequest.Enabled = true;
                        lblNoResults.Text = "";
                        lblNoResults.Visible = false;
                    }
                    else
                    {
                        pnlRequest.Enabled = false;
                        lblNoResults.Text = "Insufficient permissions to submit request...!";
                        lblNoResults.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblNoResults.Text = string.Format("Error : {0} - StackTracke : {1}", ex.Message, ex.StackTrace);
                    lblNoResults.Visible = true;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmitRequest_Click(object sender, EventArgs e)
        {
            //btnSubmit.Enabled = false;
            try
            {
                SPWeb mySite = SPContext.Current.Web;
                SPListItemCollection listItems = mySite.Lists["KMTDivisionalSites"].Items;
                SPListItem item = listItems.Add();

                item["SiteName"] = txtSiteName.Text;
                item["SiteDescription"] = txtSiteDescription.Text;
                item["SiteURL"] = hdnSiteRoot.Value + txtLawsonDivisionNo.Text.ToString();
                item["LawsonDivisionNo"] = txtLawsonDivisionNo.Text.ToString();
                item["ZipCode"] = txtZipCode.Text.ToString();
                item["PrimaryOwner"] = txtPrimaryOwner.Text.ToString();
                item["SecondayOwner"] = txtSecondayOwner.Text.ToString();
                item["Approver"] = GetListValue("KMTConfig", "Key", "Approver", "Value", false);

                item.Update();
            }
            catch (Exception ex)
            {
                lblError.Text = "Error : " + ex.Message.ToString() + "  Trace : " + ex.Source.ToString();
                //LogException(ex);
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        protected void btnCreateSiteCol_Click(object sender, EventArgs e)
        {
            //    try
            //    {
            //        string parentSiteURL = GetListValue("KMTConfig", "Key", "RootSiteCollectionURL", "Value", false);
            //        string sTemplateName = GetListValue("KMTConfig", "Key", "TemplateName", "Value", false);

            //        string siteURLRequested = "sites/" + txtLawsonDivisionNo.Text.ToString();
            //        //bool isSiteCreated = CreateSite(
            //        //    parentSiteURL,
            //        //    siteURLRequested,
            //        //    txtSiteName.Text.ToString(),
            //        //    sTemplateName);

            //        CreateSiteCollection(parentSiteURL, txtLawsonDivisionNo.Text.ToString(), txtSiteName.Text,
            //            "", 1033, sTemplateName, "repsrv\\udhatsu", "Suresh Kumar", "udhatsu@republicservices.com");

            //        lblError.Text = "Site Collection sucessfully created...!";
            //    }
            //    catch (Exception ex)
            //    {
            //        lblError.Text = "Error : " + ex.Message.ToString() + "  Trace : " + ex.Source.ToString();
            //    }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="parentSiteURL"></param>
        ///// <param name="siteURLRequested"></param>
        ///// <param name="siteTitle"></param>
        ///// <param name="siteTemplateName"></param>
        ///// <returns></returns>
        //public bool CreateSite(
        //   string parentSiteURL,
        //   string siteURLRequested,
        //   string siteTitle,
        //   string siteTemplateName)
        //{
        //    bool returnCondition = false; // Assume failure.

        //    const Int32 LOCALE_ID_ENGLISH = 1033;

        //    using (SPSite siteCollection = new SPSite(parentSiteURL))
        //    {
        //        SPWeb parentWeb = siteCollection.OpenWeb();
        //        SPWebTemplateCollection Templates = siteCollection.GetWebTemplates(Convert.ToUInt32(LOCALE_ID_ENGLISH));
        //        SPWebTemplate siteTemplate = Templates[siteTemplateName];
        //        if (parentWeb.Webs[siteURLRequested].Exists)
        //        {
        //            parentWeb.Webs.Delete(siteURLRequested);
        //        }

        //        parentWeb.Webs.Add(
        //            siteURLRequested,
        //            siteTitle,
        //            "",
        //            Convert.ToUInt32(LOCALE_ID_ENGLISH),
        //            siteTemplate,
        //            false, false);

        //        // All is good? 
        //        returnCondition = true;
        //    }

        //    return returnCondition;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listName"></param>
        /// <param name="colName"></param>
        /// <param name="colValue"></param>
        /// <param name="retunColumn"></param>
        /// <param name="inHTML"></param>
        /// <returns></returns>
        private string GetListValue(string listName, string colName, string colValue, string retunColumn, bool inHTML)
        {
            string colResult = string.Empty;

            try
            {
                SPWeb webSite = SPContext.Current.Web;
                SPList list = webSite.Lists[listName];
                SPQuery query = new SPQuery();

                query.Query = string.Format(
       "<Where><Eq><FieldRef Name=\"{0}\"/>" +
       "<Value Type=\"Text\">{1}</Value></Eq></Where>", colName, colValue);

                SPListItemCollection myItems = list.GetItems(query);
                foreach (SPListItem item in myItems)
                {
                    if (inHTML)
                        colResult = item[retunColumn].ToString();
                    else
                        colResult = SPHttpUtility.ConvertSimpleHtmlToText(item[retunColumn].ToString(), -1);
                }
            }
            catch (Exception ex)
            {
                colResult = "Error : " + ex.Message.ToString() + "  Trace : " + ex.Source.ToString();
                //LogException(ex);
            }
            return colResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnGetZipCodes_Click(object sender, EventArgs e)
        {
            txtZipCode.Text = GetZipCodesForLawsonDivisionNumber(txtLawsonDivisionNo.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lawsonDivisionNumber"></param>
        /// <returns></returns>
        private string GetZipCodesForLawsonDivisionNumber(string lawsonDivisionNumber)
        {
            RSG_ZipCodes zCodes = new RSG_ZipCodes();
            string zipCodes = "";
            ServiceLocationClient wcLocation = new ServiceLocationClient();
            try
            {
                if (wcLocation != null)
                {
                    zCodes = wcLocation.GetZipCodesByLawsonDivisionNumbers(lawsonDivisionNumber);
                    if (zCodes != null)
                    {
                        if (zCodes.RSG_ZipCode.Count() > 0)
                        {
                            foreach (RSG_ZipCode cd in zCodes.RSG_ZipCode)
                            {
                                if (zipCodes == "")
                                    zipCodes = cd.ZipCode;
                                else
                                    zipCodes += "," + cd.ZipCode;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingService.LogErrorInULS(ex);
            }
            finally
            {
                if (wcLocation.State == System.ServiceModel.CommunicationState.Faulted)
                    wcLocation.Abort();
                else
                    wcLocation.Close();
            }

            return zipCodes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grpName"></param>
        /// <returns></returns>
        private bool CurrentUserExistsInGroup(string grpName)
        {
            return IsUserAuthorized(grpName);
            //bool bFlag = false;

            //SPSecurity.RunWithElevatedPrivileges(delegate()
            //{
            //    //try
            //    //{
            //    //    SPWeb cWeb = SPContext.Current.Web;
            //    //    bFlag = cWeb.IsCurrentUserMemberOfGroup(cWeb.Groups[grpName].ID);
            //    //}
            //    //catch (Exception)
            //    //{
            //    //    //do nothing
            //    //}


            //});

            //return bFlag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public bool IsUserAuthorized(string groupName)
        {
            bool isFlag = false;

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                try
                {
                    //Retreiving the current context    
                    SPSite site = SPContext.Current.Site;
                    //Opening a current web and creating a instance         
                    using (SPWeb web = site.OpenWeb())
                    {
                        //Retreiving the current logged in user  
                        SPUser currentUser = web.CurrentUser;
                        //Retrieving all the user groups in the site/web       
                        SPGroupCollection userGroups = currentUser.Groups;
                        //Loops through the grops and check if the user is part of given group or not.   
                        foreach (SPGroup group in userGroups)
                        {
                            //Checking the group     
                            if (group.Name.Contains(groupName))
                                isFlag = true;
                        }
                    }
                }
                catch (Exception)
                {
                    //do nothing
                }
            });
            return isFlag;
        }
    }
}
