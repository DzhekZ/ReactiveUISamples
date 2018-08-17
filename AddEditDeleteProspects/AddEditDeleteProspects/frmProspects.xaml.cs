using System.Windows;
using System.Collections.ObjectModel;
using IdeaBlade.Core;
using IdeaBlade.EntityModel;

namespace AddEditDeleteProspects
{
	public partial class frmProspects : Window
	{ 
		frmProspects_VM ViewModel;

		public frmProspects()
		{ InitializeComponent();
			ViewModel = new frmProspects_VM();
			DataContext = ViewModel;
			// Some people use this one-line "improvement" instead: 
			// Loaded += delegate { DataContext = new Prospects_VM(); }; }
			// However, the explicit declaration makes calling Enabler from the ViewModel simpler.
			Enabler(true);
		}

		private void btnClose_Click(object sender, RoutedEventArgs e)
		{ Close(); }

		public void Enabler ( bool OnOff)  // called from within the ViewModel!
		{ btnAdd.IsEnabled = OnOff;
			btnClose.IsEnabled = OnOff;
			btnDelete.IsEnabled = OnOff;
			btnEdit.IsEnabled = OnOff;
			btnFind.IsEnabled = OnOff;
			btnSave.IsEnabled = !OnOff;
			btnCancel.IsEnabled = !OnOff;

			txtFirstName.IsEnabled = !OnOff;
			txtLastName.IsEnabled = !OnOff;
			txtAddress.IsEnabled = !OnOff;
			txtCity.IsEnabled = !OnOff;
			txtState.IsEnabled = !OnOff;
			txtZIP.IsEnabled = !OnOff;
		}
	}
}