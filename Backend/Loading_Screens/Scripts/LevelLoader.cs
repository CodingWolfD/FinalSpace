using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    private Text progressText; // REFERENCE TO THE TEXT COMPONENT USED TO DISPLAY PROGRESS
    [SerializeField] private int levelIndex; // USED TO TELL THE LEVEL LOADER WHICH LEVEL TO LOAD

    private void Start()
    {
        progressText = GameObject.Find("progressText").GetComponent<Text>(); // ACCESSES THE TEXT COMPONENT OF THE PROGRESSTEXT GAMEOBJECT
        loadLevel(levelIndex); // CALLS THE LOADLEVEL METHOD WHICH LOADS THE 2 LEVEL HE BUILD INDEX
    }

    public void loadLevel(int levelIndex)
    {
        StartCoroutine(LoadAsyncronysly(levelIndex)); // STARTS THE COROUTINE LOADASYNCRONYSLY WITH THE LEVEL SELECTED AS A PAREMETER
    }

	private IEnumerator LoadAsyncronysly(int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex); // STARTS A ASYNCOPERATION WHICH CALLS THE SCENEMANAGERS LOADSCENEASYNC METHOD

        while(!operation.isDone) // THIS WHILE LOOP WILL RUN UNTIL THE LEVEL IS LOADED
        {
            float progress = Mathf.Clamp01(operation.progress / 1); // CLAMPS THE PROGRESS VALUE BETWEEN 0 AND 1
            progressText.text = "Progress:" + progress * 100 + "%"; // SETS THE PROGRESS TEXT TO THE CURRENT PROGRESS * 100  
            yield return null; // RETURNS OUT THE METHOD WHEN THE OPERATION IS COMPLETE
        }
    }
}