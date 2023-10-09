using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour {
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemSlotTemplate;

    [SerializeField] private Button openBackpackButton, closeBackpackButton;
    private bool isOpenBackpack = false;
    private Inventory inventory;


    public void SetInventory(Inventory inventory) {
        this.inventory = inventory;
        RefreshInventoryDisplay();
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        openBackpackButton.onClick.AddListener(() => OnOpenBackpack());
        closeBackpackButton.onClick.AddListener(() => OnCloseBackpack());
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e) {
        RefreshInventoryDisplay();
    }

    private void OnOpenBackpack() {
        isOpenBackpack = true;
        RefreshInventoryDisplay();
    }
    private void OnCloseBackpack() {
        isOpenBackpack = false;
    }

    private void RefreshInventoryDisplay() {
        if(isOpenBackpack) {
            foreach(Transform child in itemSlotContainer) {
                if(child == itemSlotTemplate) continue;
                Destroy(child.gameObject);
            }

            foreach(Item item in inventory.GetItemList()) {
                RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
                itemSlotRectTransform.gameObject.SetActive(true);
                Image image = itemSlotRectTransform.GetComponentInChildren<Image>();
                image.sprite = item.GetSprite();

                itemSlotRectTransform.GetComponent<DeleteItemButton>().GetButton().onClick.AddListener(() => DeleteButtonClicked(item));

                TextMeshProUGUI text = itemSlotRectTransform.GetComponentInChildren<TextMeshProUGUI>();
                if(item.Amount > 1) {
                    text.SetText(item.Amount.ToString());
                } else {
                    text.SetText("");
                }
            }
        }
    }

    private void DeleteButtonClicked(Item item) {
        inventory.RemoveItem(item);
    }
}
