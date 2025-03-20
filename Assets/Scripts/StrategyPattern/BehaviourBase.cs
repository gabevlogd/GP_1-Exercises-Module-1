using UnityEngine;

[CreateAssetMenu(fileName = "Behaviour", menuName = "Scriptable Objects/Behaviour")]
public class BehaviourBase : ScriptableObject
{
    public virtual void PerformBehaviour()
    {
        Debug.Log("Performing behaviour");
    }
}
