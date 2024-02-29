﻿namespace AFSInterview.Items
{
	using UnityEngine;

	public class ItemsManager : MonoBehaviour
	{
		[Header("Settings")]
        [SerializeField] private int itemSellMaxValue;
        [SerializeField] private float itemSpawnInterval;

        [Header("References")]
		[SerializeField] private InventoryController inventoryController;
		[SerializeField] private Transform itemSpawnParent;
		[SerializeField] private GameObject itemPrefab;
		[SerializeField] private BoxCollider itemSpawnArea;
		[SerializeField] private Camera raycastCamera;

		private int ItemsCount => inventoryController.ItemsCount;

		private float nextItemSpawnTime;
		private int itemLayerMaskId;

        private void Start()
        {
            itemLayerMaskId = LayerMask.GetMask("Item");
        }

        private void Update()
		{
			if (Time.time >= nextItemSpawnTime)
				SpawnNewItem();
			
			if (Input.GetMouseButtonDown(0))
				TryPickUpItem();
			
			if (Input.GetKeyDown(KeyCode.Space))
				inventoryController.SellAllItemsUpToValue(itemSellMaxValue);
		}

		private void SpawnNewItem()
		{
			nextItemSpawnTime = Time.time + itemSpawnInterval;
			
			var spawnAreaBounds = itemSpawnArea.bounds;
			var position = new Vector3(
				Random.Range(spawnAreaBounds.min.x, spawnAreaBounds.max.x),
				0f,
				Random.Range(spawnAreaBounds.min.z, spawnAreaBounds.max.z)
			);
			
			Instantiate(itemPrefab, position, Quaternion.identity, itemSpawnParent);
		}

		private void TryPickUpItem()
		{
			var ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
			
			if (!Physics.Raycast(ray, out var hit, 100f, itemLayerMaskId) || !hit.collider.TryGetComponent<IItemHolder>(out var itemHolder))
				return;
			
			var item = itemHolder.GetItem(true);
            inventoryController.AddItem(item);

            Debug.Log("Picked up " + item.Name + " with value of " + item.Value + " and now have " + ItemsCount + " items");
		}
	}
}