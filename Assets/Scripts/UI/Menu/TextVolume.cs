using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextVolume : MonoBehaviour
{
    [SerializeField]
    private Slider volumeSlider;
    private TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (text.text != volumeSlider.value.ToString())
            text.text = volumeSlider.value.ToString();
    }
}
