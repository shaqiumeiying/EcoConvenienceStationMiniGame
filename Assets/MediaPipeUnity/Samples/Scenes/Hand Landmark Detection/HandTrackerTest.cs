//using UnityEngine;
//using Mediapipe.Unity.Sample.HandLandmarkDetection;

//public class HandTrackerTest : MonoBehaviour
//{
//    public HandLandmarkerRunner runner;
//    public Vector3 handCenter;


//    void Update()
//    {
//        var result = runner.LatestResult;
//        if (result.handLandmarks != null && result.handLandmarks.Count > 0)
//        {
//            var hand = result.handLandmarks[0];
//            var landmarks = hand.landmarks;

//            Vector3 handCenter = Vector3.zero;
//            foreach (var lm in landmarks)
//            {
//                handCenter += new Vector3(lm.x - 0.5f, -(lm.y - 0.5f), 0);
//            }
//           // handCenter /= landmarks.Count;

//            handCenter = handCenter / landmarks.Count * 5f;


//            //Debug.Log($" Hand center: {handCenter}");
//        }


//    }
//}

using UnityEngine;
using Mediapipe.Unity.Sample.HandLandmarkDetection;
using System.Collections.Generic;

public class HandTrackerTest : MonoBehaviour
{
    public HandLandmarkerRunner runner;
    public Vector3 handCenter;

    void Update()
    {
        var result = runner.LatestResult;

        if (result.handLandmarks == null || result.handLandmarks.Count == 0)
            return;

        var hand = result.handLandmarks[0];

        var landmarksCopy = new List<Vector3>();
        for (int i = 0; i < hand.landmarks.Count; i++)
        {
            var lm = hand.landmarks[i];
            landmarksCopy.Add(new Vector3(lm.x, lm.y, lm.z));
        }

        handCenter = Vector3.zero;
        for (int i = 0; i < landmarksCopy.Count; i++)
        {
            var pos = landmarksCopy[i];
            handCenter += new Vector3(pos.x - 0.5f, -(pos.y - 0.5f), 0);
        }

        handCenter /= landmarksCopy.Count;
        handCenter *= 5f;
    }
}
