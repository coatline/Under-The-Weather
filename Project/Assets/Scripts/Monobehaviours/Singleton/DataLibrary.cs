using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class DataLibrary : Singleton<DataLibrary>
{
    public Getter<DisasterType> Disasters { get; private set; }
    public Getter<SoundType> Sounds { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Disasters = new Getter<DisasterType>(Resources.LoadAll<DisasterType>("Disasters"));
        Sounds = new Getter<SoundType>(Resources.LoadAll<SoundType>("Sounds"));
    }
}

public class Getter<T>
{
    readonly Dictionary<string, T> nameToItem;
    public readonly T[] UnsortedArray;

    public readonly int Length;

    public Getter(T[] ts)
    {
        nameToItem = new Dictionary<string, T>();

        UnsortedArray = ts;
        Length = ts.Length;

        Initialize(ts);
    }

    protected virtual void Initialize(T[] ts)
    {
        for (int i = 0; i < ts.Length; i++)
        {
            if (ts[i] == null) { Debug.Log("Null Data!"); continue; }
            nameToItem.Add((ts[i] as Object).name, ts[i]);
        }
    }

    public T GetRandom() => UnsortedArray[Random.Range(0, UnsortedArray.Length)];

    public T this[string n]
    {
        get
        {
            nameToItem.TryGetValue(n, out T j);
            if (j == null) { Debug.LogError($"Couldn't get {n}"); }
            return j;
        }
    }

    public T this[int n] => UnsortedArray[n];
}