using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Food", order = 1)]
public class FoodItem : ScriptableObject
{
    public GameObject Model;

    public FoodItem Cooked;

    public float CookTime;
    public bool isCookFood;


    public List<Recipe> Recipes;
}

[System.Serializable]
public class Recipe
{
    public FoodItem OtherIngredient;
    public FoodItem Result;
}