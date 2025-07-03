using UnityEngine;
using UnityEngine.Video;
using UnityEngine.InputSystem;

public class VideoController : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        // Get the VideoPlayer component
        videoPlayer = GetComponent<VideoPlayer>();

        // Make sure the video doesn't play automatically
        videoPlayer.Stop();
    }

    void Update()
    {
        // Check if there is a touch input
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            // Get the touch position
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            // Create a ray from the touch position
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hit;

            // Check if the ray hits any object
            if (Physics.Raycast(ray, out hit))
            {
                // If the object hit is the video plane, toggle play/pause
                if (hit.collider.gameObject == gameObject)
                {
                    TogglePlayPause();
                }
            }
        }
    }

    void TogglePlayPause()
    {
        // Pause if the video is playing, otherwise play the video
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            Debug.Log("Video paused.");
        }
        else
        {
            videoPlayer.Play();
            Debug.Log("Video playing.");
        }
    }
}
