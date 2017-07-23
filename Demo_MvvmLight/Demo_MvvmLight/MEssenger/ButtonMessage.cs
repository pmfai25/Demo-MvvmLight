using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_MvvmLight.MEssenger
{
    public class ButtonMessage
    {public string ButtonText { get;private set; }
        public ButtonMessage(string bt )
        {
            ButtonText = bt;
        }
        
    }
}
