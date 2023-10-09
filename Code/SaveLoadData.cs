using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class SavedData {
    public float CurrentHealth;
    public float x, y;
    public List<string> IdList = new List<string>();
    public List<int> AmountList = new List<int>();

}

public class SaveLoadData: MonoBehaviour {
    private PlayerInstance player;
    private Inventory inventory;
    private UnitHealth playerHealth;
    [SerializeField] private List<ItemSO> itemSOList;

    SavedData data = new SavedData();
    
    public void SaveData() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SavedData.dat");
        SavedData data = new SavedData();

        foreach(Item item in inventory.GetItemList()) {
            if(item != null) {
                data.IdList.Add(item.ItemSO.id);
                data.AmountList.Add(item.Amount);
            }
        }

        data.CurrentHealth = playerHealth.GetCurrentHealth();
        
        data.x = player.transform.position.x;
        data.y = player.transform.position.y;
        bf.Serialize(file, data);
        file.Close();
    }

    public bool LoadData() {
        if(File.Exists(Application.persistentDataPath + "/SavedData.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SavedData.dat", FileMode.Open);
            data = (SavedData)bf.Deserialize(file);
            file.Close();
            return true;
        } else {
            Debug.Log("No saved data to load");
        }
        return false;
    }

    private void Start() {
        player = PlayerInstance.Instance;
        inventory = player.gameObject.GetComponent<Inventory>();
        playerHealth = player.gameObject.GetComponent<UnitHealth>();
        
        if(LoadData()) {
            PlayerSetUp();
        }
    }

    private void PlayerSetUp() {
        player.transform.position = new Vector3(data.x, data.y, 0f);
        playerHealth.SetCurrentHealth(data.CurrentHealth);

        inventory.ClearInventory();

        int i = 0;
        foreach(string id in data.IdList) {
            foreach(ItemSO itemSO in itemSOList) {
                if(itemSO.id == id) {
                    Item item = new Item {ItemSO = itemSO, Amount = data.AmountList[i]};
                    inventory.AddItem(item);
                }
            }
            i++;
        }
    }

    private void OnApplicationQuit() {
        SaveData();
    }
}
