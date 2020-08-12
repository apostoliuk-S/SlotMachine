using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterToGame : MonoBehaviour
{
    public InputField nickName;


    public void ButtonEnter()
    {
        PlayerPrefs.SetString("NewPlayer", nickName.text);

        SceneManager.LoadScene(1);
        
    }


    public void ResetButton()
    {
        PlayerPrefs.DeleteAll();
    }
}
