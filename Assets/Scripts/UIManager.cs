using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] TextMeshProUGUI goldUI;
    [SerializeField] TextMeshProUGUI scoreUI;
    [SerializeField] GameObject startUI;

    public GameObject FinishLevelUI;
    public GameObject LeaderboardUI;
    public GameObject PlayerNameUI;
    public GameObject GameOverUI;
    public GameObject PopUpUI;
    public Canvas Canvas;
    public RectTransform TargetCoinUI;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnEnable()
    {
        Coin.OnCoinCollected += UpdateGoldUI;
        ActionController.OnScoreAdded += UpdateScoreUI;
        ActionController.OnLevelFinished += OpenFinishUI;
        ActionController.OnGameOver += OpenGameOverUIs;
        ActionController.OnPopUpOpened += OpenPopUp;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= UpdateGoldUI;
        ActionController.OnScoreAdded -= UpdateScoreUI;
        ActionController.OnLevelFinished -= OpenFinishUI;
        ActionController.OnGameOver -= OpenGameOverUIs;
        ActionController.OnPopUpOpened -= OpenPopUp;
    }

    public void CloseFinishUI()
    {
        FinishLevelUI.SetActive(false);
    }

    public void OpenFinishUI()
    {
        FinishLevelUI.SetActive(true);
    }
    public void OpenGameOverUIs()
    {
        GameOverUI.SetActive(true);
        PlayerNameUI.SetActive(true);
        LeaderboardUI.SetActive(true);
    }

    /// <summary>
    ///  Close startUI ,finishLevelUI,playerNameUI,leaderboardUI,gameOverUI and call UpdateScoreUI, UpdateGoldUI
    /// </summary>
    public void DeactiveUIs()
    {
        if (!string.IsNullOrWhiteSpace(gameManager.PlayerName))
        {
            //Debug.Log("DeactiveUIs");
            startUI.SetActive(false);
            FinishLevelUI.SetActive(false);
            PlayerNameUI.SetActive(false);
            LeaderboardUI.SetActive(false);
            GameOverUI.SetActive(false);
            UpdateScoreUI(gameManager.Score);
            UpdateGoldUI();
        }
    }

    public void UpdateGoldUI()
    {
        goldUI.text = gameManager.Gold.ToString();
    }

    public void UpdateScoreUI(int addedScore)
    {
        scoreUI.text = gameManager.Score.ToString();
    }

    public void OpenPopUp()
    {
        StopAllCoroutines();
        PopUpUI.transform.localScale = Vector3.zero;
        PopUpUI.SetActive(true);
        PopUpUI.transform.DOScale(1, Utils.POP_UP_ANIMATION_DURATION).OnComplete(() =>
        {
            StartCoroutine(ClosePopUpAfterDelay(2f));
        });
    }

    private IEnumerator ClosePopUpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PopUpUI.transform.DOScale(0, Utils.POP_UP_ANIMATION_DURATION).OnComplete(() =>
        {
            PopUpUI.SetActive(false);
        });
    }
}

public static partial class ActionController
{
    public static Action OnPopUpOpened;
}
