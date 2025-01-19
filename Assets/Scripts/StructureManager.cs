using SVS;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using TMPro;

public class StructureManager : MonoBehaviour
{
    public StructurePrefabWeighted[] housesPrefabe, specialPrefabs;

    public StructurePrefabWH[] bigStructuresPrefabs;
    public PlacementManager placementManager;

    public InventoryManager inventoryManager;

    public TMP_Text[] costs;

    public TMP_Text[] times;

    private float[] houseWeights, specialWeights, bigStructureWeights;

    private void Start()
    {
        houseWeights = housesPrefabe.Select(prefabStats => prefabStats.weight).ToArray();
        specialWeights = specialPrefabs.Select(prefabStats => prefabStats.weight).ToArray();
        bigStructureWeights = bigStructuresPrefabs.Select(prefabStats => prefabStats.weight).ToArray();
        for (int i = 0; i < costs.Length; i++)
        {
            costs[i].text = "$" + housesPrefabe[i].weight.ToString();
        }
        for (int i = 0; i < times.Length; i++)
        {
            times[i].text = housesPrefabe[i].time.ToString() + "s";
        }
    }

    // public void PlaceHouse(Vector3Int position)
    // {
    //     if (CheckPositionBeforePlacement(position))
    //     {
    //         int randomIndex = GetRandomWeightedIndex(houseWeights);
    //         placementManager.PlaceObjectOnTheMap(position, housesPrefabe[randomIndex].scale, housesPrefabe[randomIndex].prefab, CellType.Structure);
    //         AudioPlayer.instance.PlayPlacementSound();
    //     }
    // }



    internal void PlaceHouseBufferedDelayed(Vector3Int position, int houseNum)
    {
        if (CheckPositionBeforePlacement(position) && inventoryManager.CanBuy(housesPrefabe[houseNum].weight))
        {
            StartCoroutine(DelayedPlacement(position, houseNum));
        }
        else if (!inventoryManager.CanBuy(housesPrefabe[houseNum].weight))
        {
            Debug.Log("Not enough money");
        }
    }

    private IEnumerator DelayedPlacement(Vector3Int position, int houseNum)
    {
        float placementTime = housesPrefabe[houseNum].time;
        GameObject gameObject = placementManager.CreateANewStructureModelGameObject(position, housesPrefabe[houseNum].scale, housesPrefabe[houseNum].prefab, CellType.Structure);
        Renderer renderer = gameObject.GetComponentsInChildren<Renderer>()[0];
        Material oldMaterial = renderer.material;
        Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        material.color = Color.Lerp(Color.green, Color.red, housesPrefabe[houseNum].aiPercentage); ;

        for (int i = 0; i < 2 * placementTime; i++)
        {
            if (i % 2 == 1)
            {
                renderer.material = oldMaterial;
            }
            else
            {
                renderer.material = material;
            }
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(gameObject);
        placementManager.PlaceObjectOnTheMap(position, housesPrefabe[houseNum].scale, housesPrefabe[houseNum].prefab, CellType.Structure);
        inventoryManager.Buy(housesPrefabe[houseNum].weight);
        AudioPlayer.instance.PlayPlacementSound();
        Debug.Log("Placement completed.");
    }



    internal void PlaceHouseBuffered(Vector3Int position, int houseNum)
    {
        if (CheckPositionBeforePlacement(position) && inventoryManager.CanBuy(housesPrefabe[houseNum].weight))
        {

            placementManager.PlaceObjectOnTheMap(position, housesPrefabe[houseNum].scale, housesPrefabe[houseNum].prefab, CellType.Structure);
            inventoryManager.Buy(housesPrefabe[houseNum].weight);
            AudioPlayer.instance.PlayPlacementSound();
        }
        else if (inventoryManager.CanBuy(housesPrefabe[houseNum].weight) == false)
        {
            Debug.Log("Not enough money");
        }
    }

    internal void PlaceBigStructure(Vector3Int position, int width, int height, int bigStructureIndex)
    {
        if (CheckBigStructure(position, width, height))
        {
            Debug.Log("Placing big structure at" + position);
            placementManager.PlaceObjectOnTheMap(position, bigStructuresPrefabs[bigStructureIndex].scale, bigStructuresPrefabs[bigStructureIndex].prefab, CellType.Structure, width, height);
            AudioPlayer.instance.PlayPlacementSound();
        }
    }

    private bool CheckBigStructure(Vector3Int position, int width, int height)
    {
        bool nearRoad = false;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var newPosition = position + new Vector3Int(x, 0, z);

                if (DefaultCheck(newPosition) == false)
                {
                    return false;
                }
                if (nearRoad == false)
                {
                    nearRoad = RoadCheck(newPosition);
                }
            }
        }
        return nearRoad;
    }

    public void PlaceSpecial(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            int randomIndex = GetRandomWeightedIndex(specialWeights);
            placementManager.PlaceObjectOnTheMap(position, specialPrefabs[randomIndex].scale, specialPrefabs[randomIndex].prefab, CellType.Structure);
            AudioPlayer.instance.PlayPlacementSound();
        }
    }

    private int GetRandomWeightedIndex(float[] weights)
    {
        float sum = 0f;
        for (int i = 0; i < weights.Length; i++)
        {
            sum += weights[i];
        }

        float randomValue = UnityEngine.Random.Range(0, sum);
        float tempSum = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            //0->weihg[0] weight[0]->weight[1]
            if (randomValue >= tempSum && randomValue < tempSum + weights[i])
            {
                return i;
            }
            tempSum += weights[i];
        }
        return 0;
    }

    private bool CheckPositionBeforePlacement(Vector3Int position)
    {
        if (DefaultCheck(position) == false)
        {
            return false;
        }

        if (RoadCheck(position) == false)
            return false;

        return true;
    }

    private bool RoadCheck(Vector3Int position)
    {
        if (placementManager.GetNeighboursOfTypeFor(position, CellType.Road).Count <= 0)
        {
            Debug.Log("Must be placed near a road");
            return false;
        }
        return true;
    }

    private bool DefaultCheck(Vector3Int position)
    {
        if (placementManager.CheckIfPositionInBound(position) == false)
        {
            Debug.Log("This position is out of bounds");
            return false;
        }
        if (placementManager.CheckIfPositionIsFree(position) == false)
        {
            Debug.Log("This position is not EMPTY");
            return false;
        }
        return true;
    }
}

[Serializable]
public struct StructurePrefabWeighted
{
    public GameObject prefab;
    [Range(1000, 10_000)]
    public float weight;
    public Vector3 scale;

    public float time;

    [Range(0, 1)]
    public float aiPercentage;

    public StructurePrefabWeighted(GameObject prefab, float weight, float time, float aiPercentage)
    {
        this.prefab = prefab;
        this.weight = weight;
        this.time = time;
        this.aiPercentage = aiPercentage;
        scale = new Vector3(1, 1, 1);

    }
}


[Serializable]
public struct StructurePrefabWH
{
    public GameObject prefab;
    [Range(1000, 10_000)]
    public float weight;
    public Vector3 scale;

    public int width;
    public int height;

    public StructurePrefabWH(GameObject prefab, float weight, int width, int height)
    {
        this.prefab = prefab;
        this.weight = weight;
        this.width = width;
        this.height = height;
        scale = new Vector3(1, 1, 1);

    }
}
