using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mova.Logic.Models.Entities;

namespace Mova.Logic.Models.Args
{
    public class RetrieveVetementArgs
    {
        public int IdVetement { get; set; }
        public TypeVetement Type { get; set; }  
        public Couleur CouleurVetement { get; set; }   
        public string NomVetement { get; set; }
        public string ImageURL { get; set; }
        public int Prix { get; set; }
        public bool EstHomme { get; set; }
        public bool EstFemme { get; set; }
        public List<Activite> ListeActivites { get; set; }
        public List<StyleVetement> ListeStyles { get; set; }
        public List<Temperature> ListeTemperatures { get; set; }
    }
}
