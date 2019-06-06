using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void OnStartButton()
    {
        GameManager.Instance.LoadCurrentLevel();
        GameManager.Instance.ClosePopup();
        GameManager.Instance.ResetGame();
        GameManager.Instance.SetState(GameManager.State.Game);
    }
    
    public void OnRetryButton()
    {
        GameManager.Instance.ReloadLevel();
        GameManager.Instance.ClosePopup();
        GameManager.Instance.ResetGame();
        GameManager.Instance.SetState(GameManager.State.Game);
    }

    public void OnMenuButton()
    {
        GameManager.Instance.UnloadLevel();
        GameManager.Instance.ClosePopup();
        GameManager.Instance.SetState(GameManager.State.LoadingGame);
    }
    
    public void OnNextLevelButton()
    {
        GameManager.Instance.LoadCurrentLevel();
        GameManager.Instance.ClosePopup();
        GameManager.Instance.ResetGame();
        GameManager.Instance.SetState(GameManager.State.Game);
    }

    public void OnAbandonButton()
    {
        GameManager.Instance.LoadCurrentLevel();
        GameManager.Instance.ResetGame();
        GameManager.Instance.SetState(GameManager.State.Game);   
    }
}
