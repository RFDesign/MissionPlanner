using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFDCommon
{
    public class MessageBoxEventArgs : EventArgs
    {
        public string Title { get; set; }        
        public string Text { get; set; }

        public MessageBoxEventArgs(string title, string text)
        {
            Title = title;            
            Text = text;
        }
    }
    
    public class ConsoleEventArgs : EventArgs
    {
        public string Text { get; set; }
        public ConsoleEventArgs(string text)
        {
            Text = text;
        }
    }
}
