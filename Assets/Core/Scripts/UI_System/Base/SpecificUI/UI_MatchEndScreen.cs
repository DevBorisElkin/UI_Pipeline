using System.Collections;
using System.Collections.Generic;
using UI_Pipeline;
using UnityEngine;

public class UI_MatchEndScreen : BaseView
{
    public override void Hide(IView next)
    {
        UI_System.Instance.level++;
        base.Hide(next);
    }
}
