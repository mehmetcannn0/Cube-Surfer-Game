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
            Debug.Log("Kay�t dosyas� ba�ar�yla silindi: " + filePath);
            EditorUtility.DisplayDialog("Ba�ar�l�", "Kay�t dosyas� silindi.", "Tamam");
        }
        else
        {
            Debug.LogWarning("Silinecek dosya bulunamad�: " + filePath);
            EditorUtility.DisplayDialog("Dosya Bulunamad�", "Silinecek kay�t dosyas� bulunamad�.", "Tamam");
        }
    }
}
