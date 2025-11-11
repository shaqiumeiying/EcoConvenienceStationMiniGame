using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int goodCount = 0;
    public static int badCount = 0;

    [Header("UI (Optional)")]
    public Text goodText;
    public Text badText;

    void Update()
    {
        if (goodText != null) goodText.text = "Good: " + goodCount;
        if (badText != null) badText.text = "Bad: " + badCount;
    }

    public static void AddGood() => goodCount++;
    public static void AddBad() => badCount++;
}
