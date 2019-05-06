using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Net;
using Microsoft.SharePoint.Administration;
using System.Collections.Specialized;

namespace RSG.Inside.KMTAdmin.CreateNewKMTDivisionalSite
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class CreateNewKMTDivisionalSite : SPItemEventReceiver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="webSite"></param>
        /// <param name="listName"></param>
        /// <param name="colName"></param>
        /// <param name="colValue"></param>
        /// <param name="retunColumn"></param>
        /// <param name="inHTML"></param>
        /// <returns></returns>
        private string GetListValue(SPWeb webSite, string listName, string colName, string colValue, string retunColumn, bool inHTML)
        {
            string colResult = string.Empty;

            try
            {
                //SPWeb webSite = SPContext.Current.Web;
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
        /// An item was updated.
        /// </summary>
        public override void ItemUpdated(SPItemEventProperties properties)
        {
            base.ItemUpdated(properties);

            //string listName = "KMTDivisionalSites";
            string emailSubject = "", emailBody = "";
            string listName = GetListValue(properties.Web, "KMTConfig", "Key", "KMTDivisionalSitesListName", "Value", false);
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                if (properties.ListTitle == listName)
                {
                    //get the item updated
                    SPListItem item = properties.ListItem;
                    if (item["ApprovalStatus"].ToString().Trim().ToUpper() == "APPROVED")
                    {                        
                        string parentSiteURL = GetListValue(properties.Web, "KMTConfig", "Key", "RootSiteCollectionURL", "Value", false);
                        string sTemplateName = GetListValue(properties.Web, "KMTConfig", "Key", "TemplateName", "Value", false);

                        string siteURLRequested = "sites/" + item["LawsonDivisionNo"].ToString();
                        //bool isSiteCreated = CreateSiteCollection(parentSiteURL, item["LawsonDivisionNo"].ToString(), item["SiteName"].ToString(),
                        //    item["SiteDescription"].ToString(), 1033, sTemplateName, "repsrv\\udhatsu", "Suresh Kumar", "udhatsu@republicservices.com");

                        bool isSiteCreated = CreateSiteCollection(parentSiteURL, item["LawsonDivisionNo"].ToString(), item["SiteName"].ToString(),
                           item["SiteDescription"].ToString(), 1033, sTemplateName, item["PrimaryOwner"].ToString(), "", item["PrimaryOwner"].ToString());

                        if (isSiteCreated)
                        {
                            using (DisabledEventsScope scope = new DisabledEventsScope())
                            {
                                SPListItem listItem = properties.ListItem;
                                listItem["SiteCreated"] = "Yes";
                                listItem.Update();

                                emailSubject = string.Format("KMT Site {0} Creation - Sucess", item["LawsonDivisionNo"].ToString());
                                emailBody = "KMT Site Creation - Sucess";
                                SendEmail(emailSubject, emailBody, properties.Web);
                            }
                        }
                    }
                    else if (item["ApprovalStatus"].ToString().Trim().ToUpper() == "REJECTED")
                    {
                        using (DisabledEventsScope scope = new DisabledEventsScope())
                        {
                            SPListItem listItem = properties.ListItem;
                            listItem["SiteCreated"] = "No";
                            listItem.Update();

                            emailSubject = string.Format("KMT Site {0} Creation - Failure", item["LawsonDivisionNo"].ToString());
                            emailBody = "KMT Site Creation - Failure";
                            SendEmail(emailSubject, emailBody, properties.Web);
                        }
                    }
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webApplicationURL"></param>
        /// <param name="siteName"></param>
        /// <param name="siteTitle"></param>
        /// <param name="siteDesc"></param>
        /// <param name="siteLanguage"></param>
        /// <param name="templateName"></param>
        /// <param name="ownerLoginName"></param>
        /// <param name="ownerName"></param>
        /// <param name="ownerEmail"></param>
        /// <returns></returns>
        public bool CreateSiteCollection(
                   String webApplicationURL,
                   string siteName,
                   string siteTitle,
                   string siteDesc,
                   int siteLanguage,
                   string templateName,
                   string ownerLoginName,
                   string ownerName,
                   string ownerEmail
                  )
        {
            bool isSiteCreated = false;
            try
            {
                LoggingService.LogErrorInULS("RSG.Inside.SPSiteCol.ExternalService - CreateSiteCollection - Start");
                LoggingService.LogErrorInULS("SIte Info: " + "\nWebApplication URL: " + webApplicationURL +
                                            "\nSiteName: " + siteName +
                                            "\nSiteTitle: " + siteTitle +
                                            "\nSiteDesc: " + siteDesc +
                                            "\nSiteLanguage: " + siteLanguage +
                                            "\nTemplateName: " + templateName +
                                            "\nownerLoginName: " + ownerLoginName +
                                            "\nownerName: " + ownerName +
                                            "\nownerEmail: " + ownerEmail
                                           );

                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    using (SPSite siteCollectionOuter = new SPSite(webApplicationURL))
                    {
                        SPWebApplication webApp = siteCollectionOuter.WebApplication;
                        LoggingService.LogErrorInULS("SiteCreationActivity :" + webApplicationURL + siteName + templateName + siteLanguage);

                        SPSiteCollection siteCollection = webApp.Sites;
                        UInt32 lang = Convert.ToUInt32(siteLanguage);
                        SPWebTemplateCollection myTemplates = siteCollectionOuter.RootWeb.GetAvailableWebTemplates(lang);
                        SPWebTemplate reqTemplate = myTemplates[templateName];

                        if (reqTemplate != null)
                        {
                            using (SPSite site = siteCollection.Add("sites/" + siteName, siteTitle, siteDesc, lang, reqTemplate.Name, ownerLoginName, ownerName, ownerEmail))
                            {
                                isSiteCreated = true;
                                try
                                {
                                    site.Features.Add(new Guid("F6924D36-2FA8-4f0b-B16D-06B7250180FA"), true);
                                }
                                catch (Exception expub)
                                {
                                    LoggingService.LogErrorInULS("Publishing Feature Activation Issue" + expub.ToString());
                                }
                                try
                                {
                                    site.Features.Add(new Guid("c7c893fd-9cbe-4f40-ad1d-990f69978203"), true);
                                }
                                catch (Exception expub)
                                {
                                    LoggingService.LogErrorInULS("Site Provision Feature Activation Issue" + expub.ToString());
                                }
                            }
                        }
                    }
                });
                LoggingService.LogErrorInULS("RSG.Inside.SPSiteCol.ExternalService - CreateSiteCollection - End");
            }
            catch (Exception ex)
            {
                LoggingService.LogErrorInULS("RSG.Inside.SPSiteCol.ExternalService CreateSiteCollection Issue :" + ex.Message);
            }

            return isSiteCreated;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="webSite"></param>
        /// <returns></returns>
        private string SendEmail(string subject, string body, SPWeb webSite)
        {
            string sendEmail = "Yes";
            try
            {
                string sendEmailStatus = GetListValue(webSite, "KMTConfig", "Key", "SendEmail", "Value", false).ToUpper();
                if (sendEmailStatus.Contains("YES"))
                {
                    try
                    {
                        string toField = GetListValue(webSite, "KMTConfig", "Key", "EmailRecipients", "Value", false);
                       
                        StringDictionary headers = new StringDictionary();
                        headers.Add("to", toField);
                        //headers.Add("cc", "xyz@abc.com");
                        //headers.Add("bcc", "");
                        //headers.Add("from", "email@add.com");
                        headers.Add("subject", subject);
                        headers.Add("content-type", "text/html");
                        string bodyText = body;
                        SPUtility.SendEmail(webSite, headers, bodyText.ToString());

                    }
                    catch (Exception ex1)
                    {
                        sendEmail = "Error : " + ex1.Message.ToString() + "  Trace : " + ex1.Source.ToString();
                        LoggingService.LogErrorInULS(sendEmail);
                    }
                }
            }
            catch (Exception ex)
            {
                sendEmail = "Error : " + ex.Message.ToString() + "  Trace : " + ex.Source.ToString();
                LoggingService.LogErrorInULS(ex);
            }
            return sendEmail;
        }

        //public bool CreateSiteCollection(
        //    SPWeb webSite,
        //    string siteURL,
        //    string siteTitle,
        //    string siteDescription,
        //    string primaryAdmin,
        //    string secondayAdmin,
        //    string LawsonDivisionNo
        //    )
        //{
        //    bool SiteCreated = false;
        //    //try
        //    //{
        //    //String user = Environment.UserDomainName + "\\" + Environment.UserName;
        //    //SPSecurity.RunWithElevatedPrivileges(delegate()
        //    //{
        //    //    devshpt19_AdminSVC.Admin admService = new devshpt19_AdminSVC.Admin();
        //    //    //admService.Credentials = System.Net.CredentialCache.DefaultCredentials;

        //    //    NetworkCredential myCred = new NetworkCredential("_spdev", "s3rv1c3", "repsrv");
        //    //    admService.Credentials = myCred;

        //    //    admService.CreateSite(
        //    //        siteURL,
        //    //        siteTitle,
        //    //        siteDescription,
        //    //        1033,
        //    //        "STS#0",
        //    //        user,
        //    //        "Suresh",
        //    //        "udhatsu@republicservices.com", "", "");
        //    //});

        //    string rootSiteURL = GetListValue(webSite, "KMTConfig", "Key", "RootSiteCollectionURL", "Value", false);
        //    SPSecurity.RunWithElevatedPrivileges(delegate()
        //    {
        //        using (SPSite spsite = new SPSite(rootSiteURL))
        //        {
        //            using (SPWeb mySite = spsite.OpenWeb())
        //            {
        //                SPWebCollection subSites = mySite.Webs;
        //                //string currentTemplate = "STS#0";
        //                string siteUrl = "sites/" + LawsonDivisionNo;
        //                //subSites.Add(siteUrl, siteTitle, siteDescription, 1033,
        //                //   currentTemplate, true, false);
        //                String user = Environment.UserDomainName + "\\" + Environment.UserName;
        //                subSites.Add(siteUrl, siteTitle, siteDescription, 1033, "STS#0", true, false);
        //            }
        //        }
        //    });

        //    SiteCreated = true;
        //    //}
        //    //catch (Exception ex)
        //    //{

        //    //}
        //    return SiteCreated;
        //}

        //public static bool CreateSite(
        //    string parentSiteURL,
        //    string siteURLRequested,
        //    string siteTitle,
        //    string siteTemplateName)
        //{
        //    bool returnCondition = false; // Assume failure.

        //    const Int32 LOCALE_ID_ENGLISH = 1033;

        //    using (SPSite siteCollection = new SPSite(parentSiteURL))
        //    {
        //        SPWeb parentWeb = siteCollection.OpenWeb();
        //        SPWebTemplateCollection Templates = siteCollection.GetWebTemplates(Convert.ToUInt32(LOCALE_ID_ENGLISH));
        //        SPWebTemplate siteTemplate = Templates[siteTemplateName];
        //        //if (parentWeb.Webs[siteURLRequested].Exists)
        //        //{
        //        //    parentWeb.Webs.Delete(siteURLRequested);
        //        //}

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
    }

    class DisabledEventsScope : SPItemEventReceiver, IDisposable
    {
        // Boolean to hold the original value of the EventFiringEnabled property           
        bool _originalValue;
        public DisabledEventsScope()
        {
            // Save off the original value of EventFiringEnabled           
            _originalValue = base.EventFiringEnabled;
            // Set EventFiringEnabled to false to disable it        
            base.EventFiringEnabled = false;
        }

        public void Dispose()
        {
            // Set EventFiringEnabled back to its original value   
            base.EventFiringEnabled = _originalValue;
        }
    }
}

