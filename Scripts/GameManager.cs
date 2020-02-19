using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int Lives {get; private set; }
    public int Coins { get; private set; }
    public bool LevelChanging;

    public event Action<int> OnLivesChanged; 
    public event Action<int> OnCoinsChanged; 
    public event Func<IEnumerator> OnLevelChanged; 
 
    private int currentLevelIndex;

    /* private void Update() {
        System.Threading.Thread.Sleep(50);
    } */

    //  Instance is basically a reference to the game object this script is attached to
    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject); 
        }
        else {
            Instance = this;  
            DontDestroyOnLoad(gameObject); 

            RestartGame(); 
        }
    }

    // Internal means that method cannot be accessed from object of class or derived 
    // classes. Makes sense here because this is a class that's only used once and
    // no instances are made. Officially says, "internal members are accessible only
    // within files in the same assembly"
    internal void KillPlayer() 
    {
        if (!LevelChanging)
        {
            Lives--; 
            
            if(OnLivesChanged != null) {
                OnLivesChanged(Lives);
            }

            if (Lives <= 0) {
                RestartGame(); 
            } 
            else 
                SendPlayerToCheckPoint();
        }
    }

    private void SendPlayerToCheckPoint()
    {
        var checkpointManager = FindObjectOfType<CheckpointManager>(); 

        var checkpoint = checkpointManager.GetLastCheckpointThatWasPassed();
 
        var player = FindObjectOfType<PlayerMovementController>(); 

        player.transform.position = checkpoint.transform.position; 
    }

    internal void AddCoin()
    {
        if (!LevelChanging){
            Coins++; 
            if (OnCoinsChanged != null) {
                OnCoinsChanged(Coins); 
            }
        }
    }

    public void MoveToNextLevel()
    {
        if (currentLevelIndex == 0)
            HandleMoveToNextLevel(); 
        else if (currentLevelIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            currentLevelIndex = 0; 
            HandleMoveToNextLevel(); 
            Debug.Log("The equivalent case is being called");
        }
        else
        {
            Debug.Log("I'm being called" + currentLevelIndex);
            if (OnLevelChanged != null)
                StartCoroutine(OnLevelChanged()); 
        }
    }

    public void HandleMoveToNextLevel()
    {
        currentLevelIndex++;
        Coins = 0; 
        SceneManager.LoadScene(currentLevelIndex);
    }

    private void RestartGame()
    {
        currentLevelIndex = 0; 
        
        Lives = 3;        
        Coins = 0; 

        if (OnCoinsChanged != null) {
            OnCoinsChanged(Coins); 
        }

        SceneManager.LoadScene(0);
    }
}
