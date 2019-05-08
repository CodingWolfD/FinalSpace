using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_States : MonoBehaviour
{
    private int timer; // USED TO KEEP TRACK OF HOW LONG THE DOOR HAS BEEN OPEN FOR
    public bool isOpen; // USED TO KNOW IF THE DOOR IS CURRENTLY OPEN OR NOT
    public bool canOpen; // USED TO TELL IF THE CURRENT DOOR CAN BE OPENED OR NOT

    public void Update()
    {
        if(canOpen)
        {
            if (isOpen) // IF THE DOOR IS CURRENTLY OPEN
            {
                timer++; // INCREASE THE TIMER EVERY TICK

                if (timer == 300) // IF THE TIMER IS EQUAL TO 300
                {
                    print("Closing Door");
                    isOpen = false; // SET ISOPEN TO FALSE
                    this.GetComponent<Animator>().Play("Close"); // PLAY THE DOOR CLOSE ANIMATION
                    timer = 0; // SET THE TIMER TO 0
                }
            }
        }
    }

    public bool getDoorOpen()
    {
        return isOpen; // RETURNS ISOPEN
    }

    public bool getDoorCanOpen()
    {
        return canOpen;
    }

    public void setDoorState(bool newDoorState)
    {
        isOpen = newDoorState; // SETS ISOPEN TO NEWDOORSTATE
    }

    public void setDoorCanBeOpened(bool canOpenDoor)
    {
        canOpen = canOpenDoor; // SETS canOpen TO canOpenDoor
    }
}