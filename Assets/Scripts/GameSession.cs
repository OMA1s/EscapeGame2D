using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    int playerScore = 0;


    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    void Awake()
    {
        int numInstances = FindObjectsOfType<GameSession>().Length;
        if (numInstances > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = playerScore.ToString();
    }

    public void processPlayerDeaths()
    {
        if (playerLives > 1)
        {
            takeLife();
        }
        else
        {
            resetGameSession();
        }
    }

    public void processScore(int score)
    {
        playerScore += score;
        scoreText.text = playerScore.ToString();
    }

    void takeLife()
    {
        playerLives--;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        livesText.text = playerLives.ToString();
    }

    void resetGameSession()
    {
        FindObjectOfType<ScenePersist>().resetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
