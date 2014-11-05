using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Mova.Logic.Models
{
    public abstract class Validation
    {

    }

    // Le prénom et le nom auront 1 caractère minimum et 50 caractères maximum. La casse sera sensible.
    public class RegleNom : Validation
    {
        private int _min;
        private int _max;

        public RegleNom()
        {
            _min = 1;
            _max = 50;
        }

        public int Min
        {
            get { return _min; }
            set { _min = value; }
        }

        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public bool valider(string nom)
        {
            if ((nom.Length >= Min) && (nom.Length <= Max) && Regex.IsMatch(nom, @"^([ \u00c0-\u01ffa-zA-Z'\-])+$"))
            {
                return true;
            }
            else
            {
                return false;

            }
        }
    }

    public class RegleEmail : Validation
    {
        private int _min;
        private int _max;

        public RegleEmail()
        {
            _min = 5;
            _max = 50;
        }

        public int Min
        {
            get { return _min; }
            set { _min = value; }
        }

        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public bool valider(string email)
        {
            if ((email.Length >= Min) && (email.Length <= Max) && Regex.IsMatch(email, @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class RegleIdentifiant : Validation
    {
        private int _min;
        private int _max;

        public RegleIdentifiant()
        {
            _min = 5;
            _max = 24;
        }

        public int Min
        {
            get { return _min; }
            set { _min = value; }
        }

        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public bool valider(string identifiant)
        {
            if ((identifiant.Length >= Min) && (identifiant.Length <= Max) && Regex.IsMatch(identifiant, @"[a-zA-Z]|\d*"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class RegleMotDePasse : Validation
    {
        private int _min;
        private int _max;

        public RegleMotDePasse()
        {
            _min = 5;
            _max = 24;
        }

        public int Min
        {
            get { return _min; }
            set { _min = value; }
        }

        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public bool valider(string motDePasse)
        {
            if ((motDePasse.Length >= Min) && (motDePasse.Length <= Max))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
