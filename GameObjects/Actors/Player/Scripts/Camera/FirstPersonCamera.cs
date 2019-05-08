using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private string mouseXInput, mouseYInput; // USED TO DEFINE WHAT STRING WE USE FOR THE MOUSEXINPUT AND MOUSEYINPUT 
    [SerializeField] private float mouseSensitivity; // USED TO SET THE MOUSESENSITIVITY

    [SerializeField] private Transform playerBody; // USED TO REFERENCE THE PLAYERBODY 

    private float xAxisClamp; // USED TO CLAMP THE CAMERA 

	private void Awake ()
    {
        xAxisClamp = 0; // SETS THE XAXISCLAMP TO 0
        lockCursor(); // CALLS THE LOCKCURSOR METHOD
	}

    private void lockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // SETS THE CURSOR TO LOCKED
        Cursor.visible = false; // SETS THE CURSOR TO INVISIBLE WHEN THE LEVEL IS LOADED
    }

    private void Update()
    {
        cameraRotation(); // CALLS THE CAMERAROTATION METHOD
    }

    private void cameraRotation()
    {
         float mouseX = Input.GetAxis(mouseXInput) * mouseSensitivity * Time.deltaTime; // SETS THE MOUSEX TO THE INPUT.GETAXIS OF THE MOUSEXINPUT * MOUSESENSITIVITY * FPS
         float mouseY = Input.GetAxis(mouseYInput) * mouseSensitivity * Time.deltaTime; // SETS THE MOUSEY TO THE INPUT.GETAXIS OF THE MOUSEYINPUT * MOUSESENSITIVITY * FPS

         xAxisClamp += mouseY; // SETS THE XAXISCLAMP += MOUSEY 

         if (xAxisClamp > 90) // IF THE XAXIS IS GREATER THAN 90
         {
            xAxisClamp = 90; // CLAMPS THE XAXIS AT 90
            mouseY = 0; // SETS THE MOUSEY TO 0
            clampxAxis(270); // CALLS THE CLAMAXIS METHOD WITH THE PAREMETER OF 270
         }
         else if (xAxisClamp < -90) // IF THE XAXIS IS LESS THAN 90
         {
            xAxisClamp = -90; // CLAMPS THE XAXIS AT -90
            mouseY = 0; // SETS THE MOUSEY TO 0
            clampxAxis(90); // CALLS THE CLAMAXIS METHOD WITH THE PAREMETER OF 90
         }

         transform.Rotate(Vector3.left * mouseY); // ROTATE THE CAMERA USING THE VECTOR3.LEFT CALL * MOUSEY CO-ORDINATES
         playerBody.Rotate(Vector3.up * mouseX); // ROTATE THE PLAYER BODY WITH THE VECTOR3.UP CALL * MOUSEX
    }

    private void clampxAxis(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles; // SETS THE VECTOR3 EULERROTATION TO THE CURRENT PLAYERS EULERROTATION
        eulerRotation.x = value; // SETS THE EULERROTATION.X TO THE PAREMETER VALUE
        transform.eulerAngles = eulerRotation; // SETS THE PLAYERS ROTATION TO THE NEW EULERROTATION
    }
}