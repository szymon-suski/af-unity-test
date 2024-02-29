namespace AFSInterview.Items
{
    using UnityEngine;

    public class ConsumableItemPresenter : MonoBehaviour, IItemHolder
    {
        [SerializeField] private ConsumableItem consumableItem;

        public Item GetItem(bool disposeHolder)
        {
            if (disposeHolder)
                Destroy(gameObject);

            return consumableItem;
        }
    }
}
