using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using EmailScanner.Helpers;

namespace EmailScanner
{
    public partial class ThisAddIn
    {
        private List<InspectorWrapper> _inspectorWrappersValue = new List<InspectorWrapper>();
        private Outlook.Inspectors _inspectors;
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            _inspectors = this.Application.Inspectors;
            _inspectors.NewInspector += new Outlook.InspectorsEvents_NewInspectorEventHandler(Inspectors_NewInspector);
        }

        /// <summary>
        /// Create New Inspector for Mail items
        /// </summary>
        /// <param name="Inspector"></param>
        void Inspectors_NewInspector(Outlook.Inspector Inspector)
        {
            try
            {
                if (Inspector?.CurrentItem != null)
                {
                    if (Inspector.CurrentItem is Outlook.MailItem)
                    {
                        _inspectorWrappersValue.Add(new InspectorWrapper(Inspector));
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see http://go.microsoft.com/fwlink/?LinkId=506785
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
