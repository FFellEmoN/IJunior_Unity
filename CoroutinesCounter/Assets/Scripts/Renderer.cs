using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Renderer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void DisplayCountdown(int number)
    {
        _text.text = number.ToString("");
    }
}
