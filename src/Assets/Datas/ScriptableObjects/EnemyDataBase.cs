using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataBase", menuName = "OriginalScriptableObjects/EnemyDataBase")]

[System.Serializable]
public class EnemyDataBase : ScriptableObject
{
    public string FileName;
    public string FileCaption;

    public Menber[] ID;

    [System.Serializable]
    public class Menber
    {
        public GameObject Object;

        public string Name;
        public int max_hp;

        public int atk;
        public int def;
        public int luk;
        public int agi;

        public int exp;
        public string caption;
    }
    public EnemyDataBase Clone()
    {
        return Instantiate(this);
    }
}
