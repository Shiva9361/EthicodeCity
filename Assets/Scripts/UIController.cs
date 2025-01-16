using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Action OnRoadPlacement, OnSpecialPlacement, OnBigStructurePlacement;

    public Action<int> OnHousePlacement;
    public Button placeRoadButton, placeSpecialButton, placeBigStructureButton;

    public Button[] placeHouseButtons;

    public Color outlineColor;
    List<Button> buttonList;

    private void Start()
    {
        buttonList = new List<Button> { placeRoadButton, placeSpecialButton, placeBigStructureButton };

        placeRoadButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeRoadButton);
            OnRoadPlacement?.Invoke();

        });
        // placeHouseButton.onClick.AddListener(() =>
        // {
        //     ResetButtonColor();
        //     ModifyOutline(placeHouse1Button);
        //     OnHousePlacement?.Invoke();

        // });
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
        placeBigStructureButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeBigStructureButton);
            OnBigStructurePlacement?.Invoke();

        });
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
