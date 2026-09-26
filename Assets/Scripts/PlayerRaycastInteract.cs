using UnityEngine;

public class PlayerRaycastInteract : MonoBehaviour
{
    public float maxDistance = 6f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                // Check for Painting
                SlidingPainting painting = hit.collider.GetComponent<SlidingPainting>();
                if (painting != null)
                {
                    painting.InteractWithPainting();
                    return;
                }

                // Check for Books
                BookInteractable book = hit.collider.GetComponent<BookInteractable>();
                if (book != null)
                {
                    book.OnClicked();
                    return;
                }
            }
        }
    }
}