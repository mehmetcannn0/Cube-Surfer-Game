using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldUI;
    [SerializeField] TextMeshProUGUI scoreUI;

    [SerializeField] GameObject startUI;
    public GameObject finishLevelUI;
    public GameObject leaderboardUI;
    public GameObject playerNameUI;
    public GameObject gameOverUI;

    GameManager gameManager;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void MakeDeactiveUI(GameObject UIObject)
    {
        UIObject.SetActive(false);

    }
    public void MakeActiveUI(GameObject UIObject)
    {
        UIObject.SetActive(true);

    }

    /// <summary>
    ///  Close startUI ,finishLevelUI,playerNameUI,leaderboardUI,gameOverUI and call UpdateScoreUI, UpdateGoldUI
    /// </summary>
    public void DeactiveUIs()
    {
        startUI.SetActive(false);
        finishLevelUI.SetActive(false);
        playerNameUI.SetActive(false);
        leaderboardUI.SetActive(false);
        gameOverUI.SetActive(false);
        UpdateScoreUI();
        UpdateGoldUI();
    }

    private void OnEnable()
    {
        Coin.CollectCoinAction += UpdateGoldUI;
    }

    private void OnDisable()
    {
        Coin.CollectCoinAction -= UpdateGoldUI;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void UpdateGoldUI()
    {
        goldUI.text = gameManager.gold.ToString();
    }
    public void UpdateScoreUI()
    {
        scoreUI.text = gameManager.score.ToString();
    }

}
