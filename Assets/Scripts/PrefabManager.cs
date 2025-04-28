using System;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoSingleton<PrefabManager>
{
    [SerializeField] List<PrefabData> prefabs = new List<PrefabData>();

    public GameObject InstantiateObjet(PrefabType prefabType, Vector3 objectPosition, Transform parent = null)
    {
        PrefabData data = prefabs.Find(p => p.Type == prefabType);

        return Instantiate(data.Prefab, objectPosition, Quaternion.identity, parent);
    }
}

public enum PrefabType
{
    Cube,
    CubeTower,
    Wall,
    Coin,
    FinishGroup,
    CoinUI,
    WallTower
}

[Serializable]
public class PrefabData
{
    public PrefabType Type;
    public GameObject Prefab;
}