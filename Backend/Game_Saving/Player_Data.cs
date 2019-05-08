using System;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Save_Data
{ 
    public float[] position; // USED TO STORE THE CURRENT POSITION OF THE PLAYER IN THE GAME WORLD
    public float[] rotation; // USED TO STORE THE CURRENT ROTATION OF THE PLAYER IN THE GAME WORLD

    public string time; // USED TO STORE THE CURRENT TIME 
    public string date; // USED TO STORE THE CURRENT DATE
    public int level; // USED TO STORE WHAT LEVEL THE PLAYER IS CURRENTLY ON
    public int currentDialogueIndex; // USED TO STORE THE LAST DIALOGUEINDEX USED IN GAME

    public string character; // USED TO STORE WHAT CHARACTER WAS LAST USING THE DIALOGUE SYSTEM

    public Save_Data(FPSMovement player) // THIS IS A CONSTRUCTOR THAT GETS CALLED WHEN THE SAVE BUTTON IS PRESSED
    {
        savePlayersLocationInWorld(player); // CALLS THE SAVEPLAYERLOCATIONINWORLD WHICH SAVES THE PLAYERS CURRENT POSITION AND ROTATION
        saveLevel(); // CALLS THE SAVELEVEL METHOD WHICH SAVES WHAT LEVEL THE PLAYER IS CURRENTLY ON
        saveDateAndTime(); // CALLS THE SAVEDATEANDTIME METHOD WHICH SAVES THE DATE AND TIME OF WHEN THE GAME WAS LAST SAVED
        saveCurrentDialogueIndex(); // CALLS THE SAVECURRENTDIALOGUETEXT METHOD TO SAVE HOW FAR THROUGH THE DIALOGUE THE PLAYER IS
    }

    private void savePlayersLocationInWorld(FPSMovement player)
    {
        position = new float[3]; // INSTANTIATES THE POSITION FLOAT ALLOCATING 3 VALUES
        rotation = new float[3]; // INSTANTIATES THE ROTATION FLOAT ALLOCATING 1 VALUE

        position[0] = player.transform.position.x; // SETS THE FIRST ARRAY INDEX TO THE PLAYERS X POSITION
        position[1] = player.transform.position.y; // SETS THE SECOND ARRAY INDEX TO THE PLAYERS Y POSITION
        position[2] = player.transform.position.z; // SETS THE THIRD ARRAY INDEX TO THE PLAYERS Z POSITION

        rotation[0] = player.transform.eulerAngles.x; // SETS THE FIRST ARRAY INDEX TO THE PLAYERS X ROTATION
        rotation[1] = player.transform.eulerAngles.y; // SETS THE FIRST ARRAY INDEX TO THE PLAYERS Y ROTATION
        rotation[2] = player.transform.eulerAngles.z; // SETS THE FIRST ARRAY INDEX TO THE PLAYERS Z ROTATION
    }

    private void saveLevel()
    {
        level = SceneManager.GetActiveScene().buildIndex - 1; // SETS THE LEVEL INT TO THE CURRENT GAMES BUILDINDEX - 1 SO WE LOAD THE LOADING SCREEN EVERYTIME
    }

    private void saveDateAndTime()
    {
        date = DateTime.Now.ToString("dd/MM/yyyy"); // SETS THE DATE STRING TO THE CURRENT DATE USING .NETS DATETIME.NOW FUNCTION
        time = DateTime.Now.ToString("HH:mm"); // SETS THE TIME STRING TO THE CURRENT DATE USING .NETS DATETIME.NOW FUNCTION
    }

    private void saveCurrentDialogueIndex()
    {
        currentDialogueIndex = NewDialogue.getCurrentDialogueIndex(); // SETS THE CURRENTDIALOGUEINDEX TO THE LAST INDEX OF THE ARRAY
        character = NewDialogue.getCharacter(); // SETS THE CHARACTER STRING TO THE CURRENT CHARACTER IN THE GAME
    }
}