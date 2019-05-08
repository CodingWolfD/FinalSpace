using UnityEngine.UI;
using UnityEngine;

public class LoadingScreenHints : MonoBehaviour
{
    private TextAsset hints; // CREATES A NEW TEXTASSET CALLED HINTS TO BE USED AS A REFERENCE TO THE TEXTFILE THAT STORES THE LOADING SCREEN HINTS
    private string hint; // CREATES A NEW STRING THAT WILL HOLD THE CHOSEN HINT 
    private string[] allHints; // CREATES A NEW STRING ARRAY TO STORE ALL THE HINTS FOUND INSIDE THE TEXT FILE

    private Text hintText; // CREATES A NEW TEXT REFERENCE CALLED HINTTEXT TO DISPLAY THE CHOSEN HINT ON THE LOADING SCREEN

    void Awake()
    {
        hintText = GameObject.Find("hintText").GetComponent<Text>(); // FINDS THE HINT TEXT GAMEOBJECT IN THE SCENE AND ACCESSES THE TEXT COMPONENT

        hints = Resources.Load("Text_Files/LoadingScreenHints") as TextAsset; // INITIALISES THE TEXT ASSET TO THE PATH OF THE TEXT FILE THAT HOLDS ALL THE HINTS
      
        // THIS CHECK BELOW ALLOWS US THE CHECK IF THE FILE WAS FOUND, IF IT WASN'T THIS CHECK HANDLES THE NULLPOINTER EXCEPTION
        if(hints != null) // IF THE HINT TEXT FILE WAS FOUND
        {
            Debug.Log("Hints Found!"); // PRINT TO THE CONSOLE THAT THE HINTS HAVE BEEN FOUND
        }
        else // IF THE HINT TEXT FILE WASN'T FOUND
        {
            Debug.Log("Hints Not Found!"); // PRINT TO THE CONSOLE THAT THE HINTS HAVE NOT BEEN FOUND
        }

        allHints = (hints.text.Split('\n')); // POPULATES THE ALLHINTS ARRAY WITH THE STRING STORED IN THE TEXT FILE SPLITTING THEM AT A NEW LINTG
        setNewHint(Random.Range(0, 4)); // CALLS THE SETNEWHINT METHOD PASSING IN A RANDOM VALE BETWEEN 0 AND 4\
    }

    private void setNewHint(int index) // THIS METHOD SETS THE NEW HINT TO BE DISPLAYED ON THE LOADING SCREEN
    {
        hint = allHints[index]; // SETS THE HINT STRING TO THE STRING STORED IN THE ALLHINTS ARRAY AND THE PASSED INDEX POSITION
        hintText.text += hint; // ADDS THE NEW HINT TO THE CURRENT TEXT OF THE HINTTEXT
    }
}