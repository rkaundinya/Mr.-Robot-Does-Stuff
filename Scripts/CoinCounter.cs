using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public int TotalNumOfCoinsInLevel { get; private set; }

    private Coin[] individualCoins; 
    private CoinBox[] coinBoxes; 

    private void Awake() {
        individualCoins = GetComponentsInChildren<Coin>(); 
        coinBoxes = GetComponentsInChildren<CoinBox>(); 

        foreach (var individualCoin in individualCoins)
        {
            TotalNumOfCoinsInLevel++; 
        }

        foreach (var coin in coinBoxes)
        {
            TotalNumOfCoinsInLevel += coin.TotalCoins; 
        }
    }
}
