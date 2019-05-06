using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace RSG.Inside.KMTTemplateWebParts.CitiesLeftNavigation
{
    public partial class CitiesLeftNavigationUserControl : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDefaults();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected void LoadDefaults()
        {
            lblCitiesNavigation.Text = GetLinks();
            //DataTable dt = null;
            //dt = GetDisplayColumns(GetCities());

            //if (dt != null)
            //{
            //    //if (dt.Rows.Count > 0)
            //    //    rptLinks.DataSource = dt;
            //    //else
            //    //    rptLinks.DataSource = null;
            //}
            //else
            //{
            //    //rptLinks.DataSource = null;
            //}

            ////rptLinks.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetLinks()
        {
            string lnks = "<div style='width: 200px' id='accordion'>", tempState = "";


            DataTable dt = null;
            int index = 0;
            dt = GetDisplayColumns(GetCities());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    lnks += @"<h2><b>" + dt.Rows[i]["State"].ToString() + @"</b></h2><div style='height=auto !important;'>";
                }

                if ((tempState != dt.Rows[i]["State"].ToString()) && (i != 0))
                {
                    lnks += @"</div><h2><b>" + dt.Rows[i]["State"].ToString() + @"</b></h2><div style='height=auto !important;'>";
                    index++;
                }

                lnks += @"<a href='" + dt.Rows[i]["NavigationLink"].ToString() + @"?sindex=" + index.ToString() + "'>" + dt.Rows[i]["EntityName"].ToString() + @"</a><br/>";

                if (i + 1 == dt.Rows.Count)
                {
                    lnks += @"</div>";
                }

                tempState = dt.Rows[i]["State"].ToString();

            }

            lnks += "</div>";

            return lnks;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt1"></param>
        /// <returns></returns>
        private DataTable GetDisplayColumns(DataTable dt1)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("State", typeof(string)));
            dt.Columns.Add(new DataColumn("EntityName", typeof(string)));
            dt.Columns.Add(new DataColumn("NavigationLink", typeof(string)));

            if (dt1 != null)
            {
                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        string stateName = dr["StateName"].ToString();
                        string fName = dr["EntityName"].ToString();
                        string fNameURL = SPContext.Current.Web.Url + dr["NavigationLink"].ToString();
                        dt.Rows.Add(stateName, fName, fNameURL);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable GetCities()
        {
            SPWeb oWebsiteRoot = SPContext.Current.Site.RootWeb;
            SPList oList = oWebsiteRoot.Lists["Cities"];

            SPQuery oQuery = new SPQuery();
            //oQuery.Query = "<OrderBy><FieldRef Name='EntityName' Ascending='True' /></OrderBy>";
            oQuery.Query = @"
                            <OrderBy>
                                  <FieldRef Name='StateName' Ascending='True' />
                                  <FieldRef Name='EntityName' Ascending='True' />
                               </OrderBy>
                            ";

            return oList.GetItems(oQuery).GetDataTable();
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
                colResult = "Error : " + ex.Message.ToString() + "  Trace : " + ex.Source.ToString();
                //LogException(ex);
            }
            return colResult;
        }
    }
}
