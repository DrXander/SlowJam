using UnityEngine;
using UnityEngine.UI;

public class Ingredientbuttons : MonoBehaviour
{
    public MixingStation mixingStation;
    public IngredientType ingredientType;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(
            () => mixingStation.Ingredientclicked(ingredientType));
    }
}
