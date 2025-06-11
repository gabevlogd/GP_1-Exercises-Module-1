using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmorSet", menuName = "Scriptable Objects/ArmorSet")]
public class ArmorSet : ScriptableObject
{
    public List<BodyArmor> _armorSet;
}
