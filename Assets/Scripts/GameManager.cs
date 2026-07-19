using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    [SerializeField] private int maxProgressLevel;
    [SerializeField] private int progressLevel = 1;

    public int MaxProgressLevel => maxProgressLevel;
    public int ProgressLevel => progressLevel;
    
    [SerializeField] private List<Character> activeEnemyCharacters = new(); 
    [SerializeField] private bool _isGameActive;

    public bool IsGameActive => _isGameActive;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        MusicManager.Instance.PlayMusic("GamePlay_Story");
    }

    public void AddCharacter(Character character)
    {
        activeEnemyCharacters.Add(character);
    }

    public void RemoveCharacter(Character character)
    {
        if (!_isGameActive)
            return; // avoid triggering level-clear logic during cleanup/transitions

        activeEnemyCharacters.Remove(character);
        
        if (activeEnemyCharacters.Count == 0)
            OnAllEnemiesDefeated();
    }

    private void OnAllEnemiesDefeated()
    {
        Debug.Log("All enemies defeated — advancing to next level");
        
        progressLevel++;
        TransitionManager.Instance.LoadScene($"GamePlay_Story_{progressLevel}", "CrossFade");
        MusicManager.Instance.PlayMusic("GamePlay_Story");
        
    }

    public void ContinueToMainGamePlay()
    {
        TransitionManager.Instance.LoadScene($"GamePlay_Main_{progressLevel}", "CrossFade");
        MusicManager.Instance.PlayMusic("GamePlay_Main");
    }
    
    public void StartGame()
    {
        PanelManager.Instance.ClosePanel("Panel - Tutorial");
        
        _isGameActive = true;
    }

    public void EndGame()
    {
        TransitionManager.Instance.LoadScene($"Credit", "CrossFade");
    }

    public void RestartGame()
    {
        PanelManager.Instance.ClosePanel("Panel - Failed");
        
        TransitionManager.Instance.LoadScene($"GamePlay_Main_{progressLevel}", "CrossFade");
    }
    
    public void FailGame()
    {
        PanelManager.Instance.OpenPanel("Panel - Failed");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
