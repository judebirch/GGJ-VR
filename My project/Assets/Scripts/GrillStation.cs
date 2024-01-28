using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrillStation : Station
{
    // private float _timer;
    [SerializeField]
    private List<CustomerController> queuingCustomers;

    [SerializeField]
    private Vector3 spawnDirection;

    [SerializeField]
    private float spawnDistance;

    private CustomerController currentCustomer => queuingCustomers[0];

    public override void OnFoodAdded(FoodItem food)
    {
    }

    public override void OnInteract()
    {
    }

    private void Update()
    {
    }

    public void Enqueue(CustomerController customerController)
    {
        queuingCustomers.Add(customerController);
    }

    public Vector3 GetNextSpawnLocation()
    {
        return transform.position + transform.rotation*(spawnDirection.normalized*spawnDistance * (1 + queuingCustomers.Count));
    }

    public void Dequeue(CustomerController customerController)
    {
        
        for (var i = queuingCustomers.Count-1; i > 0; i--)
        {
            var customer = queuingCustomers[i];
            customer.transform.position = queuingCustomers[i - 1].transform.position;
        }

        queuingCustomers.Remove(customerController);
    }
}