using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class CustomerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject customerBase;
    [SerializeField]
    private List<CustomerController> generatedCustomers;
    [SerializeField]

    private FoodItem[] foodList;
    [SerializeField]
    
    private FoodItem[] cookedFoodList;
    
    
    [Header("Spawning")]
    [SerializeField]
    List<GrillStation> grillStations;

    [SerializeField]
    private int customersSpawned;

    [SerializeField]
    private AnimationCurve spawnCurve;

    public static CustomerManager current;

    private void Awake()
    {
        current = this;
        
        foodList = Resources.LoadAll<FoodItem>("Food");
        List<FoodItem> cooked = new List<FoodItem>();
        foreach (FoodItem item in foodList)
        {
            if (item.isCookFood)
            {
                cooked.Add(item);
            }
        }

        cookedFoodList = cooked.ToArray();
    }

    private void Update()
    {
        
    }

    [ContextMenu("SpawnCustomer")]
    public CustomerController SpawnCustomer()
    {
        GrillStation randomGrill = grillStations[Random.Range(0, grillStations.Count)];
        CustomerController newCustomer = SpawnCustomerAt(randomGrill.GetNextSpawnLocation());
        randomGrill.Enqueue(newCustomer);
        return newCustomer;
    }

    public CustomerController SpawnCustomerAt(Vector3 position)
    {
        CustomerController newCustomer = Instantiate(customerBase, position, Quaternion.identity, transform).GetComponent<CustomerController>();
        newCustomer.SetFood(cookedFoodList[Random.Range(0,cookedFoodList.Length)]);
        generatedCustomers.Add(newCustomer);
        return newCustomer;
    }

    [ContextMenu("Test_Spawn")]
    public void Test_Spawn()
    {
        SpawnCustomerAt(new Vector3(Random.Range(-10,10),0,Random.Range(-10,10)));
    }
}
