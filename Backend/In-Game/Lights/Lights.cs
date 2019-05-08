using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public int speed = 500; // DEFINES THE SPEED OF THE LIGHT
    public string direction; // USED TO TELL WHAT DIRECTION TO SPIN THE LIGHT IN 

    private void Update()
    {
        moveLight(); // CALLS THE MOVELIGHT METHOD EVERY TICK
    }

    private void moveLight()
    {
        if (direction.Equals("left")) // IF THE DIRECTION IS LEFT
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - speed * Time.deltaTime, transform.eulerAngles.z); // ROTATE THE LIGHT ON THE Y AXIS - SPEED * Time.deltaTime
        }
        else if(direction.Equals("right")) // IF THE DIRECTION IS RIGHT
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + speed * Time.deltaTime, transform.eulerAngles.z);  // ROTATE THE LIGHT ON THE Y AXIS + SPEED * Time.deltaTime
        }
    }
}