using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
	[InspectorName("����")] Weapon,
	[InspectorName("�h��")] Armor,
	[InspectorName("��")] Heal,
	[InspectorName("�")] Trap,
	[InspectorName("�����f��")] ReinforcementMaterial,
	[InspectorName("�����f��")] EvolutionaryMaterial,
	[InspectorName("�d�v")] Important,
}

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData")]
[System.Serializable]
public class ItemData : ScriptableObject
{

	public string FileName;
	public string FileCaption;
	[System.Serializable]
	public class Item
	{
		public int id;
		public string name;
		public string caption;
		public ItemType type;
	}

	public Item[] items;

	public ItemData Clone()
	{
		return Instantiate(this);
	}
}
