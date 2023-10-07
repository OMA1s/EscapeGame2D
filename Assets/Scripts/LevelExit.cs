using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    int nextLevelIndex;
    int currentLevelIndex;

    [SerializeField] int loadLevelDelay = 2;
    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(loadNextLevel());
        }
    }

    IEnumerator loadNextLevel()
    {
        yield return new WaitForSecondsRealtime(loadLevelDelay);
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        nextLevelIndex = currentLevelIndex + 1;

        if (nextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0;
        }

        FindObjectOfType<ScenePersist>().resetScenePersist();
        SceneManager.LoadScene(nextLevelIndex);
    }
}
