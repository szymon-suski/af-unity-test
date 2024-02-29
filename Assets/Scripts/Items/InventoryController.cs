namespace AFSInterview.Items
{
	using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

	public class InventoryController : MonoBehaviour
	{
		[Header("Settings")]
		[SerializeField] private List<Item> items;
		[SerializeField] private int money;

		[Header("Reference")]
		[SerializeField] private TextMeshProUGUI moneyText;

		public int ItemsCount => items.Count;

        private void Start()
        {
			UpdateMoneyText();
        }

        public void SellAllItemsUpToValue(int maxValue)
		{
			var orgMoney = money;

			for (var i = items.Count - 1; i >= 0; i--)
			{
				var itemValue = items[i].Value;
				if (itemValue > maxValue)
					continue;
				
				money += itemValue;
				items.RemoveAt(i);
			}

			// Update money text only if money changed.
			if (orgMoney == money) return;
            UpdateMoneyText();
        }

		public void AddItem(Item item)
		{
			items.Add(item);
		}

		private void UpdateMoneyText()
		{
            moneyText.text = "Money: " + money;
        }
	}
}