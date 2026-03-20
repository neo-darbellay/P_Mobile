# P_AppMobile - Cahier des charges

## 1 INFORMATIONS GENERALES

> Lieu de travail : ETML
>
> Client : PPT/RFA/JMY
>
> Apprentis : Jonathand Junod et Néo Darbellay
>
> Dates de réalisation : 2ème année
>
> Charge de travail : 37p

## 2 PROCÉDURE

> - Tous les apprentis réalisent le projet sur la base d'un cahier des charges.
> - Le cahier des charges est présenté, commenté et discuté en classe.
> - Les apprentis sont entièrement responsables de la sécurité et sauvegarde de leurs
>   données.
> - En cas de problème, les apprentis avertissent leur chef de projet au plus vite.
> - Les apprentis ont la possibilité d’obtenir de l’aide externe, mais ils doivent le mentionner.
> - Les informations utiles à l’évaluation de ce projet sont disponibles au chapitre 7.5

## 3 TITRE

Task Manager

## 4 SUJET

La société _Task4all_ souhaite développer et commercialiser une application de gestion des tâches. Afin d'être opérationnelle au plus vite, sans sacrifier des fonctionnalités, l'application fonctionnera en local et s'appuyera sur une base de données dans le même appareil. L'application servira à aider les utilisateurs à gérer leurs tâches et leur permettra de suivre leur avancée facilement.

## 5 MATÉRIEL ET LOGICIEL À DISPOSITION

• PC ETML
• Accès à Internet
• Visual Studio

## 6 PRÉREQUIS

Modules de programmation C# (319,329,335)
Module d'UI/UX (322)

<div class="page"/>

## 7 CAHIERS DES CHARGES

### 7.1 Éléments obligatoires

#### 7.1.1 Maquette de l’application

Tout commence par une phase créative où le papier et le crayon sont autorisés,
même recommandés tout en étant remplaçables par une application de votre
choix (figma, excalidraw, Visio, PowerPoint, …).
Le but est d’avoir chaque écran ainsi qu’une information de navigation entre ceux
ci.
Finalement, un document numérique au format PDF sera livré.

#### 7.1.2 Base de donnée

- La base de données sera réalisée avec SQLite.
- La db sera stockée en local.
- La gestion des utilisateurs sera donc ignorée, puisque plusieurs utilisateurs n'utilisent pas la même base de données.
- Le MCD et le MLD seront réalisés en utilisant l'application Looping et convertir en png, puis mis dans un fichier PDF
- La structure de la DB sera fait en sorte que :
  - On puisse ajouter des catégories/listes aux tâches
  - Un status (fait/non fait)

#### 7.1.3 Taches

- En tant qu'utilisateur, je souhaite :
  - Avoir une vue sur les tâches d'une liste, afin de voir ce qu'il me reste à faire.
  - Avoir une vue sur les tâches d'une liste qui ont été réalisées, afin de voir ce que j'ai déjà fait.
  - Pouvoir créer des tâches, afin d'organiser ma journée.
    - Les tâches contiennent un titre, et une description, qui est optionnelle
  - Pouvoir valider des tâches, afin de constater mon avancée.
  - Pouvoir annuler la validation de la tâche, afin de revenir sur ma décision en cas de missclick.
  - Pouvoir modifier des tâches, afin de corriger/adapter la description, le titre ou la/les conditions de validation.
  - Pouvoir supprimer des tâches, afin de tenir mes listes à jour.

#### 7.1.4 Listes

- En tant qu'utilisateur, je souhaite :
  - Avoir une vue sur toutes mes listes, afin de voir comment j'ai organisé mes tâches.
  - Pouvoir créer des listes de tâches, afin de m'organiser.
    - Une liste contient un titre, une description optionnelle et une couleur.
  - Pouvoir modifier mes listes, afin de tenir leurs informations à jour.
  - Pouvoir ajouter des tâches aux listes, afin de les trier.
  - Pouvoir supprimer des listes, afin de ne pas avoir de listes trop anciennes inutilisées (la suppression d'une liste supprime aussi les tâches).

<div class="page"/>

#### 7.1.5 Tags

- En tant qu'utilisateur, je souhaite :
  - Pouvoir ajouter des tags aux tâches, afin de voir rapidement ce qu'elle concerne.
    - Un tag contient un titre et une couleur.
  - Pouvoir supprimer des tags, afin de ne pas en avoir d'inutiles.
  - Pouvoir modifier un tag, afin de le mettre à jour.

#### 7.1.6 Tests

- Un document PDF contenant au moins 3 scénarios de tests manuels doit être
  créé et passé ainsi que présent dans l’archive livrée.

#### 7.1.7 Journal de travail

- Le JDT est fait en utilisant l'application GitJournal ([https://github.com/ETML-INF/gitjournal](https://github.com/ETML-INF/gitjournal))

#### 7.1.8 Versioning

- Utilisation de GitHub
  - Au moins un commit par jour de travail
  - Message de commit précis
  - .gitignore pertinent (pas de binaires dans le dépôt, à part les fichiers PDF de retour)

### 7.2 Eléments optionnels

- En tant qu'utilisateur, je souhaite :
  - Pouvoir trier mes tâches, afin de repérer facilement celles qui m'intéresses
    - Par date de création croissant/décroissant
    - En rapport au status de la tâche (Fait/Non-fait)
    - Par tag
    - Par délai
    - Custom (l'utilisateur peut lui-même bouger les tâches dans l'ordre qu'il veut)

  - Pouvoir faire une recherche textuelle de tâches depuis la page d'accueil ainsi que depuis la page d'une liste, afin de retrouver les tâches qui m'intéressent.

  - Pouvoir appuyer sur un tag pour voir toutes les tâches qui ont le même tag, afin de voir, par exemple, totues les tâches avec le tag "Important", peut importe la liste

  - Pouvoir ajouter des sous-tâches, afin de ne pas avoir trop de listes

  - Pouvoir faire des tâches répetées toutes les X temps (minutes, heures, jours), afin de ne pas avoir à recréer / annuler les tâches à chaque fois et à se simplifier la vie

  - Pouvoir ajouter des deadlines aux tâches, afin de se donner un point précis dans le temps pour finir la tâche

  - Un guide d'utilisant sous le format d'un bouton aide, afin de faire comprendre à l'utilisateur comment l'application fonctionne

  - Avoir une liste dynamique "Aujourd'hui" qui nous permet de sélectionner des tâches d'autre listes, afin que je puisse gérer ma journée correctement
    - Il faudra aussi que, si une tâche a une deadline ou se répète, qu'elle se mette automatiquement dans la liste "Aujourd'hui"

  - Pouvoir définir une règle de suppression des tâches lors de la création d'une liste
    - Après X temps qu'une tâche est noté "done", elle se fait effacer automatiquement
    - Par défaut, la règle n'est pas active, et n'efface rien si l'utilisateur ne le précise pas

### 8 Livrables

- Archive au format ZIP contenant :
  - Un document PDF avec les maquettes de l’application
  - Un document PDF contenant au moins 3 scénarios de tests manuels
  - Les journaux de travail en format PDF
  - Le code source complet de l’application respectant les normes de codage
    - Incluant le dossier .git

### 9 Évaluation

Selon le règlement pratique ETML.
