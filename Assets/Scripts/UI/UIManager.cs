using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.GameState = GameManager.eGameState.GameStart;
    }

    public void GoToTitle()
    {
        GameManager.Instance.GameState = GameManager.eGameState.Title;
    }

    public void QuitGame()
    { 
        Application.Quit();
    }
}
