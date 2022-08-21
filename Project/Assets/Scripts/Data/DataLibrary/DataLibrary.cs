using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-2)]
public class DataLibrary : Singleton<DataLibrary>
{
    public Getter<BuildingMaterial> BuildingMaterials { get; private set; }
    public Getter<Sound> Sounds { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Sounds = new Getter<Sound>(Resources.LoadAll<Sound>("Sounds"));
        BuildingMaterials = new Getter<BuildingMaterial>(Resources.LoadAll<BuildingMaterial>("BuildingMaterials"));
    }
}
