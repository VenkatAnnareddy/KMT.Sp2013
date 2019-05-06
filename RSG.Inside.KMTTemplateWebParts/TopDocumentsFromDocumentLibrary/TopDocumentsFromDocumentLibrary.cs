using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace RSG.Inside.KMTTemplateWebParts.TopDocumentsFromDocumentLibrary
{
    [ToolboxItemAttribute(false)]
    public class TopDocumentsFromDocumentLibrary : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/RSG.Inside.KMTTemplateWebParts/TopDocumentsFromDocumentLibrary/TopDocumentsFromDocumentLibraryUserControl.ascx";

        #region Webpart Custom Properties

        //for DocumentLibraryName
        private string _DocumentLibraryName = "";
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("DocumentLibraryName"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("KMT"),
         System.ComponentModel.DefaultValue("")
        ]
        public string DocumentLibraryName
        {
            get { return _DocumentLibraryName; }
            set { _DocumentLibraryName = value; }
        }

        //for DocumentFullURL
        private string _DocumentFullURL = "";
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("DocumentFullURL"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("KMT"),
         System.ComponentModel.DefaultValue("")
        ]
        public string DocumentFullURL
        {
            get { return _DocumentFullURL; }
            set { _DocumentFullURL = value; }
        }

        //for CityorEntityColumnName
        private string _CityorEntityColumnName = "";
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("CityorEntityColumnName"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("KMT"),
         System.ComponentModel.DefaultValue("")
        ]
        public string CityorEntityColumnName
        {
            get { return _CityorEntityColumnName; }
            set { _CityorEntityColumnName = value; }
        }

        //for CityorEntityColumnValue
        private string _CityorEntityColumnValue = "";
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("CityorEntityColumnValue"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("KMT"),
         System.ComponentModel.DefaultValue("")
        ]
        public string CityorEntityColumnValue
        {
            get { return _CityorEntityColumnValue; }
            set { _CityorEntityColumnValue = value; }
        }

        //for LOBColumnName
        private string _LOBColumnName = "";
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("LOBColumnName"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("KMT"),
         System.ComponentModel.DefaultValue("")
        ]
        public string LOBColumnName
        {
            get { return _LOBColumnName; }
            set { _LOBColumnName = value; }
        }

        //for LOBColumnValue
        private string _LOBColumnValue = "";
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("LOBColumnValue"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("KMT"),
         System.ComponentModel.DefaultValue("")
        ]
        public string LOBColumnValue
        {
            get { return _LOBColumnValue; }
            set { _LOBColumnValue = value; }
        }

        //for RankColumnName
        private string _RankColumnName = "";
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("RankColumnName"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("KMT"),
         System.ComponentModel.DefaultValue("")
        ]
        public string RankColumnName
        {
            get { return _RankColumnName; }
            set { _RankColumnName = value; }
        }

        //for RankColumnValue
        private string _RankColumnValue = "";
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("RankColumnValue"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("KMT"),
         System.ComponentModel.DefaultValue("")
        ]
        public string RankColumnValue
        {
            get { return _RankColumnValue; }
            set { _RankColumnValue = value; }
        }

        //for NoDocumentsMessage
        private string _NoDocumentsMessage = "";
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("NoDocumentsMessage"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("KMT"),
         System.ComponentModel.DefaultValue("")
        ]
        public string NoDocumentsMessage
        {
            get { return _NoDocumentsMessage; }
            set { _NoDocumentsMessage = value; }
        }

        #endregion

        public TopDocumentsFromDocumentLibrary()
        {
            DocumentLibraryName = "DivisionalDocuments";
            DocumentFullURL = "/DivisionalDocuments/";
            CityorEntityColumnName = "CityOrEntity";
            CityorEntityColumnValue = "City1";
            LOBColumnName = "LOB";
            LOBColumnValue = "Residential";
            RankColumnName = "Rank";
            RankColumnValue = "5";
            NoDocumentsMessage = "No Documents Exists...!";
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
