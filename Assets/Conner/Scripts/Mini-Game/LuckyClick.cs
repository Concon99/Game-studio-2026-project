using UnityEngine;
using System.Collections;


public class LuckyClick : MonoBehaviour
{
    public int chances;
    public SpriteRenderer render;

    private bool Bad;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroyCount());
        int RandomChance = Random.Range(1, chances);

        if (RandomChance == 1)
        {
            Bad = true;
            render.color = Color.red;
        }
        else
        {
            Bad = false;
            render.color = Color.green;
        }
    }

    void OnMouseDown()
    {
        if (Bad)
        {
            GameManager.Instance.Suceed = false;
            print("Lost Mini game.");
            GameManager.Instance.MiniGameDamage = 0;
            
            
            GameObject obj = GameObject.FindWithTag("Blur");

            BlurActive _BlurActive = obj.GetComponent<BlurActive>();
            
            _BlurActive.SlowMoOver = true;
            Destroy(gameObject);
        }
        else
        {
            GameManager.Instance.Points++;
            Destroy(gameObject);
        }
    }
    
    IEnumerator DestroyCount()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        Destroy(gameObject);
    }
}
