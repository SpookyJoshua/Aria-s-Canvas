using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DigitHandler : MonoBehaviour
{
    public int digit;

    public TMP_Text digitText;

    public void RaiseDigit()
    {
        if(digit == 9)
        {
            digit = -1;
        }
        digit = digit + 1;
    }

    public void LowerDigit()
    {
        if (digit == 0)
        {
            digit = 10;
        }
        digit = digit - 1;
    }

    private void Update()
    {
        digitText.text = digit.ToString();
    }
}
