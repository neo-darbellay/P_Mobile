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
    <td style="border: 1px solid black; text-align: center; padding: 6px;"></td>
  </tr>
  <tr>
    <th style="border: 1px solid black; text-align: left; padding: 6px;">
      Nom du testeur
    </th>
    <td style="border: 1px solid black; text-align: center; padding: 6px;"></td>
  </tr>
</table>

| Étape           | Description                                                                    | Remarque                                                          |
| :-------------- | :----------------------------------------------------------------------------- | :---------------------------------------------------------------- |
| Arrange / Given | Aller sur la page NewTask                                                      | On arrive à NewTask en appuyant sur le bouton plus dans une liste |
| Act / When      | Entrer une description (sans titre), puis sauvegarder                          | Il ne faut PAS ajouter de titre dans ce test                      |
| Assert / Then   | Vérifier qu'une erreur est apparue pour nous prévenir qu'il n'y a pas de titre |                                                                   |

Résultat :  
[ ] OK  
[ ] KO

Remarque :

>
