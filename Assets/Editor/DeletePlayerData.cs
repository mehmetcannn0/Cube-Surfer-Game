using UnityEngine;
using UnityEditor;
using System.IO;

public class DeletePlayerData : MonoBehaviour
{
    [MenuItem("Tools/Delete Data/Delete Player Data")]
    public static void DeletePlayerDataFile()
    {
        string filePath = Application.persistentDataPath + "/PlayerData.json";

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Kayýt dosyasý baþarýyla silindi: " + filePath);
            EditorUtility.DisplayDialog("Baþarýlý", "Kayýt dosyasý silindi.", "Tamam");
        }
        else
        {
            Debug.LogWarning("Silinecek dosya bulunamadý: " + filePath);
            EditorUtility.DisplayDialog("Dosya Bulunamadý", "Silinecek kayýt dosyasý bulunamadý.", "Tamam");
        }
    }
}
