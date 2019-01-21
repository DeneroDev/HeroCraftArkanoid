using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    [Header("UIElementsMenu")]
    [SerializeField]
    private RectTransform StartGameBtn;
    [SerializeField]
    private RectTransform SettingGameBtn;
    [SerializeField]
    private RectTransform QuitGameBtn;
    [SerializeField]
    private RectTransform BackgroundUIMenu;
    [SerializeField]
    private RectTransform PanelLevel;
    [Header("UIElementsGame")]
    [SerializeField]
    private Text textScore;
    [SerializeField]
    private RectTransform pauseBtn;
    [SerializeField]
    private RectTransform panelPreGame;
    [SerializeField]
    private RectTransform panelGoodEndGame;
    [SerializeField]
    private RectTransform panelBadEndGame;
    [SerializeField]
    private Text finalScoreText;
    [SerializeField]
    private RectTransform panelPauseGame;

    // Use this for initialization
    void Start () {
        DOTween.Init();
    }


    public void OnMenu() {
        OffGame();
        BackgroundUIMenu.DOAnchorPos(new Vector2(0, 0), Data.TIME_SLIDE_ANIM_PANEL);
        StartGameBtn.DOAnchorPos(new Vector2(-395, 400), Data.TIME_SLIDE_ANIM_PANEL);
        SettingGameBtn.DOAnchorPos(new Vector2(-240, 250), Data.TIME_SLIDE_ANIM_PANEL);
        QuitGameBtn.DOAnchorPos(new Vector2(-150, 110), Data.TIME_SLIDE_ANIM_PANEL);
    }

    public void OnPanelLevel() {
        PanelLevel.DOAnchorPos(Vector2.zero, Data.TIME_SLIDE_ANIM_PANEL);
    }

    public void OffPanelLevel()
    {
        PanelLevel.DOAnchorPos(new Vector2(2000,0), Data.TIME_SLIDE_ANIM_PANEL);
    }

    public void OffMenu()
    {
        BackgroundUIMenu.DOAnchorPos(new Vector2(-2000, 0), Data.TIME_SLIDE_ANIM_PANEL);
        StartGameBtn.DOAnchorPos(new Vector2(395, 400), Data.TIME_SLIDE_ANIM_PANEL);
        SettingGameBtn.DOAnchorPos(new Vector2(240, 250), Data.TIME_SLIDE_ANIM_PANEL);
        QuitGameBtn.DOAnchorPos(new Vector2(150, 110), Data.TIME_SLIDE_ANIM_PANEL);
        OnGame();
        OnPreGamePanel();
    }

    public void OnPreGamePanel() {
        panelPreGame.DOAnchorPos(Vector2.zero, Data.TIME_SLIDE_ANIM_PANEL);
    }

    public void OffPreGamePanel() {
        panelPreGame.DOAnchorPos(new Vector2(0, -1000), Data.TIME_SLIDE_ANIM_PANEL);
    }

    public void OnEndGamePanel(bool dead)
    {
        if (dead)
        {
            panelBadEndGame.DOAnchorPos(Vector2.zero, Data.TIME_SLIDE_ANIM_PANEL);
        }
        else {
            panelGoodEndGame.DOAnchorPos(Vector2.zero, Data.TIME_SLIDE_ANIM_PANEL);
            finalScoreText.text = "SCORE:" + GameController.GetInstance().GetScore();
        }
    }


    public void OffEndGamePanel()
    {
            panelBadEndGame.DOAnchorPos(new Vector2(0, -1000), Data.TIME_SLIDE_ANIM_PANEL);
            panelGoodEndGame.DOAnchorPos(new Vector2(0, -1000), Data.TIME_SLIDE_ANIM_PANEL);
    }

    public void OnGame() {
        textScore.rectTransform.DOAnchorPos(new Vector2(150, -150), Data.TIME_SLIDE_ANIM_PANEL);
        pauseBtn.DOAnchorPos(new Vector2(-200, -150), Data.TIME_SLIDE_ANIM_PANEL);
    }


    public void OffGame()
    {
        textScore.rectTransform.DOAnchorPos(new Vector2(-200, -150), Data.TIME_SLIDE_ANIM_PANEL);
    }

    public void OnPause() {
        panelPauseGame.DOAnchorPos(Vector2.zero, Data.TIME_SLIDE_ANIM_PANEL);
    }

    public void OffPause()
    {
        panelPauseGame.DOAnchorPos(new Vector2(0,-1000), Data.TIME_SLIDE_ANIM_PANEL);
    }

    public void ScoreUpdate(int score) {
        textScore.text = "SCORE:"+score;
    }
}
