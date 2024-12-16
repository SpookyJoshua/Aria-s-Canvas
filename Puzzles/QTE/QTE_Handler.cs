using System.Collections.Generic;
using UnityEngine;
using Michsky.MUIP;
using TMPro;
using System.Collections;

public class QTE_Handler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private Josh_Player_Move player;
    [SerializeField] private TMP_Text m_Text;
    [SerializeField] private GameObject QTE_GUI;
    [SerializeField] private PowerUps_Handler powerUps_Handler;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private List<KeyCode> keys = new List<KeyCode>();
    [SerializeField] private XP_Script xp_Handler;
    [SerializeField] private TMP_Text pbText;
    private bool isGold;
    private int xpR1;
    private int xpR2;
    private Vector3Int chestPos;
    private string chestNam;
    public ChestScript chestHandler = null;
    private KeyCode keyToPress;
    private bool keyIsHeldDown = false;
    private bool keyGeneratedAt50;

    private void Start()
    {
        StartCoroutine("PulseText");
    }

    private void Update()
    {
        if (Input.GetKey(keyToPress) && !keyIsHeldDown)
        {
            // Only start coroutine if it's not already in progress
            if (!keyIsHeldDown)
            {
                keyIsHeldDown = true;
                StartCoroutine(FillProgressBar());
            }
        }
        else if (!Input.GetKey(keyToPress) && keyIsHeldDown)
        {
            keyIsHeldDown = false;
            // No need to explicitly stop the coroutine here as it checks keyIsHeldDown
        }
    }

    private IEnumerator FillProgressBar()
    {
        bool keyGeneratedAt50 = false; // Flag to ensure we only generate a new key once at 50%

        while (keyIsHeldDown)
        {
            progressBar.currentPercent += Time.deltaTime * 100 / 4; // Fill rate adjustment
            pbText.text = progressBar.currentPercent.ToString("F0") + "%";

            if (progressBar.currentPercent >= 50 && !keyGeneratedAt50)
            {
                GenerateKeyToPress(); // Generate a new key
                keyIsHeldDown = false; // Reset the flag to require new key press
                keyGeneratedAt50 = true; // Prevent further key generation at 50%
            }

            if (progressBar.currentPercent >= 100)
            {
                Open();
                yield break;
            }

            yield return null;
        }

        // Reset progress if the loop exits without reaching 100%
        if (progressBar.currentPercent < 100)
        {
            ResetProgressBar();
        }
    }

    private void ResetProgressBar()
    {
        progressBar.currentPercent = 0;
        pbText.text = "0%";
    }

    private void Open()
    {
        // Logic when the progress bar reaches 100% or when the QTE is completed
        pbText.text = "0%";
        progressBar.currentPercent = 0;
        progressBar.UpdateUI();
        if (isGold)
        {
            powerUps_Handler.AssignRandomPowerUp();
        }
        if (chestHandler != null)
        {
            chestHandler.GrantXP(xpR1, xpR2);
            chestHandler.cReset(chestPos, chestNam);
            chestHandler = null;
        }
        else
        {
            xp_Handler.AddXP(Random.Range(xpR1, xpR2));
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        player.isFrozen = false;
        QTE_GUI.SetActive(false);
    }

    public void ChestAttempt(int xp1, int xp2, bool chestGold, Vector3Int chestP, string chestName)
    {
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        player.isFrozen = true;
        Cursor.visible = true;
        QTE_GUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        xpR1 = xp1;
        xpR2 = xp2;
        isGold = chestGold;
        chestPos = chestP;
        chestNam = chestName;
        GenerateKeyToPress();
    }

    private IEnumerator PulseText()
    {
        while (true)
        {
            // Scale up
            for (float i = 1f; i <= 1.2f; i += 0.01f)
            {
                m_Text.transform.localScale = new Vector3(i, i, i);
                yield return new WaitForSeconds(0.05f);
            }

            // Scale down
            for (float i = 1.2f; i >= 1f; i -= 0.01f)
            {
                m_Text.transform.localScale = new Vector3(i, i, i);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    private void GenerateKeyToPress()
    {
        keyToPress = keys[Random.Range(0, keys.Count)];
        m_Text.text = keyToPress.ToString();
    }
}
