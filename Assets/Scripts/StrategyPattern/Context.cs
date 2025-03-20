using UnityEngine;

public class Context : MonoBehaviour
{
    [SerializeField]
    private BehaviourBase behaviour;

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    behaviour = new BehaviourA(); ;
        //    Debug.Log("Behaviour A selected");
        //}
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    behaviour = new BehaviourB(); ;
        //    Debug.Log("Behaviour B selected");
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            behaviour.PerformBehaviour();
        }
    }
}
