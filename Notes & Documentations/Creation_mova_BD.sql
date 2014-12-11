/*SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";

CREATE DATABASE IF NOT EXISTS 5a5_a14_mova DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE 5a5_a14_mova;*/

DROP TABLE IF EXISTS UtilisateursEnsembles;
DROP TABLE IF EXISTS UtilisateursVetements;
DROP TABLE IF EXISTS EnsemblesVetements;
DROP TABLE IF EXISTS ActivitesMoments;
DROP TABLE IF EXISTS ActivitesVetements;
DROP TABLE IF EXISTS VetementsTemperatures;
DROP TABLE IF EXISTS StylesVetements;
DROP TABLE IF EXISTS Temperatures;
DROP TABLE IF EXISTS Styles;
DROP TABLE IF EXISTS Vetements;
DROP TABLE IF EXISTS Utilisateurs;
DROP TABLE IF EXISTS Ensembles;
DROP TABLE IF EXISTS TypesVetements;
DROP TABLE IF EXISTS Moments;
DROP TABLE IF EXISTS Couleurs;
DROP TABLE IF EXISTS Activites;

CREATE TABLE IF NOT EXISTS `activites` (
  `idActivite` int(11) NOT NULL AUTO_INCREMENT,
  `nomActivite` varchar(50) NOT NULL,
  `estOuvrable` tinyint(1) NOT NULL,
  `estConge` tinyint(1) NOT NULL,
  PRIMARY KEY (`idActivite`),
  UNIQUE KEY `nomActivite` (`nomActivite`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=20 ;

CREATE TABLE IF NOT EXISTS `activitesmoments` (
  `idActiviteMoment` int(11) NOT NULL AUTO_INCREMENT,
  `idActivite` int(11) NOT NULL,
  `idMoment` int(11) NOT NULL,
  PRIMARY KEY (`idActiviteMoment`),
  KEY `ActivitiesMoments_Moments_FK` (`idMoment`),
  KEY `ActivitesMoments_Activites_FK` (`idActivite`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=38 ;

CREATE TABLE IF NOT EXISTS `activitesvetements` (
  `idActiviteVetement` int(11) NOT NULL AUTO_INCREMENT,
  `idVetement` int(11) NOT NULL,
  `idActivite` int(11) NOT NULL,
  PRIMARY KEY (`idActiviteVetement`),
  KEY `ActivitesVetements_Vetements_FK` (`idVetement`),
  KEY `ActivitesVetements_Activities_FK` (`idActivite`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=590 ;


CREATE TABLE IF NOT EXISTS `couleurs` (
  `idCouleur` int(11) NOT NULL AUTO_INCREMENT,
  `nomCouleur` varchar(50) NOT NULL,
  PRIMARY KEY (`idCouleur`),
  UNIQUE KEY `nomCouleur` (`nomCouleur`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=17 ;

CREATE TABLE IF NOT EXISTS `ensembles` (
  `idEnsemble` int(11) NOT NULL AUTO_INCREMENT,
  `nomEnsemble` varchar(50) NOT NULL,
  PRIMARY KEY (`idEnsemble`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

CREATE TABLE IF NOT EXISTS `ensemblesvetements` (
  `idEnsembleVetement` int(11) NOT NULL AUTO_INCREMENT,
  `idEnsemble` int(11) NOT NULL,
  `idVetement` int(11) NOT NULL,
  PRIMARY KEY (`idEnsembleVetement`),
  KEY `EnsemblesVetements_Ensembles_FK` (`idEnsemble`),
  KEY `EnsemblesVetements_Vetements_FK` (`idVetement`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=13 ;

CREATE TABLE IF NOT EXISTS `moments` (
  `idMoment` int(11) NOT NULL AUTO_INCREMENT,
  `nomMoment` varchar(40) NOT NULL,
  `heureDebut` time NOT NULL,
  `heureFin` time NOT NULL,
  PRIMARY KEY (`idMoment`),
  UNIQUE KEY `nomMoment` (`nomMoment`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

CREATE TABLE IF NOT EXISTS `styles` (
  `idStyle` int(11) NOT NULL AUTO_INCREMENT,
  `nomStyle` varchar(30) NOT NULL,
  PRIMARY KEY (`idStyle`),
  UNIQUE KEY `nomStyle` (`nomStyle`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=14 ;

CREATE TABLE IF NOT EXISTS `stylesvetements` (
  `idStyleVetement` int(11) NOT NULL AUTO_INCREMENT,
  `idStyle` int(11) NOT NULL,
  `idVetement` int(11) NOT NULL,
  PRIMARY KEY (`idStyleVetement`),
  KEY `StylesVetements_Styles_FK` (`idStyle`),
  KEY `StylesVetements_Vetements_FK` (`idVetement`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=241 ;

CREATE TABLE IF NOT EXISTS `temperatures` (
  `idTemperature` int(11) NOT NULL AUTO_INCREMENT,
  `nomClimat` varchar(50) NOT NULL,
  PRIMARY KEY (`idTemperature`),
  UNIQUE KEY `nomClimat` (`nomClimat`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

CREATE TABLE IF NOT EXISTS `typesvetements` (
  `idTypeVetement` int(11) NOT NULL AUTO_INCREMENT,
  `nomType` varchar(50) NOT NULL,
  PRIMARY KEY (`idTypeVetement`),
  UNIQUE KEY `nomType` (`nomType`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

CREATE TABLE IF NOT EXISTS `utilisateurs` (
  `idUtilisateur` int(11) NOT NULL AUTO_INCREMENT,
  `prenom` varchar(50) DEFAULT NULL,
  `nom` varchar(50) DEFAULT NULL,
  `nomUtilisateur` varchar(50) NOT NULL,
  `motPasse` varchar(50) NOT NULL,
  `courriel` varchar(75) NOT NULL,
  `sexe` varchar(1) NOT NULL,
  `estAdmin` tinyint(1) NOT NULL,
  PRIMARY KEY (`idUtilisateur`),
  UNIQUE KEY `nomUtilisateur` (`nomUtilisateur`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

CREATE TABLE IF NOT EXISTS `utilisateursensembles` (
  `idUtilisateurEnsemble` int(11) NOT NULL AUTO_INCREMENT,
  `idUtilisateur` int(11) NOT NULL,
  `idEnsemble` int(11) NOT NULL,
  `dateCreation` datetime NOT NULL,
  `estFavori` tinyint(1) NOT NULL,
  `estDansGardeRobe` tinyint(1) NOT NULL,
  PRIMARY KEY (`idUtilisateurEnsemble`),
  KEY `UtilisateursEnsembles_Utilisateurs_FK` (`idUtilisateur`),
  KEY `UtilisateursEnsembles_Ensembles_FK` (`idEnsemble`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;


CREATE TABLE IF NOT EXISTS `utilisateursvetements` (
  `idUtilisateurVetement` int(11) NOT NULL AUTO_INCREMENT,
  `idUtilisateur` int(11) NOT NULL,
  `idVetement` int(11) NOT NULL,
  PRIMARY KEY (`idUtilisateurVetement`),
  KEY `UtilisateursVetements_Utilisateurs_FK` (`idUtilisateur`),
  KEY `UtilisateursVetements_Vetements_FK` (`idVetement`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;


CREATE TABLE IF NOT EXISTS `vetements` (
  `idVetement` int(11) NOT NULL AUTO_INCREMENT,
  `idTypeVetement` int(11) NOT NULL,
  `idCouleur` int(11) NOT NULL,
  `nomVetement` varchar(100) NOT NULL,
  `imageURL` varchar(255) NOT NULL,
  `prix` float DEFAULT NULL,
  `estHomme` tinyint(1) NOT NULL,
  `estFemme` tinyint(1) NOT NULL,
  PRIMARY KEY (`idVetement`),
  KEY `Vetements_TypesVetements_FK` (`idTypeVetement`),
  KEY `Vetements_Couleurs_FK` (`idCouleur`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=94 ;


CREATE TABLE IF NOT EXISTS `vetementstemperatures` (
  `idVetementTemperature` int(11) NOT NULL AUTO_INCREMENT,
  `idVetement` int(11) NOT NULL,
  `idTemperature` int(11) NOT NULL,
  PRIMARY KEY (`idVetementTemperature`),
  KEY `VetementsTemperatures_Vetements_FK` (`idVetement`),
  KEY `VetementsTemperatures_idTemperature_FK` (`idTemperature`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=434 ;



ALTER TABLE `activitesmoments`
  ADD CONSTRAINT `ActivitesMoments_Activites_FK` FOREIGN KEY (`idActivite`) REFERENCES `activites` (`idActivite`),
  ADD CONSTRAINT `ActivitiesMoments_Moments_FK` FOREIGN KEY (`idMoment`) REFERENCES `moments` (`idMoment`);

ALTER TABLE `activitesvetements`
  ADD CONSTRAINT `ActivitesVetements_Activities_FK` FOREIGN KEY (`idActivite`) REFERENCES `activites` (`idActivite`),
  ADD CONSTRAINT `ActivitesVetements_Vetements_FK` FOREIGN KEY (`idVetement`) REFERENCES `vetements` (`idVetement`);

ALTER TABLE `ensemblesvetements`
  ADD CONSTRAINT `EnsemblesVetements_Vetements_FK` FOREIGN KEY (`idVetement`) REFERENCES `vetements` (`idVetement`),
  ADD CONSTRAINT `EnsemblesVetements_Ensembles_FK` FOREIGN KEY (`idEnsemble`) REFERENCES `ensembles` (`idEnsemble`);

ALTER TABLE `stylesvetements`
  ADD CONSTRAINT `StylesVetements_Vetements_FK` FOREIGN KEY (`idVetement`) REFERENCES `vetements` (`idVetement`),
  ADD CONSTRAINT `StylesVetements_Styles_FK` FOREIGN KEY (`idStyle`) REFERENCES `styles` (`idStyle`);

ALTER TABLE `utilisateursensembles`
  ADD CONSTRAINT `UtilisateursEnsembles_Ensembles_FK` FOREIGN KEY (`idEnsemble`) REFERENCES `ensembles` (`idEnsemble`),
  ADD CONSTRAINT `UtilisateursEnsembles_Utilisateurs_FK` FOREIGN KEY (`idUtilisateur`) REFERENCES `utilisateurs` (`idUtilisateur`);

ALTER TABLE `utilisateursvetements`
  ADD CONSTRAINT `UtilisateursVetements_Vetements_FK` FOREIGN KEY (`idVetement`) REFERENCES `vetements` (`idVetement`),
  ADD CONSTRAINT `UtilisateursVetements_Utilisateurs_FK` FOREIGN KEY (`idUtilisateur`) REFERENCES `utilisateurs` (`idUtilisateur`);

ALTER TABLE `vetements`
  ADD CONSTRAINT `Vetements_Couleurs_FK` FOREIGN KEY (`idCouleur`) REFERENCES `couleurs` (`idCouleur`),
  ADD CONSTRAINT `Vetements_TypesVetements_FK` FOREIGN KEY (`idTypeVetement`) REFERENCES `typesvetements` (`idTypeVetement`);

ALTER TABLE `vetementstemperatures`
  ADD CONSTRAINT `VetementsTemperatures_idTemperature_FK` FOREIGN KEY (`idTemperature`) REFERENCES `temperatures` (`idTemperature`),
  ADD CONSTRAINT `VetementsTemperatures_Vetements_FK` FOREIGN KEY (`idVetement`) REFERENCES `vetements` (`idVetement`);
