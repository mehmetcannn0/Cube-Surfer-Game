using UnityEngine;

public class FinishLine : MonoBehaviour ,IFinishLevel
{
    public void FinishLevel()
    {
        LevelManager.Instance.OnLevelFinished?.Invoke();
    }     
}
