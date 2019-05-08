using UnityEngine;

public class SpaceShip_Camera : MonoBehaviour
{
    [SerializeField] private string mouseXInput, mouseYInput; // USED TO DEFINE WHAT STRING WE USE FOR THE MOUSEXINPUT AND MOUSEYINPUT 
    [SerializeField] private float mouseSensitivity; // USED TO SET THE MOUSESENSITIVITY

    private float xAxisClamp; // USED TO CLAMP THE CAMERA 
    [SerializeField] private Transform playerBody; // USED TO REFERENCE THE PLAYERBODY 

    void Start()
    { 
        lockCursor(); // CALLS THE LOCK CURSOR METHOD WHEN THE PLAYER ENTERS THE SHIP TO DISABLE THE ABILITY TO CLICK OUT OF THE GAME
        xAxisClamp = 0; // SETS THE CAMERS XAXIS CLAMP TO 0
    }

    void FixedUpdate()
    {
        rotateCameraAroundShip(); // CALLS THE ROTATE CAMERA AROUND SHIP SCRIPT
    }

    private void lockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // SETS THE CURSOR TO LOCKED
        Cursor.visible = false; // SETS THE CURSOR TO INVISIBLE WHEN THE LEVEL IS LOADED
    }

    private void rotateCameraAroundShip()
    {
        float mouseX = Input.GetAxis(mouseXInput) * mouseSensitivity * Time.deltaTime; // SETS THE MOUSEX TO THE INPUT.GETAXIS OF THE MOUSEXINPUT * MOUSESENSITIVITY * FPS
        float mouseY = Input.GetAxis(mouseYInput) * mouseSensitivity * Time.deltaTime; // SETS THE MOUSEY TO THE INPUT.GETAXIS OF THE MOUSEYINPUT * MOUSESENSITIVITY * FPS

        xAxisClamp += mouseY; // SETS THE XAXISCLAMP += MOUSEY 

        playerBody.Rotate(Vector3.back * mouseY); // ROTATE THE CAMERA USING THE VECTOR3.LEFT CALL * MOUSEY CO-ORDINATES
        playerBody.Rotate(Vector3.up * mouseX); // ROTATE THE PLAYER BODY WITH THE VECTOR3.UP CALL * MOUSEX
    }

    private void clampxAxis(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles; // SETS THE VECTOR3 EULERROTATION TO THE CURRENT PLAYERS EULERROTATION
        eulerRotation.x = value; // SETS THE EULERROTATION.X TO THE PAREMETER VALUE
        transform.eulerAngles = eulerRotation; // SETS THE PLAYERS ROTATION TO THE NEW EULERROTATION
    }
}