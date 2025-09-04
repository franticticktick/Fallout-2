using UnityEngine;

public class UserCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float smoothSpeed = 0.125f;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private Quaternion targetRotation;

    [SerializeField]
    private float rotationSpeed = 90f;

    [SerializeField]
    private float edgePanSpeed = 25f;

    [SerializeField]
    private int panBorder = 25;

    [SerializeField]
    private float zoomSpeed = 5f;

    [SerializeField]
    private float minOrthographicSize = 1f;

    [SerializeField]
    private float maxOrthographicSize = 18f;

    private Camera cam;


    public Vector3 minBounds = new Vector3(-10f, 1f, -10f);
    public Vector3 maxBounds = new Vector3(10f, 10f, 10f);

    private void Start()
    {
        cam = GetComponent<Camera>();

        if (cam == null)
        {
            Debug.LogError("OrthographicZoom script requires a Camera component on the same GameObject.");
            enabled = false;
        }
        else if (!cam.orthographic)
        {
            Debug.LogWarning("Camera is not set to Orthographic. Zooming will not work as expected.");
        }

        Vector3 desiredPosition = target.position + offset;
        transform.position = desiredPosition;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.C))
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        Vector3 newPosition = transform.position;

        // Edge Panning
        if (Input.mousePosition.x < panBorder)
        {
            newPosition.x += edgePanSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.x >= Screen.width - panBorder)
        {
            newPosition.x -= edgePanSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y < panBorder)
        {
            newPosition.z += edgePanSpeed * Time.deltaTime; // Assuming Z is forward
        }
        else if (Input.mousePosition.y >= Screen.height - panBorder)
        {
            newPosition.z -= edgePanSpeed * Time.deltaTime; // Assuming Z is forward
        }

        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);
        newPosition.z = Mathf.Clamp(newPosition.z, minBounds.z, maxBounds.z);

        transform.position = newPosition;

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            cam.orthographicSize -= scroll * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minOrthographicSize, maxOrthographicSize);
        }
    }
}
