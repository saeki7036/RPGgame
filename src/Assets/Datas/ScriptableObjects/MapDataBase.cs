using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Map_Object
{
    EndWall = 0,
    Spase = 1,
    Water = 2,
    Wall = 3,
    Floor = 4,
    Road = 5,
}
[CreateAssetMenu(fileName = "MapDataBase", menuName = "OriginalScriptableObjects/MapDataBase")]

[System.Serializable]

public class MapDataBase : ScriptableObject
{
    public Preset[] preset;

    [System.Serializable]
    public class Preset
    {
        public Map_Object[,] presetData;
        public Floor[] Point;

        [System.Serializable]
        public class Floor
        {
            public Vector2Int LowerLeftPos;
            public Vector2Int UpperRightPos;
        }
    }
    public MapDataBase Clone()
    {
        return Instantiate(this);
    }
}
