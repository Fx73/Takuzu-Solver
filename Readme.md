<h1>Projet Logique : Takuzu </h1>

<h2> Présentation </h2>

Ce programme a été réalisé dans le cadre d'un cours de Logique, en L2.

L'objectif est de résoudre une grille de Takuzu, grace aux regles de logique propositionnelles.
Les règles du jeu et de logiques sont présentes dans le rapport, accessible via le programme, ou dans les <a href="https://github.com/Fx73/Takuzu-Solver/blob/master/ProjetLogique2019/Resources/Rapport%20INF402%20Takuzu.pdf">ressources</a>.

L'interface réalisée permet de voir les différentes étapes de résolution.


<h2> Fonctionnement </h2>

On entre la grille à resoudre d'abord.

La résolution se fait en plusieurs étapes :
 - Création de l'ensemble de règles logiques à résoudre
 - Conversion des règles vers un format adapté au SAT
 - Résolution par un SAT au choix (2 inclus : 1 avec l'algorithme de base codé dans le programme, 1 récupéré ici : https://www.msoos.org/cryptominisat5, compilé et appelé dans les ressources)
 
La grille résolue peut-être affichée.

<h2> Compilation </h2>
<i>Il s'agit d'une application .Net, donc compilable et executable sous Windows.</i>

- Telecharger le projet et le décompresser

- Ouvrir l'invite de commande et se placer dans le repertoire de projet (où doit se trouver "ProjetLogique2019.sln")

- Executer :

      C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild "ProjetLogique2019.sln" /p:Configuration=Release
   Ou tout autre version du framework disponible (à partir du v4)
 
 
Le depot original est disponible sur Azure:
https://dev.azure.com/CochonCorp/ProjetLogique2019
