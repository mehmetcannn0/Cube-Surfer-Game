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
    public GameObject finishLevelUI;
    public GameObject leaderboardUI;
    public GameObject playerNameUI;
    public GameObject gameOverUI;
    public GameObject PopUpUI;
    public Canvas canvas;
    public RectTransform targetCoinUI;
 

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnEnable()
    {
        Coin.OnCoinCollected += UpdateGoldUI;
        PlayerInteractionController.Instance.OnScoreAdded += UpdateScoreUI;
        LevelManager.Instance.OnLevelFinished += CloseFinishUI;
        PlayerInteractionController.Instance.OnGameOver += OpenGameOverUIs;
        ActionController.OnLevelStart += DeactiveUIs;
        LevelManager.Instance.OnLevelRestarted += DeactiveUIs;
        ActionController. OnPopUpOpened += OpenPopUp;

    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= UpdateGoldUI;
        PlayerInteractionController.Instance.OnScoreAdded -= UpdateScoreUI;
        LevelManager.Instance.OnLevelFinished -= CloseFinishUI;
        PlayerInteractionController.Instance.OnGameOver -= OpenGameOverUIs;
        ActionController.OnLevelStart -= DeactiveUIs;
        LevelManager.Instance.OnLevelRestarted -= DeactiveUIs;
        ActionController.OnPopUpOpened -= OpenPopUp;

    }

    public void MakeDeactiveUI(GameObject UIObject)
    {
        UIObject.SetActive(false);
    }

    public void MakeActiveUI(GameObject UIObject)
    {
        UIObject.SetActive(true);
    }

    public void CloseFinishUI()
    {
        finishLevelUI.SetActive(false);
    }
    public void OpenGameOverUIs()
    {
        gameOverUI.SetActive(true);
        playerNameUI.SetActive(true);
        leaderboardUI.SetActive(true);
    }

    /// <summary>
    ///  Close startUI ,finishLevelUI,playerNameUI,leaderboardUI,gameOverUI and call UpdateScoreUI, UpdateGoldUI
    /// </summary>
    public void DeactiveUIs()
    {
        Debug.Log("DeactiveUIs");
        startUI.SetActive(false);
        finishLevelUI.SetActive(false);
        playerNameUI.SetActive(false);
        leaderboardUI.SetActive(false);
        gameOverUI.SetActive(false);
        UpdateScoreUI(0);
        UpdateGoldUI();
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
        Debug.Log(" pop up");
        StopAllCoroutines();
        PopUpUI.transform.localScale = Vector3.zero;  
        PopUpUI.SetActive(true);
        PopUpUI.transform.DOScale(1, 0.5f).OnComplete(() =>
        {
            StartCoroutine(ClosePopUpAfterDelay(2f));  
        });
    }

    private IEnumerator ClosePopUpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PopUpUI.transform.DOScale(0, 0.5f).OnComplete(() =>
        {
            PopUpUI.SetActive(false);  
        });
    }


}

public static partial class ActionController
{
    public static Action OnPopUpOpened;
}
