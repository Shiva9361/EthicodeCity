using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private float dragSpeed = 2f; // Speed of the camera movement
    public float dragRatio = 5; // Ratio of the camera movement speed to the orthographic size
    private Vector3 dragOrigin; // Stores the initial mouse position
    private bool isDragging = false;

    public bool cameraDragEnabled = true;

    void Update()
    {
        HandleCameraDrag();
        HandleZoom();
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0 && cameraDragEnabled)
        {
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - scroll * 2, 2, 7);
            dragSpeed = Camera.main.orthographicSize / dragRatio;
        }
    }
    void HandleCameraDrag()
    {
        if (Input.GetMouseButtonDown(0) && cameraDragEnabled) // Left mouse button pressed
        {
            dragOrigin = Input.mousePosition;
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0)) // Left mouse button released
        {
            isDragging = false;
        }

        if (isDragging && cameraDragEnabled)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 difference = dragOrigin - currentMousePosition;

            // Move the camera based on the difference
            Vector3 move = new Vector3(difference.x, 0, difference.y) * dragSpeed * Time.deltaTime;
            transform.Translate(move, Space.World);

            // Update drag origin
            dragOrigin = currentMousePosition;
        }
    }
}
