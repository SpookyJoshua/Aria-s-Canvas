using System.Collections;
using UnityEngine;
using TMPro;

public class Health_Script : MonoBehaviour
{
    public float currentHealth = 100; // The current XP value
    private float duration = 1.0f; // Duration over which XP increases
    [SerializeField] private TMP_Text hpText;
    public Josh_Player_Move playerInfo;
    private bool canDie;

    private void Awake()
    {
        currentHealth = 100;
    }
    private void Update()
    {
        playerInfo.info.playerHealth = currentHealth;
        hpText.text = "Current Health: " + currentHealth;
        if(currentHealth <= 0 && canDie)
        {
            playerInfo.Die();
        }
    }


    // Call this method to add XP
    public void AddHealth(float amountOfHP)
    {
        StartCoroutine(AnimateHPIncrease(amountOfHP));
    }


    private IEnumerator AnimateHPIncrease(float amountOfHP)
    {
        float startHP = currentHealth;
        float endHP = startHP + amountOfHP;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float fraction = time / duration; // Fraction of the duration that has passed

            // Interpolate XP value between startXP and endXP over 'duration' seconds
            currentHealth = (int)Mathf.Lerp(startHP, endHP, fraction);
            UpdateHPDisplay(); // Update the XP display (UI or otherwise)

            yield return null; // Wait for the next frame
        }

        // Ensure XP is set to the exact final value after animation
        currentHealth = endHP;
        UpdateHPDisplay();
    }

    // Call this method to decrease health
    public void DecreaseHealth(float amountOfHP)
    {
        StartCoroutine(AnimateHPDecrease(amountOfHP));
    }

    private IEnumerator AnimateHPDecrease(float amountOfHP)
    {
        float startHP = currentHealth;
        float endHP = startHP - amountOfHP; // Decrease instead of increase
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float fraction = time / duration; // Fraction of the duration that has passed

            // Interpolate health value between startHP and endHP over 'duration' seconds
            currentHealth = (int)Mathf.Lerp(startHP, endHP, fraction);
            UpdateHPDisplay(); // Update the health display (UI or otherwise)

            yield return null; // Wait for the next frame
        }

        // Ensure health is set to the exact final value after animation
        currentHealth = (int)endHP;
        canDie = true;
        UpdateHPDisplay();
    }

    // Method to update XP display - implement as needed
    private void UpdateHPDisplay()
    {
        // Update your XP display (like UI Text) here

        hpText.text = "Current Health: " + currentHealth;
    }
}
