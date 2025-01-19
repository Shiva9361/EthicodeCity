using System;
using System.Collections.Generic;
using UnityEngine;

public class StructureInfoManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public StructureInfo[] buildingStructureInfos;

    public Dictionary<int, StructureInfo> structureInfoDictionary = new Dictionary<int, StructureInfo>();

    private void Start()
    {
        foreach (StructureInfo structureInfo in buildingStructureInfos)
        {
            structureInfoDictionary.Add(structureInfo.id, structureInfo);
        }
    }


}

[Serializable]
public struct StructureInfo
{
    public int cost;
    public int time;

    public int id;

    public RenderTexture image;

    public StructureInfo(int cost, int time, RenderTexture image, int id)
    {
        this.cost = cost;
        this.time = time;
        this.image = image;
        this.id = id;
    }
}
