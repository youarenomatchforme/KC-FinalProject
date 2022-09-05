using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderhealth : MonoBehaviour
{
    public Slider hpBar;

    public void SetHUD(Unit unit)
    {
        hpBar.maxValue = unit.maxHP;
        hpBar.value = unit.currentHP;
    }

    public void SetHP(int hp)
    {
        hpBar.value = hp;
    }

}
