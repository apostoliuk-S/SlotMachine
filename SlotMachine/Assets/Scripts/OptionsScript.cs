using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScript : MonoBehaviour
{
    public GameObject panelOptions;
    public GameObject exitGamePanel;
    public void OnClick_Options()
    {
        panelOptions.SetActive(true);
    }

    public void OnClick_CloseOptions()
    {
        panelOptions.SetActive(false);
    }

    public void Exit_Game()
    {
        exitGamePanel.SetActive(true);
        panelOptions.SetActive(false);
    }
    public void Yes()
    {
        Application.Quit();
    }
    public void No()
    {
        exitGamePanel.SetActive(false);
    }
}
