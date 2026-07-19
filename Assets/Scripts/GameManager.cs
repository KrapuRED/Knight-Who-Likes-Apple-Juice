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
            return;
        }
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

        if (progressLevel >= maxProgressLevel)
        {
            EndGame(); // reached the final level, game complete
            return;
        }

        progressLevel++;
        TransitionManager.Instance.LoadScene($"GamePlay_Main_{progressLevel}", "CrossFade");
    }
    
    public void StartGame()
    {
        
    }

    public void EndGame()
    {
        
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
