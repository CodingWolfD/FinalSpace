using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    private Camera cam; // USED FOR A REFERENCE TO THE CAMERA
    [SerializeField] private LayerMask layer; // USED TO SET WHAT LAYER THE RAYCAST COLLIDER CAN DETECT
    private Text interactionText; // REFERENCE TO THE TEXT COMPONENT TO BE USED WHEN THE PLAYER PRESSES THE USE KEY
    private GameObject spc; // USED AS A REFERENCE TO THE SPACESHIP GAMEOBJECT
    private SpaceShip_Movement sm; // USED AS A REFERENCE TO THE SHIP MOVEMENT SCRIPT
    private GameObject inGameHUD; // USED AS A REFERENCE TO THE INGAME HUD GAMEOBJECT
    private GameObject keypad_UI_errorScreen; // USED AS A REFERENCE TO THE KEYPADUI GAMEOBJECT
    private GameObject keypad_UI_keypadScreen; // USED AS A REFERENCE TO THE KEYPADUI GAMEOBJECT
    private GameObject player; // USED AS A REFERENCE TO THE PLAYER GAMEOBJECT
    private GameObject HUD;

	private void Awake ()
    {
        cam = GetComponent<Camera>(); // GETS THE CAMERA COMPONENT FROM THE GAMEOBJECT THE SCRIPT IS ATTACHED TO 
        player = GameObject.Find("Player"); // FINDS THE PLAYER GAMEOBJECT IN THE SCENE
        interactionText = GameObject.Find("interactionText").GetComponent<Text>(); // FINDS THE INTERACTIONTEXT IN THE SCENE
        inGameHUD = GameObject.Find("HUD"); // FINDS THE GAMEOBJECT HUD IN THE SCENE
        keypad_UI_errorScreen = GameObject.Find("Error_Screen"); // FINDS THE GAMEOBJECT KEYCODE_MENU IN THE SCENE
        keypad_UI_keypadScreen = GameObject.Find("Keypad");

        keypad_UI_errorScreen.SetActive(false);
        keypad_UI_keypadScreen.SetActive(false);
        HUD = GameObject.Find("HUD");

        interactionText.text = ""; // SETS THE INTERACTION TEXT TO NOTHING WHEN THE GAME STARTS
        findCamerasAndMovementScripts(); // CALLS THE FINDCAMERASANDMOVEMENTSCRIPTS METHOD
    }
	
	private void Update ()
    {
        enableRayCast(); // CALLS THE ENABLE RAYCAST METHOD EVERY TICK
	}

    private void findCamerasAndMovementScripts()
    {
        spc = GameObject.Find("Spaceship_Camera"); // FINDS THE SPACESHIP_CAMERA GAMEOBJECT ATTACHED TO THE SHIP

        player = GameObject.Find("Player"); // FINDS THE PLAYER GAMEOBJECT IN THE SCENE

        sm = GameObject.Find("Ship").GetComponent<SpaceShip_Movement>(); //FINDS THE SHIP GAMEOBJECT AND ALLOWS US ACCES TO THE SHIP_MOVEMENT SCRIPT
        spc.SetActive(false); // SETS  THE SPACESHIP_CAMERA TO DISABLED

        sm.enabled = false; // SETS THE SHIPMOVEMENT SCRIPT TO DISABLED
    }

    private void enableRayCast()
    {
        RaycastHit hit; // DEFINES THE RAYCAST COLLIDER

        Ray ray = cam.ScreenPointToRay(Input.mousePosition); // SETS THE RAYCAST TO BE IN THE MIDDLE OF THE SCREEN
        Debug.DrawRay(new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z), ray.direction, Color.black); // DRAWS THE RAY IN THE EDITOR FOR DEBUGGING

        if(Physics.Raycast(ray, out hit, 30, layer)) // IF THE RAYCAST HITS AN OBJECT ON THE SET LAYERMASK
        {
            if(hit.collider != null) // IF THE COLLIDER FOR THE RAYCAST IS HITTING AGAINST A GAMEOBJECT
            {
                if(hit.collider.tag == "Interactable") // IF THE HIT COLLIDER HAS HIT AN OBJECT WITH THE TAG INTERACTABLE
                {
                    hit.collider.enabled = true; // ENABLE THE COLLIDER
                    interactionText.text = "Press [E] to interact";

                    if(Input.GetKeyDown(KeyCode.E)) // IF THE PLAYER PRESSES E WHILE THE RAYCAST IS OVER AN OBJECT WITH THE TAG INTERACTABLE
                    {
                        checkObject(hit.collider.name, hit); // CALLS THE CHECKOBJECT METHOD AND PASSES IN THE NAME OF THE OBJECT AND THE RAYCAST HIT COLLIDER
                    }
                }
                else if(hit.collider.tag == "equipable") // IF THE HIT COLLIDER HAS HIT AN OBJECT WITH THE TAG EQUIPABLE
                {
                    hit.collider.enabled = true; // ENABLE THE COLLIDER
                    interactionText.text = "Press [E] to pickup";

                    if (Input.GetKeyDown(KeyCode.E)) // IF THE PLAYER PRESSES E WHILE THE RAYCAST IS OVER AN OBJECT WITH THE TAG EQUIPABLE 
                    {
                        checkObject(hit.collider.name, hit); // CALLS THE CHECKOBJECT METHOD AND PASSES IN THE NAME OF THE OBJECT AND THE RAYCAST HIT COLLIDER
                    }
                }
                else if (hit.collider.tag == "Vehicle") // IF THE HIT COLLIDER HAS HIT AN OBJECT WITH THE TAG VEHICLE
                {
                    hit.collider.enabled = true; // ENABLE THE COLLIDER
                    interactionText.text = "Press [E] to Fly";
                        
                    if (Input.GetKeyDown(KeyCode.E)) // IF THE PLAYER PRESSES E WHILE THE RAYCAST IS OVER AN OBJECT WITH THE TAG VEHICLE
                    {
                        checkObject(hit.collider.name, hit); // CALLS THE CHECKOBJECT METHOD AND PASSES IN THE NAME OF THE OBJECT AND THE RAYCAST HIT COLLIDER
                    }
                }
                else if (hit.collider.tag == "NPC") // IF THE HIT COLLIDER HAS HIT AN OBJECT WITH THE TAG VEHICLE
                {
                    hit.collider.enabled = true; // ENABLE THE COLLIDER
                    interactionText.text = "Press [E] to talk";

                    if (Input.GetKeyDown(KeyCode.E)) // IF THE PLAYER PRESSES E WHILE THE RAYCAST IS OVER AN OBJECT WITH THE TAG VEHICLE
                    {
                        checkObject(hit.collider.name, hit); // CALLS THE CHECKOBJECT METHOD AND PASSES IN THE NAME OF THE OBJECT AND THE RAYCAST HIT COLLIDER
                    }
                }
                else // IF THE PLAYERS RAYCAST IS OVER ANY OBJECTS THAT DON'T HAVE THE SPECIFIED TAGS ABOVE
                { 
                    interactionText.text = ""; // SETS THE INTERACTION TEXT TO EMPTY
                }
            }
        }
    }

    private void checkObject(string objectName, RaycastHit hit)
    {
        switch(objectName) // THIS SWITCH STATEMENT WILL HANDLE ALL THE OBJECT NAMES PASSED INTO THIS METHOD
        {
           case "Tablet": // IF THE PLAYER HAS PRESSED E OVER THE TABLE
           {
              Destroy(hit.collider.gameObject); // DESTROY THE TABLET GAMEOBJECT
           }
               break;
          case "Ship": // IF THE PLAYER HAS PRESSED E OVER THE SHIP
          {
              SceneManager.LoadScene(6); // LOADS THE END GAME SCENE
              Cursor.lockState = CursorLockMode.None; // UNLOCKS THE CURSOR ON THE SCREEN
              Cursor.visible = true; // SETS THE CURSOR TO VISIBLE
          }
              break;
          case "Broken_Ship": // IF THE PLAYER HAS PRESSED E OVER THE BROKEN SHIP
          {
              // was going to have something here when the player pressed E on a broken ship
          }
                break;
          case "Dying_NPC": // IF THE PLAYER HAS PRESSED E OVER THE Dying NPC
          {
              if(!NewDialogue.instance.getSpeakingInProgress()) 
              {
                 NewDialogue.instance.createNewDialogue(0, 5, "NPC", NewDialogue.instance.getNPCDialogue());
              }
          }
             break;
          case "Keycode_Card":
          {
              NewDialogue.instance.createNewDialogue(3, 6, "Player", NewDialogue.instance.getPlayerDialogue());
              Destroy(hit.collider.gameObject);
          }
             break;
          case "Keycode_Computer":
          {
              Level1UIManager.instance.showErrorScreen();
              HUD.SetActive(false);
          }
             break;
        }
    }
}