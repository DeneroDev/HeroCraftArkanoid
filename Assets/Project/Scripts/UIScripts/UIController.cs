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
    private RectTransform panelPreGame;
    [SerializeField]
    private RectTransform panelGoodEndGame;
    [SerializeField]
    private RectTransform panelBadEndGame;
    [SerializeField]
    private Text finalScoreText;

    // Use this for initialization
    void Start () {
        DOTween.Init();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void OnMenu() {
        OffGame();
        BackgroundUIMenu.DOAnchorPos(new Vector2(0, 0), 1);
        StartGameBtn.DOAnchorPos(new Vector2(-395, 400), 1);
        SettingGameBtn.DOAnchorPos(new Vector2(-240, 250), 1);
        QuitGameBtn.DOAnchorPos(new Vector2(-150, 110), 1);
    }

    public void OnPanelLevel() {
        PanelLevel.DOAnchorPos(Vector2.zero, 1);
    }

    public void OffPanelLevel()
    {
        PanelLevel.DOAnchorPos(new Vector2(2000,0), 1);
    }

    public void OffMenu()
    {
        BackgroundUIMenu.DOAnchorPos(new Vector2(-2000, 0), 1);
        StartGameBtn.DOAnchorPos(new Vector2(395, 400), 1);
        SettingGameBtn.DOAnchorPos(new Vector2(240, 250), 1);
        QuitGameBtn.DOAnchorPos(new Vector2(150, 110), 1);
        OnGame();
        OnPreGamePanel();
    }

    public void OnPreGamePanel() {
        panelPreGame.DOAnchorPos(Vector2.zero, 1);
    }

    public void OffPreGamePanel() {
        panelPreGame.DOAnchorPos(new Vector2(0, -1000), 1);
    }

    public void OnEndGamePanel(bool dead)
    {
        if (dead)
        {
            panelBadEndGame.DOAnchorPos(Vector2.zero, 1);
        }
        else {
            panelGoodEndGame.DOAnchorPos(Vector2.zero, 1);
            finalScoreText.text = "SCORE:" + GameController.GetInstance().GetScore();
        }
            
    }


    public void OffEndGamePanel()
    {
            panelBadEndGame.DOAnchorPos(new Vector2(0, -1000), 1);
            panelGoodEndGame.DOAnchorPos(new Vector2(0, -1000), 1);
    }

    public void OnGame() {
        textScore.rectTransform.DOAnchorPos(new Vector2(150, -150), 1);
    }


    public void OffGame()
    {
        textScore.rectTransform.DOAnchorPos(new Vector2(-200, -150), 1);
    }

    public void ScoreUpdate(int score) {
        textScore.text = "SCORE:"+score;
    }
}
