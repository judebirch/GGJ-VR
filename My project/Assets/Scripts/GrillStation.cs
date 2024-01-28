using System;
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

    private void FixedUpdate()
    {
        if (queuingCustomers.Count == 1)
        {
            if (queuingCustomers[0].CustomerState == CustomerController.CustomerStateEnum.Queuing)
            {
                queuingCustomers[0].StartWait();
            }
        }
    }

    public void Enqueue(CustomerController customerController)
    {
        queuingCustomers.Add(customerController);
        if (queuingCustomers.Count == 1)
        {
            customerController.StartWait();
        }
    }

    public Vector3 GetNextSpawnLocation()
    {
        return new Vector3(transform.position.x,0, transform.position.z) + transform.rotation*(spawnDirection.normalized*spawnDistance * (1 + queuingCustomers.Count));
    }

    public void Dequeue(CustomerController customerController)
    {
        
        for (var i = queuingCustomers.Count-1; i > 0; i--)
        {
            var customer = queuingCustomers[i];
            customer.transform.position = queuingCustomers[i - 1].transform.position;
        }
        if (queuingCustomers.Count == 1)
        {
            queuingCustomers[0].StartWait();
        }

        queuingCustomers.Remove(customerController);
    }

    [ContextMenu("Test_Dequeue")]
    public void Test_Dequeue()
    {
        if (queuingCustomers.Count > 0)
        {
            queuingCustomers[0].AcceptFood();
        }
    }
}