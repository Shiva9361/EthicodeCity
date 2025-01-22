using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    private float dragSpeed = 2f; // Speed of the camera movement
    public float dragRatio = 5; // Ratio of the camera movement speed to the orthographic size
    private Vector3 dragOrigin; // Stores the initial mouse position
    private bool isDragging = false;

    public GameObject parent;

    public Button[] disableButtons;

    public Button[] enableButtons;

    [SerializeField]
    public bool cameraDragEnabled = true;

    void Start()
    {
    
    }
    void Update()
    {
        
    }


}
