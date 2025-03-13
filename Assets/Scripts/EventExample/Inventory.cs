using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int Ammo;


    private void OnEnable()
    {
        Collectible.OnCollected += AddItem;
    }

    private void OnDisable()
    {
        Collectible.OnCollected -= AddItem;
    }

    private void AddItem(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Ammo:
                Ammo++;
                Debug.Log("Ammo: " + Ammo);
                break;
            case ItemType.Key:
                Debug.Log("Key");
                break;
            case ItemType.Score:
                Debug.Log("Score");
                break;
            default:
                break;
        }
    }
}
