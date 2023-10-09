using System.Collections.Generic;
using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour {
    [SerializeField] private List<Item> dropdownItemList;
    [SerializeField] private Transform itemWorldPrefab;

    public Item GetRandomItem() {
        return dropdownItemList[Random.Range(0, dropdownItemList.Count)];
    }

    public void SpawnRandomItem() {
        ItemWorld.SpawnItemWorld(transform.position, GetRandomItem(), itemWorldPrefab);
    }
    
}
