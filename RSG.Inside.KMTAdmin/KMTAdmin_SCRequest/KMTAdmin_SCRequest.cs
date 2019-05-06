using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace RSG.Inside.KMTAdmin.KMTAdmin_SCRequest
{
    [ToolboxItemAttribute(false)]
    public class KMTAdmin_SCRequest : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/RSG.Inside.KMTAdmin/KMTAdmin_SCRequest/KMTAdmin_SCRequestUserControl.ascx";

        #region Webpart Custom Properties

        //for WebPart Title
        private string _WebPartTitle = "";
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("WebPartTitle"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("KMTGroup"),
         System.ComponentModel.DefaultValue("")
        ]
        public string WebPartTitle
        {
            get { return _WebPartTitle; }
            set { _WebPartTitle = value; }
        }

        //for SiteNameLabel
        private string _SiteNameLabel = "";
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("SiteNameLabel"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("KMTGroup"),
         System.ComponentModel.DefaultValue("")
        ]
        public string SiteNameLabel
        {
            get { return _SiteNameLabel; }
            set { _SiteNameLabel = value; }
        }

        //for SiteDescriptionLabel Title
        private string _SiteDescriptionLabel = "";
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("SiteDescriptionLabel"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("KMTGroup"),
         System.ComponentModel.DefaultValue("")
        ]
        public string SiteDescriptionLabel
        {
            get { return _SiteDescriptionLabel; }
            set { _SiteDescriptionLabel = value; }
        }

        //for LawsonDivisionNumberLabel
        private string _LawsonDivisionNumberLabel = "";
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("LawsonDivisionNumberLabel"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("KMTGroup"),
         System.ComponentModel.DefaultValue("")
        ]
        public string LawsonDivisionNumberLabel
        {
            get { return _LawsonDivisionNumberLabel; }
            set { _LawsonDivisionNumberLabel = value; }
        }

        //for SiteURLLabel
        private string _SiteURLLabel = "";
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("SiteURLLabel"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("KMTGroup"),
         System.ComponentModel.DefaultValue("")
        ]
        public string SiteURLLabel
        {
            get { return _SiteURLLabel; }
            set { _SiteURLLabel = value; }
        }

        //for ZipCodeLabel
        private string _ZipCodeLabel = "";
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("ZipCodeLabel"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("KMTGroup"),
         System.ComponentModel.DefaultValue("")
        ]
        public string ZipCodeLabel
        {
            get { return _ZipCodeLabel; }
            set { _ZipCodeLabel = value; }
        }

        //for PrimarySiteAdminLabel
        private string _PrimarySiteAdminLabel = "";
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("PrimarySiteAdminLabel"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("KMTGroup"),
         System.ComponentModel.DefaultValue("")
        ]
        public string PrimarySiteAdminLabel
        {
            get { return _PrimarySiteAdminLabel; }
            set { _PrimarySiteAdminLabel = value; }
        }

        //for SecondarySiteAdminLabel
        private string _SecondarySiteAdminLabel = "";
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("SecondarySiteAdminLabel"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("KMTGroup"),
         System.ComponentModel.DefaultValue("")
        ]
        public string SecondarySiteAdminLabel
        {
            get { return _SecondarySiteAdminLabel; }
            set { _SecondarySiteAdminLabel = value; }
        }

        //for SubmitButtonLabel
        private string _SubmitButtonLabel = "";
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("SubmitButtonLabel"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("KMTGroup"),
         System.ComponentModel.DefaultValue("")
        ]
        public string SubmitButtonLabel
        {
            get { return _SubmitButtonLabel; }
            set { _SubmitButtonLabel = value; }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public KMTAdmin_SCRequest()
        {
            WebPartTitle = "Create KMT Divisional Site";
            SiteNameLabel = "Site Name :";
            SiteDescriptionLabel = "Site Description : ";
            LawsonDivisionNumberLabel = "Lawson-Division Number :";
            ZipCodeLabel = "Zip Code(s) :";
            PrimarySiteAdminLabel = "Primary Site Collection Administrator :";
            SecondarySiteAdminLabel = "Secondary Site Collection Administrator :";
            SubmitButtonLabel = "Submit";
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }
    }
}
