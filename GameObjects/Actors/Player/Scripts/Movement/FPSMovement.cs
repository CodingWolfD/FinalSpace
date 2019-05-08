using UnityEngine;
using System.Collections;

public class FPSMovement : MonoBehaviour
{
    [SerializeField] private string horizontalInputName; // CREATES A NEW STRING TO STORE THE INPUT NAME FOR THE HORIZONTAL INPUT SETTINGS SPECIFIED IN THE EDITOR
    [SerializeField] private string verticalInputName; // CREATES A NEW STRING TO STORE THE INPUT NAME FOR THE VERTICAL INPUT SETTINGS SPECIFIED IN THE EDITOR
    [SerializeField] private float moveSpeed; // CREATES A NEW FLOAT TO STORE THE SPEED VALUE THE PLAYER WILL MOVE AT 
    [SerializeField] private AnimationCurve jumpFallOff; // CREATES A NEW ANIMATION CURVE FOR THE PLAYERS JUMP  
    [SerializeField] private float jumpMultipler; // CREATES A NEW FLOAT TO STORE HOW HIGH THE PLAYER CAN JUMP WHEN THE JUMP KEY IS PRESSED
    [SerializeField] private KeyCode jumpKey; // CREATES A NEW KEYCODE TO STORE THE JUMP KEY (THIS CAN BE USED TO ALLOW THE PLAYER TO CHANGE THEIR KEY BINDS)

    [SerializeField] private bool isJumping; // CREATES A NEW BOOLEAN TO TELL THE PLAYER CONTROLLER IF THE PLAYER IS JUMPING OR NOT 
    [SerializeField] private bool isSprinting; // CREATES A NEW BOOLEAN TO TELL THE PLAYER CONTROLLER IF THE PLAYER IS SPRINTING OR NOT
    [SerializeField] private bool isCrouched; // CREATES A NEW BOOLEAN TO TELL THE PLAYER CONTROLLER IF THE PLAYER IS CROUCHING OR NOT

    private Animation_States animStates; // CREATES A NEW REFERENCE TO THE ANIMATION_STATES SCRIPT (USED TO KNOW WHAT ANIMATION TO PLAY BASED ON THE PLAYERS CURRENT STATE) 
    private CharacterController charController; // CREATES A REFERENCE TO UNITS BUILT IN CHARACTER-CONTROLLER SCRIPT

    private void Awake()
    {
        isCrouched = false; // SETS THE ISCROUCHED BOOLEAN TO FALSE WHEN THE GAME STARTS
        isSprinting = false; // SETS THE ISSPRINTING BOOLEAN TO FALSE WHEN THE GAME STARTS
        moveSpeed = 10; // SETS THE PLAYERS MOVESPEED TO 10 WHEN THE GAME STARTS

        charController = GetComponent<CharacterController>(); // INITIALISES THE CHARCONTROLLER VARIABLE TO THE CHARACTERCONTROLLER SCRIPT ATTATCHED TO THE GAMEOBJECT THIS SCRIPT IS ATTATCHED TO
        animStates = GetComponent<Animation_States>(); // INITIALISES THE ANIMSTATES VARIABLE TO THE ANIMSTATES SCRIPT ATTATCHED TO THE GAMEOBJECT THIS SCRIPT IS ATTATCHED TO

        if(SaveGame.foundSaveGame()) // IF THERE IS A SAVEGAME FILE FOUND WHEN THE LEVEL LOADS
        {
            loadPlayer(); // CALL THE LOADPLAYER METHOD WHICH LOADS IN THE DATA FROM THE .JSON FILEq
        }
    }

    private void Update()
    {
        playerMovement(); // CALLS THE PLAYERMOVEMENT SCRIPT EVERY TICK 
    }

    private void playerMovement()
    { 
        float horInput = Input.GetAxisRaw(horizontalInputName); // CREATES A NEW FLOAT CALLED HORINPUT AND INITIALISES IT TO THE SPECIFIED AXIS NAME IN THE INPUT SETTINGS
        float verInput = Input.GetAxisRaw(verticalInputName); // CREATES A NEW FLOAT CALLED VERINPUT AND INITIALISES IT TO THE SPECIFIED AXIS NAME IN THE INPUT SETTINGS

        Vector3 forwardMovement = transform.forward * verInput; // CREATES A NEW VECTOR3 (X, Y, Z) AND INITIALISES IT WITH THE VALUE OF THE PLAYERS FORWARD AXIS MULTIPLIED BY THE VERTICAL AXIS
        Vector3 rightMovement = transform.right * horInput; // CREATES A NEW VECTOR3 (X, Y, Z) AND INITIALISES IT WITH THE VALUE OF THE PLAYERS RIGHT AXIS MULTIPLIED BY THE HORIZONTAL AXIS

        charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1) * moveSpeed); /* CALLS THE CHARACTERCONTROLLERS SIMPLE MOVE FUNCTION AND
         PASSES IN A CLAMPED FORWARD MOVEMENT + RIGHTMOVEMENT AND GIVING IT A MAX VALUE OF 1 THE MULTIPLYING THAT VALUE BY THE MOVESPEED */

        jumpInput(); // CALLS THE JUMPINPUT METHOD TO HANDLE THE PLAYERS JUMP STATE
        sprint(); // CALLS THE SPRINT METHOD TO HANDLE THE PLAYERS SPRINT STATE
        crouch(); // CALLS THE CROUCH METHOD TO HANDLE THE PLAYERS CROUCH STATE

        playAnim(); // CALLS THE PLAYANIM METHOD WHICH IS RESPONSIBLE FOR HANDLING ALL THE ANIMATION FOR EACH STATE USING A ENUM 
    }

    private void playAnim()
    {
        if (charController.velocity.sqrMagnitude == 0) // IF THE PLAYER IS CURRENTLY IDLE OR NOT MOVING
        {
            animStates.animationStates = Animation_States.states.idle; // SET THE PLAYER ANIMATION STATE TO IDLE
        }
        else // IF THE PLAYER IS CURRENTLY MOVING
        {
            animStates.animationStates = Animation_States.states.walking; // SET THE PLAYER ANIMAITON TO WALK
        }

        if(isJumping) // IF THE PLAYER IS CURRENTLY JUMPING
        {
            animStates.animationStates = Animation_States.states.jumping; // SET THE PLAYER ANIMATION TO JUMP
        }
        else if (isSprinting && charController.velocity.sqrMagnitude != 0) // IF THE PLAYER IS SPRINTING AND THE PLAYER IS MOVING
        {
            animStates.animationStates = Animation_States.states.running; // SET THE PLAYER ANIMATION TO RUNNING
        }
    }

    private void jumpInput() // THIS METHOD HANDLES THE JUMP INPUT FOR THE PLAYER
    {
        if (Input.GetKeyDown(jumpKey) && !isJumping) // IF THE PLAYER HAS PRESSED THE JUMP KEY AND THE PLAYER ISN'T CURRENTLY JUMPING
        {
            isJumping = true; // SET ISJUMPING TO TRUE 
            StartCoroutine(jumpEvent()); // START THE COROUTINE JUMPEVENT
        }
    }

    private IEnumerator jumpEvent() 
    {
        yield return new WaitForSeconds(0.5f); // TELLS THE METHOD TO PAUSE FOR HALF A SECOND BEFORE RUNNING THE CODE BELOW

        charController.slopeLimit = 90; // SET THE CHARACTERS SLOPELIMIT TO 90

        float timeInAir = 0; // CREATES A NEW FLOAT TO STORE THE TIME IN THE AIR AND INITIALISE IT AS 0

        // THIS DO WHILE LOOP WILL RUN UNTIL THE PLAYER IS GROUNDED AND THE CHARACTERCOLLISION FLAGS ARE NOT EQUAL TO ABOVE
        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir); // CREATES A NEW FLOAT AND INITIALISES IT TO THE ANIMAITON CURVES EVALUATION FUNCTION PASSING IN THE TIMEINAIR
            charController.Move(Vector3.up * jumpForce * jumpMultipler * Time.deltaTime); // TELLS THE CHARACTERCONTROLLER TO MOVE UP ON THE Y-AXIS MULTUPLIED BY THE JUMPFORCE AND THE JUMPMULTIPLER THEN MULTIPLED BY TIME.DELTATIME
            timeInAir += Time.deltaTime; // INCREASE THE TIMEINAIR BY TIME.DELTATIME
            yield return null; // RETURN OUT OF THE LOOP
        }
        while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

        charController.slopeLimit = 90; // RESETS THE CHARACTERCONTROLLER SLOPELIMIT TO 45 DEGREES
        isJumping = false; // SET ISJUMPING TO FALSE
    }

    private void crouch() // THIS METHOD HANDLES THE PLAYERS CROUCHING STATE 
    {   
         if (Input.GetKeyDown(KeyCode.LeftControl) && !isCrouched) // IF THE PLAYER PRESSES THE LEFT CONTROL KEY AND THE PLAYER ISN'T CROUCHED
         {
            charController.height = 1.5f; // SET THE CHARACTER CONTROLLERS HEIGHT TO 1.5
            isCrouched = true; // SET ISCROUCHED TO TRUE
         }

        if (Input.GetKeyDown(KeyCode.LeftControl) && isCrouched) // IF THE PLAYER PRESSES THE LEFT CONTROL KEY AND THE PLAYER IS CROUCHED
         {
            charController.height = 2f; // SET THE CHARACTER CONTROLLER HEIGHT TO 2
            isCrouched = false; // SET ISCROUCHED TO FALSE
         }
    }

    private void sprint() // THIS METHOD HANDLES THE PLAYERS SPRINT STATE
    {
        if(Input.GetKey(KeyCode.LeftShift) && !isSprinting) // IF THE PLAYER PRESSES THE LEFT SHIFT KEY AND THE PLAYER IS CURRENTLY NOT SPRINTING
        { 
            moveSpeed = 20; // INCREASE THE PLAYERS MOVESPEED BY 10
            isSprinting = true; // SET ISSPRINTING TO TRUE
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && isSprinting) // IF THE PLAYER PRESSES THE LEFT SHIFT KEY AND THE PLAYER IS CURRENTLY SPRINTING
        { 
            moveSpeed = 10; // RESET THE MOVESPEED BACK TO 10
            isSprinting = false; // SET ISSPRINTING TO FALSE
        }
    }

    private void loadPlayer() // THIS METHOD IS ONLY CALLED IF THERE WAS A SAVE FILE LOCATED, THIS METHOD LOADS THE PLAYERS LAST POSITION IN THE WORLD
    {
        Save_Data data = SaveGame.loadGameFromJson(); // CREATES A NEW INSTANCE OF THE SAVE_DATA CLASS AND INITIALISES IT AS THE LOADGAMEFROMJSON METHOD WHICH RETURNS DATA STORED IN THE SAVEFILE

        Vector3 newPos; // CREATES A NEW VECTOR3 (X, Y, Z) TO STORE THE POSITION LAST SAVED
        Vector3 rotation; // CREATES A NEW VECTOR3 (X, Y, Z) TO STORE THE ROTATION LAST SAVED

        newPos.x = data.position[0]; // SETS THE NEWPOSITION.X TO THE SAVED POSITION ARRAY AT INDEX 0
        newPos.y = data.position[1]; // SETS THE NEWPOSITION.Y TO THE SAVED POSITION ARRAY AT INDEX 1
        newPos.z = data.position[2]; // SETS THE NEWPOSITION.Z TO THE SAVED POSITION ARRAY AT INDEX 2

        rotation.x = data.rotation[0]; // SETS THE ROTATION.X TO THE SAVED ROTATION ARRAY AT INDEX 0
        rotation.y = data.rotation[1]; // SETS THE ROTATION.Y TO THE SAVED ROTATION ARRAY AT INDEX 1
        rotation.z = data.rotation[2]; // SETS THE ROTATION.Z TO THE SAVED ROTATION ARRAY AT INDEX 2

        transform.position = newPos; // SETS THE PLAYERS POSITION TO THE NEWLY CREATED VECTOR3 NEWPOS
        transform.eulerAngles = rotation; // SETS THE PLAYERS ROTATION TO THE NEWLY CREATED VECTOR3 ROTATION
    }
}