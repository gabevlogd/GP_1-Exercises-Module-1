using UnityEngine;

[CreateAssetMenu(fileName = "BehaviourA", menuName = "Scriptable Objects/BehaviourA")]
public class BehaviourA : BehaviourBase
{
    public override void PerformBehaviour()
    {
        base.PerformBehaviour();
        Debug.Log("Performing behaviour A (override)");
    }
}
