using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManaBar : MonoBehaviour
{
    public static UIManaBar instance { get; private set; }

    public Image maskP1;
    public Image maskP2;
    public GameObject HealthBarP2;

    private float originalSize = 125;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        originalSize = maskP1.rectTransform.rect.width;
        if (!Options.CoopMode)
        {
            Destroy(HealthBarP2);
        }
    }

    public void SetValue(float value, int player_number)
    {
        float newMaskSize = originalSize * value;
        switch (player_number)
        {
            case 1:
                maskP1.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newMaskSize); 
                return;
            case 2:
                maskP2.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newMaskSize); 
                return;
        }
    }
}
