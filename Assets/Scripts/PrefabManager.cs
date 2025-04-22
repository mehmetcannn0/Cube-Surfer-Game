using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public RectTransform coinUIPrefab;
    public GameObject playerPrefab;
    public GameObject cubePrefab;
    public GameObject wallPrefab;
    public GameObject coinPrefab;
    public GameObject finishGroup;

    public static PrefabManager Instance;
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
}
