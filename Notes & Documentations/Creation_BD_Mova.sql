/* Nom du fichier: Creation_BD_Mova.sql
   Auteur: David Cantin, Maxime Laramée, Alexandre Godard, Gabriel Piché Cloutier
   Date: 2014-09-09
   Description: Création des tables et de leurs liaisons pour la base de données BD_Mova */

/* Modification: On Supprime les tables en premier.
   Auteur: Gabriel Piché Cloutier
   Date: 09/09/2014 */
CREATE DATABASE IF NOT EXISTS 5a5_a14_mova DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE 5a5_a14_mova;

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

/*Table Températures*/
CREATE TABLE IF NOT EXISTS Temperatures
( idTemperature INT NOT NULL PRIMARY KEY AUTO_INCREMENT
, nomClimat VARCHAR(50) NOT NULL UNIQUE
);

/*Table Styles*/
CREATE TABLE IF NOT EXISTS Styles
( idStyle INT NOT NULL PRIMARY KEY AUTO_INCREMENT
, nomStyle VARCHAR(30) NOT NULL UNIQUE
);

/*Table Moments*/
CREATE TABLE IF NOT EXISTS  Moments
( idMoment INT NOT NULL PRIMARY KEY AUTO_INCREMENT
, nomMoment VARCHAR(40) NOT NULL UNIQUE
, heureDebut TIME NOT NULL
, heureFin TIME NOT NULL
);

/*Table Utilisateurs*/
CREATE TABLE IF NOT EXISTS Utilisateurs
( idUtilisateur INT NOT NULL PRIMARY KEY AUTO_INCREMENT
, prenom VARCHAR(50)
, nom VARCHAR(50)
, nomUtilisateur VARCHAR(50) UNIQUE NOT NULL
, motPasse VARCHAR(50) NOT NULL
, courriel VARCHAR(75) NOT NULL
, sexe VARCHAR(1) NOT NULL
, estAdmin BOOL NOT NULL
);

/* Table Ensembles */
CREATE TABLE IF NOT EXISTS Ensembles
( idEnsemble INT NOT NULL PRIMARY KEY AUTO_INCREMENT
, nomEnsemble VARCHAR(50) NOT NULL
);

/* Table Couleurs */
CREATE TABLE IF NOT EXISTS Couleurs
( idCouleur INT NOT NULL PRIMARY KEY AUTO_INCREMENT
, nomCouleur VARCHAR(50) NOT NULL UNIQUE
);

/* Table Activites */
CREATE TABLE IF NOT EXISTS Activites
( idActivite INT NOT NULL PRIMARY KEY AUTO_INCREMENT
, nomActivite VARCHAR(50) NOT NULL UNIQUE
, estOuvrable BOOL NOT NULL
, estConge BOOL NOT NULL
);

/* Table Vetements */
CREATE TABLE IF NOT EXISTS Vetements
( idVetement INT NOT NULL PRIMARY KEY AUTO_INCREMENT
, idTypeVetement INT NOT NULL
, idCouleur INT NOT NULL
, nomVetement VARCHAR(100) NOT NULL
, imageURL VARCHAR(100) NOT NULL
, prix FLOAT
, estHomme BOOL NOT NULL
, estFemme BOOL NOT NULL
);

/* Table UtilisateursEnsembles */
CREATE TABLE IF NOT EXISTS UtilisateursEnsembles
( idUtilisateurEnsemble INT NOT NULL PRIMARY KEY AUTO_INCREMENT
, idUtilisateur INT NOT NULL
, idEnsemble INT NOT NULL
, dateCreation DATETIME NOT NULL
, estFavori BOOL NOT NULL
, estDansGardeRobe BOOL NOT NULL
);

/* Table UtilisateursVetements */
CREATE TABLE IF NOT EXISTS UtilisateursVetements
( idUtilisateurVetement INT NOT NULL PRIMARY KEY AUTO_INCREMENT
, idUtilisateur INT NOT NULL 
, idVetement INT NOT NULL
);

/* Table EnsemblesVetements */
CREATE TABLE IF NOT EXISTS EnsemblesVetements
( idEnsembleVetement INT NOT NULL PRIMARY KEY AUTO_INCREMENT
, idEnsemble INT NOT NULL
, idVetement INT NOT NULL
);

/* Table ActivitesMoments */
CREATE TABLE IF NOT EXISTS ActivitesMoments
( idActiviteMoment INT NOT NULL PRIMARY KEY AUTO_INCREMENT
, idActivite INT NOT NULL
, idMoment INT NOT NULL
);

/* Table ActivitesVetements */
CREATE TABLE IF NOT EXISTS ActivitesVetements
( idActiviteVetement INT NOT NULL PRIMARY KEY AUTO_INCREMENT
, idVetement INT NOT NULL
, idActivite INT NOT NULL
);

/* Table VetementsTemperatures */
CREATE TABLE IF NOT EXISTS VetementsTemperatures
( idVetementTemperature INT NOT NULL PRIMARY KEY AUTO_INCREMENT
, idVetement INT NOT NULL
, idTemperature INT NOT NULL
);

/* Table StylesVetements */
CREATE TABLE IF NOT EXISTS StylesVetements
( idStyleVetement INT NOT NULL PRIMARY KEY AUTO_INCREMENT
, idStyle INT NOT NULL
, idVetement INT NOT NULL
);

/* Table TypesVetements */
CREATE TABLE IF NOT EXISTS TypesVetements
( idTypeVetement INT NOT NULL PRIMARY KEY AUTO_INCREMENT
, nomType VARCHAR(50) NOT NULL UNIQUE
);

/* Clés étrangères ***************************************************************/

/* Clés étrangères de la table UtilisateursEnsembles */
ALTER TABLE UtilisateursEnsembles
ADD CONSTRAINT UtilisateursEnsembles_Utilisateurs_FK
FOREIGN KEY (idUtilisateur) REFERENCES Utilisateurs (idUtilisateur);

ALTER TABLE UtilisateursEnsembles
ADD CONSTRAINT UtilisateursEnsembles_Ensembles_FK 
FOREIGN KEY (idEnsemble) REFERENCES Ensembles(idEnsemble);

/* Clés étrangères de la table EnsemblesVetements */
ALTER TABLE EnsemblesVetements
ADD CONSTRAINT EnsemblesVetements_Ensembles_FK 
FOREIGN KEY (idEnsemble) REFERENCES Ensembles(idEnsemble);

ALTER TABLE EnsemblesVetements
ADD CONSTRAINT EnsemblesVetements_Vetements_FK 
FOREIGN KEY (idVetement) REFERENCES Vetements(idVetement);

/* Clés étrangères de la table UtilisateursVetements */
ALTER TABLE UtilisateursVetements
ADD CONSTRAINT UtilisateursVetements_Utilisateurs_FK 
FOREIGN KEY (idUtilisateur) REFERENCES Utilisateurs(idUtilisateur);

ALTER TABLE UtilisateursVetements
ADD CONSTRAINT UtilisateursVetements_Vetements_FK 
FOREIGN KEY (idVetement) REFERENCES Vetements(idVetement);

/* Clés étrangères de la table StylesVetements */
ALTER TABLE StylesVetements
ADD CONSTRAINT StylesVetements_Styles_FK
FOREIGN KEY (idStyle) REFERENCES Styles(idStyle);

ALTER TABLE StylesVetements
ADD CONSTRAINT StylesVetements_Vetements_FK
FOREIGN KEY (idVetement) REFERENCES Vetements(idVetement);


/* Clés étrangères de la table Vetements */
ALTER TABLE Vetements
ADD CONSTRAINT Vetements_TypesVetements_FK
FOREIGN KEY (idTypeVetement) REFERENCES TypesVetements(idTypeVetement);

ALTER TABLE Vetements
ADD CONSTRAINT Vetements_Couleurs_FK
FOREIGN KEY (idCouleur) REFERENCES Couleurs(idCouleur);

/* Clés étrangères de la table VetementsTemperatures */
ALTER TABLE VetementsTemperatures
ADD CONSTRAINT VetementsTemperatures_Vetements_FK
FOREIGN KEY (idVetement) REFERENCES Vetements(idVetement);

ALTER TABLE VetementsTemperatures
ADD CONSTRAINT VetementsTemperatures_idTemperature_FK
FOREIGN KEY (idTemperature) REFERENCES Temperatures(idTemperature);

/* Clés étrangères de la table ActivitesVetements */
ALTER TABLE ActivitesVetements
ADD CONSTRAINT ActivitesVetements_Vetements_FK
FOREIGN KEY (idVetement) REFERENCES Vetements(idVetement);

ALTER TABLE ActivitesVetements
ADD CONSTRAINT ActivitesVetements_Activities_FK
FOREIGN KEY (idActivite) REFERENCES Activites(idActivite);

/* Clés étrangères de la table ActivitesMoments */
ALTER TABLE ActivitesMoments
ADD CONSTRAINT ActivitiesMoments_Moments_FK
FOREIGN KEY (idMoment) REFERENCES Moments(idMoment);

ALTER TABLE ActivitesMoments
ADD CONSTRAINT ActivitesMoments_Activites_FK
FOREIGN KEY (idActivite) REFERENCES Activites(idActivite);

/* Insertion BD */

USE 5a5_a14_mova;

/* Insertion dans la table Utilisateurs *************************************************************************/
INSERT INTO Utilisateurs
(prenom,nom,nomUtilisateur,motPasse,courriel,sexe,estAdmin)
VALUES
(
  "David"
, "Cantin"
, "DavidC"
, "admin"
, "test@hotmail.com"
, "M"
, true
);

INSERT INTO Utilisateurs
(prenom,nom,nomUtilisateur,motPasse,courriel,sexe,estAdmin)
VALUES
(
  "Maxime"
, "Laramee"
, "MaximeL"
, "admin"
, "test2@hotmail.com"
, "M"
, true
);

INSERT INTO Utilisateurs
(prenom,nom,nomUtilisateur,motPasse,courriel,sexe,estAdmin)
VALUES
(
  "Alexendre"
, "Gordard"
, "AlexG"
, "admin"
, "test3@hotmail.com"
, "M"
, true
);

INSERT INTO Utilisateurs
(prenom,nom,nomUtilisateur,motPasse,courriel,sexe,estAdmin)
VALUES
(
  "Gabriel"
, "Piche-Cloutier"
, "GabrielPC"
, "admin"
, "test4@hotmail.com"
, "M"
, true
);

INSERT INTO Utilisateurs
(prenom,nom,nomUtilisateur,motPasse,courriel,sexe,estAdmin)
VALUES
(
  "Test"
, "Un testeur"
, "Test"
, "admin"
, "test5@hotmail.com"
, "M"
, true
);

/* Insertion dans la table Style *************************************************************************/
INSERT INTO Styles
(nomStyle)
VALUES
(
  "Sport"
);

INSERT INTO Styles
(nomStyle)
VALUES
(
  "Gothique"
);

INSERT INTO Styles
(nomStyle)
VALUES
(
  "Gamer"
);

INSERT INTO Styles
(nomStyle)
VALUES
(
  "Hippie"
);

INSERT INTO Styles
(nomStyle)
VALUES
(
  "Relaxe"
);

INSERT INTO Styles
(nomStyle)
VALUES
(
  "Propre"
);

/*Insertion du 11/16/2014*/
INSERT INTO Styles
(nomStyle)
VALUES
(
  "Hipster"
);

INSERT INTO Styles
(nomStyle)
VALUES
(
  "Gangster"
);

INSERT INTO Styles
(nomStyle)
VALUES
(
  "Punk"
);

INSERT INTO Styles
(nomStyle)
VALUES
(
  "Tous les jours"
);

INSERT INTO Styles
(nomStyle)
VALUES
(
  "Far-West"
);

INSERT INTO Styles
(nomStyle)
VALUES
(
  "Futuristique"
);

INSERT INTO Styles
(nomStyle)
VALUES
(
  "Friperie"
);



/* Insertion dans la table Couleurs *************************************************************************/
INSERT INTO Couleurs
(nomCouleur)
VALUES
(
  "Rouge"
);

INSERT INTO Couleurs
(nomCouleur)
VALUES
(
  "Bleu"
);

INSERT INTO Couleurs
(nomCouleur)
VALUES
(
  "Jaune"
);

INSERT INTO Couleurs
(nomCouleur)
VALUES
(
  "Noir"
);

INSERT INTO Couleurs
(nomCouleur)
VALUES
(
  "Blanc"
);

INSERT INTO Couleurs
(nomCouleur)
VALUES
(
  "Gris"
);

INSERT INTO Couleurs
(nomCouleur)
VALUES
(
  "Bleu Marin"
);

INSERT INTO Couleurs
(nomCouleur)
VALUES
(
  "Vert"
);

INSERT INTO Couleurs
(nomCouleur)
VALUES
(
  "Rose"
);

INSERT INTO Couleurs
(nomCouleur)
VALUES
(
  "Brun"
);

INSERT INTO Couleurs
(nomCouleur)
VALUES
(
  "Beige"
);

INSERT INTO Couleurs
(nomCouleur)
VALUES
(
  "Mauve"
);

INSERT INTO Couleurs
(nomCouleur)
VALUES
(
  "Khaki"
);

/*Insertion du 11/16/2014*/
INSERT INTO Couleurs
(nomCouleur)
VALUES
(
  "Or"
);

INSERT INTO Couleurs
(nomCouleur)
VALUES
(
  "Argent"
);

INSERT INTO Couleurs
(nomCouleur)
VALUES
(
  "Turquoise"
);

/* Insertion dans la table Activites *************************************************************************/
INSERT INTO Activites
(nomActivite,estOuvrable,estConge)
VALUES
(
  "Courir à l'extérieur"
  , TRUE
  , TRUE
);

INSERT INTO Activites
(nomActivite,estOuvrable,estConge)
VALUES
(
  "Faire un sport d'équipe"
  , TRUE
  , TRUE
);

INSERT INTO Activites
(nomActivite,estOuvrable,estConge)
VALUES
(
  "Faire un sport intérieur"
  , TRUE
  , TRUE
);

INSERT INTO Activites
(nomActivite,estOuvrable,estConge)
VALUES
(
  "Jouer aux golf"
  , TRUE
  , TRUE
);

INSERT INTO Activites
(nomActivite,estOuvrable,estConge)
VALUES
(
  "Aller se muscler"
  , TRUE
  , TRUE
);

INSERT INTO Activites
(nomActivite,estOuvrable,estConge)
VALUES
(
  "Aller à l'école"
  , TRUE
  , FALSE
);

INSERT INTO Activites
(nomActivite,estOuvrable,estConge)
VALUES
(
  "Aller travailler"
  , TRUE
  , FALSE
);

INSERT INTO Activites
(nomActivite,estOuvrable,estConge)
VALUES
(
  "Aller à l'aventure"
  , TRUE
  , TRUE
);

INSERT INTO Activites
(nomActivite,estOuvrable,estConge)
VALUES
(
  "Aller chez le dentiste"
  , TRUE
  , TRUE
);

INSERT INTO Activites
(nomActivite,estOuvrable,estConge)
VALUES
(
  "Faire l'épicerie"
  , TRUE
  , TRUE
);

INSERT INTO Activites
(nomActivite,estOuvrable,estConge)
VALUES
(
  "Aller manger"
  , TRUE
  , TRUE
);

INSERT INTO Activites
(nomActivite,estOuvrable,estConge)
VALUES
(
  "Aller à un rendez-vous"
  , TRUE
  , TRUE
);

INSERT INTO Activites
(nomActivite,estOuvrable,estConge)
VALUES
(
  "Aller fêter"
  , FALSE
  , TRUE
);

INSERT INTO Activites
(nomActivite,estOuvrable,estConge)
VALUES
(
  "Aller au cinéma"
  , TRUE
  , TRUE
);

INSERT INTO Activites
(nomActivite,estOuvrable,estConge)
VALUES
(
  "Aller à la plage"
  , TRUE
  , TRUE
);

INSERT INTO Activites
(nomActivite,estOuvrable,estConge)
VALUES
(
  "Jouer aux jeux vidéo"
  , TRUE
  , TRUE
);

/* Insertion dans la table Moments *************************************************************************/
INSERT INTO Moments
(nomMoment,heureDebut,heureFin)
VALUES
(
  "Matin"
, "4:00:00"
, "11:59:59"
);

INSERT INTO Moments
(nomMoment,heureDebut,heureFin)
VALUES
(
  "Après-midi"
, "12:00:00"
, "16:59:59"
);

INSERT INTO Moments
(nomMoment,heureDebut,heureFin)
VALUES
(
  "Soir"
, "17:00:00"
, "22:59:59"
);

INSERT INTO Moments
(nomMoment,heureDebut,heureFin)
VALUES
(
  "Nuit"
, "23:00:00"
, "03:59:59"
);

/* Insertion dans la table ActivitesMoments  ***********************************************/

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Courir à l'extérieur")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Matin")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Courir à l'extérieur")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Après-midi")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Courir à l'extérieur")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Soir")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Courir à l'extérieur")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Nuit")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport d'équipe")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Matin")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport d'équipe")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Après-midi")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport d'équipe")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Soir")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport d'équipe")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Nuit")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport intérieur")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Matin")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport intérieur")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Après-midi")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport intérieur")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Soir")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport intérieur")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Nuit")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux golf")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Matin")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux golf")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Après-midi")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Aller se muscler")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Matin")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Aller se muscler")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Après-midi")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Aller se muscler")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Soir")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Aller se muscler")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Nuit")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Matin")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Après-midi")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Matin")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Après-midi")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Soir")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Nuit")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Matin")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Après-midi")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Soir")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Soir")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Nuit")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Après-midi")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Soir")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Matin")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Après-midi")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Soir")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Nuit")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à la plage")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Matin")
);

INSERT INTO ActivitesMoments
(idActivite, idMoment)
VALUES
( (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à la plage")
, (SELECT idMoment FROM Moments WHERE nomMoment = "Après-midi")
);

/* Insertion dans la table TypesVetements *************************************************************************/
INSERT INTO TypesVetements
(nomtype)
VALUES
(
  "Haut"
);

INSERT INTO TypesVetements
(nomtype)
VALUES
(
  "Bas"
);

INSERT INTO TypesVetements
(nomtype)
VALUES
(
  "Chaussures"
);

/* Insertion dans la table Temperatures *************************************************************************/
INSERT INTO Temperatures
(nomClimat)
VALUES
(
  "Nuageux"
);

INSERT INTO Temperatures
(nomClimat)
VALUES
(
  "Ensoleillé"
);

INSERT INTO Temperatures
(nomClimat)
VALUES
(
  "Pluie"
);

INSERT INTO Temperatures
(nomClimat)
VALUES
(
  "Venteux"
);

INSERT INTO Temperatures
(nomClimat)
VALUES
(
  "Froid"
);

INSERT INTO Temperatures
(nomClimat)
VALUES
(
  "Neige"
);

/* Insertion dans la table Vetements *************************************************************************/
INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Noir")
, "Pantalon d'entraînement Adidas"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/PantalonDEntrainementAdidas.png"
, 60
, TRUE
, TRUE
);


INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Haut")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Rouge")
, "Chandail du FC Bayern"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/hauts/ChandailDuFCBayern.png"
, 50
, TRUE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Chaussures")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Rouge")
, "Chaussures Adidas"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/chaussures/ChaussuresAdidas.png"
, 70
, TRUE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Brun")
, "Pantalon Chino"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/PantalonChino.png"
, 40
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Haut")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Rose")
, "Chandail à manches longues Kirby"
, "defaut.png"
, 40
, TRUE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Chaussures")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Bleu")
, "Chaussures Mario"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/chaussures/ChaussuresMario.png"
, 30
, TRUE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Chaussures")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Noir")
, "Bottes de combat"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/chaussures/BottesDeCombat.png"
, 50
, TRUE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Noir")
, "Jeans cargo"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/JeansCargo.png"
, 40
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Haut")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Noir")
, "Chemise du chagrin"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/hauts/ChemiseDuChagrin.png"
, 80
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Vert")
, "Pantalon nomade"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/PantalonNomade.png"
, 20
, TRUE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Haut")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Mauve")
, "Chandail psychédélique"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/hauts/ChandailPsychedelique.png"
, 10
, TRUE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Chaussures")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Brun")
, "Sandales Birkenstock"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/chaussures/SandalesBirkenstock.png"
, 60
, TRUE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Chaussures")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Noir")
, "Nike Freerun"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/chaussures/NikeFreerun.png"
, 110
, TRUE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Haut")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Mauve")
, "Kangourou Underarmour"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/hauts/KangourouUnderarmour.png"
, 60
, TRUE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Noir")
, "Jogging Underarmour"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/JoggingUnderarmour.png"
, 40
, TRUE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Haut")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Gris")
, "T-shirt gris"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/hauts/TShirtGris.png"
, 10
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Haut")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Mauve")
, "Chandail à manches longues mauve"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/hauts/ChandailAManchesLonguesMauve.png"
, 30
, TRUE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Haut")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Rouge")
, "Chandail à manches longues rouge"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/hauts/ChandailAManchesLonguesRouge.png"
, 30
, TRUE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Khaki")
, "Short Khaki"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/ShortKhaki.png"
, 40
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Gris")
, "Short Gris"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/ShortGris.png"
, 40
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Bleu")
, "Short carreauté"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/ShortCarreaute.png"
, 20
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Chaussures")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Noir")
, "Chaussures Oxford"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/chaussures/ChaussuresOxford.png"
, 80
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Haut")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Noir")
, "Polo rayé"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/hauts/PoloRaye.png"
, 20
, FALSE
, TRUE
);


INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Haut")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Gris")
, "Polo gris"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/hauts/PoloGris.png"
, 20
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Bleu")
, "Short Nike"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/ShortNike.png"
, 30
, TRUE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Gris")
, "Jogging retroussé"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/JoggingRetrousse.png"
, 60
, TRUE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Noir")
, "Pantalon Cargo Levis Commuter"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/PantalonCargoLevisCommuter.png"
, 80
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Rouge")
, "Pantalon avec patch"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/PantalonAvecParch.png"
, 200
, TRUE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Chaussures")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Noir")
, "Adidas Tech Runner"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/chaussures/AdidasTechRunner.png"
, 600
, TRUE
, TRUE
);
/* TAGS VÊTEMENT EN COURS (ALEX GODARD) */

/*Insertion du 11/16/2014*/

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Noir")
, "Pantalon camouflage"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/PantalonCamouflage.jpg"
, 35
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Bleu")
, "Jeans 8bit"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/Jeans8bit.jpg"
, 20
, TRUE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Bleu")
, "Jeans"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/JeansNormal.jpg"
, 30
, TRUE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Rose")
, "Jeans rose"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/JeansRose.png"
, 35
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Rouge")
, "Jeans rouge"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/JeansRouge.jpg"
, 35
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Noir")
, "Jeans serré"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/JeansSerre.jpg"
, 45
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Khaki")
, "Levis 513"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/Levis513.jpg"
, 30
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Vert")
, "Pantalon à poches"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/PantalonAvecPlusieursPoches.jpg"
, 20
, FALSE
, TRUE
);


INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Brun")
, "Pantalon Ck"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/PantalonCk.jpg"
, 40
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Noir")
, "Pantalon de cuir"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/PantalonDeCuire.jpg"
, 70
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Gris")
, "Pantalon de golf"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/PantalonDeGolf.jpg"
, 50
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Brun")
, "Pantalon de golf orange"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/PantalonDeGolfOrange.jpg"
, 40
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Gris")
, "Pantalon Gucci"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/PantalonGucci.jpg"
, 200
, FALSE
, TRUE
);
INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Brun")
, "Pantalon Metal Gear Solid"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/PantalonMetalGearSolid.png"
, 45
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Gris")
, "Pantalon de compression Nike"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/PantalonNikeDeCompression.jpg"
, 60
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Gris")
, "Pantalon propre Azad Point"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/PantalonPropreAzadPoint.jpg"
, 100
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Vert")
, "Pantalon vert"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/PantalonVert.jpg"
, 30
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Bas")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Gris")
, "Nike5"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/bas/ShortsNike5.jpg"
, 15
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Chaussures")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Bleu")
, "Chassures bateau 1"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/chaussures/chaussuresBateau1.jpg"
, 70
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Chaussures")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Noir")
, "Chassures bateau 2"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/chaussures/chaussuresBateau2.jpg"
, 70
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Chaussures")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Brun")
, "Chassures simple"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/chaussures/chaussuresSimple.jpg"
, 40
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Chaussures")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Brun")
, "Chassures cheville haute"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/chaussures/ChaussuresChevilleHaute.jpg"
, 80
, FALSE
, TRUE
);


INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Chaussures")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Noir")
, "Chaussures Salomon de randonnée"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/chaussures/ChaussuresDeRandonnee.jpg"
, 70
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Chaussures")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Noir")
, "Chaussures Adidas Propre"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/chaussures/ChaussuresAdidasPropre.jpg"
, 65
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Chaussures")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Blanc")
, "Chaussures BD"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/chaussures/ChaussuresBD.jpg"
, 20
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Chaussures")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Vert")
, "NikeCamo"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/chaussures/ChaussuresCamo.jpg"
, 22
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Chaussures")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Rouge")
, "Classic Converse"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/chaussures/ChaussuresConverse.jpg"
, 40
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Chaussures")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Noir")
, "Chaussures dangereuse"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/chaussures/ChaussuresDangereuse.jpg"
, 12
, FALSE
, TRUE
);

INSERT INTO Vetements
(idTypeVetement,idCouleur,nomVetement,imageURL,prix,estFemme,estHomme)
VALUES
(
  (SELECT idTypeVetement FROM TypesVetements WHERE nomType = "Chaussures")
, (SELECT idCouleur FROM Couleurs WHERE nomCouleur = "Blanc")
, "DC Fresh"
, "420.cstj.qc.ca/gabrielpichecloutier/images_mova/chaussures/ChaussuresDC.jpg"
, 47
, FALSE
, TRUE
);

/* Styles */
INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Sport")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon d'entraînement Adidas")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon d'entraînement Adidas")
);

/* Activités */
INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon d'entraînement Adidas")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Courir à l'extérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon d'entraînement Adidas")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport d'équipe")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon d'entraînement Adidas")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport intérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon d'entraînement Adidas")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller se muscler")
);

/* Température */

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon d'entraînement Adidas")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon d'entraînement Adidas")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon d'entraînement Adidas")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon d'entraînement Adidas")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon d'entraînement Adidas")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

/* Vêtement 2:

/* Styles */
INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Sport")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail du FC Bayern")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail du FC Bayern")
);

/* Activités */
INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail du FC Bayern")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Courir à l'extérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail du FC Bayern")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport d'équipe")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail du FC Bayern")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport intérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail du FC Bayern")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller se muscler")
);

/* Température */

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail du FC Bayern")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/* Vêtement 3: adidas shoes */

/* Styles */
INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Sport")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas")
);

/* Activités */
INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Courir à l'extérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport d'équipe")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport intérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller se muscler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);


/* Température */

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

/* Vêtement 4: Pantalon Chino

/* Styles */
INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Propre")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Chino")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Chino")
);

/* Activités */
INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Chino")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport d'équipe")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Chino")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux golf")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Chino")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Chino")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Chino")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Chino")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Chino")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Chino")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);


/* Température */

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Chino")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Chino")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Chino")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Chino")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Chino")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

/* Vêtement 5: Chandail à manches longues Kirby */

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues Kirby")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues Kirby")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues Kirby")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues Kirby")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues Kirby")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues Kirby")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues Kirby")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues Kirby")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues Kirby")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues Kirby")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues Kirby")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

/* Vêtement 6: Chaussures Mario */

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Mario")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Mario")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Mario")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Mario")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Mario")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Mario")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Mario")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Mario")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Mario")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

 /*Vêtement 7: Bottes de combat */

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gothique")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Bottes de combat")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Bottes de combat")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);
INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Bottes de combat")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Bottes de combat")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Bottes de combat")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Bottes de combat")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Bottes de combat")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Bottes de combat")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Bottes de combat")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Bottes de combat")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Bottes de combat")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Bottes de combat")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Bottes de combat")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/* Vêtement 8: Jeans cargo */

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gothique")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans cargo")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans cargo")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans cargo")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans cargo")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans cargo")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans cargo")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans cargo")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans cargo")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans cargo")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans cargo")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans cargo")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans cargo")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans cargo")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans cargo")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans cargo")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans cargo")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/* Vêtement 9: Chemise du chagrin */

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gothique")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chemise du chagrin")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chemise du chagrin")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chemise du chagrin")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chemise du chagrin")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chemise du chagrin")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chemise du chagrin")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chemise du chagrin")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chemise du chagrin")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chemise du chagrin")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à la plage")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chemise du chagrin")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chemise du chagrin")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chemise du chagrin")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chemise du chagrin")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chemise du chagrin")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chemise du chagrin")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/* Vêtement 10: Pantalon nomade */

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Hippie")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon nomade")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon nomade")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon nomade")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon nomade")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon nomade")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon nomade")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller se muscler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon nomade")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon nomade")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon nomade")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon nomade")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à la plage")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon nomade")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon nomade")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon nomade")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/* Vêtement 11: Chandail psychédélique */

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Hippie")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail psychédélique")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail psychédélique")
);

#################################################### Activités 
INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail psychédélique")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Courir à l'extérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail psychédélique")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail psychédélique")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail psychédélique")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail psychédélique")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail psychédélique")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail psychédélique")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail psychédélique")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail psychédélique")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à la plage")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail psychédélique")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail psychédélique")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail psychédélique")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail psychédélique")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/* Vêtement 12: Sandales Birkenstock */

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Hippie")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Sandales Birkenstock")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Sandales Birkenstock")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Sandales Birkenstock")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Sandales Birkenstock")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Sandales Birkenstock")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Sandales Birkenstock")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Sandales Birkenstock")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Sandales Birkenstock")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à la plage")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Sandales Birkenstock")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Sandales Birkenstock")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Sandales Birkenstock")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/* Vêtement 13: Nike Freerun 5.0 */

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Sport")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gothique")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
);

#################################################### Activités 
INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Courir à l'extérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport d'équipe")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux golf")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller se muscler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport intérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike Freerun")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/* Vêtement 14: Kangourou Underarmour */

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Sport")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Kangourou Underarmour")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Kangourou Underarmour")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Kangourou Underarmour")
);

#################################################### Activités 
INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Kangourou Underarmour")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Courir à l'extérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Kangourou Underarmour")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport d'équipe")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Kangourou Underarmour")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Kangourou Underarmour")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Kangourou Underarmour")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller se muscler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Kangourou Underarmour")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Kangourou Underarmour")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Kangourou Underarmour")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Kangourou Underarmour")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Kangourou Underarmour")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Kangourou Underarmour")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Kangourou Underarmour")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

/* Vêtement 15: Jogging Underarmour */

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Sport")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging Underarmour")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gothique")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging Underarmour")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging Underarmour")
);

#################################################### Activités 
INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging Underarmour")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Courir à l'extérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging Underarmour")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport d'équipe")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging Underarmour")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging Underarmour")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging Underarmour")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging Underarmour")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller se muscler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging Underarmour")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging Underarmour")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport intérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging Underarmour")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging Underarmour")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging Underarmour")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging Underarmour")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging Underarmour")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging Underarmour")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

/* Vêtement 16: t-shirt gris */

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Sport")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "T-shirt gris")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "T-shirt gris")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "T-shirt gris")
);

#################################################### Activités 
INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "T-shirt gris")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Courir à l'extérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "T-shirt gris")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport d'équipe")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "T-shirt gris")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "T-shirt gris")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "T-shirt gris")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "T-shirt gris")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "T-shirt gris")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller se muscler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "T-shirt gris")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "T-shirt gris")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport intérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "T-shirt gris")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "T-shirt gris")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à la plage")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "T-shirt gris")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/* Vêtement 17 : Chandail à manches longues mauve */

######################################################### Styles 
INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Propre")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues mauve")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues mauve")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Hippie")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues mauve")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues mauve")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues mauve")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues mauve")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues mauve")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues mauve")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues mauve")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues mauve")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues mauve")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues mauve")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues mauve")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues mauve")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues mauve")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues mauve")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues mauve")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

/* Vêtement 18 : Chandail à manches longues rouge */

######################################################### Styles 
INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Propre")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues rouge")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues rouge")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Hippie")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues rouge")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues rouge")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues rouge")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues rouge")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues rouge")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues rouge")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues rouge")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues rouge")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues rouge")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues rouge")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues rouge")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues rouge")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues rouge")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues rouge")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chandail à manches longues rouge")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

/* Vêtement 19: Short Khaki */

######################################################### Styles 
INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Khaki")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Khaki")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux golf")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Khaki")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Khaki")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Khaki")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Khaki")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Khaki")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Gris")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Gris")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/* Vêtement 20: Short Gris */

######################################################### Styles 
INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Gris")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Gris")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux golf")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Gris")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Gris")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Gris")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Gris")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Gris")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Gris")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Gris")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Sport")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Carreauté")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Carreauté")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Hippie")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Carreauté")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Carreauté")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Carreauté")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport d'équipe")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Carreauté")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Carreauté")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Carreauté")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Carreauté")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Carreauté")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport intérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Carreauté")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Carreauté")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à la plage")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Carreauté")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/* Vêtement 21: Chaussures Oxford */

######################################################### Styles 
INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Propre")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Oxford")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Oxford")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Oxford")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Oxford")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Oxford")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Oxford")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Oxford")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Oxford")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/* Polo gris */
######################################################### Styles 
INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Propre")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Polo gris")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Sport")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Polo gris")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Polo gris")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Polo rayé")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Polo rayé")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux golf")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Polo rayé")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Polo rayé")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Polo rayé")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Polo rayé")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Polo rayé")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Polo rayé")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Polo rayé")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Polo rayé")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à la plage")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Polo rayé")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Polo rayé")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/* Vêtement 22: Short Nike */

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Sport")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Nike")
);

#################################################### Activités 
INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Nike")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Courir à l'extérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Nike")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport d'équipe")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Nike")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller se muscler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Nike")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Nike")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport intérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Nike")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à la plage")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Nike")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Nike")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Nike")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Short Nike")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/* VÊtement 23: Jogging retroussé */

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Sport")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging retroussé")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging retroussé")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Hippie")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging retroussé")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging retroussé")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging retroussé")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport d'équipe")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging retroussé")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging retroussé")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging retroussé")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging retroussé")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller se muscler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging retroussé")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging retroussé")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport intérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging retroussé")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging retroussé")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging retroussé")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging retroussé")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging retroussé")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging retroussé")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jogging retroussé")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/* Vêtement 24 : Pantalon Cargo Levis Commuter */

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Sport")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Cargo Levis Commuter")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gothique")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Cargo Levis Commuter")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Cargo Levis Commuter")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Cargo Levis Commuter")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Cargo Levis Commuter")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Cargo Levis Commuter")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Cargo Levis Commuter")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Cargo Levis Commuter")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Cargo Levis Commuter")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Cargo Levis Commuter")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Cargo Levis Commuter")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Cargo Levis Commuter")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Cargo Levis Commuter")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Cargo Levis Commuter")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Cargo Levis Commuter")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Cargo Levis Commuter")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/* Vêtement 25: Pantalon avec patch */

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Hippie")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon avec patch")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon avec patch")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon avec patch")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon avec patch")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon avec patch")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon avec patch")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon avec patch")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à la plage")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon avec patch")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon avec patch")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon avec patch")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon avec patch")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon avec patch")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon avec patch")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/* Vêtement 26: Adidas Tech Runner */

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Sport")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Adidas Tech Runner")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gothique")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Adidas Tech Runner")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Adidas Tech Runner")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Adidas Tech Runner")
);

#################################################### Activités 
INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Adidas Tech Runner")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Courir à l'extérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Adidas Tech Runner")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport d'équipe")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Adidas Tech Runner")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Adidas Tech Runner")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Adidas Tech Runner")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Adidas Tech Runner")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller se muscler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Adidas Tech Runner")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Adidas Tech Runner")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport intérieur")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Adidas Tech Runner")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Adidas Tech Runner")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Adidas Tech Runner")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Adidas Tech Runner")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Adidas Tech Runner")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Adidas Tech Runner")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/*Insertion du 11/26/2014*/

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon camouflage")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gangster")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon camouflage")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Punk")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon camouflage")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon camouflage")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon camouflage")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon camouflage")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon camouflage")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon camouflage")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon camouflage")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon camouflage")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon camouflage")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon camouflage")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans 8bit")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans 8bit")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Tous les jours")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans 8bit")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Friperie")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans 8bit")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans 8bit")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans 8bit")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans 8bit")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans 8bit")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans 8bit")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans 8bit")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans 8bit")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans 8bit")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Tous les jours")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Friperie")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Far-West")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Hippie")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller chez le dentiste")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rose")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Hipster")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rose")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Propre")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rose")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rose")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rose")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller chez le dentiste")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rose")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rose")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rose")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rose")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rose")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rose")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rose")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rose")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rose")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rose")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rose")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/*------------------------------------------------------------------------*/

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rouge")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Hipster")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rouge")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Propre")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rouge")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rouge")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rouge")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rouge")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller chez le dentiste")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rouge")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rouge")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rouge")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rouge")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rouge")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rouge")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rouge")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rouge")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rouge")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rouge")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans rouge")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/*------------------------------------------------------------------------*/

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gothique")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans serré")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Punk")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans serré")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans serré")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Hipster")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans serré")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans serré")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans serré")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans serré")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller chez le dentiste")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans serré")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans serré")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans serré")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans serré")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans serré")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans serré")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans serré")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans serré")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans serré")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans serré")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Jeans serré")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/*----------------------------------------------------------------------------*/


######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Levis 513")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gangster")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Levis 513")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Far-West")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Levis 513")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Levis 513")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);
########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Levis 513")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Levis 513")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Levis 513")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Levis 513")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Levis 513")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Levis 513")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/*----------------------------------------------------------------------------*/


######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon à poches")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Friperie")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon à poches")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Hippie")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon à poches")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon à poches")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);
INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon à poches")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon à poches")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon à poches")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon à poches")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon à poches")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon à poches")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon à poches")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon à poches")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon à poches")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/*-------------------------------------------------------------------------------*/


######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Hipster")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Ck")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Propre")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Ck")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Ck")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Ck")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);
INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Ck")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Ck")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Ck")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Ck")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller chez le dentiste")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Ck")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Ck")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Ck")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Ck")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Ck")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Ck")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Ck")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Ck")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Ck")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Ck")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Ck")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/*-------------------------------------------------------------------------*/


######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gothique")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de cuir")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gangster")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de cuir")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de cuir")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);
INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de cuir")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de cuir")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de cuir")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller chez le dentiste")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de cuir")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de cuir")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de cuir")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de cuir")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de cuir")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de cuir")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de cuir")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de cuir")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de cuir")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de cuir")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/*--------------------------------------------------------------------------*/


######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Sport")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de golf")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de golf")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux golf")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de golf")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de golf")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de golf")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de golf")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de golf")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/*-----------------------------------------------------------------------------*/

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Sport")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de golf orange")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de golf orange")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux golf")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de golf orange")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de golf orange")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de golf orange")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de golf orange")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de golf orange")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/*-----------------------------------------------------------------------------------*/

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Propre")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Gucci")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Gucci")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Gucci")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller chez le dentiste")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Gucci")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Gucci")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Gucci")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Gucci")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Gucci")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Gucci")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Gucci")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Gucci")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

/*------------------------------------------------------------------------------*/

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Metal Gear Solid")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Metal Gear Solid")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Metal Gear Solid")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller chez le dentiste")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Metal Gear Solid")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Metal Gear Solid")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Metal Gear Solid")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Metal Gear Solid")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Metal Gear Solid")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Metal Gear Solid")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Metal Gear Solid")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Metal Gear Solid")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Metal Gear Solid")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon Metal Gear Solid")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

/*----------------------------------------------------------------------------------*/


######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Sport")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de compression Nike")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de compression Nike")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller se muscler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de compression Nike")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport intérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de compression Nike")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Courir à l'extérieur")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de compression Nike")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de compression Nike")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de compression Nike")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de compression Nike")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de compression Nike")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon de compression Nike")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

/*-------------------------------------------------------------------------------------*/


######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Propre")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon propre Azad Point")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Tous les jours")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon propre Azad Point")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon propre Azad Point")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon propre Azad Point")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller chez le dentiste")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon propre Azad Point")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon propre Azad Point")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon propre Azad Point")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon propre Azad Point")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon propre Azad Point")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon propre Azad Point")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon propre Azad Point")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon propre Azad Point")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Pantalon propre Azad Point")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

/*--------------------------------------------------------------------------------------*/


######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Sport")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike5")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Tous les jours")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike5")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike5")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gangster")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike5")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike5")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Courir à l'extérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike5")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport d'équipe")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike5")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire un sport intérieur")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike5")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller se muscler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike5")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike5")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à la plage")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike5")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike5")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike5")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike5")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Nike5")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/*----------------------------------------------------------------------------*/

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Propre")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 1")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Hipster")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 1")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 1")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 1")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 1")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 1")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 1")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller chez le dentiste")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 1")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 1")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 1")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 1")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 1")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);


INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 1")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);


/*------------------------------------------------------------------------------*/

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Propre")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 2")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Hipster")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 2")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 2")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 2")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 2")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 2")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 2")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller chez le dentiste")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 2")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 2")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 2")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 2")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 2")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures bateau 2")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/*-------------------------------------------------------------------------------*/

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Propre")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures simple")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Tous les jours")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures simple")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Hipster")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures simple")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures simple")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures simple")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures simple")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures simple")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures simple")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller chez le dentiste")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures simple")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures simple")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures simple")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures simple")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures simple")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures simple")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/*---------------------------------------------------------------------------------*/

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Propre")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures cheville haute")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Tous les jours")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures cheville haute")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Hipster")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures cheville haute")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures cheville haute")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures cheville haute")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures cheville haute")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures cheville haute")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures cheville haute")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller chez le dentiste")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures cheville haute")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures cheville haute")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures cheville haute")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures cheville haute")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures cheville haute")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures cheville haute")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures cheville haute")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chassures cheville haute")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Neige")
);

/*---------------------------------------------------------------------------------*/

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Sport")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Salomon de randonnée")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Salomon de randonnée")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'aventure")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Salomon de randonnée")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Courir à l'extérieur")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Salomon de randonnée")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Salomon de randonnée")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Salomon de randonnée")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Salomon de randonnée")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Salomon de randonnée")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

/*------------------------------------------------------------------------------------*/

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas Propre")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Sport")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas Propre")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas Propre")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Tous les jours")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas Propre")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas Propre")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas Propre")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas Propre")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller chez le dentiste")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas Propre")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas Propre")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas Propre")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas Propre")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas Propre")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas Propre")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas Propre")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas Propre")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas Propre")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas Propre")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures Adidas Propre")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

/*------------------------------------------------------------------------------------*/


######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gangster")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "NikeCamo")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "NikeCamo")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "NikeCamo")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "NikeCamo")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller chez le dentiste")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "NikeCamo")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "NikeCamo")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "NikeCamo")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "NikeCamo")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "NikeCamo")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "NikeCamo")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "NikeCamo")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "NikeCamo")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "NikeCamo")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/*-------------------------------------------------------------------------*/


######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Classic Converse")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Hippie")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Classic Converse")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Classic Converse")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Punk")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Classic Converse")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Friperie")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Classic Converse")
);
#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Classic Converse")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Classic Converse")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Classic Converse")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller chez le dentiste")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Classic Converse")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Classic Converse")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Classic Converse")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Classic Converse")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Classic Converse")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);


########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Classic Converse")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Classic Converse")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Classic Converse")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Classic Converse")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

/*-----------------------------------------------------------------------------*/

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gothique")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures dangereuse")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures dangereuse")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures dangereuse")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures dangereuse")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller chez le dentiste")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures dangereuse")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures dangereuse")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures dangereuse")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures dangereuse")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures dangereuse")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures dangereuse")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures dangereuse")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures dangereuse")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures dangereuse")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures dangereuse")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "Chaussures dangereuse")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);

/*---------------------------------------------------------------------------------*/

######################################################### Styles 

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Gamer")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "DC Fresh")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Hipster")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "DC Fresh")
);

INSERT INTO StylesVetements
(idStyle, idVetement)
VALUES
( (SELECT idStyle FROM Styles WHERE nomStyle = "Relaxe")
, (SELECT idVetement FROM Vetements WHERE nomVetement = "DC Fresh")
);

#################################################### Activités 

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "DC Fresh")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à l'école")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "DC Fresh")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller travailler")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "DC Fresh")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller chez le dentiste")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "DC Fresh")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Faire l'épicerie")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "DC Fresh")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller manger")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "DC Fresh")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller à un rendez-vous")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "DC Fresh")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller au cinéma")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "DC Fresh")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Jouer aux jeux vidéo")
);

INSERT INTO ActivitesVetements
(idVetement, idActivite)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "DC Fresh")
, (SELECT idActivite FROM Activites WHERE nomActivite = "Aller fêter")
);

########################################################### Température

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "DC Fresh")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Pluie")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "DC Fresh")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Venteux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "DC Fresh")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Nuageux")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "DC Fresh")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Ensoleillé")
);

INSERT INTO VetementsTemperatures
(idVetement, idTemperature)
VALUES
( (SELECT idVetement FROM Vetements WHERE nomVetement = "DC Fresh")
, (SELECT idTemperature FROM Temperatures WHERE nomClimat = "Froid")
);
/*--------------------------------------------------------------------------------*/

COMMIT;