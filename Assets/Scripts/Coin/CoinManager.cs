using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int coinCount = 0;
    public TextMeshProUGUI coinText;
    
    void Update()
    {
        coinText.text = "Score: " + coinCount.ToString();
    }
}
