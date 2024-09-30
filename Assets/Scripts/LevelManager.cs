using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public GameObject levelCompleteUI;
    public float delayBeforeLoad = 3.0f; // Set the delay time in seconds
    private bool hasTriggered = false;

    void Start()
    {

    }

    public void StartCountdownAndLoadNextLevel()
    {
        if (!hasTriggered)
        {
            hasTriggered = true;
            levelCompleteUI.SetActive(true);
            StartCoroutine(CountdownAndLoadNextLevel());
        }
    }

    private IEnumerator CountdownAndLoadNextLevel()
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadScene("Level 2");
    }
}
