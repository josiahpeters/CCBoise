using System;
using MonoTouch.UIKit;

namespace CCBoise.iOSApp
{
	public class TableViewController : UIViewController
	{
		UITableView table;

		public TableViewController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			table = new UITableView(View.Bounds); // defaults to Plain style
			table.AutoresizingMask = UIViewAutoresizing.All;
			string[] tableItems = new string[] {"Messages","Daily","Connect","About"};
			table.Source = new TableSource(tableItems);
			Add (table);
		}
	}
}

