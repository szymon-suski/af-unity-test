namespace AFSInterview.Items
{
    using System.Collections.Generic;
    using UnityEngine;

	public class ItemsManager : MonoBehaviour
	{
		[Header("Settings")]
        [SerializeField] private int itemSellMaxValue;
        [SerializeField] private float itemSpawnInterval;
		[SerializeField] private List<GameObject> itemsPrefabs;

        [Header("References")]
		[SerializeField] private InventoryController inventoryController;
		[SerializeField] private Transform itemSpawnParent;
		[SerializeField] private BoxCollider itemSpawnArea;
		[SerializeField] private Camera raycastCamera;

		private float nextItemSpawnTime;
		private int itemLayerMaskId;

        private void Start()
        {
            itemLayerMaskId = LayerMask.GetMask("Item");
        }

        private void Update()
		{
			if (Time.time >= nextItemSpawnTime && itemsPrefabs.Count > 0)
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

			var prefab = itemsPrefabs[Random.Range(0, itemsPrefabs.Count)];

            Instantiate(prefab, position, Quaternion.identity, itemSpawnParent);
		}

		private void TryPickUpItem()
		{
			var ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
			
			if (!Physics.Raycast(ray, out var hit, 100f, itemLayerMaskId) || !hit.collider.TryGetComponent<IItemHolder>(out var itemHolder))
				return;
			
			var item = itemHolder.GetItem(true);
			item.Use(inventoryController);
		}
	}
}