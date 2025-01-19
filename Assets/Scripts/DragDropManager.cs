using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDropManager : MonoBehaviour
{
    private StructurePrefabWeighted[] prefabs;

    private StructurePrefabWH[] bigStructuresPrefabs;
    public Button[] buttons;

    public Button[] bigStructureButtons;

    public CameraManager cameraManager;

    public PlacementManager placementManager;

    public StructureManager structureManager;
    bool isDragging = false;

    bool isBigStructure = false;

    int currentPrefabIndex = 0;

    public InputManager inputManager;
    void Start()
    {
        bigStructuresPrefabs = structureManager.bigStructuresPrefabs;
        prefabs = structureManager.housesPrefabe;
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            EventTrigger trigger = buttons[index].gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((data) =>
            {
                isDragging = true;
                Vector3Int? pos = inputManager.RaycastGround();

                if (pos != null)
                {
                    currentPrefabIndex = index;
                    placementManager.PlaceCurrentSelection(pos.Value, prefabs[index].scale, prefabs[index].prefab, CellType.Structure);
                }
            });
            trigger.triggers.Add(entry);
        }
        for (int i = 0; i < bigStructureButtons.Length; i++)
        {
            int index = i;
            EventTrigger trigger = bigStructureButtons[index].gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((data) =>
            {
                isDragging = true;
                Vector3Int? pos = inputManager.RaycastGround();

                if (pos != null)
                {
                    currentPrefabIndex = index;
                    isBigStructure = true;
                    placementManager.PlaceCurrentSelection(pos.Value, bigStructuresPrefabs[index].scale, bigStructuresPrefabs[index].prefab, CellType.Structure);
                }
            });
            trigger.triggers.Add(entry);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging && Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            placementManager.RemoveSelectedPrefab();
            Vector3Int? pos = inputManager.RaycastGround();
            // cameraManager.cameraDragEnabled = true;

            if (pos != null && isBigStructure)
            {
                structureManager.PlaceBigStructure(pos.Value, bigStructuresPrefabs[currentPrefabIndex].width, bigStructuresPrefabs[currentPrefabIndex].height, currentPrefabIndex);
                isBigStructure = false;
            }
            else if (pos != null)
            {
                // structureManager.PlaceHouseBuffered(pos.Value, currentPrefabIndex);
                structureManager.PlaceHouseBufferedDelayed(pos.Value, currentPrefabIndex);
            }
        }
        if (isDragging)
        {
            Vector3Int? pos = inputManager.RaycastGround();
            if (pos != null)
            {
                placementManager.ReplaceCurrentSelection(pos.Value);
            }
        }
    }
}
