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
                // 1. Painting
                SlidingPainting painting = hit.collider.GetComponentInParent<SlidingPainting>();
                if (painting != null)
                {
                    painting.InteractWithPainting();
                    return;
                }

                // 2. Books
                BookInteractable book = hit.collider.GetComponentInParent<BookInteractable>();
                if (book != null)
                {
                    book.OnClicked();
                    return;
                }

                // 3. TV Remote
                TVRemoteInteractable remote = hit.collider.GetComponentInParent<TVRemoteInteractable>();
                if (remote != null)
                {
                    remote.UseRemote();
                    return;
                }

                // 4. Clock
                ClockPuzzle clock = hit.collider.GetComponentInParent<ClockPuzzle>();
                if (clock != null)
                {
                    clock.OpenClockPrompt();
                    return;
                }

                // 5. Door
                DoorLock door = hit.collider.GetComponentInParent<DoorLock>();
                if (door != null)
                {
                    door.OpenDoorPrompt();
                    return;
                }
            }
        }
    }
}