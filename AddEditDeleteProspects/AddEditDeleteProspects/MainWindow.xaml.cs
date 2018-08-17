using System.Windows;

namespace AddEditDeleteProspects
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{ InitializeComponent(); }

		private void MenuItem_Click(object sender, RoutedEventArgs e)
		{ App.Current.Shutdown(); }

		private void TablesProspects_Click(object sender, RoutedEventArgs e)
		{ frmProspects pr = new frmProspects();
			pr.Show();
		}

	}
}