using System.Windows;
using System.Windows.Input;

namespace PointInTime.Views
{
    /// <summary>
    /// Interaction logic for Blackout.xaml
    /// </summary>
    public partial class Blackout : Window
    {
        public Blackout()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }
    }
}
