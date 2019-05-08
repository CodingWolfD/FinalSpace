using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1UIManager : MonoBehaviour
{
    private GameObject pauseMenu;
    private GameObject settingsMenu; 
    private GameObject dialogueBox;
    private GameObject HUD;
    private GameObject player;
    private GameObject fpsCamera;
    private GameObject savingText;

    [SerializeField] private GameObject keyCodeScreen;
    [SerializeField] private GameObject errorMessageScreen;

    public static Level1UIManager instance;

    private void Start ()
    {
        instance = this;

        keyCodeScreen.SetActive(false);
        errorMessageScreen.SetActive(false);

        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);

        settingsMenu = GameObject.Find("SettingsMenu");
        settingsMenu.SetActive(false);

        dialogueBox = GameObject.Find("DialogueBox");

        player = GameObject.Find("Player");
        fpsCamera = GameObject.Find("FPSCamera");

        HUD = GameObject.Find("HUD");

        savingText = GameObject.Find("SavingText");
        savingText.SetActive(false);
    }

    private void Update ()
    {
        getEscapePressed();
        disableSavingText();
    }

    private void getEscapePressed()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf)
        {
            pause();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && settingsMenu.activeSelf)
        {
            settingsMenuHide();
        }
    }

    public void saveCurrentGame()
    {
        SaveGame.saveGameToJson(player.GetComponent<FPSMovement>());
        StartCoroutine(disableSavingText());
    }

    private void pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        HUD.SetActive(false);
        savingText.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void unPause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        HUD.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if(settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(false);
        }
    }

    public void returnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main_Menu");
    }

    public void settingsMenuShow()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void settingsMenuHide()
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void showErrorScreen()
    {
        HUD.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0;
        keyCodeScreen.SetActive(false);
        errorMessageScreen.SetActive(true);
    }

    public void returnToGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1;
        keyCodeScreen.SetActive(false);
        errorMessageScreen.SetActive(false);
        HUD.SetActive(true);
    }

    public void showKeyPad()
    {
        keyCodeScreen.SetActive(true);
        errorMessageScreen.SetActive(false);
    }

    private IEnumerator disableSavingText()
    {
        savingText.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        savingText.SetActive(false);
    }
}