using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public class PlayerUI
    {
        public TMP_Text nameText;
        public TMP_Text goldText;
        public TMP_Text scoreText;
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
            ui.nameText = child.Find("NameText").GetComponent<TextMeshProUGUI>();
            ui.goldText = child.Find("GoldText").GetComponent<TextMeshProUGUI>();
            ui.scoreText = child.Find("ScoreText").GetComponent<TextMeshProUGUI>();

            playerUIList.Add(ui);
        }
    }

    public void UpdateLeaderboardUI()
    {
        var players = saveData.Leaderboard.players;
        players.Sort((a, b) => b.score.CompareTo(a.score));

        for (int i = 0; i < playerUIList.Count; i++)
        {
            if (i < players.Count)
            {
                playerUIList[i].nameText.text = players[i].name;
                playerUIList[i].goldText.text = players[i].gold.ToString();
                playerUIList[i].scoreText.text = players[i].score.ToString();
            }
            else
            {
                playerUIList[i].nameText.text = "-";
                playerUIList[i].goldText.text = "";
                playerUIList[i].scoreText.text = "";
            }
        }
    }
}
