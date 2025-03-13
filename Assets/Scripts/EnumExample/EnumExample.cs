using UnityEngine;

public class EnumExample : MonoBehaviour
{

    

    [SerializeField]
    private ItemType _itemType;

    private void Start()
    {
        switch (_itemType)
        {
            case ItemType.Ammo:
                Debug.Log("Ammo");
                break;
            case ItemType.Key:
                Debug.Log("Key");
                break;
            case ItemType.Score:
                Debug.Log("Score");
                break;
            default:
                Debug.Log("Default");
                break;
        }
    }

}

public enum ItemType
{
    Ammo,
    Key,
    Score
}

public enum KeyType
{
    red,
    green,
    blue
}


