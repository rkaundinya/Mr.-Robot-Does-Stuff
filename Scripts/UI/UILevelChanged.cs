using System.Collections;
using TMPro;
using UnityEngine;

public class UILevelChanged : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tmproText; 
    private CoinCounter coinCounter; 

    private void Awake() 
    {
        coinCounter = FindObjectOfType<CoinCounter>(); 
    }

    void Start()
    {
        tmproText.gameObject.SetActive(false);
        GameManager.Instance.OnLevelChanged += OnLevelChanged; 
    }

    private IEnumerator OnLevelChanged()
    {
        GameManager.Instance.LevelChanging = true; 
        tmproText.gameObject.SetActive(true);

        tmproText.text = "What skill! You got " + 
            GameManager.Instance.Coins.ToString() + "/" + 
            coinCounter.TotalNumOfCoinsInLevel + " coins";
        yield return new WaitForSeconds(5f);

        GameManager.Instance.HandleMoveToNextLevel();

        tmproText.gameObject.SetActive(false);
        GameManager.Instance.LevelChanging = false; 
    }

    private void OnDestroy() 
    {
        GameManager.Instance.OnLevelChanged -= OnLevelChanged;     
    }
}
