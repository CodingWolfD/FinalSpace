using System.Collections;
using UnityEngine;

public class SpaceShip_Movement : MonoBehaviour
{
    private float speed; // USED TO DETERMINE THE SPEED OF THE SHIP'S MOVEMENT
    private int rotationSpeed; // USED TO DETERMINE THE SPEED OF THE SHIP'S ROTATION SPEED
    private bool forwardKey; // USED TO SPECIFY WHAT KEY IS THE FORWARD KEY
    private bool backKey; // USED TO SPECIFY WHAT KEY IS THE BACK KEY
    private bool shiftKey; // USED TO DETERMINE WHAT KEY IS THE HYPERBOOST KEY

    private void Start()
    {
        speed = 100; // SETS THE SHIPS MOVEMENT SPEED TO 100 WHEN THAT GAME STARTS
        rotationSpeed = 10; // SETS THE SHIPS ROTATION SPEED TO 10 WHEN THE GAME STARTS
    }

    private void FixedUpdate()
    {
        inputMovement(); // CALLS THE INPUTMOVEMENT EVERY 10 TICKS OR SO, WE ARE USING THE FIXEDUPDATE INSTEAD OF THE NORMAL UPDATE FOR OPTIMIZATION
    }

    private void inputMovement()
    {
        forwardKey = Input.GetKey(KeyCode.W); // INITIALIZES THE FORWARD KEY AS THE W KEY
        backKey = Input.GetKey(KeyCode.S); // INITIALIZES THE BACK KEY AS THE S KEY
        shiftKey = Input.GetKeyDown(KeyCode.LeftShift); // INITIALIZES THE HYPERBOOST KEY AS THE SHIFT KEY

        if (forwardKey) // IF THE FORWARD KET IS BEING PRESSED
        { 
            transform.position += transform.right * speed * Time.deltaTime; // SET THE SHIPS CURRENT POSITION PLUS TRANSFORM.RIGHT MUITPLY BY THE SPEED MULTIPLY BY THE TIME.DELTATIME
        }
        else if(backKey) // iF THE BACK KEY IS BEING PRESSED
        {
            transform.position -= transform.right * speed * Time.deltaTime; // SET THE SHIPS CURRENT TRANSFORM.POSITION - TRANSFORM.RIGHT * MULTIPLY BY SPEED MULTIPLY BY TIME.DELTATIME
        }

        if(shiftKey) // IF THE HYPERBOOST KEY IS BEING PRESSED
        {
            StartCoroutine(speedUpShip()); // CALLS THE COROUTINE SPEEDUPSHIIP
        }
    }   

    private IEnumerator speedUpShip()
    {
        speed = 400; // SETS THE SHIPS SPEED TO 400

        yield return new WaitForSeconds(3); // WAITS IN THIS METHOD FOR 3 SECONDS

        speed = 100; // RESETS THE SPEED BACK TO 100
    }
}