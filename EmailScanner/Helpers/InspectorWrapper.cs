using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace EmailScanner.Helpers
{
    /// <summary>
    ///  An inspector wrapper handles multiple instances of Microsoft Outlook inspector windows. 
    /// </summary>
    public class InspectorWrapper
    {
        #region Members - Properties
        private Outlook.MailItem _mail;
        private Outlook.Inspector _inspector;
        System.Timers.Timer _mailComposeTimer = null;
        bool isWarningDisplayed = false;
        bool isBlacklisted = false;
        #endregion

        #region Methods
        public InspectorWrapper(Outlook.Inspector Inspector)
        {
            _inspector = Inspector;

            if (_inspector == null)
            {
                return;
            }
            ((Outlook.InspectorEvents_Event)_inspector).Close -= InspectorWrapper_Close;
            ((Outlook.InspectorEvents_Event)_inspector).Close += InspectorWrapper_Close;

            _mail = _inspector.CurrentItem as Outlook.MailItem;
            InitiateTimer();
        }

        void InspectorWrapper_Close()
        {
            StopTimer();

            ((Outlook.InspectorEvents_Event)_inspector).Close -=
                new Microsoft.Office.Interop.Outlook.InspectorEvents_CloseEventHandler(InspectorWrapper_Close);
            
            // Clean up resources and do a GC.Collect().
            _inspector = null;
            _mail = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #region Timer
        private void InitiateTimer()
        {
            _mailComposeTimer = new System.Timers.Timer();
            _mailComposeTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _mailComposeTimer.Interval = 2000;
            _mailComposeTimer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            StopTimer();
            bool hasBody = IsEmailHasBody();
            if (!hasBody)
            {
                InitiateTimer();
            }
            else
            {
                // Perform check
                CheckEmailBody();
                InitiateTimer();
            }
        }

        private void CheckEmailBody()
        {
            string body = _mail.Body;

        }

        private bool IsEmailHasBody()
        {
            var length = 0;
            if (!String.IsNullOrEmpty(_mail.Body))
            {
                length = _mail.Body.Length;
            }
            if (length > 0)
            {
                return true;
            }
            return false;
        }

        private void StopTimer()
        {
            if (_mailComposeTimer != null)
            {
                _mailComposeTimer.Enabled = false;
                _mailComposeTimer = null;
            }
        }
        #endregion

        #endregion
    }
}
