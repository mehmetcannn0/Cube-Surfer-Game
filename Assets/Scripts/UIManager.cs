using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI GoldUI;

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
    public void CloseUIs()
    {
        startUI.SetActive(false);
        finishLevelUI.SetActive(false);
        playerNameUI.SetActive(false);
        leaderboardUI.SetActive(false);
        gameOverUI.SetActive(false);

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
        GoldUI.text = gameManager.gold.ToString();
    }

}
