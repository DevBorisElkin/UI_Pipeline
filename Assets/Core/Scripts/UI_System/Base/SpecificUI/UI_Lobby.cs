using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Lobby : BaseView
{
    public TMP_Text levelTxt;

    private int recordedLevel;
    public override void Init()
    {
        levelTxt.text = "Level: " + UI_System.Instance.level;
        
        if(UI_System.Instance.level >= 3 && UI_System.Instance.level != recordedLevel) Dependant.Show();
        
        recordedLevel = UI_System.Instance.level;
    }
}
