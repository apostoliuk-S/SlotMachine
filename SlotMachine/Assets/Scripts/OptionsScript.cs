using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScript : MonoBehaviour
{
    public GameObject panelOptions;
    public GameObject exitGamePanel;
    public GameObject LeaderBoard;


    public void OnClick_Options()
    {
        panelOptions.SetActive(true);
    }       // Открытие окна опций
    public void OnClick_CloseOptions()
    {
        panelOptions.SetActive(false);
    } //  Закрытие окна опций


    public void Exit_Game()
    {
        exitGamePanel.SetActive(true);
        panelOptions.SetActive(false);
    } // Метод выхода из игры
    public void Yes()
    {
        Application.Quit();
    }     //   Потверждение выхода из игры
    public void No()
    {
        exitGamePanel.SetActive(false);
    }    //     Отказ от выхода из игры


    public void OpenLeaderBoard()
    {
        LeaderBoard.SetActive(true);
    }   // Метод вызова окна лидерборда
    public void CloseLeaderBoard()
    {
        LeaderBoard.SetActive(false);
    } //  Метод закрытия окна лидербора
}
