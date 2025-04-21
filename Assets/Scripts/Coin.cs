using DG.Tweening;
using System;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public static event Action CollectCoinAction;

    PrefabManager prefabManager;
    LevelManager levelManager;
    
    private void Awake()
    {
        levelManager = LevelManager.Instance;
        prefabManager = PrefabManager.Instance;
    }

    private void Start()
    {
        RotateCoin();
    }

    private void RotateCoin()
    {
        transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Restart);
    }

    public void Collect()
    {
        CollectCoinAction?.Invoke();
        PlayCoinCollectAnimation();
        PlayCoinSound();

        Debug.Log("collected a coin");
        Destroy(gameObject);
    }

    public void PlayCoinCollectAnimation()
    {
        RectTransform animatedCoin = Instantiate(prefabManager.coinUIPrefab, levelManager.canvas.transform);
        animatedCoin.anchoredPosition3D = Vector3.zero;

        animatedCoin.DOMove(levelManager.targetCoinUI.position, 0.8f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => Destroy(animatedCoin.gameObject));
    }
    private void PlayCoinSound()
    {
        Debug.Log("PlayCoinSound");

    }

}
