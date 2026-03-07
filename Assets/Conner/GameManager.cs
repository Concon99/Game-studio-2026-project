using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Points;
    public bool Suceed = false;
    
    public static GameManager Instance;  // Singleton instance

    
    void Awake()
    {
        // Singleton pattern: keep only one GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate
        }
    }
}
