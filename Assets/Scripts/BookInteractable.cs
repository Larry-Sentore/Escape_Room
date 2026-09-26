using UnityEngine;

public class BookInteractable : MonoBehaviour
{
    // 0 = red, 1 = yellow, 2 = green
    public int bookColorIndex;

    public void OnClicked()
    {
        if (BookSequencePuzzle.Instance != null)
        {
            BookSequencePuzzle.Instance.SelectBook(bookColorIndex);
        }
    }
}