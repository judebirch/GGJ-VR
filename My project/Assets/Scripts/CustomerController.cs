using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [Serializable]
    public enum CustomerStateEnum
    {
        Queuing,
        Waiting,
        Annoyed,
        Served,
        Angry
    }
    [SerializeField]
    private CustomerStateEnum CustomerState = CustomerStateEnum.Queuing;
    [Header("Food")]
    [SerializeField]
    private FoodItem requestFood;

    [Header("Patients")]
    [SerializeField]
    [Range(0,1f)]
    private float currentPatientValue = 1f;

    [SerializeField]
    private float patientDecay = 0.01f;

    [SerializeField]
    private GrillStation grillStation;

    [Header("Ragdoll")]
    [SerializeField]
    private GameObject ragdoll;
    [SerializeField]
    GameObject model;

    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private Rigidbody ragdollRB;
    [SerializeField]
    float ragdollForce = 10f;

    public ItemRequestUI ItemRequestUI;


    [Header("VFX")]
    [SerializeField]
    private ParticleSystem[] vfx_Annoy;
    [SerializeField]
    private ParticleSystem[] vfx_Success;
    
    [Header("SFX")]

    [SerializeField]
    private AudioSource[] sfx_Annoy;
    [SerializeField]
    private AudioSource[] sfx_Success;
    // Start is called before the first frame update

    
    private void Awake()
    {
        // var[] loadedFood
        // requestFood = ;
        ragdoll.SetActive(false);
    }

    public void SetFood(FoodItem foodItem)
    {
        requestFood = foodItem;
        ItemRequestUI.Setup(foodItem);
    }

    public void SetGrill(GrillStation grillStation)
    {
        this.grillStation = grillStation;
    }

    void Start()
    {
        //DEBUG
        // CustomerState = CustomerStateEnum.Queuing;
    }

    // Update is called once per frame
    void Update()
    {
        ItemRequestUI.SetProgress(currentPatientValue);

        switch (CustomerState)
        {
            case CustomerStateEnum.Queuing:
                break;
            case CustomerStateEnum.Waiting:
                if (currentPatientValue > 0f)
                {
                    currentPatientValue -= patientDecay * Time.deltaTime;
                }

                if (currentPatientValue < .3f)
                {
                    ChangeState(CustomerStateEnum.Annoyed);
                }
                break;
            case CustomerStateEnum.Annoyed:
                if (currentPatientValue > 0f)
                {
                    currentPatientValue -= patientDecay * Time.deltaTime;
                }

                else
                {
                    ChangeState(CustomerStateEnum.Angry);
                }
                break;
            case CustomerStateEnum.Served:
                break;
            case CustomerStateEnum.Angry:
                break;
        }
    }

    void ChangeState(CustomerStateEnum newState)
    {
        CustomerState = newState;
        switch (newState)
        {
            case CustomerStateEnum.Queuing:
                break;
            case CustomerStateEnum.Waiting:
                break;
            case CustomerStateEnum.Annoyed:
                Debug.Log("Customer is annoyed!!!");
                PlayerMovement.current.SetStation(GameManager.Instance.Stations.IndexOf(grillStation));
                foreach (ParticleSystem system in vfx_Annoy)
                {
                    system.Play();
                }
                foreach (AudioSource system in sfx_Annoy)
                {
                    system.Play();
                }

                break;
            case CustomerStateEnum.Served:
                GameManager.Instance.Served++;
                model.SetActive(false);
                ragdoll.SetActive(true);
                characterController.enabled = false;
                foreach (ParticleSystem system in vfx_Success)
                {
                    system.Play();
                }
                foreach (AudioSource system in sfx_Success)
                {
                    system.Play();
                }

                Invoke(nameof(PushRagdoll),.2f);
                break;
            case CustomerStateEnum.Angry:
                Debug.Log("Customer is ANGRYU!!!");
                GameManager.Instance.GameOver();
                break;
        }
    }

    private void PushRagdoll()
    {
        ragdollRB.AddForce(ragdollForce * -transform.forward, ForceMode.Acceleration);
    }

    [ContextMenu("AcceptFood")]
    public void AcceptFood(FoodGameObject foodGO = null)
    {
        grillStation.Dequeue(this);
        ChangeState(CustomerStateEnum.Served);
        
        
        //TODO: change destroy to ragdoll
        Destroy(gameObject,10f);
        if (foodGO)
        {
            Destroy(foodGO.gameObject);
        }
    }

    public void StartWait()
    {
        ChangeState(CustomerStateEnum.Waiting);
    }

    private void OnCollisionEnter(Collision other)
    {
        // if(other.collider.CompareTag("Food"))
        FoodGameObject foodGO = other.gameObject.GetComponentInChildren<FoodGameObject>();
        if (!foodGO)
        {
            foodGO = other.gameObject.GetComponentInParent<FoodGameObject>();

        }
        if (foodGO)
        {
            if (foodGO.Item.name.Equals(requestFood.name))
            {
                AcceptFood(foodGO);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        FoodGameObject foodGO = other.gameObject.GetComponentInChildren<FoodGameObject>();
        if (!foodGO)
        {
            foodGO = other.gameObject.GetComponentInParent<FoodGameObject>();

        }
        
        if (foodGO)
        {
            if (foodGO.Item.name.Equals(requestFood.name))
            {
                AcceptFood(foodGO);
            }
        }
    }
}
