using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [SerializeField]
    private float lastSpawnTime = 10;

    [SerializeField]
    private float nextSpawnTime = 0;

    [SerializeField]
    private float spawnSpeedMultiplier = 1;

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
        if (Time.time - lastSpawnTime > nextSpawnTime)
        {
            SpawnCustomer();
            nextSpawnTime = spawnCurve.Evaluate(customersSpawned)/spawnSpeedMultiplier;
            lastSpawnTime = Time.time;
        }
    }

    [ContextMenu("SpawnCustomer")]
    public CustomerController SpawnCustomer()
    {
        GrillStation randomGrill = grillStations[Random.Range(0, grillStations.Count)];
        CustomerController newCustomer = SpawnCustomerAt(randomGrill.GetNextSpawnLocation(),Quaternion.Euler(randomGrill.transform.eulerAngles.x,randomGrill.transform.eulerAngles.y-180,randomGrill.transform.eulerAngles.z));
        randomGrill.Enqueue(newCustomer);
        newCustomer.SetGrill(randomGrill);
        customersSpawned++;
        GameManager.Instance.WaitingCustomers = customersSpawned - GameManager.Instance.Served;
        return newCustomer;
    }

    public CustomerController SpawnCustomerAt(Vector3 position,Quaternion rotation)
    {
        CustomerController newCustomer = Instantiate(customerBase, position, rotation, transform).GetComponent<CustomerController>();
        newCustomer.SetFood(cookedFoodList[Random.Range(0,cookedFoodList.Length)]);
        generatedCustomers.Add(newCustomer);
        return newCustomer;
    }

    [ContextMenu("Test_Spawn")]
    public void Test_Spawn()
    {
        SpawnCustomerAt(new Vector3(Random.Range(-10,10),0,Random.Range(-10,10)),Quaternion.identity);
    }
}
