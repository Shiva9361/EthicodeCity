using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class StructureClickController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int id;

    public bool isBigStructure = false;

    public bool isAi = false;

    public float time;
    public StructureInfoManager structureInfoManager;
    public InventoryManager inventoryManager;

    public PlacementManager placementManager;

    public GameObject detailsPanel;

    public HashSet<Vector3Int> positions;

    private void Update()
    {
        time += Time.deltaTime;
    }

    void OnMouseDown()
    {
        if (!isBigStructure)
        {
            Debug.Log("placed " + structureInfoManager.structureInfoDictionary.TryGetValue(id, out StructureInfo info));
            Debug.Log("ID " + id);
            UpdateDetailsPanel(info.weightedPrefab.weight, info.weightedPrefab.time, info.image);
        }
        else
        {
            Debug.Log("placed multi " + structureInfoManager.multiStructureInfoDictionary.TryGetValue(id, out StructureInfoMulti info));
            Debug.Log("ID " + id);
            UpdateDetailsPanel(info.weightedPrefab.weight, info.weightedPrefab.time, info.image);
        }

        detailsPanel.SetActive(true);
    }

    internal void UpdateDetailsPanel(float cost, float time, RenderTexture image)
    {
        detailsPanel.SetActive(true);
        detailsPanel.transform.Find("Cost").GetComponent<TMP_Text>().text = "$" + cost;
        detailsPanel.transform.Find("Time").GetComponent<TMP_Text>().text = time + "s";
        detailsPanel.transform.Find("Image").GetComponent<RawImage>().texture = image;
        detailsPanel.transform.Find("RemoveButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            Destroy(gameObject);
            inventoryManager.AddMoney(cost);
            detailsPanel.transform.Find("RemoveButton").GetComponent<Button>().onClick.RemoveAllListeners();
            foreach (Vector3Int position in positions)
            {
                placementManager.ClearLocation(position);
            }
            detailsPanel.SetActive(false);
        });


    }
}
