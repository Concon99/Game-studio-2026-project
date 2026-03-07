using UnityEngine;
using System.Collections;

public class MiniGame : MonoBehaviour
{

    private int i;
    public string Type;
    public GameObject Click;    
    
    public Vector2 minPosition;// Bottom-left
    public Vector2 maxPosition;// Bottom-right
    public float MiniGame1Spawn = 1;

    [SerializeField] private BlurActive _BlurActive;

    public void StartMiniGame()
    {
        StartCoroutine(MiniGame1());
    }


    public IEnumerator MiniGame1()
    {
        while (i < 5)
        {
            float randomX = Random.Range(minPosition.x, maxPosition.x);
            float randomY = Random.Range(minPosition.y, maxPosition.y);

            Vector2 spawnPos = new Vector2(randomX, randomY);

            Instantiate(Click, spawnPos, Quaternion.identity);
            
            yield return new WaitForSeconds(MiniGame1Spawn);
            i++;
        }

        if (GameManager.Instance.Points == 5)
        {
            _BlurActive.SlowMoOver = true;
            GameManager.Instance.Suceed = true;
        }
        GameManager.Instance.Points = 0;

        yield return true;
    }
}
