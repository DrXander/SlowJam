using NUnit.Framework;
using System.Collections.Generic;
using Unity.Multiplayer.Center.Common;
using UnityEngine;

public enum IngredientType
    {
        milk,
        strawberry,
        banana
    }

[System.Serializable]
public class recepie
    {
        public string drinknames;
        public List<IngredientType> ingredients = new List<IngredientType>();
        public powerEffect effect;
}

public class MixingStation : MonoBehaviour
{
    public recepie[] recepies;
    public int Stealing;
    public int ServingCorrect;
    public int ServingWrong;
    public int reputation;
    List<IngredientType> selected = new List<IngredientType>();

    public void Start()
    {
        CustomerArrive();
    }

    public void CustomerArrive()
    {
        
    }
    public void Ingredientclicked(IngredientType picked)
    {
        selected.Add(picked);
        Debug.Log("Selected Ingredients: " + string.Join(", ", selected));
    }

    public void Mixing()
    {
        foreach (var recipe in recepies)
        {
            if (selected.Count == recipe.ingredients.Count)
            {
                bool isMatch = true;
                for (int i = 0; i < selected.Count; i++)
                {
                    if (selected[i] != recipe.ingredients[i])
                    {
                        isMatch = false;
                        break;
                    }
                }
                if (isMatch)
                {
                    ServingCorrect++;
                    reputation++;
                    selected.Clear();
                    return;
                }
            }
        }
        ServingWrong++;
        reputation--;
        selected.Clear();
    }
}
