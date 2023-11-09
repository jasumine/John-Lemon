using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextManager : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public Gun gunScript;

    private void Update()
    {
        AmmoText();
    }

    private void AmmoText()
    {
        ammoText.text = gunScript.currentAmmo.ToString() + " / " + gunScript.maxAmmo.ToString();
    }
}
