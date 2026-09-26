using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    public TextMeshProUGUI progressText;
    public TextMeshProUGUI timerText;
    public GameObject loseScreenPanel;

    public AudioSource audioSource;
    public AudioClip clickSFX;
    public AudioClip successSFX;
    public AudioClip wrongSFX;

    public float totalTimeInSeconds = 180f;
    private int completedTasks = 0;
    private bool isGameOver = false;

    void Awake()
    {
        Instance = this;

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
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

    public void PlayClickSound()
    {
        if (audioSource != null && clickSFX != null)
        {
            audioSource.clip = clickSFX;
            audioSource.time = 0.4f;
            audioSource.Play();
        }
    }

    public void PlaySuccessSound()
    {
        if (audioSource != null && successSFX != null)
        {
            audioSource.clip = successSFX;
            audioSource.time = 0.7f;
            audioSource.Play();
        }
    }

    public void PlayWrongSound()
    {
        if (audioSource != null && wrongSFX != null)
        {
            audioSource.clip = wrongSFX;
            audioSource.time = 0.45f;
            audioSource.Play();
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
        PlaySuccessSound();
        UpdateUI();
    }

    public void ApplyPenalty(float secondsToDeduct)
    {
        totalTimeInSeconds = Mathf.Max(0, totalTimeInSeconds - secondsToDeduct);
        PlayWrongSound();
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