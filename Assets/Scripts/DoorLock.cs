using System.Collections;
using UnityEngine;
using TMPro;

public class DoorLock : MonoBehaviour
{
    public string correctPasscode = "8346";
    public GameObject keypadPanel;
    public TMP_InputField inputField;
    public GameObject winScreenPanel;

    public Vector3 openRotation = new Vector3(0f, 90f, 0f);
    public float openSpeed = 1.5f;
    public float wrongPenalty = 20f;

    private bool isUnlocked = false;

    public void OpenDoorPrompt()
    {
        if (isUnlocked) return;

        if (keypadPanel != null)
        {
            keypadPanel.SetActive(true);
            if (inputField != null)
            {
                inputField.text = "";
                inputField.ActivateInputField();
            }
        }
    }

    public void SubmitCode()
    {
        if (isUnlocked || inputField == null) return;

        if (inputField.text.Trim() == correctPasscode)
        {
            isUnlocked = true;
            if (keypadPanel != null)
            {
                keypadPanel.SetActive(false);
            }

            StartCoroutine(SwingDoorOpen());

            if (PuzzleManager.Instance != null)
            {
                PuzzleManager.Instance.CompleteTask();
            }

            if (winScreenPanel != null)
            {
                winScreenPanel.SetActive(true);
            }
        }
        else
        {
            inputField.text = "";
            if (PuzzleManager.Instance != null)
            {
                PuzzleManager.Instance.ApplyPenalty(wrongPenalty);
            }
        }
    }

    public void CloseKeypad()
    {
        if (keypadPanel != null)
        {
            keypadPanel.SetActive(false);
        }
    }

    private IEnumerator SwingDoorOpen()
    {
        Quaternion startRot = transform.rotation;
        Quaternion targetRot = startRot * Quaternion.Euler(openRotation);
        float elapsed = 0f;

        while (elapsed < openSpeed)
        {
            elapsed += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRot, targetRot, elapsed / openSpeed);
            yield return null;
        }

        transform.rotation = targetRot;
    }
}