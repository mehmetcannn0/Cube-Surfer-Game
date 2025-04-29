using System.IO;
using UnityEditor;
using UnityEngine;

public class ShowPlayerDataWindow : EditorWindow
{
    private PlayerList playerList;

    [MenuItem("Tools/Player Tools/Oyuncu Verilerini G�ster-Sil")]
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
        if (playerList == null || playerList.Players == null)
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

        foreach (PlayerData player in playerList.Players)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Ad:", player.Name);
            EditorGUILayout.LabelField("Alt�n:", player.Gold.ToString());
            EditorGUILayout.LabelField("Skor:", player.Score.ToString());
            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("Veriyi Yeniden Y�kle"))
        {
            LoadPlayerData();
        }

        if (GUILayout.Button("Oyuncu Verilerini Sil"))
        {
            DeletePlayerData.DeletePlayerDataFile();
            LoadPlayerData();   
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public string Name;
    public int Score;
    public int Gold;
}
[System.Serializable]
public class PlayerList
{
    public PlayerData[] Players;
}