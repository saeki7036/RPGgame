using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
	[InspectorName("ïêäÌ")] Weapon,
	[InspectorName("ñhãÔ")] Armor,
	[InspectorName("âÒïú")] Heal,
	[InspectorName("„©")] Trap,
	[InspectorName("çáê¨ëfçﬁ")] ReinforcementMaterial,
	[InspectorName("ã≠âªëfçﬁ")] EvolutionaryMaterial,
	[InspectorName("èdóv")] Important,
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
