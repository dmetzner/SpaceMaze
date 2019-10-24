using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsBrain : MonoBehaviour
{
    public Slider mSlider;

    // Start is called before the first frame update
    void Start()
    {
        mSlider.value = Options.difficulty * 10;
    }
}
