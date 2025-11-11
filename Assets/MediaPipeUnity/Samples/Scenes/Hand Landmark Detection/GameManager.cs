using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 
    public AudioSource audioSource;
    public AudioClip catchSound;    

    private int score = 0;

    void Awake()
    {

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void OnBallCaught()
    {
        score++;
        //Debug.Log($"Score: {score}");

        if (audioSource != null)
        {
            if (catchSound != null)
                audioSource.PlayOneShot(catchSound);
            else
                audioSource.Play();
        }
    }
}
