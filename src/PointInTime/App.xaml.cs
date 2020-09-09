using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;
using PointInTime.Views;

namespace PointInTime
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Opens specific windows depending on the set parameters.
        /// </summary>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length > 0)
            {
                if (e.Args[0].ToLower().Substring(0, 2) == "/c")
                {
                    LoadSettings();
                }
                else if (e.Args[0].ToLower().Substring(0, 2) == "/s")
                {
                    LoadScreenSaver();
                }
                else if (e.Args[0].ToLower().Substring(0, 2) == "/p")
                {
                    // TODO: get the preview to work.
                    // e.Args[1] will be the window handle of the Windows screensaver settings window.
                    //LoadScreenSaver(true);
                }
            }
            else
            {
                LoadScreenSaver();
            }
        }

        /// <summary>
        /// Load the screensaver window.
        /// </summary>
        /// <param name="isPreview">Indicates whether the screensaver should be launched in preview mode.</param>
        private void LoadScreenSaver(bool isPreview = false)
        {
            foreach (Screen s in Screen.AllScreens)
            {
                Window window;
                if (s != Screen.PrimaryScreen)
                {
                    if (isPreview)
                    {
                        continue;
                    }
                    window = new Blackout();
                }
                else
                {
                    window = new ScreenSaver();
                }
                window.Left = s.WorkingArea.Left;
                window.Top = s.WorkingArea.Top;
                window.Width = s.WorkingArea.Width;
                window.Height = s.WorkingArea.Height;
                window.Topmost = true;
                window.Show();
            }
        }

        /// <summary>
        /// Load the settings window.
        /// </summary>
        private void LoadSettings()
        {
            Settings settings = new Settings();
            settings.Show();
        }
    }
}
