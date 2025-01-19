using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Action OnRoadPlacement, OnSpecialPlacement, OnClear;

    public Action<int> OnHousePlacement;
    public StructureManager structureManager;
    public Action<int, int, int> OnBigStructurePlacement;
    public Button placeRoadButton, placeSpecialButton, placeHouseButton;

    public Button[] placeHouseButtons, placeBigStructureButtons;

    public Color outlineColor;
    List<Button> buttonList;

    private void Start()
    {
        buttonList = new List<Button> { placeRoadButton, placeSpecialButton, };

        placeRoadButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeRoadButton);
            OnRoadPlacement?.Invoke();

        });

        placeHouseButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            OnClear?.Invoke();

        });
        for (int i = 0; i < placeHouseButtons.Length; i++)
        {
            int index = i;
            placeHouseButtons[index].onClick.AddListener(() =>
            {
                ResetButtonColor();
                ModifyOutline(placeHouseButtons[index]);
                OnHousePlacement?.Invoke(index);
            });
        }

        placeSpecialButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeSpecialButton);
            OnSpecialPlacement?.Invoke();

        });
        for (int i = 0; i < placeBigStructureButtons.Length; i++)
        {
            int index = i;
            placeBigStructureButtons[index].onClick.AddListener(() =>
            {
                ResetButtonColor();
                Debug.Log(index);
                ModifyOutline(placeBigStructureButtons[index]);
                OnBigStructurePlacement?.Invoke(index, structureManager.bigStructuresPrefabs[index].width, structureManager.bigStructuresPrefabs[index].height);

            });
        }
    }

    private void ModifyOutline(Button button)
    {
        var outline = button.GetComponent<Outline>();
        outline.effectColor = outlineColor;
        outline.enabled = true;
    }

    private void ResetButtonColor()
    {
        foreach (Button button in buttonList)
        {
            button.GetComponent<Outline>().enabled = false;
        }
    }
}
