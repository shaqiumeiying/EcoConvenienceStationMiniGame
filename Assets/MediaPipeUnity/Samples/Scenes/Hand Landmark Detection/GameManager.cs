using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class MaterialInfo
{
    public string name;   
    public Sprite icon;    
    [TextArea(2, 4)] public string description;
    public bool isEcoFriendly;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Audio Settings")]
    public AudioClip goodSound;
    public AudioClip badSound;
    public AudioClip finishSound;
    private AudioSource audioSource;

    [Header("Score UI")]
    public TMP_Text scoreText;

    [Header("Countdown UI")]
    public TMP_Text countdownText;
    public float countdownTime = 3f;

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

    [Header("Performance Settings")]
    public int threshold = 6;
    public int maxScore = 15;
    public float minPercent = 60f;
    public float maxPercent = 98f;

    [Header("Material Info UI")]
    public GameObject materialPanel;
    public Image materialIconUI;
    public TMP_Text materialTextUI;

    [Header("Materials List (8 items)")]
    public MaterialInfo[] materials;

    private int score = 0;
    private int goodCount = 0;
    private int badCount = 0;

    void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Audio source
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        StartCoroutine(StartCountdown());

        // Init state
        timer = gameTime;
        summaryPanel.SetActive(false);
        materialPanel.SetActive(false);
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
        if (!gameRunning)
            return;

        UpdateScoreText();
        UpdateTimer();

        // Press Enter to restart
        if (Input.GetKeyDown(KeyCode.Return))
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
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

        // Stop spawner
        BallSpawner spawner = FindObjectOfType<BallSpawner>();
        if (spawner != null)
            spawner.enabled = false;

        // Destroy all balls
        foreach (BallCatcher ball in FindObjectsOfType<BallCatcher>())
            Destroy(ball.gameObject);

        PlaySound(finishSound);

        // Show summary
        summaryPanel.SetActive(true);
        goodText.text = goodCount.ToString() + " eco-friendly materials";
        badText.text = badCount.ToString() + " non-eco-friendly materials";


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

        StartCoroutine(ShowMaterialInfoAfterDelay());
    }

    private IEnumerator ShowMaterialInfoAfterDelay()
    {
        materialPanel.SetActive(false);
        yield return new WaitForSeconds(1.2f);

        if (materials.Length > 0)
        {
            int randomIndex = Random.Range(0, materials.Length);
            MaterialInfo mat = materials[randomIndex];

            materialPanel.SetActive(true);
            materialIconUI.sprite = mat.icon;

            string ecoWord = "<color=#4BCF6A><b>Eco-Friendly</b></color>";
            string nonEcoWord = "<color=#FF4A4A><b>Non-Eco-Friendly</b></color>";

            string desc = mat.description;

            if (mat.isEcoFriendly)
            {
                desc = desc.Replace("Eco-Friendly", ecoWord);
            }
            else
            {
                desc = desc.Replace("Non-Eco-Friendly", nonEcoWord);
            }

            materialTextUI.text =
                $"<b>{mat.name}</b>\n<size=80%>{desc}</size>";
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
        score++;
        badCount++;
        PlaySound(badSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }
}
