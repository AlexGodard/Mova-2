using Mova.Logic.Models;
using Mova.Logic.Models.Args;
using Mova.Logic.Services.Definitions;
using Mova.Logic.Services.Helpers;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mova.Logic.Models.Entities;

namespace Mova.Logic.Services.NHibernate
{
    public class NHibernateUtilisateurService : IUtilisateurService
    {
        
        private ISession session = NHibernateConnexion.OpenSession();

        public IList<Utilisateur> RetrieveAll()
        {
            return session.Query<Utilisateur>().ToList();
        }

        public Utilisateur Retrieve(RetrieveUtilisateurArgs args)
        {
            var utilisateurs = session.Query<Utilisateur>().AsQueryable();
            var result = from p in utilisateurs
                         select p;

            if (args.NomUtilisateur != null && args.NomUtilisateur != String.Empty && args.MotPasse != null && args.MotPasse != String.Empty)
            {
                result = from p in result
                         where p.NomUtilisateur == args.NomUtilisateur && p.MotDePasse == args.MotPasse
                         select p;
            }

            if(result.Any())
                return result.FirstOrDefault();
            else
                return new Utilisateur();
        }

        public Utilisateur RetrieveIdentifiant(RetrieveUtilisateurArgs args)
        {
            var utilisateurs = session.Query<Utilisateur>().AsQueryable();
            
            var result = from p in utilisateurs
                         select p;

            if (args.NomUtilisateur != null && args.NomUtilisateur != String.Empty)
            {
                // On regarde si l'identifiant existe déjà dans la BD
                result = from p in result
                         where p.NomUtilisateur == args.NomUtilisateur
                         select p;
            }

                return result.FirstOrDefault();
        }

        public Utilisateur RetrieveCourriel(RetrieveUtilisateurArgs args)
        {
            var utilisateurs = session.Query<Utilisateur>().AsQueryable();
            var result = from p in utilisateurs
                         select p;

            if (args.Courriel != null && args.Courriel != String.Empty)
            {
                // On regarde si l'identifiant existe déjà dans la BD
                result = from p in result
                         where p.Courriel == args.Courriel
                         select p;
            }

            return result.FirstOrDefault();
        }

        public void Create(Utilisateur utilisateur)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(utilisateur);
                transaction.Commit();
            }
        }
    }
}