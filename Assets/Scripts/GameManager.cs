using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public string currentScene;
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject enermyPanel;
    [SerializeField] private GameObject gameOverUi;
    private bool isGameOver = false;
    [SerializeField] private GameObject gameWinUi;
    private bool isGameWin = false;
    private int currentEnergy;
    [SerializeField] private int energyThreshold = 3;
    [SerializeField] private GameObject bossUndeadSurvivor;
    [SerializeField] private GameObject enemySpaner;
    private bool bossCalled = false;
    [SerializeField] private Image energyBar;
    [SerializeField] private GameObject gameUIUndeadSurvivor;

    private void Awake() 
    { 
        currentScene = SceneManager.GetActiveScene().name; 
    }

    void Start()
    {
        UpdateScore();
        gameOverUi.SetActive(false);
        gameWinUi.SetActive(false);

        currentEnergy = 0;
        UpdateEnergyBar();
        bossUndeadSurvivor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddScore(int points)
    {
        if (!isGameOver && !isGameOver)
        {
            score += points;
            UpdateScore();
        }
        
    }

    public void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        if (currentScene == "MarioTrialsScene")
        {
            isGameOver = true;
            score = 0;
            Time.timeScale = 0;
            gameOverUi.SetActive(true);
        }
        else if (currentScene == "UndeadSurvivorScene")
        {
            isGameOver = true;
            Time.timeScale = 0;
            gameOverUi.SetActive(true);
        }
    }

    public void GameWin()
    {
        isGameWin = true;
        Time.timeScale = 0;
        gameWinUi.SetActive(true);
    }

    public void RestartGame()
    {
        if (currentScene == "MarioTrialsScene")
        {
            isGameOver = false;
            score = 0;
            Time.timeScale = 1;
            gameOverUi.SetActive(false);
            UpdateScore();
            SceneManager.LoadScene("MarioTrialsScene");
        }
        else if (currentScene == "UndeadSurvivorScene")
        {
            Time.timeScale = 1;
            gameOverUi.SetActive(false);
            SceneManager.LoadScene("UndeadSurvivorScene");
        }
    }

    public void GotoMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public bool IsGameWin()
    {
        return isGameWin;
    }

    public void AddEnergy()
    {
        if (bossCalled)
        {
            return; // Nếu boss đã được gọi, không cần thêm năng lượng nữa
        }
        currentEnergy += 1;
        UpdateEnergyBar();
        if (currentEnergy >= energyThreshold)
        {
            CallBoss();
        }
    }
    private void CallBoss()
    {
        if (!bossCalled)
        {
            Instantiate(bossUndeadSurvivor, new Vector3(0, 0, 0), Quaternion.identity);
            bossCalled = true;
            bossUndeadSurvivor.SetActive(true);
            enemySpaner.SetActive(false);
            enermyPanel.SetActive(false);
        }
    }
    private void UpdateEnergyBar()
    {
        if(energyBar != null)
        {
            float fillAmount = (float)currentEnergy / energyThreshold;
            energyBar.fillAmount = fillAmount;
        }
    }
}
