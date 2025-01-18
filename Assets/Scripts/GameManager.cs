using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraManager cameraManager;
    public RoadManager roadManager;
    public InputManager inputManager;

    public UIController uiController;

    public StructureManager structureManager;

    private void Start()
    {
        uiController.OnRoadPlacement += RoadPlacementHandler;
        uiController.OnHousePlacement += HousePlacementHandler;
        uiController.OnClear += ClearInputActions;
        uiController.OnSpecialPlacement += SpecialPlacementHandler;
        uiController.OnBigStructurePlacement += BigStructurePlacementHandler;

    }

    private void BigStructurePlacementHandler()
    {
        ClearInputActions();
        cameraManager.cameraDragEnabled = false;
        inputManager.OnMouseClick += structureManager.PlaceBigStructure;
    }

    private void SpecialPlacementHandler()
    {
        ClearInputActions();
        cameraManager.cameraDragEnabled = false;
        inputManager.OnMouseClick += structureManager.PlaceSpecial;
    }

    private void HousePlacementHandler(int houseNum)
    {
        ClearInputActions();
        cameraManager.cameraDragEnabled = false;
        // inputManager.OnMouseUpWithLocation += (location) => structureManager.PlaceHouseBuffered(location, houseNum);
        inputManager.OnMouseUpWithLocation += (location) => structureManager.PlaceHouseBufferedDelayed(location, houseNum);
    }

    private void RoadPlacementHandler()
    {
        ClearInputActions();
        cameraManager.cameraDragEnabled = false;
        inputManager.OnMouseClick += roadManager.PlaceRoad;
        inputManager.OnMouseHold += roadManager.PlaceRoad;
        inputManager.OnMouseUp += roadManager.FinishPlacingRoad;
    }

    public void ClearInputActions()
    {
        inputManager.OnMouseClick = null;
        inputManager.OnMouseHold = null;
        inputManager.OnMouseUp = null;
        inputManager.OnMouseUpWithLocation = null;
        cameraManager.cameraDragEnabled = true;

    }

}
