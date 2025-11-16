using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketFollower : MonoBehaviour
{
    public HandTrackerTest handTracker;
    public float followSpeed = 10f;

    void Update()
    {
        if (handTracker == null) return;

        if (!handTracker.hasHand) return;

        Vector3 target = handTracker.fingerTipPos;

        transform.position = Vector3.Lerp(
            transform.position,
            target,
            Time.deltaTime * followSpeed
        );
    }
}
