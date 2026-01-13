using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject restartPanel;
    [SerializeField] private GameObject score;
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource coinAudioSource;
    [SerializeField] private AudioSource finishAudioSource;
    [SerializeField] private AudioSource deathAudioSource;
    
    public CoinManager cm;

    void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.CompareTag("Trap"))
        {
            Debug.Log("Hit a Trap");
            RestartLevel();
            player.SetActive(false);
            // score.SetActive(false);
            bgm.Stop();
            deathAudioSource.PlayOneShot(deathAudioSource.clip);
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit an Enemy");
            RestartLevel();
            player.SetActive(false);
            // score.SetActive(false);
            bgm.Stop();
            deathAudioSource.PlayOneShot(deathAudioSource.clip);

        }

        if (obj.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Collected a Coin");
            cm.coinCount++;
            coinAudioSource.PlayOneShot(coinAudioSource.clip);
            Destroy(obj.gameObject);
        }

        if (obj.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Stage1 Completed!");
            player.SetActive(false);
            bgm.Stop();
            finishAudioSource.PlayOneShot(finishAudioSource.clip);
            Invoke("LoadNextScene", 3f);
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("Stage2");
    }

    void RestartLevel()
    {
        restartPanel.SetActive(true);
    }
}