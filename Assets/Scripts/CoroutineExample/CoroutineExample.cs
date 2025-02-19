using System.Collections;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{

    public float Delay;
    private float _elapsedTime = 0;

    private void OnEnable()
    {
        StartCoroutine(MyTimer(Delay));
    }

    private void Update()
    {
        if (_elapsedTime < Delay && _elapsedTime != -1)
        {
            _elapsedTime += Time.deltaTime;
        }
        else if (_elapsedTime != -1)
        {
            _elapsedTime = -1;
            Debug.Log("Do stuff in Update");
        }
    }
    private IEnumerator MyTimer(float timeToWait)
    {
        //yield return null; //aspetta un frame prima di proseguire oltre

        yield return new WaitForSeconds(timeToWait); //aspetta timeToWait secondi prima di proseguire oltre

        Debug.Log("Do Stuff in coroutine");

    }
}
