using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    private ItemType itemType;

    public static event Action<ItemType> OnCollected;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (OnCollected != null)
            {
                OnCollected(itemType);
            }
        }
    }
}
