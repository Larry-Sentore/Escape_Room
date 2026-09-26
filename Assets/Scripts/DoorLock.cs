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
    private string currentEnteredCode = "";

    private void Awake()
    {
        ConfigureInputField();
    }

    private void ConfigureInputField()
    {
        if (inputField == null)
        {
            return;
        }

        inputField.onValueChanged.RemoveAllListeners();
        inputField.onEndEdit.RemoveAllListeners();
        inputField.onSubmit.RemoveAllListeners();

        inputField.onValueChanged.AddListener(value =>
        {
            currentEnteredCode = value ?? string.Empty;
        });

        inputField.onSubmit.AddListener(_ => SubmitCode());
        inputField.onEndEdit.AddListener(_ => SubmitCode());
    }

    public void AddDigit(string digit)
    {
        if (string.IsNullOrEmpty(digit))
        {
            return;
        }

        if (inputField != null)
        {
            inputField.text += digit;
        }

        currentEnteredCode = (inputField != null) ? inputField.text : currentEnteredCode + digit;
    }

    public void ClearCode()
    {
        currentEnteredCode = "";
        if (inputField != null)
        {
            inputField.text = "";
        }
    }

    private void Update()
    {
        if (inputField != null && keypadPanel != null && keypadPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SubmitCode();
            }
        }
    }

    public void OpenDoorPrompt()
    {
        if (isUnlocked) return;

        if (keypadPanel != null)
        {
            keypadPanel.SetActive(true);
            if (inputField != null)
            {
                ConfigureInputField();
                ClearCode();
                inputField.ActivateInputField();
            }
        }
    }

    public void SubmitCode()
    {
        if (isUnlocked) return;

        string enteredCode = (inputField != null) ? inputField.text : currentEnteredCode;
        string expectedCode = correctPasscode;

        if (enteredCode != null)
        {
            enteredCode = enteredCode.Trim();
            enteredCode = enteredCode.Replace(" ", "");
        }

        if (expectedCode != null)
        {
            expectedCode = expectedCode.Trim();
            expectedCode = expectedCode.Replace(" ", "");
        }

        Debug.Log("DoorLock submit: entered='" + enteredCode + "' expected='" + expectedCode + "'");

        if (enteredCode == expectedCode)
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
            ClearCode();
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