using UnityEngine;

public class DeactivatorOnLowQuality : MonoBehaviour
{
    public QualityManager.Quality qualityThreshhold = QualityManager.Quality.High;

    void Start()
    {
        if (QualityManager.quality < qualityThreshhold)
        {
            gameObject.SetActiveRecursively(false);
        }
        enabled = false;
    }
}
