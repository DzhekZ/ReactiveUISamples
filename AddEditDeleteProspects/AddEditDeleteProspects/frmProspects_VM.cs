using System;
using IdeaBlade.Core;
using IdeaBlade.EntityModel;
using System.Windows;
using CommandUtilities;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace AddEditDeleteProspects
{
	using NUnit.Framework;

	[TestFixture, Description("Search form tests")]
	[RequiresSTA]
	class frmProspects_VM : INotifyPropertyChanged
	{
		TestsEntities mgr = new TestsEntities();

		public ObservableCollection<Prospect> prospects { get; set; }

		Prospect newprospect;

		private Prospect _prospect;   
		public Prospect   prospect { get { return _prospect; } set { _prospect = value; RaisePropertyChanged("prospect"); } }

		private string lbl;
		public string     label    { get { return lbl; }       set { lbl       = value; RaisePropertyChanged("label");    } }
		//Note: To see how RaisePropertyChanged sends content back to the View,  
		// use this instead of the previous line and see what happens...
		//public string label { get { return lbl; } set { lbl = value; } }
	
		private CommandMap _commands; 
		public CommandMap Commands { get { return _commands; } }

		public frmProspects_VM()
		{ 
			mgr = new TestsEntities();

			prospects = new ObservableCollection<Prospect>();

			_commands = new CommandMap();
			_commands.AddCommand("Find",   x => FindaP() );
			_commands.AddCommand("Add",    x => Add()    );
			_commands.AddCommand("Edit",   x => Edit()   );
			_commands.AddCommand("Delete", x => Delete(), x=> CanDelete() );
			_commands.AddCommand("Save",   x => Save()   );
			_commands.AddCommand("Cancel", x => Cancel() );
		}

		public void FindaP()
		{
			FindAProspect fap = new FindAProspect();
			fap.ShowDialog();
			if (App.ID != null)
			{ prospects.Clear();
				var q = mgr.Prospects.Where(x => x.ID == App.ID);
				q.Execute().ForEach(prospects.Add);
				prospect = prospects[0];
			}
		}

		[Test, Description("Test for at least one record")]
		[RequiresSTA]
		public void GetFirstRecord()
		{ if (prospects != null) prospects.Clear();
			var query = mgr.Prospects;
			query.Execute().ForEach(prospects.Add);
			Assert.Greater(prospects.Count, 0);
		}

		public void ManageControls(bool OnOff)
		{ for (int i = 0; i < App.Current.Windows.Count; i++)
			{ Window w = App.Current.Windows[i]; if (w.Title == "Prospects") { (w as frmProspects).Enabler(OnOff); } }
		}

		public void Add()
		{ newprospect = new Prospect();
		  newprospect.ID = Guid.NewGuid();
			prospect = newprospect;
			mgr.AddEntity(newprospect);
			ManageControls(false);
			label = "Adding...";
		}

		public void Edit()
		{ ManageControls(false); label = "Editing..."; }

		public void Delete()
		{ prospects.RemoveAt(0);
			prospect.EntityAspect.Delete();
			Save();
			prospect = null;
		}

		public bool CanDelete()
		{ return (prospect != null); }

		public void Save()
		{ mgr.SaveChanges(); ManageControls(true); label = ""; }

		public void Cancel()
		{ mgr.RejectChanges(); ManageControls(true); label = ""; }

		public event PropertyChangedEventHandler PropertyChanged = delegate {};
		private void RaisePropertyChanged(string property) { PropertyChanged(this, new PropertyChangedEventArgs(property)); }
	}
}