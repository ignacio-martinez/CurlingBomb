using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public enum State
    {
        LoadingGame,
        Menu,
        Game
    }

    public int levelNumber = 1;
    public GameObject winPopup;
    public GameObject loosePopup;
    public GameObject mainMenu;
    public GameObject abandonButton;
    public float looseTime = 5;

    private GameObject openPopup = null;
    private State _state = State.LoadingGame;
    private List<BuildingPiece> _buildingPieces = new List<BuildingPiece>();
    private string _loadedLevel;
    private bool _bombHasExploded;
    private float _looseTimer = 0;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += LevelLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LevelLoaded;
    }

    public void SetState(State state)
    {
        _state = state;
    }

    public State GetState()
    {
        return _state;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.LoadingGame:
                LoadGameState();
                break;
            case State.Menu:
                MenuState();
                break;
            case State.Game:
                GameState();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (_state == State.Game && Input.GetKeyDown(KeyCode.W))
        {
            levelNumber++;
            OnWin();
        }
    }

    private void MenuState()
    {

    }

    private void LoadGameState()
    {
        OpenPopup(mainMenu);
   }

    private void GameState()
    {
        if (!_bombHasExploded) return;
        if (_buildingPieces.Count == 0) return;

        _looseTimer += Time.deltaTime;
        
        bool allDestroyed = true;

        // check win/loose
        for (int i = 0; i < _buildingPieces.Count; i++)
        {
            if (_buildingPieces[i].IsTargetPiece && 
                _buildingPieces[i].gameObject.activeSelf && 
                !_buildingPieces[i].IsDestroyed)
                allDestroyed = false;
        }
        if (allDestroyed)
        {
            levelNumber++;
            _state = State.Menu;
            if (levelNumber > 5)
            {
                OpenPopup(mainMenu);
                UnloadLevel();
                levelNumber = 1;
                abandonButton.SetActive(false);
            }
            else
            {
                OnWin();
            }
        }
        else if (_looseTimer > looseTime)
        {
            OnLoose();
        }
        
    }

    private void OnLoose()
    {
        _state = State.Menu;
        OpenPopup(loosePopup);
        abandonButton.SetActive(false);
    }

    private void OnWin()
    {
        OpenPopup(winPopup);
        abandonButton.SetActive(false);
    }

    public void LoadCurrentLevel()
    {
        UnloadLevel();
        _loadedLevel = GetLevelName(levelNumber);
        SceneManager.LoadScene(_loadedLevel, LoadSceneMode.Additive);

        _buildingPieces.Clear();
    }

    public void ReloadLevel()
    {
        string levelName = _loadedLevel;
        UnloadLevel();
        SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
        _loadedLevel = levelName;
        
        _buildingPieces = FindObjectsOfType<BuildingPiece>().ToList();
    }

    string GetLevelName(int levelNumber)
    {
        return "level" + levelNumber.ToString();
    }

    public void OpenPopup(GameObject popup)
    {
        popup.gameObject.SetActive(true);
        openPopup = popup;
        _state = State.Menu;
    }
    
    public void ClosePopup()
    {
        if(openPopup)
            openPopup.gameObject.SetActive(false);
        openPopup = null;
    }

    public void UnloadLevel()
    {
        if (string.IsNullOrEmpty(_loadedLevel))
            return;
        
        SceneManager.UnloadSceneAsync(_loadedLevel);
        _loadedLevel = null;
    }

    private void LevelLoaded(Scene arg0, LoadSceneMode arg1)
    {
        _buildingPieces = FindObjectsOfType<BuildingPiece>().ToList();
        _bombHasExploded = false;
    }

    public void BombHasExploded()
    {
        _bombHasExploded = true;
    }

    public void ResetGame()
    {
       _bombHasExploded = false;
       _looseTimer = 0;
    }
}
