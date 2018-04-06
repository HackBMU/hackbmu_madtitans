using System;
using System.Collections.Generic;
using System.Text;

namespace HelpingHand
{
    class GlobalMethods
    {

        public static string numberToStatus(string internalCountValue)
        {
            if (internalCountValue == "0")
            {
                return "Accepted";
            }
            else if (internalCountValue == "1")
            {
                return "Contacting";
            }
            else if (internalCountValue == "2")
            {
                return "Denied";
            }
            else if (internalCountValue == "3")
            {
                return "Blocked";
            }
            else if (internalCountValue == "4")
            {
                return "Open";
            }
            else
                return "Not Defined";
        }
    }
}
