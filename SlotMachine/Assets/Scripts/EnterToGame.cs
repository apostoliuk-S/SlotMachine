using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterToGame : MonoBehaviour
{
    public InputField nickName;
    public GameObject panelHelp;

    public void ButtonEnter()
    {
        PlayerPrefs.SetString("NewPlayer", nickName.text);
        SceneManager.LoadScene(1); 
    }

    public void ResetButton()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("ActivationSort", 0);
    }

    public void OpenHelp()
    {
        panelHelp.SetActive(true);
    }

    public void CloseHelp()
    {
        panelHelp.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
