using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadHelper : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            // unload scene A and load scene B

            if (SceneManager.GetSceneByName("AdditiveScene_B").isLoaded)
            {
                SceneManager.UnloadSceneAsync("AdditiveScene_B");
            }
            if (!SceneManager.GetSceneByName("AdditiveScene_A").isLoaded)
            {
                SceneManager.LoadSceneAsync("AdditiveScene_A", LoadSceneMode.Additive);
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            // unload scene B and load scene A

            if (SceneManager.GetSceneByName("AdditiveScene_A").isLoaded)
            {
                SceneManager.UnloadSceneAsync("AdditiveScene_A");
            }
            if (!SceneManager.GetSceneByName("AdditiveScene_B").isLoaded)
            {
                SceneManager.LoadSceneAsync("AdditiveScene_B", LoadSceneMode.Additive);
            }
        }

    }
}
