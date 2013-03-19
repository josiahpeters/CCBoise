{
    "title": "Calvary Chapel Boise",
	"tabs": [
		{
		    "title": "Messages",
		    "icon": "Images/TabIcons/18-envelope.png",
		    "id": "messages",
		    "sections": [
                {
                    "elements": [
                        {
                            "title": "Video",
                            "type": "customRoot",
                            "childType":"detailedImage",
                            "childAction":"video",
                            "contentNode":"videoSrc1",
                            "apiUrl":"http://www.ccboise.org/api/messages/video",
                            "sections": [
							
                            ]
                        },
                        {
                            "title": "Audio",
                            "type": "customRoot",
                            "childType":"htmlContent",
                            "childAction":"audio",
                            "contentNode":"src",
                            "apiUrl":"http://www.ccboise.org/api/messages/audio",
                            "sections": [
							
                            ]
                        }
                    ]
                }
		    ]
		},
		{
		    "title": "Daily",		
		    "icon": "Images/TabIcons/83-calendar.png",
		    "sections": [
                {
                    	
                    "elements": [
                        {
                            "title": "Pathway Devotions",
                            "type": "customRoot",
                            "childType":"htmlContent",
                            "contentNode":"content",
                            "apiUrl":"http://www.ccboise.org/api/daily/devotionals",
                            "apiItemUrl":"http://www.ccboise.org/api/daily/devotional/{0}",
                            "elements": [
                            ]
                        },
                        {
                            "title": "Prayer Task Force",
                            "type": "customRoot",                       
                            "childType":"htmlContent",
                            "contentNode":"description",
                            "apiUrl":"http://www.ccboise.org/api/daily/prayer",
                            "elements": [
                            ]
                        }
                    ]
                }
		    ]
		},
        {
            "title": "Home",
            "selectedTab": true,
            "icon": "Images/TabIcons/53-house.png",
            "sections": [
                {
                    "elements": [
                        {
                            "caption": "Welcome to the Calvary Chapel Boise Mobile app",
                            "type": "string"
                        }
                    ]
                }
            ]
        },
		{
		    "title": "Connect",		
		    "icon": "Images/TabIcons/55-network.png",
		    "sections": [
                {
                    	
                    "elements": [
                        {
                            "title": "Events",
                            "type": "customRoot",
                            "childType":"htmlContent",
                            "contentNode":"eventDescription",
                            "apiUrl":"http://www.ccboise.org/api/connect/events",
                            "sections": [
							                
                            ]
                        },
                        {
                            "title": "Calendar",
                            "type": "customRoot",
                            "childType":"htmlContent",
                            "contentNode":"description",
                            "apiUrl":"http://www.ccboise.org/api/connect/calendar",
                            "apiItemUrl":"http://www.ccboise.org/api/connect/calendar-event/{0}",
                            "elements": [
                            ]
                        },
                        {
                            "title": "Social Media",
                            "type": "root",
                            "elements": [
                            ]
                        },
                        {
                            "caption": "Location and Directions",
                            "type": "htmlstring",
                            "html": "<html><body><h1>Test</h1><p>Hello world</p>"
                        }
                    ]
                }
		    ]
		},
		{
		    "title": "About",		
		    "icon": "Images/TabIcons/60-signpost.png",
		    "sections": [
                {
                    	
                    "elements": [
                        {
                            "title": "Service Times",
                            "type": "root",
                            "sections": [
                                {
                                    "header": "Saturday Night",
                                    "elements": [
                                        {
                                            "type": "string",
                                            "caption": "6:00 PM"
                                        }
                                    ]
                                },
                                {
                                    "header": "Sunday Morning",
                                    "elements": [
                                        {
                                            "type": "string",
                                            "caption": "9:15 AM"
                                        },
                                        {
                                            "type": "string",
                                            "caption": "11:15 AM"
                                        }
                                    ]
                                },
                                {
                                    "header": "Sunday Morning Latino Service",
                                    "elements": [
                                        {
                                            "type": "string",
                                            "caption": "2:00 PM"
                                        }
                                    ]
                                },
                                {
                                    "header": "Sunday Night (College / Young Adults)",
                                    "elements": [
                                        {
                                            "type": "string",
                                            "caption": "7:30 PM"
                                        }
                                    ]
                                }
                                ,
                                {
                                    "header": "Wednesday Night",
                                    "elements": [
                                        {
                                            "type": "string",
                                            "caption": "6:30 PM"
                                        }
                                    ]
                                }

                            ]	
                        },
                        {
                            "title": "Ministries",
                            "type": "root",
                            "sections": [
                                {
                                    "header": "Children",
                                    "elements": [
                                        {
                                            "type": "html",
                                            "caption": "Nursery",
                                            "url": "http://www.ccboise.org/our-church/nursery"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "Preschool Elementary",
                                            "url": "http://www.ccboise.org/our-church/preschool-elementary"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "Calvary Christian School",
                                            "url": "http://www.ccboise.org/our-church/calvary-christian-school"
                                        }
                                    ]
                                },
                                {
                                    "header": "Students",
                                    "elements": [
                                        {
                                            "type": "html",
                                            "caption": "Jr. High",
                                            "url": "http://www.ccboise.org/our-church/jr-high"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "Sr. High",
                                            "url": "http://www.ccboise.org/our-church/sr-high"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "College Young Adult",
                                            "url": "http://www.ccboise.org/our-church/college-young-adult"
                                        }
                                    ]
                                },                        
                                {
                                    "header": "Adults",
                                    "elements": [
                                        {
                                            "type": "html",
                                            "caption": "Adoption",
                                            "url": "http://www.ccboise.org/our-church/adoption"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "Apologetics",
                                            "url": "http://www.ccboise.org/our-church/apologetics"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "Care Ministry",
                                            "url": "http://www.ccboise.org/our-church/care-ministry"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "Core Small Groups",
                                            "url": "http://www.ccboise.org/our-church/small-groups"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "Divorce",
                                            "url": "http://www.ccboise.org/our-church/divorce"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "Family Ministries",
                                            "url": "http://www.ccboise.org/our-church/family-ministries"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "Marriage",
                                            "url": "http://www.ccboise.org/our-church/marriage"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "Meals Ministry",
                                            "url": "http://www.ccboise.org/our-church/meals-ministry"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "Men's Ministry",
                                            "url": "http://www.ccboise.org/our-church/mens-ministry"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "Mid-Week Bible Study",
                                            "url": "http://www.ccboise.org/our-church/mid-week-bible-study"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "Ministry Discipleship School",
                                            "url": "http://www.ccboise.org/our-church/ministry-discipleship-school"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "Parenting",
                                            "url": "http://www.ccboise.org/our-church/parenting"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "Post College & Career",
                                            "url": "http://www.ccboise.org/our-church/post-college-career"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "Premarital Discipleship",
                                            "url": "http://www.ccboise.org/our-church/premarital-discipleship"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "Prayer",
                                            "url": "http://www.ccboise.org/our-church/prayer"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "Singles Ministry",
                                            "url": "http://www.ccboise.org/our-church/singles-ministry"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "School of the Bible",
                                            "url": "http://www.ccboise.org/our-church/school-bible"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "Single Parents",
                                            "url": "http://www.ccboise.org/our-church/single-parents"
                                        },
                                        {
                                            "type": "html",
                                            "caption": "Women's Ministry",
                                            "url": "http://www.ccboise.org/our-church/womens-ministry"
                                        }
                                    ]
                                }
                            ]
                        },
                        {
                            "caption": "Pastors",
                            "type": "htmlstring",
                            "html": "<html><body><h1>Test</h1><p>Hello world</p>"
                        },
                        {
                            "title": "Contact Information",
                            "type": "root",
                            "sections": [
                                {
                                    "header": "Phone",
                                    "elements": [
                                        {
                                            "type": "actionUrl",
                                            "caption": "208-321-7440",
                                            "url": "tel:12083217440"
                                        }
                                    ]
                                },
                                {
                                    "header": "Fax",
                                    "elements": [
                                        {
                                            "type": "string",
                                            "caption": "208-321-7440"
                                        }
                                    ]
                                },
                                {
                                    "header": "Address",
                                    "elements": [
                                        {
                                            "type": "actionUrl",
                                            "caption": "Calvary Chapel Boise\n123 Auto Drive \nBoise, Idaho 83709",
                                            "url": "http://maps.apple.com/?q=123+Auto+Drive+Boise,+Idaho+83709"
                                        }
                                    ]
                                }

                            ]				    
                        },
                        {
                            "caption": "Website",
                            "type": "htmlstring",
                            "html": "<html><body><h1>Test</h1><p>Hello world</p>"
                        }
                    ]
                }
		    ]
		}
	]
}