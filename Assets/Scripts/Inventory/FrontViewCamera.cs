using UnityEngine;

public class FrontViewCamera : MonoBehaviour
{
    [Header("Целевой объект")]
    public Transform target;
    public Vector3 targetOffset = new Vector3(0, 1.5f, 0); // Смещение к голове

    [Header("Настройки камеры")]
    public float distance = 3f;
    public float height = 1f;
    public float rotationSpeed = 5f;

    [Header("Сглаживание")]
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        Vector3 targetForward = target.forward;
        targetForward.y = 0;
        targetForward.Normalize();

        Vector3 desiredPosition = target.position + targetOffset;
        desiredPosition += targetForward * distance;
        desiredPosition += Vector3.up * height;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

        Vector3 lookAtPoint = target.position + targetOffset;
        transform.LookAt(lookAtPoint);
    }

    void OnDrawGizmosSelected()
    {
        if (target != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, target.position + targetOffset);
            Gizmos.DrawWireSphere(target.position + targetOffset, 0.2f);
        }
    }
}
