using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PickUp : MonoBehaviour//, IDataPersistence
{
//    [ContextMenu("Generate guid for id")]
//     private void GenerateGuid()
//     {
//         id = System.Guid.NewGuid().ToString();
//     }
    private Inventory inventory;


    public GameObject itemButton;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            for ( int i= 0 ; i <inventory.slots.Length;i++)
            {
                if(inventory.isFull[i]== false)
                {
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform,false);
                    Destroy(gameObject);
                    break;
                }
            }
            AudioManager.Instance.PlaySFX("Pick Up Item");
        }
    }
    // public void LoadData(GameData data)
    // {
    //     data.itemButton.TryGetValue(id, player);
    //     if(player)
    //     {
    //         itemButton.gameObject.SetActive(false);
    //     }
    // }

    // public void SaveData(ref GameData data)
    // {
    //     if (data.itemButton.ContainsKey(id))
    //     {
    //         data.itemButton.Remove(id);
    //     }
    //     data.itemButton.Add(id, player);
    // }
}
