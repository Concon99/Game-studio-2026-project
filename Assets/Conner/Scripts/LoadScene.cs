using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public int Scene;


    public void SceneLoad()
    {
        print("Load scene!");
        SceneManager.LoadScene(Scene);
    }
}
