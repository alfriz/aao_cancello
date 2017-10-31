using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CancelloiOS.CancelloWatchExtension;
using SocketLite.Services;
using UIKit;
using WatchConnectivity;

namespace CancelloiOS
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            WCSessionManager.SharedManager.ApplicationContextUpdated += DidReceiveApplicationContext;
        }

        public override void ViewDidUnload()
        {
            base.ViewDidUnload();
            WCSessionManager.SharedManager.ApplicationContextUpdated -= DidReceiveApplicationContext;
        }

        private void DidReceiveApplicationContext(WCSession session, Dictionary<string, object> applicationContext)
        {
            var message = (string)applicationContext["PippaCancello"];
            if(message != null)
                Task.Run (async () => await OpenCancello());
        }

        async Task OpenCancello()
        {
            try
            {
                var tcpClient = new TcpSocketClient();
                await tcpClient.ConnectAsync("192.168.0.170", "8090");

                var command = "/OUT1";

                var bytes = Encoding.UTF8.GetBytes(command);
                await tcpClient.WriteStream.WriteAsync(bytes, 0, bytes.Length);
                tcpClient.Disconnect();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
