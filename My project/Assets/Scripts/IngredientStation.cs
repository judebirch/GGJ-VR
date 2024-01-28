using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientStation : Station
{
    public FoodItem Ingredient;

    public GameObject FoodPrefab;

    public TMPro.TMP_Text Text;

    public SpawnIngredientButton SpawnIngredientButton;

    public Transform SpawnPoint;

    public float TimeSinceLastSpawn = 0;

    public Image ItemImage;

    public Image NameBacker;

    private void Awake()
    {
        Text.SetText(Ingredient.name);
        ItemImage.sprite = Ingredient.Icon;

        SpawnIngredientButton.Touched += Spawn;

        NameBacker.color = Ingredient.NameColour;

    }

    private void Spawn()
    {
        if(TimeSinceLastSpawn < 0.1f)
        {
            return;
        }

        var newObject = Instantiate(FoodPrefab, SpawnPoint.position, Quaternion.identity);

        newObject.GetComponentInChildren<FoodGameObject>().Setup(Ingredient);

        TimeSinceLastSpawn = 0;
    }

    public override void OnInteract()
    {
        if (GameManager.Instance.HeldFoodItem == null)
        {
            GameManager.Instance.AddFood(Ingredient);
        }
    }

    private void Update()
    {
        TimeSinceLastSpawn += Time.deltaTime;
    }
}
