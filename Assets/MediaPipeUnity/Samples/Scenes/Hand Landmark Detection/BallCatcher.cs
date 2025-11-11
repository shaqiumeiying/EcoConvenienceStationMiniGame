//using UnityEngine;

//public class BallCatcher : MonoBehaviour
//{
//    public HandTrackerTest handTracker;
//    public float catchDistance = 0.15f;
//    private bool isCaught = false;

//    void Update()
//    {
//        if (handTracker == null || isCaught)
//            return;

//        Vector3 handPos = handTracker.handCenter;
//        if (handPos == Vector3.zero)
//            return;

//        Debug.Log($"Hand: {handPos}, Ball: {transform.position}");

//        float distance = Vector3.Distance(transform.position, handPos);

//        if (distance < catchDistance)
//        {
//            Debug.Log("Ball caught!");
//            isCaught = true;
//            gameObject.SetActive(false);
//        }
//    }

//}
//using UnityEngine;

//public class BallCatcher : MonoBehaviour
//{
//    public HandTrackerTest handTracker;
//    [Header("Detection Settings")]
//    public float catchDistance = 0.25f;  
//    public float scaleMultiplier = 9f;  
//    public float interactionZ = 0f;    
//    private bool isCaught = false;

//    void Update()
//    {
//        if (handTracker == null || isCaught)
//            return;


//        Vector3 handPos = handTracker.handCenter;
//        if (handPos == Vector3.zero)
//            return;


//        handPos = new Vector3(handPos.x * scaleMultiplier, handPos.y * scaleMultiplier, interactionZ);


//        float distance = Vector3.Distance(transform.position, handPos);

//        // Debug.Log($"Hand: {handPos}, Ball: {transform.position}, dist={distance:F2}");

//        if (distance < catchDistance)
//        {
//            Debug.Log(" Ball caught!");
//            isCaught = true;
//            gameObject.SetActive(false);
//        }
//    }
//}

using UnityEngine;

public class BallCatcher : MonoBehaviour
{
    public HandTrackerTest handTracker;

    [Header("Detection Settings")]
    public float catchDistance = 0.25f;
    public float scaleMultiplier = 9f;
    public float interactionZ = 0f;
    public float fallSpeed = 1.5f;    
    public float destroyY = -1.5f;     

    void Update()
    {

        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y < destroyY)
        {
            Destroy(gameObject);
            return;
        }

        if (handTracker == null)
            return;


        Vector3 handPos = handTracker.handCenter;
        if (handPos == Vector3.zero)
            return;

        handPos = new Vector3(handPos.x * scaleMultiplier, handPos.y * scaleMultiplier, interactionZ);


        float distance = Vector3.Distance(transform.position, handPos);

        if (distance < catchDistance)
        {
            Debug.Log("Ball caught!");
            //isCaught = true;

            Destroy(gameObject);
        }
    }
}


