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
        //public Map_Object[][] presetData;
        public Floor[] Point;
        public HEIGHT[] Height;

        [System.Serializable]
        public class HEIGHT
        {
            public Map_Object[] Width;
        }

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

    public Map_Object GetMapData(int Pre_Num, int Y_Pos, int X_Pos) 
    {
        //Debug.Log(preset[Pre_Num].Height[Y_Pos].Width[X_Pos]);
        return preset[Pre_Num].Height[Y_Pos].Width[X_Pos];
    }

    public Vector2Int GetLowerLeftPos(int Pre_Num, int Point_Num) 
    { 
        return preset[Pre_Num].Point[Point_Num].LowerLeftPos; 
    }

    public Vector2Int GetUpperRightPos(int Pre_Num, int Point_Num)
    {
        return preset[Pre_Num].Point[Point_Num].UpperRightPos;
    }
}
