using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    [SerializeField] private int maxProgressLevel;
    [SerializeField] private int progressLevel;

    public int MaxProgressLevel => maxProgressLevel;
    public int ProgressLevel => progressLevel;
    
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
    
}
