using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    public TextMeshProUGUI progressText;
    public TextMeshProUGUI timerText;
    public GameObject loseScreenPanel;

    public float totalTimeInSeconds = 180f; // 3 minutes
    private int completedTasks = 0;
    private bool isGameOver = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (isGameOver) return;

        if (totalTimeInSeconds > 0)
        {
            totalTimeInSeconds -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            totalTimeInSeconds = 0;
            TriggerGameOver();
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(totalTimeInSeconds / 60);
            int seconds = Mathf.FloorToInt(totalTimeInSeconds % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void CompleteTask()
    {
        completedTasks++;
        UpdateUI();
    }

    public void ApplyPenalty(float secondsToDeduct)
    {
        totalTimeInSeconds = Mathf.Max(0, totalTimeInSeconds - secondsToDeduct);
        UpdateTimerDisplay();
    }

    void UpdateUI()
    {
        if (progressText != null)
        {
            progressText.text = "Puzzle Progress: " + completedTasks + " / 5";
        }
    }

    void TriggerGameOver()
    {
        isGameOver = true;
        if (loseScreenPanel != null)
        {
            loseScreenPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}