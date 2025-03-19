using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupItem : MonoBehaviour
{
    Inventory inventory;

    List<WorldItemDescription> pickupableItems;

    [SerializeField] InputActionReference pickupItemInput;

    void Awake()
    {
        inventory = GetComponentInParent<Inventory>();
        pickupableItems = new();
    }

    void OnEnable()
    {
        pickupItemInput.action.started += Pickup;
    }

    void OnDisable()
    {
        pickupItemInput.action.started -= Pickup;
    }

    public void Pickup(InputAction.CallbackContext callbackContext){
        if(pickupableItems.IsNullOrEmpty()) return;

        WorldItemDescription closestItem = pickupableItems.OrderBy(
            x => Vector2.Distance(transform.position, x.transform.position)
        ).ToList()[0];

        inventory.AddItem(closestItem.itemInfo);
        pickupableItems.Remove(closestItem);

        Destroy(closestItem.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent(out WorldItemDescription wid)){
            pickupableItems.Add(wid);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.TryGetComponent(out WorldItemDescription wid)){
            pickupableItems.Remove(wid);
        }
    }
}
