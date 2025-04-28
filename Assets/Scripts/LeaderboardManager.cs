using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public class PlayerUI
    {
        public TMP_Text NameText;
        public TMP_Text GoldText;
        public TMP_Text ScoreText;
    }

    private List<PlayerUI> playerUIList = new List<PlayerUI>();
    [SerializeField] Transform leaderboardParent;

    SaveData saveData;

    void Start()
    {
        saveData = SaveData.Instance;
        InitializePlayerUIs();
        saveData.LoadFromJson();
        UpdateLeaderboardUI();
    }

    private void OnEnable()
    {
        ActionController.OnGameOver += UpdateLeaderboardUI;
    }

    private void OnDisable()
    {
        ActionController.OnGameOver -= UpdateLeaderboardUI;
    }

    void InitializePlayerUIs()
    {
        playerUIList.Clear();

        foreach (Transform child in leaderboardParent)
        {
            PlayerUI ui = new PlayerUI();
            ui.NameText = child.Find("NameText").GetComponent<TextMeshProUGUI>();
            ui.GoldText = child.Find("GoldText").GetComponent<TextMeshProUGUI>();
            ui.ScoreText = child.Find("ScoreText").GetComponent<TextMeshProUGUI>();

            playerUIList.Add(ui);
        }
    }

    public void UpdateLeaderboardUI()
    {
        var players = saveData.Leaderboard.Players;
        players.Sort((a, b) => b.Score.CompareTo(a.Score));

        for (int i = 0; i < playerUIList.Count; i++)
        {
            if (i < players.Count)
            {
                playerUIList[i].NameText.text = players[i].Name;
                playerUIList[i].GoldText.text = players[i].Gold.ToString();
                playerUIList[i].ScoreText.text = players[i].Score.ToString();
            }
            else
            {
                playerUIList[i].NameText.text = "-";
                playerUIList[i].GoldText.text = "";
                playerUIList[i].ScoreText.text = "";
            }
        }
    }
}
