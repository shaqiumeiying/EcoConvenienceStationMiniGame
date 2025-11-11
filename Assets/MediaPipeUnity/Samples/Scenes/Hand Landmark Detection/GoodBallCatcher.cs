using UnityEngine;

public class GoodBallCatcher : MonoBehaviour
{
    public HandTrackerTest handTracker;

    [Header("Detection Settings")]
    public float catchDistance = 3.25f;
    public float fallSpeed = 1.5f;
    public float destroyY = -5f;
    public AudioSource audioSource;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
   
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y < destroyY)
        {
            Destroy(gameObject);
            return;
        }

     
        if (handTracker == null) return;
        Vector3 handPos = handTracker.handCenter;
        if (handPos == Vector3.zero) return;

      
        float distance = Vector3.Distance(transform.position, handPos);
        if (distance < catchDistance)
        {
            Debug.Log("Good ball caught!");

            if (audioSource != null)
                audioSource.Play();

            Destroy(gameObject, audioSource != null ? audioSource.clip.length : 0f);
        }
    }
}
