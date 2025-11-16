//using UnityEngine;

//public class BallCatcher : MonoBehaviour
//{
//    public HandTrackerTest handTracker;

//    [Header("Detection Settings")]
//    public float catchDistance = 0.25f;
//    public float fallSpeed = 1.5f;
//    public float destroyY = -1.5f;

//    void Update()
//    {

//        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

//        if (transform.position.y < destroyY)
//        {
//            Destroy(gameObject);
//            return;
//        }


//        if (handTracker == null)
//            return;

//        Vector3 handPos = handTracker.handCenter;
//        if (handPos == Vector3.zero)
//            return;


//        float distance = Vector3.Distance(transform.position, handPos);

//        if (distance < catchDistance)
//        {

//            Debug.Log("Ball caught!");

//            GameManager.Instance.OnBallCaught();

//            Destroy(gameObject);
//        }
//    }
//}
using UnityEngine;

public class BallCatcher : MonoBehaviour
{
    public HandTrackerTest handTracker;
    public bool isGoodBall = true;

    [Header("Detection Settings")]
    public float catchDistance = 0.25f;
    public float fallSpeed = 1.5f;
    public float destroyY = -1.5f;

    private bool isCaught = false;

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y < destroyY)
        {
            Destroy(gameObject);
            return;
        }

        if (handTracker == null || isCaught)
            return;

        //Vector3 handPos = handTracker.handCenter;
        Vector3 handPos = handTracker.fingerTipPos;

        if (handPos == Vector3.zero)
            return;

        float distance = Vector3.Distance(transform.position, handPos);

        if (distance < catchDistance)
        {
            isCaught = true;
            Debug.Log(isGoodBall ? "Good ball caught!" : "Bad ball caught!");

            if (GameManager.Instance != null)
            {
                if (isGoodBall)
                    GameManager.Instance.OnGoodBallCaught();
                else
                    GameManager.Instance.OnBadBallCaught();
            }

            Destroy(gameObject);
        }
    }
}