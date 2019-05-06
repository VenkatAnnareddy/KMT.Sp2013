using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Administration;

namespace RSG.Inside.KMTAdmin
{
    /// <summary>
    /// 
    /// </summary>
    public class LoggingService : SPDiagnosticsServiceBase
    {
        public static string vsDiagnosticAreaName = "KMT Logging Service";
        public static string CategoryName = "KMT_CustomLog";
        public static uint uintEventID = 700; // Event ID
        private static LoggingService _Current;

        /// <summary>
        /// 
        /// </summary>
        public static LoggingService Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new LoggingService();
                }
                return _Current;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private LoggingService()
            : base("SharePoint Logging Service", SPFarm.Local)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<SPDiagnosticsArea> ProvideAreas()
        {
            List<SPDiagnosticsArea> areas = new List<SPDiagnosticsArea>
             {
              new SPDiagnosticsArea(vsDiagnosticAreaName, new List<SPDiagnosticsCategory>
               {
                new SPDiagnosticsCategory(CategoryName, TraceSeverity.Medium, EventSeverity.Error)
               })
              };
            return areas;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static string LogErrorInULS(string errorMessage)
        {
            string strExecutionResult = "Message Not Logged in ULS. ";
            try
            {
                SPDiagnosticsCategory category = LoggingService.Current.Areas[vsDiagnosticAreaName].Categories[CategoryName];
                LoggingService.Current.WriteTrace(uintEventID, category, TraceSeverity.Unexpected, errorMessage);
                strExecutionResult = "Message Logged";
            }
            catch (Exception ex)
            {
                strExecutionResult += ex.Message;
            }
            return strExecutionResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="tsSeverity"></param>
        /// <returns></returns>
        public static string LogErrorInULS(string errorMessage, TraceSeverity tsSeverity)
        {
            string strExecutionResult = "Message Not Logged in ULS. ";
            try
            {
                SPDiagnosticsCategory category = LoggingService.Current.Areas[vsDiagnosticAreaName].Categories[CategoryName];
                LoggingService.Current.WriteTrace(uintEventID, category, tsSeverity, errorMessage);
                strExecutionResult = "Message Logged";
            }
            catch (Exception ex)
            {
                strExecutionResult += ex.Message;
            }
            return strExecutionResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string LogErrorInULS(Exception ex)
        {
            string strExecutionResult = "Message Not Logged in ULS. ";
            try
            {
                string ExceptionMsg = "Source:{0}, Message:{1}, StackTracke:{2}";

                string logMsg = string.Format(ExceptionMsg, ex.Source.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                SPDiagnosticsCategory category = LoggingService.Current.Areas[vsDiagnosticAreaName].Categories[CategoryName];
                LoggingService.Current.WriteTrace(uintEventID, category, TraceSeverity.Unexpected, logMsg);
                strExecutionResult = "Message Logged";
            }
            catch (Exception ex1)
            {
                strExecutionResult += ex1.Message;
            }
            return strExecutionResult;
        }
    }
}