using System;
using System.Xml;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable{
    public bool IsOpened{ get; private set; }
    public string ChestID{ get; private set; }
    public GameObject itemPrefab; 
    public Sprite openedSprite;

    void Start(){
        ChestID ??= GlobalHelper.GenerateUUID(gameObject);
    }

    public bool CanInteract(){
        return !IsOpened;
    }

    public void Interact(){
        if(!CanInteract()) return;
        OpenChest();
    }

    private void OpenChest(){
        SetOpened(true);

        if(itemPrefab){
            GameObject droppedItem = Instantiate(itemPrefab, transform.position + Vector3.down, Quaternion.identity);
            //droppedItem.GetComponent<BounceEffect>().StartBounce();
        }
    }

    private void SetOpened(bool opened){
        if(IsOpened = opened){
            GetComponent<SpriteRenderer>().sprite = openedSprite;
        }
    }
}
