using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Item_Type
{
    [InspectorName("����")] Weapon,
    [InspectorName("�h��")] Armor,
    [InspectorName("��")] Herbal,
    [InspectorName("�|�[�V����")] Potion,
}

[CreateAssetMenu(fileName = "ItemDataBase", menuName = "ScriptableObjects/ItemDataBase")]

[System.Serializable]
public class ItemDataBase : ScriptableObject
{
    public string FileName;
    public string FileCaption;

    public Items[] items;
    
    [System.Serializable]
    public class Items 
    {
        public Item_Type type;
        public Category category;

        [System.Serializable]
        public class Category
        {
            public GameObject Object;

            public Menber[] ID;

            [System.Serializable]
            public class Menber
            {
                public string Name;
                public int Efect1; 
                public int Efect2; 
                public int Efect3;
                public string caption;
            }
        }
    }
    public ItemDataBase Clone()
    {
        return Instantiate(this);
    }
}
