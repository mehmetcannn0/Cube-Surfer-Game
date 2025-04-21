using TMPro;
using UnityEditor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI GoldUI;

    public GameObject startUI;
    public GameObject finishLevelUI;
    public GameObject gameOverUI;
    public GameObject playerNameUI;
    
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
