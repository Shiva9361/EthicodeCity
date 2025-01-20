using System;
using System.Collections.Generic;
using UnityEngine;

public class StructureInfoManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public StructureInfo[] buildingStructureInfos;
    public StructureManager structureManager;

    public StructurePrefabWeighted[] structurePrefabWeighted;

    public Dictionary<int, StructureInfo> structureInfoDictionary = new Dictionary<int, StructureInfo>();

    private void Start()
    {
        structurePrefabWeighted = new StructurePrefabWeighted[buildingStructureInfos.Length];
        foreach (StructureInfo structureInfo in buildingStructureInfos)
        {
            structureInfoDictionary.Add(structureInfo.id, structureInfo);
            structurePrefabWeighted[structureInfo.id] = structureInfo.weightedPrefab;
        }
        structureManager.housesPrefabe = structurePrefabWeighted;
    }


}

[Serializable]
public struct StructureInfo
{

    public int id;

    public RenderTexture image;
    public StructurePrefabWeighted weightedPrefab;

    public StructureInfo(RenderTexture image, int id, StructurePrefabWeighted prefabWeighted)
    {
        this.image = image;
        this.id = id;

        weightedPrefab = prefabWeighted;
    }
}
