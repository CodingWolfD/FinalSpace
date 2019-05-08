using System.IO;
using UnityEngine;

public static class SaveGame
{
    private static bool saveFound; // THIS BOOL IS USED TO TELL THE GAME IF A PREVIOUS SAVE WAS FOUND OR NOT
    private static string path = Application.persistentDataPath + "/save1.json"; // THIS IS THE SAVE LOCATION AND NAME OF THE SAVE FILE

    public static void saveGameToJson(FPSMovement player)
    {
        Save_Data data = new Save_Data(player); // CREATES A NEW PLAYER_DATA INSTANCE PASSING IN THE PLAYER VARIABLE

        string dataAsJson = JsonUtility.ToJson(data, true); // THIS CREATES A NEW STRING CALLED DATAASJSON AND CONVERTS THE PLAYER_DATA WITH PRETTY PRINT ENABLED
        File.WriteAllText(path, dataAsJson); // THIS THEN WRITES THE CONVERTED DATA TO THE PREVIOUSLY CREATED JSON FILE
    }

    public static Save_Data loadGameFromJson()
    {
        if (File.Exists(path)) // IF THE FILE EXISTS WHEN THIS METHOD IS CALLED
        {
            string dataAsJson = File.ReadAllText(path); // CREATE A NEW STRING CALLED DATAASJSON AND READ ALL THE VALUES IN THE SAVED JSON FILE LOCATED AT THE SPECIFIED PATH
            Save_Data data = JsonUtility.FromJson<Save_Data>(dataAsJson); // CREATE A NEW PLAYER_DATA INSTANCE AND PASS IN THE VARIABLES FROM THE JSON FILE
            return data; // RETURNS THE NEW PLAYER_DATA CLASS FOR USE
        }
        else // IF THE FILE DOESN'T EXIST WHEN THIS METHOD IS CALLED
        {
            saveFound = false; // SET SAVEFOUND TO FALSE
            return null; // RETURN NULL
        }
    }

    public static bool foundSaveGame() // USED FOR GETTING THE CURRENT STATE OF FOUNDSAVEGAME
    {
        return saveFound; // RETRUNS SAVEFOUND
    } 

    public static void setSaveGameFound(bool saveGame) // USED FOR SETTING THE STATE OF FOUNDSAVEGAME
    {
        saveFound = saveGame; // SAVEFOUND IS EQUAL TO SAVEGAME PASSED IN
    }
}