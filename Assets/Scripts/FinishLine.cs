using UnityEngine;

public class FinishLine : MonoBehaviour ,IFinishLevel
{
    public void FinishLevel()
    {
        ActionController.OnLevelFinished?.Invoke();
    }     
}
