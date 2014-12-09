using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mova.Logic.Models.Helpers
{
    public class HelperDate
    {
        public static string GetDayFromDate(DateTime dt) {

            return GetFrenchDay(dt.DayOfWeek.ToString());
        }

        private static string GetFrenchDay(String s){
        
            string retour = "";

            switch(s){
                case "Sunday":
                    retour = "Dimanche";
                    break;
                case "Monday":
                    retour = "Lundi";
                    break;
                case "Tuesday":
                    retour = "Mardi";
                    break;
                case "Wednesday":
                    retour = "Mercredi";
                    break;
                case "Thursday":
                    retour = "Jeudi";
                    break;
                case "Friday":
                    retour = "Vendredi";
                    break;
                case "Saturday":
                    retour = "Samedi";
                    break;
            }
        
            return retour;
        }

    }
}
