using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Manager : MonoBehaviour
{ 
    private GameObject mainMenu; // REFERENCE TO THE MAINMENU GAMEOBJECT
    private GameObject settingsMenu; // REFERENCE TO THE SETTINGSMENU GAMEOBJECT
    private GameObject errorMessage; // REFERENCE TO THE ERRORMESSAGE GAMEOBJECT
    private GameObject continueButton; // REFERENCE TO THE CONTINUEBUTTON GAMEOBJECT
    private GameObject overWriteMenu; // REFERENCE TO THE OVERWRITEMENU GAMEOBJECT

    private Text saveFileText; // REFERENCE TO THE SAVEFILETEXT TEXT COMPONENT
    private string currentMenu; // USED TO STORE WHAT MENU THE GAME IS CURRENTLY ON

    private void Start ()
    {
        mainMenu = GameObject.Find("Main_Menu"); // FINDS THE MAINMENU GAMEOBJECT IN THE SCENE
        settingsMenu = GameObject.Find("Settings_Menu"); // FINDS THE SETTINGSMENU GAMEOBJECT IN THE SCENE
        errorMessage = GameObject.Find("ErrorMessage"); // FINDS THE ERROR MESSAGE GAMEOBJECT IN THE SCENE
        continueButton = GameObject.Find("ContinueGameBtn"); // FINDS THE CONTINUEGAMEBUTTON IN THE SCENE
        overWriteMenu = GameObject.Find("OverwriteMenu"); // FINDS THE OVERWRITEMENU IN THE SCENE
        saveFileText = GameObject.Find("SaveFileText").GetComponent<Text>(); // FINDS THE SAVEFILETEXT FOR DISPLAYING WHAT TIME AND DATE THE GAME WAS LAST SAVED

        settingsMenu.SetActive(false); // DEACTIVATES THE SETTINGSMENU ON GAME START
        errorMessage.SetActive(false); // DEACTIVATES THE ERRORMESSAGE WHEN THE GAME STARTS
        overWriteMenu.SetActive(false); // SETS THE OVERWRITEMENU TO DEACTIVE WHEN THE GAME STARTS
    }
	
	private void FixedUpdate ()
    {
        exitCurrentMenu(); // CALLS THE EXITCURRENTMENU METHOD EVERY TICK
	}

    public void continueGame() // THIS METHOD WILL BE CALLED BY THE CONTINUE BUTTON ON THE MAIN MENU
    {
        checkifSaveExists(); // CALLS THE CHECKIFSAVE EXISTS METHOD

        bool saveFilePresent = SaveGame.foundSaveGame(); // STORES THE BOOL FOUNDSAVEGAME IN SAVEFILEPRESENT

        if (saveFilePresent) // IF THE GAME HAS A SAVE FILE ALREADY
        {
            Save_Data data = SaveGame.loadGameFromJson(); // CREATE A NEW PLAYER_DATA CALLED DATA AND INITIALISE IT TO THE SAVEGAME.LOADGAMES METHOD
            SceneManager.LoadScene(data.level); // CALLS THE SCENEMANAGER.LOADSCENE TO LOAD THE SAVED LEVEL STORE IN THE PLAYER DATA CLASS
            Time.timeScale = 1; // SETS THE TIMESCALE TO 1 TO RESOLVE ANY PROBLEMS WITH TIME
        }
        else // IF THERE IS NO SAVE FILE FOUND
        {
            noSaveFound(); // CALL THE NOSAVEFOUND METHOD
        }
    }
    public void checkifSaveExists() // THIS METHOD WILL CHECK IF A SAVE FILE HAS BEEN CREATED PREVIOUSLY
    {
        string path = Application.persistentDataPath + "/save1.json"; // THIS WILL DEFINE A STRING CALLED PATH SET TO THE DEFAULT SAVE LOCATION OF THE SAVE FILE

        if (File.Exists(path)) // IF THE SAVE FILE EXISTS
        {
            SaveGame.setSaveGameFound(true); // SETS THE SAVEGAME FILEFOUND TO TRUE
        }
        else // IF THE SAVE FILE DOESN'T EXIST 
        {
            SaveGame.setSaveGameFound(false); // SETS THE SAVEGAME FILEFOUND TO FALSE
        }
    }

    private void MainMenu()
    {
        settingsMenu.SetActive(false); // SETS THE SETTINGSMENU TO DEACTIVE
        overWriteMenu.SetActive(false); // SETS THE OVERWRITEMENU TO DEACTIVE
        mainMenu.SetActive(true); // SETS THE MAINMENU TO ACTIVE

        currentMenu = mainMenu.name; // SETS THE CURRENTMENU THAT'S ACTIVE TO THE MAINMENU

        Time.timeScale = 1; // SETS THE TIME.TIMESCALE BACK TO 1
    }

    public void settingMenu()
    {
        settingsMenu.SetActive(true); // SETS THE SETTINGSMENU TO ACTIVE
        mainMenu.SetActive(false);  // SETS THE MAINMENU TO DEACTIVE
        currentMenu = settingsMenu.name; // CALLS THE KEYBOARDSELECTION'S CHECKCURRENTMENU PASSING THE MENU THATS CURRENTLY ACTIVE
    }

    private void exitCurrentMenu()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) // IF THE PLAYER PRESSES ESCAPE WHEN IN ONE OF THE MENUS
        {
            if(settingsMenu.activeSelf == true) // IF THE SETTINGSMENU IS ACTIVE
            {
                MainMenu(); // CALL THE MAINMENU METHOD
            }
        }
    }

    public void cancel() // THIS WILL BE CALLED WHENEVER THE PLAYER IS PROMTPED TO OVERRIDE A CURRENT SAVE AND THE CLICK CANCEL
    {
        MainMenu(); // CALLS THE MAINMENU METHOD
    }

    public void quitGame()
    {
        Application.Quit(); // QUITS THE APPLICATION 
    }

    public void overrideSave() // THIS METHOD IS CALLED WHEN THE USER CHOSES TO OVERRIDE THEIR CURRENT SAVE FILE
    {
        string path = Application.persistentDataPath + "/save1.json"; // CREATES A STRING CALLED PATH WHICH IS SET TO THE FILE PATH WITH THE FILES NAME
        File.Delete(path); // DELETES THE FILE FROM THE PRESET PATH
        SaveGame.setSaveGameFound(false); // SETS THE SAVEGAME GAMEFOUND TO FALSE 
        overwriteOldGame(); // CALLS THE OVERWRITE MENU GAME
    }

    public void overwriteOldGame() // CREATES A NEW GAME USING THE SLOT 1-5
    {
        if(SaveGame.foundSaveGame()) // IF THERE IS A SAVEGAME WHICH HAS BEEN FOUND AND THE PLAYER PRESSES THE NEW GAME BUTTON
        {
            Save_Data data = SaveGame.loadGameFromJson(); // CREATES A NEW SAVE_DATA INSTANCE CALLED DATA 
            overWriteMenu.SetActive(true); // SETS THE OVERWRITEMENU TO ACTIVE
            mainMenu.SetActive(false); // SETS THE MAINMENU TO DEACTIVE

            currentMenu = overWriteMenu.name; // SETS THE CURRENTMENU TO EQUAL THE OVERRIDEMENU.NAME

            saveFileText.text = "Game saved: " + data.date + " at " + data.time; // SETS THE SAVEFILETEXT TO THE DATE AND TIME THE GAME WAS LAST SAVED
        }
        else
        {
            SceneManager.LoadScene(2); // LOADS THE LOADING SCREEN FOR THE CUTSCENE
        }
    }

    public string getCurrentMenuName() // CREATES A GETTER USING A STRING FOR THE CURRENT MENU THAT'S ACTIVE
    {
        return currentMenu; // RETURNS THE NAME OF THE CURRENTLY ACTIVE SCENE
    }

    public void noSaveFound() // USED TO DETERMINE IF A SAVE HAS BEEN LOCATED IN THE CHOSEN SAVE SLOT
    {
        errorMessage.SetActive(true); // SETS THE ERROR MESSAGE STATE TO TRUE
        StartCoroutine(disableErrorMessage(3)); // STARTS THE COROUTINE DISABLEERRORMESSAGE AND WAITS FOR A PERIOD OF TIME
    }

    private IEnumerator disableErrorMessage(float waitForSeconds)
    {
        yield return new WaitForSeconds(waitForSeconds); // MAKES THE METHOD WAIT FOR HOW LONG IS SPECIFIED BEFORE THE CODE CONTINUES
        errorMessage.SetActive(false); // SETS THE ERRORMESSAGE TO DEACTIVATED ONCE TIME IS UP
    }
}