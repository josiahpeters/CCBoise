using System;

namespace CCBoise.iOSApp
{
	public class TableItem
	{
		public string Heading { get; set; }
		public string SubHeading { get; set; }
		public string ImagePath { get; set; }
		//public string Title { get; set; }

		public TableItem (string heading)
		{
			this.Heading = heading;
		}
	}
}

