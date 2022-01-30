using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Pool: MonoBehaviour
{
    public Transform parentForSpawnedViews;
    public List<BaseView> BaseViews;

    public TY CreatePanel<TY>() where TY : BaseView
    {
        TY correctView = null;
        foreach (var view in BaseViews)
        {
            correctView = view as TY;
            if(correctView != null) break;
        }

        if (correctView == null)
        {
            Debug.Log($"Could Retrieve panel from UI_Pool with type [{typeof(TY)}]");
            return null;
        }

        var spawnedItem = Instantiate(correctView, parentForSpawnedViews);
        return spawnedItem;
    }
}
