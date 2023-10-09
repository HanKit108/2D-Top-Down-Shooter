using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemWorld : MonoBehaviour {
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item, Transform prefab) {
        Transform transform = Instantiate(prefab, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }
    
    [SerializeField] private ItemSO itemSO;
    [SerializeField, Range(1, 100)] private int amount;

    private Item item;
    private SpriteRenderer spriteRenderer;
    private bool isPickUp = false;
    private TextMeshPro text;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<TextMeshPro>();

        if(item == null && itemSO != null) {
            SetItem(new Item{ItemSO = itemSO, Amount = amount});
        }
    }

    public void SetItem(Item item) {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();

        if(item.Amount > 1) {
            text.SetText(item.Amount.ToString());
        } else {
            text.SetText("");
        }
    }

    public Item GetItem() {
        return item;
    }

    private void DestroySelf() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        Inventory inventory = collider.GetComponent<Inventory>();
        if(!isPickUp && inventory != null) {
            isPickUp = true;
            inventory.AddItem(GetItem());
            DestroySelf();
        }
    }
}
