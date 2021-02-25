using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CoronaDataAlert
{
    public partial class MainWindow : Form
    {

        private const string TriggerFileName = @"trigger.txt";
        private string currDir;
        private static bool mouseDown;
        private static Point lastLocation;
        private static System.Windows.Forms.Timer checkTimer;

        private const int USCasesGrownTriggerAmount = 1000;
        private const int CACasesGrownTriggerAmount = 300;
        private int USCasesGrownSinceLastPopup;
        private int CACasesGrownSinceLastPopup;
        private const int checkIntervalInMinutes = 5;
        private static DateTime _triggerDate;

        int currentUSCases = 0;
        int currentCACases = 0;

        Thread timeLoop;

        public MainWindow()
        {
            InitializeComponent();
            updateControlLocations();

            currDir = Directory.GetCurrentDirectory() + "\\" + TriggerFileName;

            updateNumbers();

            if (!File.Exists(currDir))
            {
                using (StreamWriter sw = File.CreateText(currDir))
                {
                    sw.WriteLine(DateTime.Now.AddMinutes(checkIntervalInMinutes));
                }
            }
            using (StreamReader sr = File.OpenText(currDir))
            {
                _triggerDate = DateTime.Parse(sr.ReadToEnd());
            }

            setNextAlert();

            timeLoop = new Thread(timeCheckLoop);
            timeLoop.Start();
        }

        void timeCheckLoop()
        {
            while (true)
            {
                if (DateTime.Now >= _triggerDate.AddMinutes(checkIntervalInMinutes))
                {
                    using (StreamWriter sw = File.CreateText(currDir))
                    {
                        sw.WriteLine(DateTime.Now.AddMinutes(checkIntervalInMinutes));
                        _triggerDate = DateTime.Now.AddMinutes(checkIntervalInMinutes);
                    }
                    setNextAlert();
                }
                System.Threading.Thread.Sleep(60000 * checkIntervalInMinutes + 30000); // Sleep for X minutes + 30sec
            }
        }

        private void updateControlLocations()
        {
            this.btn_Close.Location = new Point(this.ClientSize.Width - 20, 0);
            this.btn_Minimize.Location = new Point(this.Size.Width - 40, 0);
            this.btn_DragRegion.Size = new Size(this.Size.Width - 40, 20);
            this.lbl_USCases.Location = new Point(0, this.btn_DragRegion.Location.Y + this.btn_DragRegion.Size.Height + 15);
            this.lbl_CACases.Location = new Point(0, this.btn_DragRegion.Location.Y + this.btn_DragRegion.Size.Height + 115);
        }

        private void updateNumbers()
        {
            string htmlString = "";
            try
            {
                htmlString = new System.Net.WebClient().DownloadString(@"https://www.worldometers.info/coronavirus/country/us/");
            }
            catch (System.Net.WebException) {; }

            int startIndex, endIndex;

            string US_startStrRef = @"<span style=""color:#aaa"">";
            string US_endStrRef = @"</span>";
            startIndex = htmlString.IndexOf(US_startStrRef, 0, htmlString.Length - 1) + US_startStrRef.Length;
            endIndex = htmlString.IndexOf(US_endStrRef, startIndex, htmlString.Length - 1 - startIndex);
            string USString = htmlString.Substring(startIndex, endIndex - startIndex).Trim();
            USString = USString.Replace(",", "");
            currentUSCases = Convert.ToInt32(USString);

            string CA_startStrRef = "href=\"/coronavirus/usa/california/\">California</a> </td>\n<td style=\"font-weight: bold; text-align:right\">";
            string CA_endStrRef = @"</td>";
            startIndex = htmlString.IndexOf(CA_startStrRef, 0, htmlString.Length - 1, StringComparison.Ordinal) + CA_startStrRef.Length;
            endIndex = htmlString.IndexOf(CA_endStrRef, startIndex, htmlString.Length - 1 - startIndex);
            string CAString = htmlString.Substring(startIndex, endIndex - startIndex).Trim();
            CAString = CAString.Replace(",", "");
            currentCACases = Convert.ToInt32(CAString);

            this.lbl_USCases.Text = "Total US cases: " + this.currentUSCases;
            this.lbl_CACases.Text = "Total CA cases: " + this.currentCACases;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            timeLoop.Abort();            
            this.Close();
        }
        private void btn_Minimize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Minimized;

            MainWindow_ResizeEnd(sender, e);
        }
        private void MainWindow_ResizeEnd(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                trayIcon.Visible = true;
                this.ShowInTaskbar = false;
            }
            else
                updateControlLocations();
        }
        private void trayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            restoreWindow();
        }
        private void btn_DragRegion_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }
        private void btn_DragRegion_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }
        private void btn_DragRegion_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void restoreWindow()
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            trayIcon.Visible = false;
        }

        private void setNextAlert()
        {
            disableAlert();
            checkTimer = new System.Windows.Forms.Timer();

            using (StreamWriter sw = File.CreateText(currDir))
            {
                sw.WriteLine(DateTime.Now.AddMinutes(checkIntervalInMinutes));
                _triggerDate = DateTime.Now.AddMinutes(checkIntervalInMinutes);
            }

            checkTimer.Tick += new EventHandler(alertEvent);
            checkTimer.Interval = checkIntervalInMinutes * 60000;
            checkTimer.Start();
        }
        private void alertEvent(object source, EventArgs e)
        {
            int oldUSCases = this.currentUSCases;
            int oldCACases = this.currentCACases;
            updateNumbers();
            if (oldUSCases != this.currentUSCases || oldCACases != this.currentCACases)
            {
                int USDiff = this.currentUSCases - oldUSCases;
                if (USDiff != 0)
                {
                    USCasesGrownSinceLastPopup += Math.Abs(USDiff);
                    this.lbl_USCases.Text = "Total US cases: " + this.currentUSCases + "   " + ((USCasesGrownSinceLastPopup > 0) ? '+' : '-') + USCasesGrownSinceLastPopup;
                }

                int CADiff = this.currentCACases - oldCACases;
                if (CADiff != 0)
                {
                    CACasesGrownSinceLastPopup += Math.Abs(CADiff);
                    this.lbl_CACases.Text = "Total CA cases: " + this.currentCACases + "   " + ((CACasesGrownSinceLastPopup > 0) ? '+' : '-') + CACasesGrownSinceLastPopup;
                }

                if (USCasesGrownSinceLastPopup >= USCasesGrownTriggerAmount || CACasesGrownSinceLastPopup >= CACasesGrownTriggerAmount)
                {
                    USCasesGrownSinceLastPopup = 0;
                    CACasesGrownSinceLastPopup = 0;
                    restoreWindow();
                }
            }

            setNextAlert();
        }
        private void disableAlert()
        {
            if (checkTimer != null)
            {
                checkTimer.Stop();
                checkTimer.Enabled = false;
                checkTimer = null;
            }
        }
        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.Suspend) disableAlert();
            else if (e.Mode == PowerModes.Resume) setNextAlert();
        }
    }
}
