using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collisions : MonoBehaviour
{
    [SerializeField] private Door_States ds;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Door")
        {
            if(ds.getDoorCanOpen())
            {
                print("Opening Door");
                col.gameObject.GetComponent<Animator>().Play("Open");
                ds.setDoorState(true);
            }
        }
    }
}