using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentObject : MonoBehaviour
{

    private static bool _alreadyExists = false;

    private void OnEnable()
    {
        if (_alreadyExists)
        {
            Destroy(this.gameObject); 
            return;
        }
        else
        {
            _alreadyExists = true;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        Debug.Log("PersistentObject Start");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene("SampleScene");
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("SampleScene 1");
        }
    }
}
