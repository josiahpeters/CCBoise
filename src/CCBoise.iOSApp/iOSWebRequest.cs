using CCBoise.Core;
using MonoTouch.Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CCBoise.iOSApp
{
    class iOSWebRequest : IWebRequest
    {        
        public void GetUrl(string url, Action<Stream, Exception> callback)
        {
            var request = new NSUrlRequest(new NSUrl(url), NSUrlRequestCachePolicy.UseProtocolCachePolicy, 60);
            var connection = new NSUrlConnection(request, new ConnectionDelegate((data, error) =>
            {                
                if (error == null)
                {
                    callback(data, null);
                }
                else
                    callback(data, new Exception(String.Format("Error code: {0}", error.Code)));
            }));
        }
    }
    class ConnectionDelegate : NSUrlConnectionDelegate
    {
        Action<Stream, NSError> callback;
        NSMutableData buffer;

        public ConnectionDelegate(Action<Stream, NSError> callback)
        {
            this.callback = callback;
            buffer = new NSMutableData();
        }

        public override void ReceivedResponse(NSUrlConnection connection, NSUrlResponse response)
        {
            buffer.SetLength(0);
        }

        public override void FailedWithError(NSUrlConnection connection, NSError error)
        {
            callback(null, error);
        }

        public override void ReceivedData(NSUrlConnection connection, NSData data)
        {
            buffer.AppendData(data);
        }

        public override void FinishedLoading(NSUrlConnection connection)
        {
            callback(buffer.AsStream(), null);
        }
    }
}
