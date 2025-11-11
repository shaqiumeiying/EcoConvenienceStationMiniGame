using UnityEngine;
using UnityEngine.UI;

public class WebcamMirror : MonoBehaviour
{
    [SerializeField] private RawImage img = default;
    private WebCamTexture webcamTexture;

    void Start()
    {
        webcamTexture = new WebCamTexture();
        if(!webcamTexture.isPlaying) webcamTexture.Play();
        img.texture = webcamTexture;
    }
}
