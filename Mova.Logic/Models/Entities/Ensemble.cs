﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mova.Logic.Models
{
    /// <summary>
    /// Contient un trio de trois vêtements (chaussures, bas, haut)
    /// </summary>
    public class Ensemble
    {
        #region Propriétés

        public virtual int? IdEnsemble { get; set; }
        public virtual string NomEnsemble {  get; set; }

        #endregion

        public Ensemble()
        {
            IdEnsemble = null;
            NomEnsemble = "Inconnu";
        }

        public Ensemble(string nomEnsemble)
        {
            this.NomEnsemble = nomEnsemble;
        }
    }
}
