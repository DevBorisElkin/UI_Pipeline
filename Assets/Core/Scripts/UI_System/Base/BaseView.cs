using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UI_Pipeline;
using UnityEngine;
using UnityEngine.UI;

public class BaseView : MonoBehaviour, IView
{
    protected CanvasGroup canvasGroup;
    public float fadeDuration = 0.7f;

    public List<Button> actionButtons;

    private DateTime lastTimeClicked;
    private float clickCooldown = 1f;

    protected bool overlappedView;
    
    public IView Dependant { get; set; }
    
    private void Awake()
    {
        overlappedView = false;
        if (canvasGroup == null) canvasGroup = GetComponentInChildren<CanvasGroup>();
        canvasGroup.alpha = 0;
        lastTimeClicked = DateTime.Now - TimeSpan.FromSeconds(clickCooldown);
    }
    
    public virtual void Init(){}
    public void Bind(List<IView> nextPanels)
    {
        for (int i = 0; i < nextPanels.Count; i++)
        {
            if (i < actionButtons.Count)
            {
                IView view = nextPanels[i];

                // with some views we use Modal Window mode
                // for this view we set dependent which will be opened instantly when condition arrives
                if (view is UI_UpgradesScreen)
                {
                    view.BindOverlapped(this);
                    actionButtons[i].onClick.AddListener(() => { Overlap(view);});
                }
                else
                {
                    // casual views are binded simply, they have Next target to which they transition
                    actionButtons[i].onClick.AddListener(() => { Hide(view);});
                }
            }
        }
    }
    public void Show()
    {
        Init();
        gameObject.SetActive(true);
        canvasGroup.DOFade(1f, fadeDuration);
    }
    public virtual void Hide(IView next)
    {
        if( (DateTime.Now - lastTimeClicked).TotalMilliseconds < TimeSpan.FromSeconds(clickCooldown).TotalMilliseconds) return;
        lastTimeClicked = DateTime.Now;

        UI_System.Instance.PanelTransition?.Invoke();
        
        //Debug.Log($"ButtonClickedOnPanel {gameObject}");
        
        canvasGroup.DOFade(0f, fadeDuration).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
        
        if(!overlappedView)
            next?.Show();
    }

    public void Overlap(IView overlapView)
    {
        overlapView.Show();
    }

    public void BindOverlapped(IView dependsOn)
    {
        overlappedView = true;
        dependsOn.Dependant = this;
        actionButtons[0].onClick.AddListener(() => { Hide(null);});
    }
}
