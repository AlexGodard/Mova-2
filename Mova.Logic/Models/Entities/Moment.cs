using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mova.Logic.Models
{
    /// <summary>
    /// Contient les moments de la journée selon l’heure.
    /// </summary>
    public class Moment
    {
        #region Propriétés

        public int IdMoment { get; set; }

        /// <summary>
        /// Le nom du moment. Exemple: matin
        /// </summary>
        public string NomMoment { get; set; }

        /// <summary>
        /// Heure à laquelle le moment débute.
        /// </summary>
        public DateTime heureDebut { get; set; }

        /// <summary>
        /// Heure à laquelle le moment prend fin.
        /// </summary>
        public DateTime heureFin { get; set; }

        #endregion

        #region Methodes Utilitaires

        /// <summary>
        /// Méthode qui retourne une chaine de caractères dans le format suivant : Jour MomentDeLaJournee
        /// </summary>
        /// <returns>String</returns>
        public static string GetStringTime()
        {
            Moment momentActuel = GetMomentOfTheDay();

            return GetStringDayOfWeek() + " " + momentActuel.NomMoment.ToString();
        }

        /// <summary>
        /// Méthode qui retourne une chaine de caractères du jour de la semaine
        /// </summary>
        /// <param name="dt">DateTime : Un objet DateTime</param>
        /// <returns>String</returns>
        private static string GetStringDayOfWeek()
        {

            DateTime dt = DateTime.Now;

            switch (dt.DayOfWeek.ToString())
            {
                case "Sunday":
                    return "Dimanche";
                case "Monday":
                    return "Lundi";
                case "Tuesday":
                    return "Mardi";
                case "Wednesday":
                    return "Mercredi";
                case "Thursday":
                    return "Jeudi";
                case "Friday":
                    return "Vendredi";
                default:
                    return "Samedi";

            }
        }

        /// <summary>
        /// Méthode qui retourne une chaine de caractères (En Francais) du moment de la journée (e.g. Matin) 
        /// </summary>
        /// <param name="dt">DateTime : Un objet DateTime</param>
        /// <returns>String</returns>
        private static Moment GetMomentOfTheDay()
        {
            Moment moment = new Moment();
            DateTime dt = DateTime.Now;


            if (IsMorning(dt.Hour))
            {
                moment = new Moment() { IdMoment = 1, NomMoment = "Matin" };
            }
            else if (IsAfterNoon(dt.Hour))
            {
                moment = new Moment() { IdMoment = 2, NomMoment = "Après-midi" };
            }
            else if (IsEvening(dt.Hour))
            {
                moment = new Moment() { IdMoment = 3, NomMoment = "Soir" };
            }
            else
            {
                moment = new Moment() { IdMoment = 4, NomMoment = "Nuit" };
            }

            return moment;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Integer : Le ID du moment actuel correspondant à la base de données</returns>
        public static int GetIDMomentNow()
        {
            return GetMomentOfTheDay().IdMoment;
        }

        /// <summary>
        /// Gabriel Piché Cloutier - 2014-11-19
        /// </summary>
        /// <returns></returns>
        public static bool GetEstOuvrable()
        {
            DateTime dt = DateTime.Now;

            switch (dt.DayOfWeek.ToString())
            {
                case "Sunday":
                    return false;
                case "Monday":
                    return IsWorkingTime(dt);
                case "Tuesday":
                    return IsWorkingTime(dt);
                case "Wednesday":
                    return IsWorkingTime(dt);
                case "Thursday":
                    return IsWorkingTime(dt);
                case "Friday":
                    return IsWorkingTime(dt);
                default:
                    return false;

            }
        }

        //Logique derrière tout ça : Tu penses pas aller travailler passé midi et tu peux commencer à penser à travailler à 5 heure du matin
        private static bool IsWorkingTime(DateTime dt)
        { 
            return (dt.Hour >=5 && dt.Hour <= 12)? true : false;
        }

        /// <summary>
        /// Méthode qui dit si une heure est considérée comme étant le matin ou non
        /// </summary>
        /// <param name="i">Int : L'heure à analyser</param>
        /// <returns>Boolean</returns>
        private static bool IsMorning(int i)
        {
            return (i >= 4 && i < 12) ? true : false;
        }

        /// <summary>
        /// Méthode qui dit si une heure est considérée comme étant l'après-midi ou non
        /// </summary>
        /// <param name="i">Int : L'heure à analyser</param>
        /// <returns>Boolean</returns>
        private static bool IsAfterNoon(int i)
        {
            return (i >= 12 && i < 17) ? true : false;
        }

        /// <summary>
        /// Méthode qui dit si une heure est considérée comme étant le soir ou non
        /// </summary>
        /// <param name="i">Int : L'heure à analyser</param>
        /// <returns>Boolean</returns>
        private static bool IsEvening(int i)
        {
            return (i >= 17 && i < 23) ? true : false;
        }

        #endregion;
    }
}
