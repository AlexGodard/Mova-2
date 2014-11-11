using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Controls;

namespace Mova.UI
{
    /// <summary>
    /// Classe qui servira à faire la navigation dans l'écran styliste, maintenant une classe générique
    /// </summary>
    public class History<T>
    {

        /// <summary>
        /// Une liste des UserControls ouverts par l'utilisateur
        /// </summary>
        private List<T> _listeView = new List<T>();

        /// <summary>
        /// Méthode qui permet d'ajouter un UserControl dans la liste (le stack)
        /// </summary>
        /// <param name="bvm">UserControl : Le UserControl à ajouter</param>
        public void Ajouter(T bvm)
        {
            _listeView.Add(bvm);
        }

        /// <summary>
        /// Méthode qui analyse si la liste est vide ou non
        /// </summary>
        /// <returns>Bool : True si vide, False dans le cas contraire</returns>
        public bool IsEmpty()
        {
            return (_listeView.Count == 0) ? true : false;
        }

        /// <summary>
        /// Méthode qui retourne le dernier UserControl de l'historique
        /// </summary>
        /// <returns>UserControl : Le dernier UserControl</returns>
        public T Last()
        {

            DeleteLast();

            //S'il y a des éléments dans la liste, on retourne le dernier
            if (_listeView.Count > 0)
            {
                return _listeView[_listeView.Count - 1];
            }

            return default(T);
        }

        /// <summary>
        /// Méthode qui retourne le dernier UserControl de l'historique
        /// </summary>
        /// <returns>UserControl : Le dernier UserControl</returns>
        public T ReturnLast()
        {

            //S'il y a des éléments dans la liste, on retourne le dernier
            if (_listeView.Count > 0)
            {
                return _listeView[_listeView.Count - 1];
            }

            return default(T);
        }

        /// <summary>
        /// Méthode qui supprime le dernier UserControl ajouter (quand l'utilisateur appuie sur Retour)
        /// </summary>
        public void DeleteLast()
        {
            _listeView.RemoveAt(_listeView.Count - 1);
        }

        /// <summary>
        /// Méthode qui retourne le compte du nombre d'items dans la liste
        /// </summary>
        /// <returns>Int : Le nombre d'items</returns>
        public int Compte()
        {
            //return _listeView.Count.ToString();
            return _listeView.Count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bvm"></param>
        /// <returns></returns>
        public int GetNumberOfPage(T bvm)
        {
        
            return _listeView.IndexOf(bvm);
        
        }

        /// <summary>
        /// Méthode qui nous signale que l'élément est le premier dans la liste ou non
        /// </summary>
        /// <param name="bvm"></param>
        /// <returns></returns>
        public bool IsFirst(T bvm)
        {
            return (_listeView.IndexOf(bvm) == 0)? true : false;
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bvm"></param>
        /// <returns></returns>
        public bool IsLast(T bvm)
        {
            return (_listeView.IndexOf(bvm) >= _listeView.Count-1) ? true : false;

        }

        public T GetFirst()
        {

            T t = _listeView[0];

            return t;
        
        }

        public void Reset()
        {

            _listeView = new List<T>();

        }

    }
}
