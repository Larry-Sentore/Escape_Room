using System.Collections;
using UnityEngine;

public class SlidingPainting : MonoBehaviour
{
    public Vector3 slideOffset = new Vector3(0f, -1.2f, 0f);
    public float slideDuration = 1.0f;

    private bool hasTriggered = false;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + slideOffset;
    }

    // Called when clicked by the mouse raycast
    public void InteractWithPainting()
    {
        if (hasTriggered) return;

        StartCoroutine(SlideDownRoutine());
    }

    private IEnumerator SlideDownRoutine()
    {
        hasTriggered = true;
        float elapsed = 0f;

        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / slideDuration;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }

        transform.position = targetPosition;

        if (PuzzleManager.Instance != null)
        {
            PuzzleManager.Instance.CompleteTask();
        }
    }
}