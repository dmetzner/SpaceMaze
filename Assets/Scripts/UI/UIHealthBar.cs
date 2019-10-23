using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance { get; private set; }
    
    public Image maskP1;
    public Image maskP2;
    public GameObject HealthBarP2;

    private float originalSizeP1 = 125;
    private float originalSizeP2 = 125;

    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (!Options.CoopMode)
        {
            Destroy(HealthBarP2);
        }

        originalSizeP1 = maskP1.rectTransform.rect.width;
        originalSizeP2 = maskP1.rectTransform.rect.width;
    }

    public void SetValue(float value, int player_number)
    {
        switch (player_number)
        {
            case 1:
                maskP1.rectTransform.SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Horizontal, 
                    originalSizeP1 * value
                ); 
                return;
            case 2:
                maskP2.rectTransform.SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Horizontal,
                    // 125 * value
                    originalSizeP2* value
                ); 
                return;
        }
    }
}
