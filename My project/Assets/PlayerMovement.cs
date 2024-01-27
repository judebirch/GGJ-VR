using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] ChairController controller;

    [SerializeField] int stationCount = 8;

    int targetStation = 0;





    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetButtonDown("XRI_Right_PrimaryButton"))
        {
            Debug.Log("A pressed");
            MoveLeft();
        }
        if (Input.GetButtonDown("XRI_Left_PrimaryButton"))
        {
            Debug.Log("Left pressed");
            MoveRight();
        }

        controller.SetChairAngle((360/stationCount)*targetStation);
    }

    public void MoveRight()
    {
        targetStation = targetStation + 1;
        targetStation = targetStation % stationCount;
    }

    public void MoveLeft()
    {
        targetStation = targetStation - 1 ;
        targetStation = targetStation % stationCount;
    }

    public void SetStation(int station)
    {
        targetStation = station;
    }

}
