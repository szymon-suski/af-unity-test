namespace AFSInterview.Items
{
    using System;
    using UnityEngine;
    using static UnityEditor.Progress;

    [Serializable]
    public class ConsumableItem : Item
    {
        [Header("Consumable Settings")]
        [SerializeField]
        [Tooltip("If true will add money to inventory, If false will add different item to inventory")] 
        private bool shouldAddMoney = true;

        [Header("Money Consumable")]
        [SerializeField] private int moneyToAdd;

        [Header("Item Consumable")]
        [SerializeField] private Item differentItem;

        public ConsumableItem(string name, int value, bool shouldAddMoney, int moneyToAdd, Item differentItem) : base(name, value)
        {
            this.shouldAddMoney = shouldAddMoney;
            this.moneyToAdd = moneyToAdd;
            this.differentItem = differentItem;
        }

        public override void Use(InventoryController inventoryController)
        {
            if (shouldAddMoney)
            {
                inventoryController.AddMoney(moneyToAdd);
                Debug.Log($"Picked up {name} value of {moneyToAdd} is added to inventory, no new items. You have {inventoryController.ItemsCount} items");
            }
            else
            {
                if (differentItem == null) return;
                inventoryController.AddItem(differentItem);
                Debug.Log($"Picked up {name}. Added to inventory item {differentItem.Name} with value of {differentItem.Value}. Now you have {inventoryController.ItemsCount} items");
            }
        }
    }
}
