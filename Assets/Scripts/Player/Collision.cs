using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject restartPanel;
    [SerializeField] private GameObject score;
    
    public CoinManager cm;

    void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.CompareTag("Trap"))
        {
            Debug.Log("Hit a Trap");
            RestartLevel();
            player.SetActive(false);
            score.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit an Enemy");
            RestartLevel();
            player.SetActive(false);
            score.SetActive(false);
        }

        if (obj.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Collected a Coin");
            cm.coinCount++;
            Destroy(obj.gameObject);
        }

        if (obj.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Stage1 Completed!");
            SceneManager.LoadScene("Stage2");
        }
    }

    void RestartLevel()
    {
        restartPanel.SetActive(true);
    }
}