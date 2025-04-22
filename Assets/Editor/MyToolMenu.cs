using UnityEngine;
using UnityEditor;

public class MyToolMenu : EditorWindow
{
    private string playerName = "Oyuncu";
    private float playerSpeed = 5f;
    private bool showExtraSettings = false;

    [MenuItem("Tools/My Custom Tool")]
    public static void ShowCustomTool()
    {
        EditorUtility.DisplayDialog("My Tool", "Bu benim özel aracım!", "Tamam");
    }

    [MenuItem("Tools/Özel Araç Penceresi")]
    public static void ShowWindow()
    {
        GetWindow<MyToolMenu>("Özel Araç");
    }

    void OnGUI()
    {
        GUIStyle titleStyle = new GUIStyle(EditorStyles.boldLabel);
        titleStyle.fontSize = 14;
        titleStyle.normal.textColor = Color.cyan;

        GUILayout.Label("🌟 Unity Özel Aracı 🌟", titleStyle);
        EditorGUILayout.Space();

        EditorGUILayout.HelpBox("Bu pencere, oyuncu ayarlarını test etmek için kullanılır.", MessageType.Info);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Oyuncu Ayarları", EditorStyles.boldLabel);

        playerName = EditorGUILayout.TextField("Oyuncu Adı", playerName);
        playerSpeed = EditorGUILayout.Slider("Hız", playerSpeed, 0f, 10f);

        showExtraSettings = EditorGUILayout.Toggle("Ek Ayarları Göster", showExtraSettings);

        if (showExtraSettings)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Buraya daha fazla ayar eklenebilir...");
            EditorGUILayout.Toggle("Tanımlı Özellik", true);
            EditorGUILayout.IntField("Puan", 100);
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.Space();
        if (GUILayout.Button("Ayarları Kaydet"))
        {
            Debug.Log($"Oyuncu: {playerName}, Hız: {playerSpeed}");
            EditorUtility.DisplayDialog("Başarılı", "Ayarlar kaydedildi!", "Tamam");
        }

        if (GUILayout.Button("Konsola Yazdır"))
        {
            Debug.Log("Merhaba Unity Editörü!");
        }
    }
}
