using System.Collections;
using UnityEngine;
using TMPro;

public class XP_Script : MonoBehaviour
{
    public int currentXP = 0; // The current XP value
    private float duration = 1.0f; // Duration over which XP increases
    [SerializeField] private TMP_Text xpText;
    public Josh_Player_Move playerInfo;

    private void Update()
    {
        playerInfo.info.playerXP = currentXP;
        xpText.text = "Current XP: " + currentXP;
    }


    // Call this method to add XP
    public void AddXP(int amountOfXP)
    {
        StartCoroutine(AnimateXPIncrease(amountOfXP));
    }


    private IEnumerator AnimateXPIncrease(int amountOfXP)
    {
        int startXP = currentXP;
        int endXP = startXP + amountOfXP;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float fraction = time / duration; // Fraction of the duration that has passed

            // Interpolate XP value between startXP and endXP over 'duration' seconds
            currentXP = (int)Mathf.Lerp(startXP, endXP, fraction);
            UpdateXPDisplay(); // Update the XP display (UI or otherwise)

            yield return null; // Wait for the next frame
        }

        // Ensure XP is set to the exact final value after animation
        currentXP = endXP;
        UpdateXPDisplay();
    }

    // Call this method to decrease XP
    public void DecreaseXP(int amountOfXP)
    {
        StartCoroutine(AnimateXPDecrease(amountOfXP));
    }

    private IEnumerator AnimateXPDecrease(int amountOfXP)
    {
        int startXP = currentXP;
        int endXP = Mathf.Max(startXP - amountOfXP, 0); // Ensure XP doesn't go below 0
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float fraction = time / duration; // Fraction of the duration that has passed

            // Interpolate XP value between startXP and endXP over 'duration' seconds
            currentXP = (int)Mathf.Lerp(startXP, endXP, fraction);
            UpdateXPDisplay(); // Update the XP display (UI or otherwise)

            yield return null; // Wait for the next frame
        }

        // Ensure XP is set to the exact final value after animation
        currentXP = endXP;
        UpdateXPDisplay();
    }


    // Method to update XP display - implement as needed
    private void UpdateXPDisplay()
    {
        // Update your XP display (like UI Text) here
        
        xpText.text = "Current XP: " + currentXP;
    }
}
