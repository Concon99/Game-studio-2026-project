using UnityEngine;
using UnityEngine.UI;

public class BackGroundSpawn : MonoBehaviour
{
    public GameObject Background;

    public void Start()
    {
        Background.SetActive(false);
    
    }


    public void BulletTimeActive()
    {
        Background.SetActive(true);
    }
    
    public void BulletTimeDeActive()
    {
        Background.SetActive(false);
    }
}

