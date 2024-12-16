using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Lockpick_Handler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private Josh_Player_Move player;

    [SerializeField] private Texture lockClosed;
    [SerializeField] private Texture lockOpened;

    [SerializeField] private int lock_code;

    [SerializeField] private DigitHandler d1;
    [SerializeField] private Button d1Up;
    [SerializeField] private Button d1Down;
    [SerializeField] private DigitHandler d2;
    [SerializeField] private Button d2Up;
    [SerializeField] private Button d2Down;
    [SerializeField] private DigitHandler d3;
    [SerializeField] private Button d3Up;
    [SerializeField] private Button d3Down;

    public RawImage lockImage;

    [SerializeField] private GameObject Comb_Lock_OBJ;

    public ChestScript chest_Handler = null;
    [SerializeField] private XP_Script xp_Handler;
    [SerializeField] private PowerUps_Handler powerUps_Handler;

    private bool codeRedeemed;
    private bool isGold;
    private int xpR1;
    private int xpR2;
    private Vector3Int chestPos;
    private string chestNam;

    public void Generate_New_Code()
    {
        lockImage.texture = lockClosed;
        d1.digit = 0;
        d2.digit = 0;
        d3.digit = 0;

        lock_code = Random.Range(100, 999);
        codeRedeemed = false;
    }

    private void Update()
    {
        if (!codeRedeemed)
        {
            if (lock_code != 0)
            {
                List<int> lockDigits = DigitSplitter.SplitIntoDigits(lock_code);
                if (lockDigits[0] == d1.digit)
                {
                    d1.digitText.color = Color.red;
                    d1Up.interactable = false;
                    d1Down.interactable = false;
                    if (lockDigits[1] == d2.digit)
                    {
                        d2.digitText.color = Color.red;
                        d2Up.interactable = false;
                        d2Down.interactable = false;
                        if (lockDigits[2] == d3.digit)
                        {
                            d3.digitText.color = Color.red;
                            d3Up.interactable = false;
                            d3Down.interactable = false;
                            Invoke("Open", 1.5f);
                        }
                        else
                        {
                            d3.digitText.color = Color.white;
                        }
                    }
                    else
                    {
                        d2.digitText.color = Color.white;
                    }
                }
                else
                {
                    d1.digitText.color = Color.white;
                }
            }
        }
    }

    private void Open()
    {
        d1.digitText.color = Color.white;
        d2.digitText.color = Color.white;
        d3.digitText.color = Color.white;

        d1.digit = 0;
        d2.digit = 0;
        d3.digit = 0;
        lockImage.texture = lockOpened;
        codeRedeemed = true;
        if (isGold)
        {
            isGold = false;
            powerUps_Handler.AssignRandomPowerUp();
        }
        if (chest_Handler)
        {
            chest_Handler.GrantXP(xpR1, xpR2);
            chest_Handler.cReset(chestPos, chestNam);
        }
        else
        {
            xp_Handler.AddXP(Random.Range(xpR1, xpR2));
        }
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        player.isFrozen = false;
        StartCoroutine(CloseGUI());
    }

    IEnumerator CloseGUI()
    {
        yield return new WaitForSeconds(1.5f);
        Comb_Lock_OBJ.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        xpR1 = 0;
        xpR2 = 0;
    }

    public void ChestAttempt(int xp1, int xp2, bool chestGold, Vector3Int chestP, string chestName)
    {
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        player.isFrozen = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        xpR1 = xp1;
        xpR2 = xp2;
        isGold = chestGold;
        chestPos = chestP;
        chestNam = chestName;
        d1Up.interactable = true;
        d1Down.interactable = true;
        d2Up.interactable = true;
        d2Down.interactable = true;
        d3Up.interactable = true;
        d3Down.interactable = true;
        Generate_New_Code();
        Comb_Lock_OBJ.SetActive(true);
    }
}

public class DigitSplitter
{
    public static List<int> SplitIntoDigits(int number)
    {
        List<int> digits = new List<int>();
        string numberString = number.ToString();

        foreach (char digit in numberString)
        {
            digits.Add(int.Parse(digit.ToString()));
        }

        return digits;
    }
}
