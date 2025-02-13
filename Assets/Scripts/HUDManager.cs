using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public GameObject _currentHp;

    private void OnEnable()
    {
        HealthManager.OnHPChanged += UpdateCurrentHP;
    }

    private void OnDisable()
    {
        HealthManager.OnHPChanged -= UpdateCurrentHP;
    }


    public void UpdateCurrentHP(float newValue)
    {
        _currentHp.GetComponent<TextMeshPro>().text = newValue.ToString();
    }
}
