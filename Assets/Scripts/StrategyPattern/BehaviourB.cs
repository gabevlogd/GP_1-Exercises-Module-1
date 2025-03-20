using UnityEngine;

[CreateAssetMenu(fileName = "BehaviourB", menuName = "Scriptable Objects/BehaviourB")]
public class BehaviourB : BehaviourBase
{
    override public void PerformBehaviour()
    {
        Debug.Log("Performing behaviour B (override)");
    }
}
