using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{ 
    public void loadGameScene() // THIS METHOD IS USED TO LOAD THE LOADING SCREEN ONCE THE CAMERA HAS FINISHED IT'S ANIMATION
    {
        SceneManager.LoadScene(4); // LOADS THE LOADING SCREEN FOR LEVEL 1
    }
} 