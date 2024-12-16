using UnityEngine;
using UnityEngine.UI;

public class PU_Panel_Script : MonoBehaviour
{
    public GameObject objectToTrack; // The object whose motion we're tracking
    public Image imageToFade; // The image we want to fade
    public float fadeSpeed = 1f; // Speed of fading

    private Vector3 lastPosition;
    private bool isMoving;
    private float currentFadeValue = 1f;

    void Start()
    {
        if (objectToTrack != null)
        {
            lastPosition = objectToTrack.transform.position;
        }
    }

    void Update()
    {
        if (objectToTrack != null)
        {
            // Check if the object has moved
            isMoving = (objectToTrack.transform.position != lastPosition);
            lastPosition = objectToTrack.transform.position;

            // Update the fade value based on motion
            if (isMoving)
            {
                // Fade in
                currentFadeValue = Mathf.Min(.4f, currentFadeValue + fadeSpeed * Time.deltaTime);
            }
            else
            {
                // Fade out
                currentFadeValue = Mathf.Max(0f, currentFadeValue - fadeSpeed * Time.deltaTime);
            }

            // Apply the fade value to the image
            Color color = imageToFade.color;
            color.a = currentFadeValue;
            imageToFade.color = color;
        }
    }
}
