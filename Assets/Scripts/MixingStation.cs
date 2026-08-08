using System.Collections.Generic;
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
        public int PriceOfItem;
}

public class MixingStation : MonoBehaviour
{
    public recepie[] recepies;
    public recepie Order;

    public int Stealing = 0;
    public int ServingCorrect = 0;
    public int ServingWrong = 0;
    public int reputation = 50;
    public int money = 0;
    public int tips = 0;

    List<IngredientType> selected = new List<IngredientType>();

    public void Start()
    {

        CustomerArrive();
        
    }

    public void Update()
    {
        if (reputation <= 0)
        {
            Debug.Log("Game Over");
        }
    }

    public void CustomerArrive()
    {
        int randomIndex = Random.Range(0, recepies.Length);
        Order = recepies[randomIndex];
        Debug.Log("Customer Order: " + Order.drinknames);
    }

    public void Ingredientclicked(IngredientType picked)
    {
        selected.Add(picked);
        Debug.Log("Selected Ingredients: " + string.Join(", ", selected));
    }

    public void Mixing()
    {
        if (selected.Count == Order.ingredients.Count) return;
        
            bool isMatch = selected.Count == Order.ingredients.Count;
        if (isMatch)
        {
            for (int i = 0; i < selected.Count; i++)
            {
                if (selected[i] != Order.ingredients[i])
                {
                    isMatch = false;
                    break;
                }
            }

        }
        else
        {
            ServingWrong++;
            reputation--;
            selected.Clear();
            Debug.Log("Wrong Served: " + Order.drinknames);
        } 
        CustomerArrive();

    }

    public void ServeDrink()
    {
        ServingCorrect++;
        reputation++;
    }

    public void StealDrink()
    {
        Stealing++;
        reputation--;
    }
}
