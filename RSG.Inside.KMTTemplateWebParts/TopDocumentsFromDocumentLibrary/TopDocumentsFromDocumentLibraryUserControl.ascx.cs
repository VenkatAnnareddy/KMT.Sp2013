using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using Microsoft.SharePoint;

namespace RSG.Inside.KMTTemplateWebParts.TopDocumentsFromDocumentLibrary
{
    public partial class TopDocumentsFromDocumentLibraryUserControl : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public TopDocumentsFromDocumentLibrary WebPart { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMsg.Text = "";
                this.WebPart = this.Parent as TopDocumentsFromDocumentLibrary;
                LoadDefaults();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadDefaults()
        {
            rptLinks.Visible = false;
            lblNoDocumentsExists.Visible = false;

            try
            {
                string DocumentLibraryName = WebPart.DocumentLibraryName;
                string CityorEntityColumnName = WebPart.CityorEntityColumnName;
                string CityorEntityColumnValue = WebPart.CityorEntityColumnValue;
                string LOBColumnName = WebPart.LOBColumnName;
                string LOBColumnValue = WebPart.LOBColumnValue;
                string RankColumnName = WebPart.RankColumnName;
                string RankColumnValue = WebPart.RankColumnValue;
                string DocumentFullURL = SPContext.Current.Web.Url + WebPart.DocumentFullURL;
                lblNoDocumentsExists.Text = WebPart.NoDocumentsMessage;

                DataTable dt = null, dt1 = GetTopDocuments(DocumentLibraryName,
                 CityorEntityColumnName,
                 CityorEntityColumnValue,
                 LOBColumnName,
                 LOBColumnValue,
                 RankColumnName,
                 RankColumnValue);

                dt = GetDisplayColumns(dt1, DocumentFullURL);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        rptLinks.DataSource = dt;
                        rptLinks.DataBind();

                        rptLinks.Visible = true;
                    }
                    else
                        lblNoDocumentsExists.Visible = true;
                }
                else
                {
                    lblNoDocumentsExists.Visible = true;
                }

                //Gv1.DataSource = dt1;
                //Gv1.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = string.Format("Error : {0} - Trace : {1}", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="DocumentFullURL"></param>
        /// <returns></returns>
        private DataTable GetDisplayColumns(DataTable dt1, string DocumentFullURL)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("FileURL", typeof(string)));
            dt.Columns.Add(new DataColumn("FileName", typeof(string)));

            if (dt1 != null)
            {
                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        string fName = dr["LinkFilename"].ToString();
                        //string fNameURL = DocumentFullURL + dr["LinkFilename"].ToString();
                        string fNameURL = dr["EncodedAbsUrl"].ToString();
                        dt.Rows.Add(fNameURL, fName);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentLibraryName"></param>
        /// <param name="CityorEntityColumnName"></param>
        /// <param name="CityorEntityColumnValue"></param>
        /// <param name="LOBColumnName"></param>
        /// <param name="LOBColumnValue"></param>
        /// <param name="RankColumnName"></param>
        /// <param name="RankColumnValue"></param>
        /// <returns></returns>
        private DataTable GetTopDocuments(
            string DocumentLibraryName,
            string CityorEntityColumnName,
            string CityorEntityColumnValue,
            string LOBColumnName,
            string LOBColumnValue,
            string RankColumnName,
            string RankColumnValue)
        {
            int RankValue = 0;
            int.TryParse(RankColumnValue, out RankValue); ;

            SPWeb oWebsiteRoot = SPContext.Current.Site.RootWeb;
            SPList oList = oWebsiteRoot.Lists[DocumentLibraryName];
            SPQuery oQuery = new SPQuery();
            oQuery.Query = @"<Where><And>
                                        <Eq><FieldRef Name=" + LOBColumnName + @" /><Value Type='Choice'>" + LOBColumnValue + @"</Value></Eq>
                                        <And>
                                            <Contains><FieldRef Name=" + CityorEntityColumnName + @" /><Value Type='Text'>" + CityorEntityColumnValue + @"</Value></Contains>
                                            <And>
                                                <IsNotNull><FieldRef Name=" + RankColumnName + @" /></IsNotNull>
                                                <Leq><FieldRef Name=" + RankColumnName + @" /><Value Type='Number'>" + RankValue + @"</Value></Leq>
                                            </And>
                                        </And>
                                    </And>
                                </Where><OrderBy><FieldRef Name=" + RankColumnName + @" Ascending='True' /></OrderBy>";
            oQuery.ViewAttributes = "Scope=\"Recursive\"";
            oQuery.ViewFields = "<FieldRef Name=\"EncodedAbsUrl\"/>" +
                                "<FieldRef Name=\"LinkFilename\"/>";

            SPListItemCollection myItems = oList.GetItems(oQuery);
            return oList.GetItems(oQuery).GetDataTable();
        }
    }
}
