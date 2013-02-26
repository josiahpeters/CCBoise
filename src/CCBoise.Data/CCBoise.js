// this is the default JSON that will ship with the CCBoise Mobile app
{
	Elements: [
		{
			Heading: "Messages",
			ControllerType: "TableViewController",
			Elements: [
				{
					Heading: "Video",
					ControllerType: "TableViewController",
					ApiUrl: "http://www.ccboise.org/api/messages/video"
				},
				{
					Heading: "Audio",
					ControllerType: "TableViewController",
					ApiUrl: "http://www.ccboise.org/api/messages/audio"
				}
			]
		},
		{
			Heading: "Daily",
			ControllerType: "TableViewController",
			Elements: [
				{
					Heading: "Pathway Devotions",
					ControllerType: "TableViewController",
					ApiUrl: "http://www.ccboise.org/api/daily/devotionals",
					ApiUrlDetail: "http://www.ccboise.org/api/daily/devotional/{ItemId}",
				},
				{
					Heading: "Prayer",
					ControllerType: "TableViewController",
					ApiUrl: "http://www.ccboise.org/api/daily/prayer"
				}
			]
		},
		{
			Heading: "Connect",
			ControllerType: "TableViewController",
			Elements: [
				{
					Heading: "Events",
					ControllerType: "TableViewController",
					ApiUrl: "http://www.ccboise.org/api/daily/devotionals",
					ApiUrlDetail: "http://www.ccboise.org/api/daily/devotional/{ItemId}",
				},
				{
					Heading: "Calendar",
					ControllerType: "TableViewController",
					ApiUrl: "http://www.ccboise.org/api/connect/calendar",
					ApiUrlDetail: "http://www.ccboise.org/api/connect/calendar-event/{ItemId}",
					},
				{
					Heading: "Social Media",
					ControllerType: "WebViewController",
					WebContent: "<html><body>....</body></html>"
				},
				{
					Heading: "Location and Directions",
					ControllerType: "WebViewController",
					WebContent: "<html><body>....</body></html>"
				}
			]
		},
		{
			Heading: "About",
			ControllerType: "TableViewController",
			Elements: [
				{
					Heading: "Service Times",
					ControllerType: "WebViewController",
					WebContent: "<html><body>....</body></html>"
				},
				{
					Heading: "Ministries",
					ControllerType: "WebViewController",
					WebContent: "<html><body>....</body></html>"
				}, {
					Heading: "Pastors",
					ControllerType: "WebViewController",
					WebContent: "<html><body>....</body></html>"
				},
				{
					Heading: "Contact Information",
					ControllerType: "WebViewController",
					WebContent: "<html><body>....</body></html>"
				},
				{
					Heading: "Website",
					ControllerType: "WebViewController",
					WebContent: "<html><body>....</body></html>"
				}
			]
		}
	]
}