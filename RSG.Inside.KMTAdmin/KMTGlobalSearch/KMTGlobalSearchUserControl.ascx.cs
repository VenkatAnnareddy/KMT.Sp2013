using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using RSG.Inside.KMTAdmin.ServiceLocationRef;

namespace RSG.Inside.KMTAdmin.KMTGlobalSearch
{
    public partial class KMTGlobalSearchUserControl : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            lblNoResults.Text = "";
            lblNoResults.Visible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblNoResults.Visible = false;
                string sOption = optSearchOn.SelectedItem.Value.ToString();
                string sText = txtSearchText.Text.ToString().Trim();

                DataTable dt = null;

                if (sOption == "1")
                    dt = GetDisplayColumns(GetSearchSites(sOption, sText));
                else
                    dt = GetDisplayColumns(GetMatchedSites(sText));

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        rptrSites.DataSource = dt;
                    }
                    else
                    {
                        lblNoResults.Text = "No results found...!";
                        lblNoResults.Visible = true;
                        rptrSites.DataSource = null;
                    }
                }
                else
                {
                    lblNoResults.Text = "No results found...!";
                    lblNoResults.Visible = true;
                    rptrSites.DataSource = null;
                }

                rptrSites.DataBind();
            }
            catch (Exception ex)
            {
                lblNoResults.Text = string.Format("Error : {0} - StackTracke : {1}", ex.Message, ex.StackTrace);
                lblNoResults.Visible = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sOption"></param>
        /// <param name="sText"></param>
        /// <returns></returns>
        private DataTable GetSearchSites(string sOption, string sText)
        {
            DataTable dt1 = null;
            try
            {
                SPWeb oWebsiteRoot = SPContext.Current.Site.RootWeb;
                SPList oList = oWebsiteRoot.Lists["KMTDivisionalSites"];

                SPQuery oQuery = new SPQuery();
                if (sOption == "1")
                {
                    //oQuery.Query = "<Where><Contains><FieldRef Name='LawsonDivisionNo' /><Value Type='Text'>" + sText + "</Value></Contains></Where>";
                    oQuery.Query = @"
                                       <Where>
                                          <And>
                                             <Contains>
                                                <FieldRef Name='LawsonDivisionNo' />
                                                <Value Type='Text'>" + sText + @"</Value>
                                             </Contains>
                                             <Contains>
                                                <FieldRef Name='SiteCreated' />
                                                <Value Type='Text'>Yes</Value>
                                             </Contains>
                                          </And>
                                       </Where>
                                       <OrderBy>
                                          <FieldRef Name='LawsonDivisionNo' Ascending='True' />
                                       </OrderBy>
                                    ";
                }
                else
                {
                    //oQuery.Query = "<Where><Contains><FieldRef Name='ZipCode' /><Value Type='Text'>" + sText + "</Value></Contains></Where>";
                    oQuery.Query = @"
                                       <Where>
                                          <And>
                                             <Contains>
                                                <FieldRef Name='ZipCode' />
                                                <Value Type='Note'>" + sText + @"</Value>
                                             </Contains>
                                             <Contains>
                                                <FieldRef Name='SiteCreated' />
                                                <Value Type='Text'>Yes</Value>
                                             </Contains>
                                          </And>
                                       </Where>
                                       <OrderBy>
                                          <FieldRef Name='ZipCode' Ascending='True' />
                                       </OrderBy>";
                }

                dt1 = oList.GetItems(oQuery).GetDataTable();
            }
            catch (Exception ex)
            {
                lblNoResults.Text = string.Format("Error : {0} - StackTracke : {1}", ex.Message, ex.StackTrace);
                lblNoResults.Visible = true;
            }
            return dt1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt1"></param>
        /// <returns></returns>
        private DataTable GetDisplayColumns(DataTable dt1)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SiteName", typeof(string)));
            dt.Columns.Add(new DataColumn("SiteURL", typeof(string)));
            dt.Columns.Add(new DataColumn("LandingPageURL", typeof(string)));

            try
            {
                string sLandingPageURL = GetListValue("KMTConfig", "Key", "LandingPageURL", "Value", false);

                if (dt1 != null)
                {
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt1.Rows)
                        {
                            string fName = dr["SiteName"].ToString();
                            string fNameURL = dr["SiteURL"].ToString();
                            string fLandingPageURL = dr["SiteURL"].ToString() + sLandingPageURL;
                            dt.Rows.Add(fName, fNameURL, fLandingPageURL);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblNoResults.Text = string.Format("Error : {0} - StackTracke : {1}", ex.Message, ex.StackTrace);
                lblNoResults.Visible = true;
            }
            return dt;
        }

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
                lblNoResults.Text = string.Format("Error : {0} - StackTracke : {1}", ex.Message, ex.StackTrace);
                lblNoResults.Visible = true;
            }
            //}
            //catch (Exception ex)
            //{
            //    colResult = "Error : " + ex.Message.ToString() + "  Trace : " + ex.Source.ToString();
            //    //LogException(ex);
            //}
            return colResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lawsonDivisionNumber"></param>
        /// <returns></returns>
        private string GetZipCodesForLawsonDivisionNumberAsString(string zipCode)
        {
            RSG_LawsonDivisionNumber zCodes = new RSG_LawsonDivisionNumber();
            string lawsonDivisionNos = "";
            ServiceLocationClient wcLocation = new ServiceLocationClient();
            try
            {
                if (wcLocation != null)
                {
                    zCodes = wcLocation.GetLawsonDivisionNumbersByZipCode(zipCode);
                    if (zCodes != null)
                    {
                        foreach (LawsonDivisionNumber cd in zCodes.LawsonDivisionNumber)
                        {
                            if (lawsonDivisionNos == "")
                                lawsonDivisionNos = cd.LawsonDivisonNumber;
                            else
                                lawsonDivisionNos += "," + cd.LawsonDivisonNumber;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingService.LogErrorInULS(ex);
                lblNoResults.Text = string.Format("Error : {0} - StackTracke : {1}", ex.Message, ex.StackTrace);
                lblNoResults.Visible = true;
            }
            finally
            {
                if (wcLocation.State == System.ServiceModel.CommunicationState.Faulted)
                    wcLocation.Abort();
                else
                    wcLocation.Close();
            }

            return lawsonDivisionNos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        private DataTable GetZipCodesForLawsonDivisionNumberAsDataTable(string zipCode)
        {
            DataTable dt = new DataTable("LawsonDivisionNumbers");
            dt.Columns.Add(new DataColumn("LawsonDivisionNo", typeof(string)));

            RSG_LawsonDivisionNumber zCodes = new RSG_LawsonDivisionNumber();
            ServiceLocationClient wcLocation = new ServiceLocationClient();
            try
            {
                if (wcLocation != null)
                {
                    zCodes = wcLocation.GetLawsonDivisionNumbersByZipCode(zipCode);
                    if (zCodes != null)
                    {
                        foreach (LawsonDivisionNumber cd in zCodes.LawsonDivisionNumber)
                        {
                            dt.Rows.Add(cd.LawsonDivisonNumber.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingService.LogErrorInULS(ex);

                lblNoResults.Text = string.Format("Error : {0} - StackTracke : {1}", ex.Message, ex.StackTrace);
                lblNoResults.Visible = true;
            }
            finally
            {
                if (wcLocation.State == System.ServiceModel.CommunicationState.Faulted)
                    wcLocation.Abort();
                else
                    wcLocation.Close();
            }

            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable GetAllKMTSites()
        {
            DataTable dt1 = null;
            try
            {
                SPWeb oWebsiteRoot = SPContext.Current.Site.RootWeb;
                SPList oList = oWebsiteRoot.Lists["KMTDivisionalSites"];

                SPQuery oQuery = new SPQuery();
                oQuery.Query = "<Where><IsNotNull><FieldRef Name='LawsonDivisionNo' /></IsNotNull></Where>";

                dt1 = oList.GetItems(oQuery).GetDataTable();
            }
            catch (Exception ex)
            {
                lblNoResults.Text = string.Format("Error : {0} - StackTracke : {1}", ex.Message, ex.StackTrace);
                lblNoResults.Visible = true;
            }
            return dt1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        private DataTable GetMatchedSites(string searchText)
        {
            DataTable Dt1 = GetZipCodesForLawsonDivisionNumberAsDataTable(searchText);
            DataTable Dt2 = GetAllKMTSites();

            DataTable targetTable = Dt2.Clone();
            var dt2Columns = Dt2.Columns.OfType<DataColumn>().Select(dc =>
            new DataColumn(dc.ColumnName, dc.DataType, dc.Expression, dc.ColumnMapping));
            var dt2FinalColumns = from dc in dt2Columns.AsEnumerable()
                                  where targetTable.Columns.Contains(dc.ColumnName) == false
                                  select dc;
            targetTable.Columns.AddRange(dt2FinalColumns.ToArray());

            var rowData = from row1 in Dt1.AsEnumerable()
                          join row2 in Dt2.AsEnumerable()
                          on row1.Field<string>("LawsonDivisionNo") equals row2.Field<string>("LawsonDivisionNo")
                          select row1.ItemArray.Concat(row2.ItemArray.Where(r2 => row1.ItemArray.Contains(r2) == false)).ToArray();
            foreach (object[] values in rowData)
                targetTable.Rows.Add(values);

            //gv3.DataSource = targetTable;
            //gv3.DataBind();

            return targetTable;
        }
    }
}
