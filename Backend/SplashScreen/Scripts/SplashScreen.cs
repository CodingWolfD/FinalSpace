using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

public class SplashScreen : MonoBehaviour
{
    public float waitForFadeIn; // FLOAT USED FOR FADING IN
    public float waitForFadeOut; // FLOAT USED FOR FADING OUT

    private Image splashLogo; // IMAGE USED AS A REFERENCE TO THE COMPANY LOGO
    private Image gameLogo; // IMAGE USED AS A REFERENCE TO THE COMPANY LOGO

    private void Awake()
    {
        Application.targetFrameRate = 60; // LIMITS THE GAMES FRAME RATE TO 60 FRAMES PER SECOND
        splashLogo = GameObject.Find("SplashLogo").GetComponent<Image>(); // FINDS THE SPLASHSCREEN LOGO IN THE SCENE AND ACCESSES THE IMAGE COMPONENT
        gameLogo = GameObject.Find("GameLogo").GetComponent<Image>();
        checkifSaveExists(); // CALLS THE CHECCKIFSAVEEXISTS METHOD
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

    private IEnumerator Start()
    {
        splashLogo.canvasRenderer.SetAlpha(0.0f); // SETS THE IMAGES ALPHA TO 0
        gameLogo.canvasRenderer.SetAlpha(0.0f); // SETS THE IMAGES ALPHA TO 0

        fadeIn(splashLogo); // CALLS THE FADEIN METHOD
        yield return new WaitForSeconds(waitForFadeIn); // WAITS FOR A PRESET AMOUNT OF SECONDS
        fadeOut(splashLogo); // CALLS THE FADEOUT METHOD
        yield return new WaitForSeconds(waitForFadeOut); // WAITS FOR A PRESET AMOUNT OF SECONDS 

        fadeIn(gameLogo); // CALLS THE FADEIN METHOD
        yield return new WaitForSeconds(waitForFadeIn); // WAITS FOR A PRESET AMOUNT OF SECONDS
        fadeOut(gameLogo); // CALLS THE FADEOUT METHOD
        yield return new WaitForSeconds(waitForFadeOut); // WAITS FOR A PRESET AMOUNT OF SECONDS 

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // LOADS THE NEXT SCENE BASED ON THE CURRENT SCENE INDEX + 1
    }

    private void fadeIn(Image logo)
    {
        logo.CrossFadeAlpha(1, 5, false); // TWEENS BETWEEN 0 AND 1 FOR 2 SECONDS AND IGNORES THE TIMESCALE
    }

    private void fadeOut(Image logo)
    {
        logo.CrossFadeAlpha(0, 4, false); // TWEENS BETWEEN ! AND 0 AND IGNORES THE TIMESCALE
    }
}