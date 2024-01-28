using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIngredientButton : MonoBehaviour
{
    public System.Action Touched;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Controller"))
        {
            // Spawn
            Touched?.Invoke();
        }
    }
}
