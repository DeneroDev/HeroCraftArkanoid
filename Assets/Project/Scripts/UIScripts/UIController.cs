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
    [Header("Time Animation Slide")]
    public float TimeSlidePanel = 1f;


    void Start () {
        DOTween.Init();
    }

    public void UpdateUI()
    {
        switch (GameController.GetInstance().CurrentState)
        {
            case GameController.GameState.menu:
                    OffGame();
                    OffPreGamePanel();
                    OffEndGamePanel();
                    OnMenu();
                break;
            case GameController.GameState.pregame:
                    OffPanelLevel();
                    OffEndGamePanel();
                    OffMenu();
                    OnPreGamePanel();
                break;
            case GameController.GameState.game:
                    OffPreGamePanel();
                    OffPause();
                    OnGame();
                break;
            case GameController.GameState.goodend:
                OffGame();
                OnEndGamePanel(true);
                break;
            case GameController.GameState.badend:
                OffGame();
                OnEndGamePanel(false);
                break;
            case GameController.GameState.pause:
                    OnPause();
                break;
            case GameController.GameState.end:
                    OffEndGamePanel();
                break;

        }
    }


    public void OnMenu() {
        BackgroundUIMenu.DOAnchorPos(new Vector2(0, 0), TimeSlidePanel);
        StartGameBtn.DOAnchorPos(new Vector2(-395, 400), TimeSlidePanel);
        SettingGameBtn.DOAnchorPos(new Vector2(-240, 250), TimeSlidePanel);
        QuitGameBtn.DOAnchorPos(new Vector2(-150, 110), TimeSlidePanel);
    }

    public void OnPanelLevel() {
        PanelLevel.DOAnchorPos(Vector2.zero, TimeSlidePanel);
    }

    public void OffPanelLevel()
    {
        PanelLevel.DOAnchorPos(new Vector2(2000,0), TimeSlidePanel);
    }

    public void OffMenu()
    {
        BackgroundUIMenu.DOAnchorPos(new Vector2(-2000, 0), TimeSlidePanel);
        StartGameBtn.DOAnchorPos(new Vector2(395, 400), TimeSlidePanel);
        SettingGameBtn.DOAnchorPos(new Vector2(240, 250), TimeSlidePanel);
        QuitGameBtn.DOAnchorPos(new Vector2(150, 110), TimeSlidePanel);
    }

    public void OnPreGamePanel() {
        panelPreGame.DOAnchorPos(Vector2.zero, TimeSlidePanel);
    }

    public void OffPreGamePanel() {
        panelPreGame.DOAnchorPos(new Vector2(0, -1000), TimeSlidePanel);
    }

    public void OnEndGamePanel(bool winner)
    {
        if (!winner)
            panelBadEndGame.DOAnchorPos(Vector2.zero, TimeSlidePanel);
        else
        {
            panelGoodEndGame.DOAnchorPos(Vector2.zero, TimeSlidePanel);
            finalScoreText.text = "SCORE:" + GameController.GetInstance().DataManager.GetScore().ToString();
        }
    }


    public void OffEndGamePanel()
    {
            panelBadEndGame.DOAnchorPos(new Vector2(0, -1000), TimeSlidePanel);
            panelGoodEndGame.DOAnchorPos(new Vector2(0, -1000), TimeSlidePanel);
    }

    public void OnGame() {
        textScore.rectTransform.DOAnchorPos(new Vector2(150, -150), TimeSlidePanel);
        pauseBtn.DOAnchorPos(new Vector2(-200, -150), TimeSlidePanel);
    }


    public void OffGame()
    {
        textScore.rectTransform.DOAnchorPos(new Vector2(-400, -150), TimeSlidePanel);
        pauseBtn.DOAnchorPos(new Vector2(400, -150), TimeSlidePanel);
    }

    public void OnPause() {
        panelPauseGame.DOAnchorPos(Vector2.zero, TimeSlidePanel);
    }

    public void OffPause()
    {
        panelPauseGame.DOAnchorPos(new Vector2(0,1000), TimeSlidePanel);
    }

    public void ScoreUpdate() {
        textScore.text = "SCORE:"+GameController.GetInstance().DataManager.GetScore();
    }



}
