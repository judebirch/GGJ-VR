using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientStation : Station
{
    public FoodItem Ingredient;

    public GameObject FoodPrefab;

    public TMPro.TMP_Text Text;

    public SpawnIngredientButton SpawnIngredientButton;

    public Transform SpawnPoint;

    private void Awake()
    {
        Text.SetText(Ingredient.name);

        SpawnIngredientButton.Touched += Spawn;
    }

    private void Spawn()
    {
        var newObject = Instantiate(FoodPrefab, SpawnPoint.position, Quaternion.identity);

        newObject.GetComponentInChildren<FoodGameObject>().Setup(Ingredient);
    }

    public override void OnInteract()
    {
        if (GameManager.Instance.HeldFoodItem == null)
        {
            GameManager.Instance.AddFood(Ingredient);
        }
    }
}
