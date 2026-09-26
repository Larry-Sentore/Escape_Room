using UnityEngine;

public class TVRemoteInteractable : MonoBehaviour
{
    private bool hasBeenUsed = false;

    public void UseRemote()
    {
        if (hasBeenUsed) return;

        hasBeenUsed = true;

        // Turn on the TV flashing sequence
        if (TVFlash.Instance != null)
        {
            TVFlash.Instance.StartFlashingPattern();
        }

        // Count as Task 3 completion
        if (PuzzleManager.Instance != null)
        {
            PuzzleManager.Instance.CompleteTask();
        }

    }
}