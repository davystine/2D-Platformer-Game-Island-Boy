using UnityEngine;

public class FinalEnemy : MonoBehaviour
{
    private void OnDestroy()
    {
        LevelManager transitionManager = FindObjectOfType<LevelManager>();

        if (transitionManager != null)
        {
            transitionManager.StartCountdownAndLoadNextLevel();
        }
        else
        {
            Debug.LogError("LevelManager not found in the scene.");
        }
    }
}

