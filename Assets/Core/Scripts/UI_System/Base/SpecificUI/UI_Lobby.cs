using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Lobby : BaseView
{
    public TMP_Text levelTxt;
    public override void Init()
    {
        levelTxt.text = "Level: " + UI_System.Instance.level;
        
        if(UI_System.Instance.level >= 3) dependant.Show();
    }
}
