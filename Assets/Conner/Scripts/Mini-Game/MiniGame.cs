using UnityEngine;
using System.Collections;

public class MiniGame : MonoBehaviour
{

    private int i;
    
    public GameObject Click;
    public GameObject Pop;
    public GameObject Slide;
    public GameObject mouse;
    public GameObject Luck;
    
    //mini game 1
    public Vector2 minPosition;// Bottom-left
    public Vector2 maxPosition;// Bottom-right
    public float MiniGame1Spawn = 1;
    public float MiniGame3Spawn = 1;
    public float LuckSpawn = 0.5f;

    public float MiniGame4RunTime;

    //mini game 2
    public Vector2 spawnPos;
    
    //mini game 6
    public int totalPerLine = 10;
    public float spacing = 1.5f;
    public float spawnDelay = 0.5f;
    

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
        if (GameManager.Instance.MiniGameType == "Mouse")
        {
            StartCoroutine(MiniGame4());
        }
        if (GameManager.Instance.MiniGameType == "Luck")
        {
            StartCoroutine(MiniGame5());
        }
        if (GameManager.Instance.MiniGameType == "X")
        {
            StartCoroutine(MiniGame6());
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
    
    public IEnumerator MiniGame4()
    {
        GameObject Mouse = Instantiate(mouse, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(MiniGame4RunTime);


        if (GameManager.Instance.Points >= 500)
        {
            print("Decent minigame!");
            GameManager.Instance.Suceed = false;
            GameManager.Instance.MiniGameDamage = 5;
        }

        else
        {
            print("failed mini game...");
            GameManager.Instance.MiniGameDamage = 0;
        }
    }

    public IEnumerator MiniGame5()
    {
        GameManager.Instance.Suceed = true;
        while (i < 10 && GameManager.Instance.Suceed)
        {
            
            float randomX = Random.Range(minPosition.x, maxPosition.x); 
            float randomY = Random.Range(minPosition.y, maxPosition.y);

            Vector2 spawnPos = new Vector2(randomX, randomY);

            GameObject LuckPreFab = Instantiate(Luck, spawnPos, Quaternion.identity, transform);
            
            yield return new WaitForSecondsRealtime(LuckSpawn);
            i++;
        }
        
        if (GameManager.Instance.Points >= 5 && GameManager.Instance.Suceed)
        {
            print("Won Mini game!");
            GameManager.Instance.Suceed = true;
            GameManager.Instance.MiniGameDamage = 40;
            _BlurActive.SlowMoOver = true;
            GameManager.Instance.Suceed = true;
        }
        
        else if (GameManager.Instance.Points >= 2 && GameManager.Instance.Suceed)
        {
            print("Decent minigame!");
            GameManager.Instance.Suceed = false;
            GameManager.Instance.MiniGameDamage = 10;
        }
        
        else if (GameManager.Instance.Suceed)
        {
            print("Decent minigame!");
            GameManager.Instance.Suceed = false;
            GameManager.Instance.MiniGameDamage = 5;
        }
        
    }

    public IEnumerator MiniGame6()
    {
        Vector3 center = Vector3.zero; // Forces center to (0,0)

        int perLine = 5; // 5 + 5 = 10 total

        for (int i = 0; i < perLine; i++)
        {
            float offset = (i - (perLine - 1) / 2f) * spacing;
            Vector3 pos = center + new Vector3(offset, offset, 0);

            Instantiate(Click, pos, Quaternion.identity);
            yield return new WaitForSecondsRealtime(spawnDelay);
        }

        for (int i = 0; i < perLine; i++)
        {
            float offset = (i - (perLine - 1) / 2f) * spacing;
            Vector3 pos = center + new Vector3(offset, -offset, 0);

            Instantiate(Click, pos, Quaternion.identity);
            yield return new WaitForSecondsRealtime(spawnDelay);
        }
        
        if (GameManager.Instance.Points == 10)
        {
            print("Won Mini game!");
            GameManager.Instance.Suceed = true;
            GameManager.Instance.MiniGameDamage = 60;
            _BlurActive.SlowMoOver = true;
            GameManager.Instance.Suceed = true;
        }

        if (GameManager.Instance.Points >= 5)
        {
            print("Decent Mini game!");
            GameManager.Instance.Suceed = true;
            GameManager.Instance.MiniGameDamage = 30;
            _BlurActive.SlowMoOver = true;
            GameManager.Instance.Suceed = true;
        }
        else
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
