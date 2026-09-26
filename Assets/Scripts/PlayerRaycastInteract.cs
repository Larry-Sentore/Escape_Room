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
                SlidingPainting painting = hit.collider.GetComponentInParent<SlidingPainting>();
                if (painting != null)
                {
                    if (PuzzleManager.Instance != null)
                    {
                        PuzzleManager.Instance.PlayClickSound();
                    }

                    painting.InteractWithPainting();
                    return;
                }

                BookInteractable book = hit.collider.GetComponentInParent<BookInteractable>();
                if (book != null)
                {
                    if (PuzzleManager.Instance != null)
                    {
                        PuzzleManager.Instance.PlayClickSound();
                    }

                    book.OnClicked();
                    return;
                }

                TVRemoteInteractable remote = hit.collider.GetComponentInParent<TVRemoteInteractable>();
                if (remote != null)
                {
                    if (PuzzleManager.Instance != null)
                    {
                        PuzzleManager.Instance.PlayClickSound();
                    }

                    remote.UseRemote();
                    return;
                }

                ClockPuzzle clock = hit.collider.GetComponentInParent<ClockPuzzle>();
                if (clock != null)
                {
                    if (PuzzleManager.Instance != null)
                    {
                        PuzzleManager.Instance.PlayClickSound();
                    }

                    clock.OpenClockPrompt();
                    return;
                }

                DoorLock door = hit.collider.GetComponentInParent<DoorLock>();
                if (door != null)
                {
                    if (PuzzleManager.Instance != null)
                    {
                        PuzzleManager.Instance.PlayClickSound();
                    }

                    door.OpenDoorPrompt();
                    return;
                }
            }
        }
    }
}