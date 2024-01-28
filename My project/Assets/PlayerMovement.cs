using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] ChairController controller;

    [SerializeField] int stationCount = 8;

    [SerializeField] TMP_Text debugText;

    int targetStation = 0;

    int prevStation = 1;
    




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
            Debug.Log("X pressed");
            MoveRight();
        }

        if (prevStation != targetStation) //if needs a change
        {
            controller.SetChairAngle((360 / stationCount) * targetStation);
            prevStation = targetStation;
            debugText.text = ((360 / stationCount) * targetStation).ToString();
        }
        else
        {
            //controller.SetChairSoftAngle((360 / stationCount) * targetStation);
        }
    }

    public void MoveRight()
    {
       
        targetStation = targetStation + 1;
        targetStation = targetStation % stationCount;
        Debug.Log("right " + targetStation);
        Debug.Log("calc:  " + (360 / stationCount) * targetStation);
        controller.SetChairAngle((360 / stationCount) * targetStation);
    }

    public void MoveLeft()
    {
        
        targetStation = targetStation - 1 ;
        if (targetStation < 0) { targetStation += stationCount; }
        targetStation = targetStation % stationCount;
        Debug.Log("Left " + targetStation);
        Debug.Log("calc:  " + (360 / stationCount) * targetStation);
    }

    public void SetStation(int station)
    {
        targetStation = station;
    }

}
