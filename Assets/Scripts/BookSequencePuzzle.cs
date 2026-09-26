using System.Collections;
using UnityEngine;

public class BookSequencePuzzle : MonoBehaviour
{
    public static BookSequencePuzzle Instance;

    public Transform boxesTransform; // The boxes GameObject on top of the shelf
    public Vector3 slideOffset = new Vector3(0f, 0f, 0.4f); // Distance boxes push forward
    public float slideDuration = 1.0f;
    public float wrongOrderPenalty = 15f;

    private int expectedStep = 0;
    private bool isSolved = false;

    void Awake()
    {
        Instance = this;
    }

    public void SelectBook(int colorIndex)
    {
        if (isSolved) return;

        // Check if player clicked the correct next color in order (0, then 1, then 2)
        if (colorIndex == expectedStep)
        {
            expectedStep++;

            // All 3 correct books clicked (0, 1, 2)
            if (expectedStep >= 3)
            {
                isSolved = true;
                StartCoroutine(SlideBoxesForward());

                if (PuzzleManager.Instance != null)
                {
                    PuzzleManager.Instance.CompleteTask();
                    }
            }
        }
        else
        {
            // Wrong book clicked: reset sequence and deduct time
            expectedStep = 0;

            if (PuzzleManager.Instance != null)
            {
                PuzzleManager.Instance.ApplyPenalty(wrongOrderPenalty);
            }
        }
    }

    private IEnumerator SlideBoxesForward()
    {
        if (boxesTransform == null) yield break;

        Vector3 startPos = boxesTransform.position;
        Vector3 targetPos = startPos + slideOffset;
        float elapsed = 0f;

        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / slideDuration;
            boxesTransform.position = Vector3.Lerp(startPos, targetPos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }

        boxesTransform.position = targetPos;
    }
}