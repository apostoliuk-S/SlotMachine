using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Log_In_Up : MonoBehaviour
{
    public InputField nickText;


    string playerName;
    int coins;

    

    public void NewPlayer()
    {
        playerName = nickText.text;
        coins = 50;

        PlayerPrefs.SetString("Name", playerName);
        PlayerPrefs.SetInt("Points", coins);
        SceneManager.LoadScene(1);
    }
}
