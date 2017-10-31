using System;

using WatchKit;
using Foundation;
using System.Threading.Tasks;
using SocketLite.Services;
using System.Text;
using System.Collections.Generic;

namespace CancelloiOS.CancelloWatchExtension
{
    public partial class InterfaceController : WKInterfaceController
    {
        protected InterfaceController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        int i = 0;
		partial void ButtonTapped()
		{
            i++;
            //Devo inviare ogni volta un messaggio diverso se no fa una sorta di distinct e non lo rimanda
            WCSessionManager.SharedManager.UpdateApplicationContext(new Dictionary<string, object>() { { "PippaCancello", i.ToString() } });
		}
		
        public override void Awake(NSObject context)
        {
            base.Awake(context);

            // Configure interface objects here.
            Console.WriteLine("{0} awake with context", this);
        }

        public override void WillActivate()
        {
            // This method is called when the watch view controller is about to be visible to the user.
            Console.WriteLine("{0} will activate", this);
        }

        public override void DidDeactivate()
        {
            // This method is called when the watch view controller is no longer visible to the user.
            Console.WriteLine("{0} did deactivate", this);
        }
    }
}
