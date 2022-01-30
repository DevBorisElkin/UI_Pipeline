using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NaughtyAttributes;
using UI_Pipeline;
using UnityEngine;
using UnityEngine.UI;

public class UI_System : MonoBehaviour
{
    public static UI_System Instance;

    public int level = 1;
    
    private void Awake()
    {
        Instance = this;
    }

    public UI_Pool ui_Pool;
    
    public Image backgroundFade;
    public CanvasGroup canvasForPanels;
    
    public Action PanelTransition;

    [ReadOnly]
    public List<BaseView> ViewsToShow;
    private void Start() => SetUp();

    public void SetUp()
    {
        PanelTransition += BlockClicks;
        
        UI_Lobby                   ui_lobby = ui_Pool.CreatePanel<UI_Lobby>();
        UI_Settings             ui_settings = ui_Pool.CreatePanel<UI_Settings>();
        UI_UpgradesScreen ui_upgradesScreen = ui_Pool.CreatePanel<UI_UpgradesScreen>();
        UI_InGame                 ui_inGame = ui_Pool.CreatePanel<UI_InGame>();
        UI_MatchEndScreen ui_matchEndScreen = ui_Pool.CreatePanel<UI_MatchEndScreen>();

        ViewsToShow = new List<BaseView>();
        ViewsToShow.Add(ui_lobby);
        ViewsToShow.Add(ui_settings);
        ViewsToShow.Add(ui_upgradesScreen);
        ViewsToShow.Add(ui_inGame);
        ViewsToShow.Add(ui_matchEndScreen);
        
        foreach (var a in ViewsToShow)
            a.gameObject.SetActive(false);

        ui_lobby.Bind(new List<IView>()
        {
            ui_inGame,
            ui_settings,
            ui_upgradesScreen
        });
        
        ui_settings.Bind(new List<IView>()
        {
            ui_lobby
        });
        
        ui_inGame.Bind(new List<IView>()
        {
            ui_matchEndScreen
        });
        
        ui_matchEndScreen.Bind(new List<IView>()
        {
            ui_lobby
        });

        ViewsToShow[0].Show();
    }

    async void BlockClicks()
    {
        canvasForPanels.interactable = false;
        await Task.Delay(1000);
        canvasForPanels.interactable = true;
    }
}
