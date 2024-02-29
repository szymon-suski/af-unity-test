namespace AFSInterview.Items
{
	using System;
	using UnityEngine;
    using static UnityEditor.Progress;

	[Serializable]
	public class Item
	{
		[SerializeField] protected string name;
		[SerializeField] protected int value;

		public string Name => name;
		public int Value => value;

		public Item(string name, int value)
		{
			this.name = name;
			this.value = value;
		}

		public virtual void Use(InventoryController inventoryController)
		{
            inventoryController.AddItem(this);
            Debug.Log("Picked up " + name + " with value of " + value + " and now have " + inventoryController.ItemsCount + " items");
        }
	}
}