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
    
    private void Awake()
    {
        if (canvasGroup == null) canvasGroup = GetComponentInChildren<CanvasGroup>();
        canvasGroup.alpha = 0;
        lastTimeClicked = DateTime.Now - TimeSpan.FromSeconds(clickCooldown);
    }
    public void Bind(List<IView> nextPanels)
    {
        for (int i = 0; i < nextPanels.Count; i++)
        {
            if (i < actionButtons.Count)
            {
                IView view = nextPanels[i];
                actionButtons[i].onClick.AddListener(() => { Hide(view);});
            }
        }
    }
    public void Show()
    {
        gameObject.SetActive(true);
        canvasGroup.DOFade(1f, fadeDuration);
    }
    public void Hide(IView next)
    {
        if( (DateTime.Now - lastTimeClicked).TotalMilliseconds < TimeSpan.FromSeconds(clickCooldown).TotalMilliseconds) return;
        lastTimeClicked = DateTime.Now;

        UI_System.Instance.PanelTransition?.Invoke();
        
        Debug.Log($"ButtonClickedOnPanel {gameObject}");
        
        canvasGroup.DOFade(0f, fadeDuration).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
        next?.Show();
    }
}
