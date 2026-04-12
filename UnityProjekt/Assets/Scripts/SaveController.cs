using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour{
    private string saveLocation;
    private InventoryController inventoryController;

    void Start(){
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        inventoryController = FindAnyObjectByType<InventoryController>();
        LoadGame();
    }

    public void SaveGame(){
        SaveData saveData = new SaveData{
            playerPositon = GameObject.FindGameObjectWithTag("Player").transform.position,
            inventorySaveData = inventoryController.GetInventoryItems()
        };


        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
        Debug.Log($"Saved to {saveLocation}");
    }

    public void LoadGame(){
        if(File.Exists(saveLocation)){
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPositon;
            inventoryController.SetInventoryItems(saveData.inventorySaveData);
        }else{
            SaveGame();
        }
    }
}
