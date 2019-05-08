using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class NewDialogue : MonoBehaviour
{
    private Text dialogueText; // REFERENCE TO THE TEXT USED FOR THE DIALOGUE
    private GameObject dialogueBox; // REFERENCE TO THE GAMEOBJECT FOR THE DIALOGUEBOX
    private string str; // USED TO HOLD THE CHARACTERS FOR THE DIALOGUE

    private string[] dialogueStrings_player; // USED TO STORE ALL THE STRINGS LOCATED IN THE TEXT FILE
    private string[] dialogueStrings_spaceteam; // USED TO STORE ALL THE STRINGS LOCATED IN THE TEXT FILE
    private string[] dialogueStrings_npc; // USED TO STORE ALL THE STRINGS LOCATED IN THE TEXT FILE

    private TextAsset dialoguePlayer; // USED AS A REFERENCE TO THE TEXT FILE THE STRINGS ARE STORED IN
    private TextAsset dialogueNPC; // USED AS A REFERENCE TO THE TEXT FILE THE STRINGS ARE STORED IN
    private TextAsset dialogueSpaceteam; // USED AS A REFERENCE TO THE TEXT FILE THE STRINGS ARE STORED IN

    private string newDialogue; // USED TO STORE THE CURRENTLY REQUESTED STRING FROM THE TEXT FILE

    [SerializeField] private Sprite spaceTeamImage; // USED TO STORE THE SPRITE FOR THE SPACETEAM ICON
    [SerializeField] private Image dialogueBoxImage; // USED TO STORE THE IMAGE FOR THE BACKGROUND OF THE DIALOGUE BOX
    [SerializeField] private Sprite playerImage; // USED TO STORE THE SPRITE FOR THE PLAYER ICON

    private static string chosenCharacter; // USED TO STORE THE CHARACTER THAT SPEAKS DURING THE DIALOGUE, MADE STATIC SO WE CAN SAVE THEM IN THE .JSON FILE
    private static int currentDialogueIndex; // USED TO STORE THE CURRENT DIALOGUE INDEX, MADE STATIC SO WE CAN SAVE THEM IN THE .JSON FILE
    private bool speakingInProgress;

    public static NewDialogue instance;

    private void Start ()
    {
        instance = this;
        dialogueText = GameObject.Find("DialogueText").GetComponent<Text>(); // FINDS THE DIALOGUETEXT COMPONENT IN THE SCENE
        dialogueBox = GameObject.Find("DialogueBox"); // FINDS THE DIALOGUE BOX GAMEOBJECT IN THE SCENE

        loadTextFile(); // CALLS THE LOADTEXTFILE METHOD WHICH LOADS IN THE REQUESTED FILE FROM THE RESOURCES FOLDER
    }  

    private void loadTextFile()
    {
        dialoguePlayer = Resources.Load("Text_Files/DialogueText_Player") as TextAsset; // SETS THE TEXTASSET VARIABLE "DIALOGUE" TO THE TEXT FILE FOUND AT THAT PATH AND MANUALLY CASTS IT TO A TEXT ASSET
        dialogueNPC = Resources.Load("Text_Files/DialogueText_NPC") as TextAsset; // SETS THE TEXTASSET VARIABLE "DIALOGUE" TO THE TEXT FILE FOUND AT THAT PATH AND MANUALLY CASTS IT TO A TEXT ASSET
        dialogueSpaceteam = Resources.Load("Text_Files/DialogueText_Spaceteam") as TextAsset; // SETS THE TEXTASSET VARIABLE "DIALOGUE" TO THE TEXT FILE FOUND AT THAT PATH AND MANUALLY CASTS IT TO A TEXT ASSET

        if (dialoguePlayer != null) // IF THE TEXT FILE IS LOCATED
        {
            Debug.Log("Located Player Text File!"); // PRINTS IN THE CONSOLE THAT THE TEXT FILE WAS LOCATED FOR DEBUG PURPOSES
        }
        else // IF THE TEXT FILE ISN'T LOCATED
        {
            Debug.Log("Couldn't Located Player Text File!"); // PRINTS IN THE CONSOLE THAT THE TEXT FILE WASN'T LOCATED FOR DEBUG PURPOSES
        }

        if (dialogueSpaceteam != null) // IF THE TEXT FILE IS LOCATED
        {
            Debug.Log("Located Spaceteam Text File!"); // PRINTS IN THE CONSOLE THAT THE TEXT FILE WAS LOCATED FOR DEBUG PURPOSES
        }
        else // IF THE TEXT FILE ISN'T LOCATED
        {
            Debug.Log("Couldn't Located Text File!"); // PRINTS IN THE CONSOLE THAT THE TEXT FILE WASN'T LOCATED FOR DEBUG PURPOSES
        }

        dialogueStrings_player = (dialoguePlayer.text.Split('\n')); // FILLS THE ARRAY WITH ALL THE STRINGS LOCATED IN THE TEXT FILE AND SPLITS THEM AT THE NEW LINE
        dialogueStrings_spaceteam = (dialogueSpaceteam.text.Split('\n')); // FILLS THE ARRAY WITH ALL THE STRINGS LOCATED IN THE TEXT FILE AND SPLITS THEM AT THE NEW LINE
        dialogueStrings_npc = (dialogueNPC.text.Split('\n')); // FILLS THE ARRAY WITH ALL THE STRINGS LOCATED IN THE TEXT FILE AND SPLITS THEM AT THE NEW LINE

        loadProgressFromSave(); // CALLS THE LOADPROGRESSFROMSAVE TO CHECM IF A SAVE FILE EXISTS
    }

    private void loadProgressFromSave()
    {
        if (SaveGame.foundSaveGame()) // IF THERE IS A SAVE FILE STORED IN THE USERS COMPUTER
        {
            Save_Data p = SaveGame.loadGameFromJson(); // INSTATIATES A NEW PLAYER_DATA OBJECT AND ACCESSES THE INFORMATION STORED IN THE SAVE FILES
            createNewDialogue(p.currentDialogueIndex, 1, p.character, dialogueStrings_player); // SETS THE CURRENT DIALOGUE TEXT TO THE SAVED ARRAY INDEX STORED IN THE .JSON FILE
        }
        else if(!SaveGame.foundSaveGame()) // IF THERE IS NO SAVE FILE FOUND
        {
            createNewDialogue(0, 1, "SpaceTeam", dialogueStrings_spaceteam); // START THE DIALOGUE FROM THE BEGINNING 
        }
    }

    public void createNewDialogue(int index, int dialogueTextAmount, string character, string[] text) 
    {
        speakingInProgress = true;
        currentDialogueIndex = index; // SETS THE CURRENTDIALOGUEINDEX TO THE CHOSEN INDEX
        newDialogue = text[index]; // SETS THE STRING NEWDIALOGUE EQUAL TO THE CONTENTS OF THE ARRAY AT THE SPECIFIED INDEX POSITION
        Debug.Log(newDialogue.ToString()); // PRINTS OUT THE REQUSTED STRING FOR DEBUG PURPOSES

        chosenCharacter = character; // SETS THE CHOSENCHARACTER TO THE CHARACTER PASSED IN AS A PAREMETER

        if (character.Equals("Player") || character.Equals("NPC")) // IF THE SPECIFIED CHARACTER IS PLAYER
        {
            dialogueBoxImage.sprite = playerImage; // SETS THE DIALOGUE IMAGE TO THE PLAYER IMAGE
        }

        if (character.Equals("SpaceTeam")) // IF THE SPECIFIED CHARACTER IS THE SPACETEAM
        {
            dialogueBoxImage.sprite = spaceTeamImage; // SETS THE DIALOGUE IMAGE TO THE SPACE TEAM IMAGE
        }

        dialogueBox.SetActive(true); // SETS THE DIALOGUEBOX TO ACTIVE IF IT'S FALSE
        StartCoroutine(displayDialogueFromText(index, dialogueTextAmount, text)); // CALLS THE COROUTINE WHICH WILL BE REPSONSIBLE FOR DISPLAYING THE DIALOGUE BOX AND TEXT
    }

    private IEnumerator displayDialogueFromText(int index, int dialogueStringCount, string[] dialogueFile)
    {
        speakingInProgress = true; // sets speakingInProgress to true
        int i = 0; // USED TO INCREMENT THE CHARACTERS THAT ARE DISPLAYING
        str = ""; // SETS THE CURRENT TEXT TO BLANK

        for (int j = index; j < dialogueStringCount; j++) // set J to the specified index, while j is less than the specified dialogueStringCount increase j
        {
            newDialogue = dialogueFile[j]; // set newDialogue to dialogueStrings to the current count of j
            str = ""; // reset str to nothing
            i = 0; // reset the counter to 0

            while (i < newDialogue.Length) // IF I IS LESS THAN THE NEWDIALOGUETEXT
            {
                str += newDialogue[i++]; // STR = NEWDIALOGUE CHARACTERS += 1
                dialogueText.text = str; // SETS THE DIALOGUETEXT TO WHAT STR CURRENTLY IS
                yield return new WaitForSecondsRealtime(0.05f); // WAITS FOR 0.1 SECONDS BEFORE RETURN TO LINE 28
            }

            yield return new WaitForSecondsRealtime(3); // WAITS FOR 0.1 SECONDS BEFORE RETURN TO LINE 28
        }

        yield return new WaitForSecondsRealtime(5); // WAITS FOR 5 SECONDS
        dialogueBox.SetActive(false); // SETS THE DIALOGUEBOX TO DEACTIVE
        speakingInProgress = false; // sets speakingInProgress to false
    }

    public static int getCurrentDialogueIndex()
    {
        return currentDialogueIndex; // RETURNS WHAT THE CURRENTDIALOGUEINDEX IS, THIS IS USED FOR SAVING THE GAME
    }

    public string[] getPlayerDialogue()
    {
        return dialogueStrings_player;
    }

    public string[] getNPCDialogue()
    {
        return dialogueStrings_npc;
    }

    public string[] getSpaceTeamDialogue()
    {
        return dialogueStrings_spaceteam;
    }

    public int getCurrentDialogue()
    {
        return currentDialogueIndex; // RETURNS WHAT THE CURRENTDIALOGUEINDEX IS, THIS IS USED FOR SAVING THE GAME
    }

    public static void setCurrentDialogueIndex(int nextDialogue) // USED AS A SETTER TO SET THE NEW DIALOGUEINDEX
    {
        currentDialogueIndex = nextDialogue; // SETS THE CURRENT DIALOGUE INDEX TO THE NEXTDIALOGUE PASSED INTO THE METHOD
    }

    public static string getCharacter() // USED AS A GETTER TO GET WHAT CHARACTER IS CURRENTLY SPEAKING 
    { 
        return chosenCharacter; // RETURNS THE CURRENTCHARACTER 
    }

    public bool getSpeakingInProgress()
    {
        return speakingInProgress;
    }
}