using UnityEngine;
using UnityEditor;
using System.IO;

public class ShowPlayerDataWindow : EditorWindow
{
    private PlayerList playerList;

    [MenuItem("Tools/Player Tools/Oyuncu Verilerini G�ster")]
    public static void ShowWindow()
    {
        GetWindow<ShowPlayerDataWindow>("Oyuncu Verileri");
    }

    private void OnEnable()
    {
        LoadPlayerData();
    }

    private void LoadPlayerData()
    {
        string filePath = Application.persistentDataPath + "/PlayerData.json";
        Debug.Log("Dosya yolu: " + filePath);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            Debug.Log("JSON ��eri�i:\n" + json);
            playerList = JsonUtility.FromJson<PlayerList>(json);
        }
        else
        {
            Debug.LogWarning("PlayerData.json bulunamad�.");
            playerList = null;
        }
    }

    private void OnGUI()
    {
        if (playerList == null || playerList.players == null)
        {
            EditorGUILayout.HelpBox("Oyuncu verisi y�klenemedi!", MessageType.Warning);
            if (GUILayout.Button("Yeniden Dene"))
            {
                LoadPlayerData();
            }
            return;
        }

        EditorGUILayout.LabelField("Oyuncu Listesi", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        foreach (var player in playerList.players)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Ad:", player.name);
            EditorGUILayout.LabelField("Alt�n:", player.gold.ToString());
            EditorGUILayout.LabelField("Skor:", player.score.ToString());
            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("Veriyi Yeniden Y�kle"))
        {
            LoadPlayerData();
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int level;
    public int gold;
}
[System.Serializable]
public class PlayerList
{
    public Player[] players;
}