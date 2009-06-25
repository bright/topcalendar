using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientUI.Helpers
{
    class BasicHelper
    {

        public static string DayOfWeek(int day) {

            

            switch (day) {

                case 0: return "Niedziela";
                case 1: return "Poniedziałek";
                case 2: return "Wtorek";
                case 3: return "Środa";
                case 4: return "Czwartek";
                case 5: return "Piątek";
                case 6: return "Sobota";
                
                default: return "";
            }

        }

    }
}
