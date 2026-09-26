using System.Collections;
using UnityEngine;

public class TVFlash : MonoBehaviour
{
    public static TVFlash Instance;

    public MeshRenderer tvScreenRenderer;
    public float onDuration = 0.35f;
    public float offDuration = 0.25f;
    public float pauseBetweenLoops = 2.0f;

    private bool isFlashing = false;

    void Awake()
    {
        Instance = this;
        // Start with the screen turned off
        if (tvScreenRenderer != null)
        {
            tvScreenRenderer.enabled = false;
        }
    }

    public void StartFlashingPattern()
    {
        if (!isFlashing)
        {
            isFlashing = true;
            StartCoroutine(FlashRoutine());
        }
    }

    private IEnumerator FlashRoutine()
    {
        while (isFlashing)
        {
            // Flash 3 times
            for (int i = 0; i < 3; i++)
            {
                tvScreenRenderer.enabled = true;
                yield return new WaitForSeconds(onDuration);

                tvScreenRenderer.enabled = false;
                yield return new WaitForSeconds(offDuration);
            }

            // Pause before repeating the 3-flash cycle
            yield return new WaitForSeconds(pauseBetweenLoops);
        }
    }
}