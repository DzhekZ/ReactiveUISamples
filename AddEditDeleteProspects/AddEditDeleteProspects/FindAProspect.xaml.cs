using System;
using System.Windows;
using System.Collections.ObjectModel;
using IdeaBlade.Core;
using IdeaBlade.EntityModel;

namespace AddEditDeleteProspects
{
	using NUnit.Framework;

	[TestFixture, Description("Search form tests")]
	[RequiresSTA]
	public partial class FindAProspect : Window
	{
		public ObservableCollection<Prospect> prospects { get; set; }
		TestsEntities mgr = new TestsEntities();

		public FindAProspect()
		{ InitializeComponent();
			prospects = new ObservableCollection<Prospect>();
		}

		private void btnSelect_Click(object sender, RoutedEventArgs e)
		{ App.ID = prospects[dataGrid1.SelectedIndex].ID; Close(); }

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{ App.ID = null; Close(); }

		private void btnShow_Click(object sender, RoutedEventArgs e)
		{ if (prospects != null) prospects.Clear();
			string ln = textBox1.Text.ToString().TrimEnd();
			var query = mgr.Prospects.Where(x => x.LastName.StartsWith(ln)).OrderBy(x=>x.LastName).ThenBy(x=>x.FirstName);
			query.Execute().ForEach(prospects.Add);
			dataGrid1.ItemsSource = prospects;
		}

		[Test, Description("Returns at least one LastName starting with 'S' or 'P'; fails matching on 'A'")]
		[RequiresSTA]
		public void DataTest([Values ("A","S","P")] string ln)
		{ if (prospects != null) prospects.Clear();
			var query = mgr.Prospects.Where(x => x.LastName.StartsWith(ln)).OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
			query.Execute().ForEach(prospects.Add);
			if (ln == "A") { Assert.AreEqual(prospects.Count, 0); }
 								else { Assert.Greater(prospects.Count, 0); }
		}

	}
}