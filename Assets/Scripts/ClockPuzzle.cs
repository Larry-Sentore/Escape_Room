using UnityEngine;
using TMPro;

public class ClockPuzzle : MonoBehaviour
{
    public GameObject promptPanel;
    public TMP_Dropdown timeDropdown;
    public int correctOptionIndex = 2; // e.g. "3:00"
    public GameObject codeToReveal;    // Text or paper object showing the code
    public float wrongPenalty = 20f;

    private bool isSolved = false;

    public void OpenClockPrompt()
    {
        if (isSolved) return;

        if (promptPanel != null)
        {
            promptPanel.SetActive(true);
            if (timeDropdown != null)
            {
                timeDropdown.value = 0;
            }
        }
    }

    public void SubmitAnswer()
    {
        if (isSolved || timeDropdown == null) return;

        if (timeDropdown.value == correctOptionIndex)
        {
            isSolved = true;
            promptPanel.SetActive(false);

            // Reveal the hidden door code
            if (codeToReveal != null)
            {
                codeToReveal.SetActive(true);
            }

            if (PuzzleManager.Instance != null)
            {
                PuzzleManager.Instance.CompleteTask();
            }
        }
        else
        {
            timeDropdown.value = 0;
            if (PuzzleManager.Instance != null)
            {
                PuzzleManager.Instance.ApplyPenalty(wrongPenalty);
            }
        }
    }
}