using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public AudioSource backgroundMusic;

    void Start()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }
        else
        {
            Debug.LogError("Background music AudioSource is not assigned.");
        }
    }
}
