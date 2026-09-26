using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 1.5f, -3f);
    public float followSpeed = 10f;
    public float rotationSpeed = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        // Multiply the offset by target rotation so it stays directly behind their back
        Vector3 targetPosition = target.position + (target.rotation * offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Smoothly rotate to match the player's view direction
        Vector3 lookTarget = target.position + Vector3.up * 0.7f;
        Quaternion desiredRotation = Quaternion.LookRotation(lookTarget - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
    }
}