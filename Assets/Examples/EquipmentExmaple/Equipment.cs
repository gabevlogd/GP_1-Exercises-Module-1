using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Equipment : Singleton<Equipment>
{
    [SerializeField]
    private ArmorSet _armorSet;

    private Dictionary<string, BodyArmor> _equipment;

    public static Action<BodyArmor> OnArmorEquiped;
    public static Action<BodyArmor> OnArmorUnequiped;
    public static Action<List<BodyArmor>> OnEquipementInitialized;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    public void Init()
    {
        _equipment = new Dictionary<string, BodyArmor>();
        foreach (BodyArmor armor in _armorSet._armorSet)
        {
            if (_equipment.ContainsKey(armor.Name))
            {
                Debug.LogWarning("Armor name already in use, please check the armor set entries");
                continue;
            }

            _equipment.Add(armor.Name, armor);
        }

        List<BodyArmor> temp = new List<BodyArmor>();
        foreach (KeyValuePair<string, BodyArmor> keyValuePair in _equipment)
        {
            temp.Add(keyValuePair.Value);
        }
        OnEquipementInitialized?.Invoke(temp);
    }

    public static bool Equip(BodyArmor bodyArmor)
    {
        if (This._equipment.ContainsKey(bodyArmor.Name))
        {
            Debug.LogWarning(bodyArmor.Name + " already equiped");
            return false;
        }

        This._equipment.Add(bodyArmor.Name, bodyArmor);
        OnArmorEquiped?.Invoke(bodyArmor);
        return true;
    }

    public static bool Unequip(string name)
    {
        if (!This._equipment.ContainsKey(name))
        {
            Debug.LogWarning(name + " not found");
            return false;
        }

        BodyArmor armorToUnequip = This._equipment[name];
        This._equipment.Remove(name);
        OnArmorUnequiped?.Invoke(armorToUnequip);
        return true;
    }

    public static bool TryGetArmor(string name, out BodyArmor bodyArmor)
    {
        return This._equipment.TryGetValue(name, out bodyArmor);
    }
}
