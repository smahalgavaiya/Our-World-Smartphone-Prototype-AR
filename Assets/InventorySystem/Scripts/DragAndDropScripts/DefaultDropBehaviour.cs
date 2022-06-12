using UnityEngine;
using UniversalInventorySystem;
public class DefaultDropBehaviour : DropBehaviour
{
    public GameObject droppedItemObj;

    public override void OnDropItem(object sender, InventoryHandler.DropItemEventArgs e)
    {
        switch (e.item.itemName)
        {
            case "Portal":
                Instantiate(droppedItemObj, e.positionDropped + (Vector3.forward * 5), Quaternion.identity);
                break;
            case "shrink":
                Debug.Log("Todo Add shrink functionality");
                break;
        }
        e.inv.RemoveItemInSlot(e.slot, e.amount);

    }
}

