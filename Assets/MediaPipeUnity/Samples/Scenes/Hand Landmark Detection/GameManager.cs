using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Audio Settings")]
    public AudioClip goodSound;
    public AudioClip badSound;
    public AudioClip finishSound;
    private AudioSource audioSource;

    [Header("Countdown UI")]
    public TMP_Text countdownText;
    public float countdownTime = 3f;

    [Header("Score UI")]
    public TMP_Text scoreText;

    [Header("Timer UI")]
    public TMP_Text timerText;
    public float gameTime = 15f;
    private float timer;
    private bool gameRunning = true;

    [Header("Summary UI")]
    public GameObject summaryPanel;
    public TMP_Text goodText;
    public TMP_Text badText;
    public TMP_Text percentageText;

    [Header("Tips UI")]
    public GameObject tipsPanel;
    public TMP_Text ecoText;

    [Header("Performance Settings")]
    public int threshold = 6;    
    public int maxScore = 15;   
    public float minPercent = 60f;
    public float maxPercent = 98f;

    [Header("Eco Tips")]
    [TextArea(2, 4)]
    public string[] ecoTips = {
        "Choosing clothes made from recycled fibers.",
        "Donating old clothes instead of throwing them away.",
        "Buying second-hand to reduce carbon footprint.",
        "Washing clothes in cold water to save energy.",
        "Avoiding polyester to reduce microplastic pollution.",
        "Extending the life of your clothes by repairing them.",
        "Buying fewer but higher-quality clothes to reduce waste.",
        "Supporting sustainable fashion brands."
    };

    private int score = 0;
    private int goodCount = 0;
    private int badCount = 0;

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

        StartCoroutine(StartCountdown());

        timer = gameTime;

        summaryPanel.SetActive(false);
        tipsPanel.SetActive(false);
    }

    private IEnumerator StartCountdown()
    {
        gameRunning = false;

        float c = countdownTime;

        while (c > 0.5f)
        {
            int number = Mathf.CeilToInt(c);
            yield return StartCoroutine(ShowCountdownNumber(number.ToString()));
            c -= 1f;
        }

        // GO!
        yield return StartCoroutine(ShowCountdownNumber("GO!"));

        countdownText.gameObject.SetActive(false);

        gameRunning = true;
        FindObjectOfType<BallSpawner>().enabled = true;
    }

    private IEnumerator ShowCountdownNumber(string text)
    {
        countdownText.text = text;
        countdownText.gameObject.SetActive(true);

        float duration = 0.4f;
        float t = 0f;
        Vector3 start = Vector3.one * 1.8f;
        Vector3 end = Vector3.one;

        while (t < duration)
        {
            t += Time.deltaTime;
            float lerp = t / duration;
            countdownText.rectTransform.localScale = Vector3.Lerp(start, end, lerp);
            yield return null;
        }

        countdownText.rectTransform.localScale = end;

        yield return new WaitForSeconds(0.3f);
    }



    void Update()
    {
        if (!gameRunning && Input.GetKeyDown(KeyCode.Return))
        {
            RestartGame();
        }

        if (!gameRunning) return;

        UpdateScoreUI();
        UpdateTimer();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
    }

    private void UpdateTimer()
    {
        timer -= Time.deltaTime;

        if (timer < 5)
            timerText.color = Color.red;

        if (timer <= 0)
        {
            EndGame();
            return;
        }

        timerText.text = timer.ToString("F2");
    }

    private void EndGame()
    {
        gameRunning = false;

        BallSpawner spawner = FindObjectOfType<BallSpawner>();
        if (spawner != null)
            spawner.enabled = false;

        foreach (var ball in FindObjectsOfType<BallCatcher>())
            Destroy(ball.gameObject);

        PlaySound(finishSound);

        summaryPanel.SetActive(true);
        goodText.text = goodCount.ToString() + $" recyclable materials";
        badText.text = badCount.ToString()+ $" non-recyclable materials";

        if (percentageText != null)
        {
            if (score >= threshold)
            {
                float t = (float)(score - threshold) / (maxScore - threshold);
                float percent = Mathf.Lerp(minPercent, maxPercent, t);
                percent = Mathf.Clamp(percent, minPercent, maxPercent);

                percentageText.text =
                    $"You outperformed <color=#E236DD><b>{percent:F1}%</b></color> of players!";
            }
            else
            {
                percentageText.text = $"Wanna know more about eco-friendly materials?";
            }
        }

        StartCoroutine(ShowEcoTipAfterDelay());
    }

    private IEnumerator ShowEcoTipAfterDelay()
    {
        tipsPanel.SetActive(false);

    
        yield return new WaitForSeconds(1.5f);

        tipsPanel.SetActive(true);

        string tip = ecoTips[Random.Range(0, ecoTips.Length)];
        ecoText.color = new Color(0.15f, 0.7f, 0.2f);
        ecoText.text = tip;
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
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
