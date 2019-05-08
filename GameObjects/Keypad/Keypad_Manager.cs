using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Keypad_Manager : MonoBehaviour
{
    [SerializeField] private Text codeString;
    [SerializeField] private GameObject errorMessage;

    public static Keypad_Manager instance;
    private int code = 123908;

    [SerializeField] private Animator lockHullDoorAnimator;

    private void Start()
    {
        instance = this;

        codeString.text = "";
        errorMessage.SetActive(false);
    }

    public void checkIfCodeIsCorrect()
    {
        if (codeString.text == code.ToString())
        {
            lockHullDoorAnimator.Play("Open");
            Level1UIManager.instance.returnToGame();
        }
        else if(codeString.text != code.ToString())
        {
            StartCoroutine(showErrorMessage());
            codeString.text = "";
        }
    }

    private IEnumerator showErrorMessage()
    {
        errorMessage.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        errorMessage.SetActive(false);
    }

    public void addNumberToString(int number)
    {
        codeString.text += number.ToString();
        print(codeString.text);
    }

    public string getCodeString()
    {
        return codeString.text;
    }
}