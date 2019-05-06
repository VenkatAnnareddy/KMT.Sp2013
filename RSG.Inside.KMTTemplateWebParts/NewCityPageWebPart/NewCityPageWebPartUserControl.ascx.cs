using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System.IO;
using Microsoft.SharePoint.WebPartPages;
using System.Xml;
using RSG.Inside.KMTTemplateWebParts.CitiesLeftNavigation;

namespace RSG.Inside.KMTTemplateWebParts.NewCityPageWebPart
{
    public partial class NewCityPageWebPartUserControl : UserControl
    {
        //#region Page events

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        LoadStates();
        //        LoadDocumentLibraries();
        //    }
        //}

        //#endregion

        //#region Common Methods

        ///// <summary>
        ///// 
        ///// </summary>
        //private void LoadStates()
        //{
        //    ddlState.Items.Clear();

        //    SPWeb web = SPContext.Current.Web;
        //    SPList lst = web.Lists["States"];
        //    SPListItemCollection lstItems = lst.GetItems();

        //    foreach (SPListItem itm in lstItems)
        //    {
        //        ddlState.Items.Add(new ListItem(itm.Title.ToString(), itm.Title.ToString()));
        //    }

        //    if (ddlState.Items.Count > 0)
        //        ddlState.SelectedIndex = 0;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //private void LoadDocumentLibraries()
        //{
        //    try
        //    {
        //        ddlDocLibrary.Items.Clear();
        //        SPWeb spWeb = SPContext.Current.Web;
        //        foreach (SPList spList in spWeb.Lists)
        //        {
        //            if (spList is SPDocumentLibrary)
        //            {
        //                SPDocumentLibrary spDoc = spList as SPDocumentLibrary;
        //                //We do not want to consider ootb libraries 
        //                if (spDoc.Hidden || spDoc.IsCatalog || !spDoc.AllowDeletion) continue;
        //                ddlDocLibrary.Items.Add(spDoc.Title.ToString());
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMsg.Text = string.Format("Error : {0} - Trace : {1}", ex.Message, ex.StackTrace);
        //    }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //private string GetFilename()
        //{
        //    string selState = ddlState.SelectedItem.Text.ToString();
        //    string fName = selState + "_" + txtCityPageName.Text;
        //    if (!fName.Trim().ToUpper().EndsWith(".ASPX")) fName = fName + ".aspx";
        //    return fName;
        //}

        //#endregion

        //#region Adding WebPart page and WebParts

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="files"></param>
        ///// <param name="fName"></param>
        ///// <returns></returns>
        //private bool isFileExists(SPFileCollection files, string fName)
        //{
        //    bool isFlag = false;
        //    foreach (SPFile file in files)
        //    {
        //        if (file.Name == fName)
        //        {
        //            isFlag = true;
        //            break;
        //        }
        //    }
        //    return isFlag;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="wpmngr"></param>
        ///// <param name="webpartName"></param>
        ///// <returns></returns>
        //private System.Web.UI.WebControls.WebParts.WebPart GetWebPartToAdd(SPLimitedWebPartManager wpmngr, string webpartName)
        //{
        //    string sLayoutshive = SPUtility.GetGenericSetupPath("TEMPLATE\\LAYOUTS\\RSG.INSIDE.KMTTEMPLATEWEBPARTS\\WEBPARTS\\");
        //    string webpartFilePath = sLayoutshive + webpartName;
        //    FileStream file = File.OpenRead(webpartFilePath);

        //    string error = string.Empty;
        //    System.Web.UI.WebControls.WebParts.WebPart webpart = wpmngr.ImportWebPart(XmlReader.Create(file), out error);

        //    return webpart;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="wpmngr"></param>
        ///// <param name="webpartName"></param>
        ///// <returns></returns>
        //private System.Web.UI.WebControls.WebParts.WebPart GetWebPartToAdd(SPLimitedWebPartManager wpmngr, string webpartName, string cityName, string lobValue)
        //{
        //    string sLayoutshive = SPUtility.GetGenericSetupPath("TEMPLATE\\LAYOUTS\\RSG.INSIDE.KMTTEMPLATEWEBPARTS\\WEBPARTS\\");
        //    string webpartFilePath = sLayoutshive + webpartName;
        //    FileStream file = File.OpenRead(webpartFilePath);

        //    string error = string.Empty;
        //    System.Web.UI.WebControls.WebParts.WebPart webpart = wpmngr.ImportWebPart(XmlReader.Create(file), out error);
        //    if (lobValue != string.Empty)
        //    {
        //        ((RSG.Inside.KMTTemplateWebParts.TopDocumentsFromDocumentLibrary.TopDocumentsFromDocumentLibrary)(webpart)).LOBColumnValue = lobValue;
        //        ((RSG.Inside.KMTTemplateWebParts.TopDocumentsFromDocumentLibrary.TopDocumentsFromDocumentLibrary)(webpart)).CityorEntityColumnValue = cityName;
        //    }

        //    return webpart;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="web"></param>
        ///// <param name="fileName"></param>
        ///// <param name="DocLib"></param>
        //public void CreateWebPartPage(SPWeb web, string fileName, string DocLib)
        //{
        //    SPSecurity.RunWithElevatedPrivileges(delegate()
        //    {
        //        //string templateFilename = "spstd2.aspx";
        //        string cityName = txtCityPageName.Text.Trim();
        //        string templateFilename = "NewCityPageLayout.aspx";
        //        string hive = SPUtility.GetGenericSetupPath("TEMPLATE\\" + SPContext.Current.Web.Language + "\\STS\\DOCTEMP\\SMARTPGS\\");

        //        using (FileStream stream = new FileStream(hive + templateFilename, FileMode.Open))
        //        {
        //            SPFolder libraryFolder = web.GetFolder(DocLib);
        //            SPFileCollection files = libraryFolder.Files;
        //            if (!isFileExists(files, fileName))
        //            {
        //                SPFile newFile = files.Add(fileName, stream);

        //                SPLimitedWebPartManager wpmngr = newFile.Web.GetLimitedWebPartManager(newFile.ServerRelativeUrl, PersonalizationScope.Shared);
        //                wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "NewCityDivisionHeader.dwp"), "Header", 0);
        //                wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "CitiesLeftNavigation.webpart"), "LeftColumn", 0);
        //                wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "CityPageSummaryLinks.webpart"), "RightColumn", 0);
        //                //wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "NewCityLOBs.dwp"), "MiddleColumn", 0);

        //                wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "Residential CEWP.dwp"), "Middle1Column", 0);
        //                wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "TopDocumentsFromDocumentLibrary.webpart", cityName, "Residential"), "Middle1Column", 1);
        //                wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "Commercial CEWP.dwp"), "Middle1Column", 2);
        //                wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "TopDocumentsFromDocumentLibrary.webpart", cityName, "Commercial"), "Middle1Column", 3);

        //                wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "Industrial CEWP.dwp"), "Middle2Column", 0);
        //                wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "TopDocumentsFromDocumentLibrary.webpart", cityName, "Industrial"), "Middle2Column", 1);
        //                wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "Operational CEWP.dwp"), "Middle2Column", 2);
        //                wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "TopDocumentsFromDocumentLibrary.webpart", cityName, "Operational"), "Middle2Column", 3);

        //                wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "NewCityDummyDescription.dwp"), "MiddleColumn", 2);

        //                AddToCitiesList();

        //                lblMsg.Text = string.Format("{0} Webpage successfully created...!", fileName);
        //            }
        //            else
        //            {
        //                lblMsg.Text = "page already exists...!";
        //            }
        //        }
        //    });
        //}

        //#endregion

        //#region Add Entry to Cities List

        ///// <summary>
        ///// 
        ///// </summary>
        //private void AddToCitiesList()
        //{
        //    try
        //    {
        //        SPWeb mySite = SPContext.Current.Web;
        //        SPListItemCollection listItems = mySite.Lists["Cities"].Items;
        //        SPListItem item = listItems.Add();

        //        string eName = txtCityPageName.Text;
        //        string sFilePath = "/" + ddlDocLibrary.SelectedItem.Text + "/" + GetFilename();

        //        item["Title"] = eName;
        //        item["EntityName"] = eName;
        //        item["StateName"] = ddlState.SelectedItem.Text.ToString();
        //        item["NavigationLink"] = sFilePath;

        //        item.Update();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMsg.Text = string.Format("Error : {0} - Stack Trace : {1}", ex.Message, ex.StackTrace);
        //    }
        //}

        //#endregion

        //#region Button Events

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnCreatePage_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        CreateWebPartPage(SPContext.Current.Web, GetFilename(), ddlDocLibrary.SelectedItem.Text);
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMsg.Text = string.Format("Error : {0} - Trace : {1}", ex.Message, ex.StackTrace);
        //    }
        //}

        //#endregion

        #region Page events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStates();
                LoadDocumentLibraries();
            }
        }

        #endregion

        #region Common Methods

        /// <summary>
        /// 
        /// </summary>
        private void LoadStates()
        {
            ddlState.Items.Clear();

            SPWeb web = SPContext.Current.Web;
            SPList lst = web.Lists["States"];
            SPListItemCollection lstItems = lst.GetItems();

            foreach (SPListItem itm in lstItems)
            {
                ddlState.Items.Add(new ListItem(itm.Title.ToString(), itm.Title.ToString()));
            }

            if (ddlState.Items.Count > 0)
                ddlState.SelectedIndex = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadDocumentLibraries()
        {
            try
            {
                ddlDocLibrary.Items.Clear();
                SPWeb spWeb = SPContext.Current.Web;
                foreach (SPList spList in spWeb.Lists)
                {
                    if (spList is SPDocumentLibrary)
                    {
                        SPDocumentLibrary spDoc = spList as SPDocumentLibrary;
                        //We do not want to consider ootb libraries 
                        if (spDoc.Hidden || spDoc.IsCatalog || !spDoc.AllowDeletion) continue;
                        ddlDocLibrary.Items.Add(spDoc.Title.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = string.Format("Error : {0} - Trace : {1}", ex.Message, ex.StackTrace);
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //private string GetFilename()
        //{
        //    string selState = ddlState.SelectedItem.Text.ToString();
        //    string fName = selState + "_" + txtCityPageName.Text;
        //    if (!fName.Trim().ToUpper().EndsWith(".ASPX")) fName = fName + ".aspx";
        //    return fName;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cfName"></param>
        /// <returns></returns>
        private string GetFilename(string cfName)
        {
            string selState = ddlState.SelectedItem.Text.ToString();
            string fName = selState + "_" + cfName;
            if (!fName.Trim().ToUpper().EndsWith(".ASPX")) fName = fName + ".aspx";
            return fName;
        }

        #endregion

        #region Adding WebPart page and WebParts

        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <param name="fName"></param>
        /// <returns></returns>
        private bool isFileExists(SPFileCollection files, string fName)
        {
            bool isFlag = false;
            foreach (SPFile file in files)
            {
                if (file.Name == fName)
                {
                    isFlag = true;
                    break;
                }
            }
            return isFlag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wpmngr"></param>
        /// <param name="webpartName"></param>
        /// <returns></returns>
        private System.Web.UI.WebControls.WebParts.WebPart GetWebPartToAdd(SPLimitedWebPartManager wpmngr, string webpartName)
        {
            // string sLayoutshive = SPUtility.GetGenericSetupPath("TEMPLATE\\LAYOUTS\\RSG.INSIDE.KMTTEMPLATEWEBPARTS\\WEBPARTS\\");
            string sLayoutshive = "C:\\Program Files\\Common Files\\Microsoft Shared\\Web Server Extensions\\15\\TEMPLATE\\LAYOUTS\\RSG.INSIDE.KMTTEMPLATEWEBPARTS\\WEBPARTS\\";
            string webpartFilePath = sLayoutshive + webpartName;
            FileStream file = File.OpenRead(webpartFilePath);

            string error = string.Empty;
            System.Web.UI.WebControls.WebParts.WebPart webpart = wpmngr.ImportWebPart(XmlReader.Create(file), out error);

            return webpart;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wpmngr"></param>
        /// <param name="webpartName"></param>
        /// <returns></returns>
        private System.Web.UI.WebControls.WebParts.WebPart GetWebPartToAdd(SPLimitedWebPartManager wpmngr, string webpartName, string cityName, string lobValue)
        {
            //string sLayoutshive = SPUtility.GetGenericSetupPath("15\\TEMPLATE\\LAYOUTS\\RSG.INSIDE.KMTTEMPLATEWEBPARTS\\WEBPARTS\\");
            string sLayoutshive = "C:\\Program Files\\Common Files\\Microsoft Shared\\Web Server Extensions\\15\\TEMPLATE\\LAYOUTS\\RSG.INSIDE.KMTTEMPLATEWEBPARTS\\WEBPARTS\\";
            string webpartFilePath = sLayoutshive + webpartName;
            FileStream file = File.OpenRead(webpartFilePath);

            string error = string.Empty;
            System.Web.UI.WebControls.WebParts.WebPart webpart = wpmngr.ImportWebPart(XmlReader.Create(file), out error);
            if (lobValue != string.Empty)
            {
                ((RSG.Inside.KMTTemplateWebParts.TopDocumentsFromDocumentLibrary.TopDocumentsFromDocumentLibrary)(webpart)).LOBColumnValue = lobValue;
                ((RSG.Inside.KMTTemplateWebParts.TopDocumentsFromDocumentLibrary.TopDocumentsFromDocumentLibrary)(webpart)).CityorEntityColumnValue = cityName;
            }

            return webpart;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="web"></param>
        /// <param name="fileNameWithOutExtension"></param>
        /// <param name="fileName"></param>
        /// <param name="DocLib"></param>
        public void CreateWebPartPage(string fileNameWithOutExtension, string fileName, string DocLib)
        {
               
                        SPWeb web = SPContext.Current.Web;
                        SPFolder libraryFolder = web.GetFolder(DocLib);
                        SPFileCollection files = libraryFolder.Files;

                        SPSecurity.RunWithElevatedPrivileges(delegate()
                        {
                            //string templateFilename = "spstd2.aspx";
                            //string cityName = txtCityPageName.Text.Trim();
                            string cityName = fileNameWithOutExtension;
                            string templateFilename = "NewCityPageLayout.aspx";
                            //string hive = SPUtility.GetGenericSetupPath("15\\TEMPLATE\\" + SPContext.Current.Web.Language + "\\STS\\DOCTEMP\\SMARTPGS\\");
                            string hive = "C:\\Program Files\\Common Files\\Microsoft Shared\\Web Server Extensions\\15\\TEMPLATE\\1033\\STS\\DOCTEMP\\SMARTPGS\\";

                            using (FileStream stream = new FileStream(hive + templateFilename, FileMode.Open))
                            {
                                // SPFolder libraryFolder = web.GetFolder(DocLib);
                                //SPFileCollection files = libraryFolder.Files;
                                if (!isFileExists(files, fileName))
                                {
                                    SPFile newFile = files.Add(fileName, stream);

                                    SPLimitedWebPartManager wpmngr = newFile.Web.GetLimitedWebPartManager(newFile.ServerRelativeUrl, PersonalizationScope.Shared);
                                    wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "NewCityDivisionHeader.dwp"), "Header", 0);
                                    wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "CitiesLeftNavigation.webpart"), "LeftColumn", 0);
                                  //  wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "CityPageSummaryLinks.webpart"), "RightColumn", 0);
                                    //wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "NewCityLOBs.dwp"), "MiddleColumn", 0);

                                    wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "Residential CEWP.dwp"), "Middle1Column", 0);
                                    wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "TopDocumentsFromDocumentLibrary.webpart", cityName, "Residential"), "Middle1Column", 1);
                                    wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "Commercial CEWP.dwp"), "Middle1Column", 2);
                                    wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "TopDocumentsFromDocumentLibrary.webpart", cityName, "Commercial"), "Middle1Column", 3);

                                    wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "Industrial CEWP.dwp"), "Middle2Column", 0);
                                    wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "TopDocumentsFromDocumentLibrary.webpart", cityName, "Industrial"), "Middle2Column", 1);
                                    wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "Operational CEWP.dwp"), "Middle2Column", 2);
                                    wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "TopDocumentsFromDocumentLibrary.webpart", cityName, "Operational"), "Middle2Column", 3);

                                    wpmngr.AddWebPart(GetWebPartToAdd(wpmngr, "NewCityDummyDescription.dwp"), "MiddleColumn", 2);

                                    AddToCitiesList(fileNameWithOutExtension);

                                    lblMsg.Text += string.Format("{0} Webpage successfully created...!<br>", fileName);
                                }
                                else
                                {
                                    lblMsg.Text = "page already exists...!";
                                }
                            }
                        });                   
            
           // updatedatainCEWP(fileName, DocLib, txtbxDivision.Text.Trim(), txtbxStateCode.Text.Trim(), fileNameWithOutExtension);

        }

        #endregion

        //public void updatedatainCEWP(string Cfilename, string DoclibName, string strDivision, string strStateCode, string strFileNamewithoutext)
        //{
        //    string filePath = "/" + ddlDocLibrary.SelectedItem.Text + "/" + Cfilename;
        //    string strCityndCode = strFileNamewithoutext + ", " + strStateCode;
        //    try
        //    {

        //        SPSecurity.RunWithElevatedPrivileges(delegate()
        //        {
        //            SPWeb spWebTest = SPContext.Current.Web;
        //            spWebTest.AllowUnsafeUpdates = true;

        //            using (Microsoft.SharePoint.WebPartPages.SPLimitedWebPartManager mgr = spWebTest.GetFile(filePath).GetLimitedWebPartManager(PersonalizationScope.Shared))
        //            {

        //                if (mgr != null)
        //                {

        //                    foreach (System.Web.UI.WebControls.WebParts.WebPart part in mgr.WebParts)
        //                    {
        //                        string zoneid = mgr.GetZoneID(part);
        //                        if (zoneid == "Header")
        //                        {

        //                            if (part.GetType().ToString().Equals("Microsoft.SharePoint.WebPartPages.ContentEditorWebPart"))
        //                            {
        //                                ContentEditorWebPart contentEditor = (ContentEditorWebPart)part;

        //                                // create a new XmlElement and put the results there
        //                                XmlDocument xmlDoc = new XmlDocument();
        //                                XmlElement xmlElement = xmlDoc.CreateElement("MyElement");

        //                                string CEWPText = "<div style='text-align: center'><span style='font-family: calibri; color: black; font-size: 18pt; language: en-us'>" +
        //      "<div class='ms-rteFontSize-6' style='text-align: center'><span class='ms-rteFontFace-10' style='color: black; language: en-us'><strong> <span>" +
        //       "<font face='Lucida Console'>" + strDivision + "</font></span></strong></span><span style='font-family: calibri; color: black; language: en-us'><span> </span></span></div>" +
        //      "<div style='text-align: center'><span style='font-family: calibri; color: black; font-size: 18pt; language: en-us'>" + strCityndCode + "</span></div></span></div>";

        //                                xmlElement.InnerText = CEWPText;
        //                                // we MUST set the Content property, not a property of Content.                                
        //                                contentEditor.Content = xmlElement;

        //                                // persist changes to the database
        //                                mgr.SaveChanges(contentEditor);
        //                            }

        //                        }

        //                    }


        //                }

        //            }

        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        lblError.Text = filePath + ex.Message + ex.StackTrace + GetFilename(strFileNamewithoutext);
        //    }

        //}

        #region Add Entry to Cities List

        /// <summary>
        /// 
        /// </summary>
        private void AddToCitiesList(string cfName)
        {
            try
            {
                SPWeb mySite = SPContext.Current.Web;
                SPListItemCollection listItems = mySite.Lists["Cities"].Items;
                SPListItem item = listItems.Add();

                string eName = cfName;
                string sFilePath = "/" + ddlDocLibrary.SelectedItem.Text + "/" + GetFilename(cfName);

                item["Title"] = eName;
                item["EntityName"] = eName;
                item["StateName"] = ddlState.SelectedItem.Text.ToString();
                item["NavigationLink"] = sFilePath;

                item.Update();
            }
            catch (Exception ex)
            {
                lblMsg.Text = string.Format("Error : {0} - Stack Trace : {1}", ex.Message, ex.StackTrace);
            }
        }

        #endregion

        #region Button Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreatePage_Click(object sender, EventArgs e)
        {
            try
            {
                string file = txtCityPageName.Text.ToString();
                string[] files = file.Split(';');

                foreach (string cfName in files)
                {
                    CreateWebPartPage(cfName, GetFilename(cfName), ddlDocLibrary.SelectedItem.Text);
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = string.Format("Error : {0} - Trace : {1}", ex.Message, ex.StackTrace);
            }
        }

        #endregion
    }
}
