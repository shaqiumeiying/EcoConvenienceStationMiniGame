using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Audio Settings")]
    public AudioClip goodSound;
    public AudioClip badSound;
    public AudioClip finishSound;
    private AudioSource audioSource;

    [Header("Score Settings")]
    public TMP_Text scoreText;

    [Header("Timer Settings")]
    public TMP_Text timerText;
    public float gameTime = 15f;
    private float timer;
    private bool gameRunning = true;

    [Header("Summary UI")]
    public GameObject summaryPanel;
    public TMP_Text goodText;
    public TMP_Text badText;

    [Header("Tips UI")]
    public GameObject tipsPanel;
    public TMP_Text ecoText;

    private int score = 0;
    private int goodCount = 0;
    private int badCount = 0;

    [Header("Tips")]
    [TextArea(2, 4)]
    public string[] ecoTips = {
        "Choosing clothes made from recycled fibers.",
        "Donating your old clothes instead of throwing them away.",
        "Buying second-hand to reduce carbon footprint.",
        "Washing clothes in cold water to save energy.",
        "Avoiding polyester to reduce microplastic pollution.",
        "Extending the life of your clothes by repairing them.",
        "Buying fewer but higher-quality clothes to reduce waste.",
        "Supporting sustainable fashion brands."
    };

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;

        timer = gameTime;
        summaryPanel.SetActive(false);
        tipsPanel.SetActive(false);
    }

    void Update()
    {
        if (!gameRunning)
            return;

        UpdateScoreText();
        UpdateTimer();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = $"{score}";
    }

    private void UpdateTimer()
    {
        timer -= Time.deltaTime;

        if (timer < 5)
            timerText.color = Color.red;

        if (timer < 0)
        {
            EndGame();
            return;
        }

        if (timerText != null)
            timerText.text = $"{timer.ToString("F2")}";
    }

    private void EndGame()
    {
        gameRunning = false;

        // Stop ball spawning
        BallSpawner spawner = FindObjectOfType<BallSpawner>();
        if (spawner != null)
            spawner.enabled = false;

        PlaySound(finishSound);

        // Show summary
        summaryPanel.SetActive(true);
        goodText.text = $"{goodCount}";
        badText.text = $"{badCount}";

        // Show tips after delay
        StartCoroutine(ShowEcoTipAfterDelay());
    }

    private IEnumerator ShowEcoTipAfterDelay()
    {
        tipsPanel.SetActive(false);
        yield return new WaitForSeconds(1.5f);

        if (ecoTips.Length > 0)
        {
            int randomIndex = Random.Range(0, ecoTips.Length);
            tipsPanel.SetActive(true);
            ecoText.text = ecoTips[randomIndex];
        }
    }

    public bool IsGameRunning()
    {
        return gameRunning;
    }

    public void OnGoodBallCaught()
    {
        if (!gameRunning) return;

        score++;
        goodCount++;
        PlaySound(goodSound);
    }

    public void OnBadBallCaught()
    {
        if (!gameRunning) return;

        score = Mathf.Max(0, score - 1);
        badCount++;
        PlaySound(badSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }
}
