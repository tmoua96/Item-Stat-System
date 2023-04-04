using ScriptableObjectArchitecture;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI inventoryText;

    [SerializeField]
    private InventoryEventDataGameEvent InventoryChangedEvent;

    private void OnEnable()
    {
        InventoryChangedEvent.AddListener(OnInventoryChanged);
    }

    private void OnInventoryChanged(InventoryEventData data)
    {
        string action = data.WasItemAdded ? "Added" : "Removed";
        string addedMsg = $"{action} {data.ModifiedAmount} {data.ModifiedItem.Data.Name} to inventory. Current amount: {data.ModifiedItem.Amount}";

        inventoryText.text = addedMsg;
    }

    private void OnDisable()
    {
        InventoryChangedEvent.RemoveListener(OnInventoryChanged);
    }
}
