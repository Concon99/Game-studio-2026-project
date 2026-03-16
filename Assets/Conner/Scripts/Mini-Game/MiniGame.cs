using UnityEngine;
using System.Collections;

public class MiniGame : MonoBehaviour
{

    private int i;
    
    public GameObject Click;
    public GameObject Pop;
    public GameObject Slide;
    
    //mini game 1
    public Vector2 minPosition;// Bottom-left
    public Vector2 maxPosition;// Bottom-right
    public float MiniGame1Spawn = 1;
    public float MiniGame3Spawn = 1;

    //mini game 2
    public Vector2 spawnPos;
    
    //mini game 3
    

    [SerializeField] private BlurActive _BlurActive;

    public void StartMiniGame()
    {
        GameManager.Instance.Suceed = false;
        i = 0;
        DestroyAllChildren();

        if (GameManager.Instance.MiniGameType == "Click")
        {
            StartCoroutine(MiniGame1());
        }

        if (GameManager.Instance.MiniGameType == "Pop")
        {
            StartCoroutine(MiniGame2());
        }
        if (GameManager.Instance.MiniGameType == "Slide")
        {
            StartCoroutine(MiniGame3());
        }
    }


    public IEnumerator MiniGame1()
    {
        while (i < 5)
        {
            float randomX = Random.Range(minPosition.x, maxPosition.x);
            float randomY = Random.Range(minPosition.y, maxPosition.y);

            Vector2 spawnPos = new Vector2(randomX, randomY);

            Instantiate(Click, spawnPos, Quaternion.identity, transform);
            
            yield return new WaitForSecondsRealtime(MiniGame1Spawn);
            i++;
        }

        if (GameManager.Instance.Points == 5)
        {
            print("Won Mini game!");
            GameManager.Instance.Suceed = true;
            GameManager.Instance.MiniGameDamage = 20;
            _BlurActive.SlowMoOver = true;
            GameManager.Instance.Suceed = true;
        }
        
        else if (GameManager.Instance.Points >= 1)
        {
            print("Decent minigame!");
            GameManager.Instance.Suceed = false;
            GameManager.Instance.MiniGameDamage = 5;
        }
        
        else if (GameManager.Instance.Points <= 0)
        {
            print("failed mini game...");
            GameManager.Instance.MiniGameDamage = 0;
        }
        

        yield return true;
    }

    public IEnumerator MiniGame2()
    {

        GameObject PopPreFab = Instantiate(Pop, spawnPos, Quaternion.identity, transform);
        yield return new WaitForSecondsRealtime(7);
        Destroy(PopPreFab);
        
        if (!GameManager.Instance.Suceed && GameManager.Instance.Points >= 20)
        {
            print("Decent minigame!");
            GameManager.Instance.Suceed = false;
            GameManager.Instance.MiniGameDamage = 15;
        }
        
        else if (!GameManager.Instance.Suceed)
        {
            print("failed mini game...");
            GameManager.Instance.MiniGameDamage = 0;
        }
        
    }

    public IEnumerator MiniGame3()
    {
        
        while (i < 5)
        {
            Vector2 spawnPos = new Vector2(0, 0);

            GameObject SlidePreFab = Instantiate(Slide, spawnPos, Quaternion.identity, transform);
            
            yield return new WaitForSecondsRealtime(MiniGame3Spawn);
            i++;
        }

        if (GameManager.Instance.Points == 5)
        {
            print("Won Mini game!");
            GameManager.Instance.Suceed = true;
            GameManager.Instance.MiniGameDamage = 20;
            _BlurActive.SlowMoOver = true;
            GameManager.Instance.Suceed = true;
        }
        
        else if (GameManager.Instance.Points >= 1)
        {
            print("Decent minigame!");
            GameManager.Instance.Suceed = false;
            GameManager.Instance.MiniGameDamage = 5;
        }
        
        else if (GameManager.Instance.Points <= 0)
        {
            print("failed mini game...");
            GameManager.Instance.MiniGameDamage = 0;
        }
    }
    
    public void DestroyAllChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
