# Protocoles de test : TaskManager

<table style="border-collapse: collapse; width: 400px;">
  <tr>
    <th style="border: 1px solid black; text-align: left; padding: 6px;">
      Version de l’application testée
    </th>
    <td style="border: 1px solid black; text-align: center; padding: 6px;">
      1.0.0
    </td>
  </tr>
  <tr>
    <th style="border: 1px solid black; text-align: left; padding: 6px;">
      Date du test
    </th>
    <td style="border: 1px solid black; text-align: center; padding: 6px;">
    28.05.2026
    </td>
  </tr>
  <tr>
    <th style="border: 1px solid black; text-align: left; padding: 6px;">
      Nom du testeur
    </th>
    <td style="border: 1px solid black; text-align: center; padding: 6px;">
      Jonathan Junod
    </td>
  </tr>
</table>

| Étape           | Description                                       | Remarque                                        |
| :-------------- | :------------------------------------------------ | :---------------------------------------------- |
| Arrange / Given | Aller sur la page ShowTasks                       | On arrive à ShowTasks en appuyant sur une liste |
| Act / When      | Secouer le téléphone                              | Assez fort, mais pas trop                       |
| Assert / Then   | Vérifier que la page CreateTask a bien été ouvert |                                                 |

Résultat :  
[ ] OK  
[x] KO

Remarque :

> Pécision : le CdC ayant changé, la page d'accueil est la page des cartes. Nous arrivons donc déjà au bon endroit.  
> Souci : Le test échoue par ce que l'emulateur utilisé n'est pas capable d'envoyer les données de l'accéléromètre. Pour plus de précision, veuillez vous référer au code en commentaire [dans ce commit](https://github.com/neo-darbellay/P_Mobile/commit/acf1c9e1c7794605f0e6efe1115157c333835556)
