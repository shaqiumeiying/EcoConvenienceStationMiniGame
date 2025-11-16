using UnityEngine;
using Mediapipe.Unity.Sample.HandLandmarkDetection;

public class HandTrackerTest : MonoBehaviour
{
    public HandLandmarkerRunner runner;
    public Vector3 fingerTipPos;

    //comment to debug
    void Start()
    {
        foreach (var anno in FindObjectsOfType<MonoBehaviour>())
        {
            if (anno.name.Contains("Annotation"))
                anno.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        var result = runner.LatestResult;
        if (result.handLandmarks == null || result.handLandmarks.Count == 0)
            return;

        var hand = result.handLandmarks[0];

        var tip = hand.landmarks[8];

        float x = (tip.x - 0.5f) * 5f;
        float y = -(tip.y - 0.5f) * 5f;

        fingerTipPos = new Vector3(x, y, 0);
    }
}
