using UnityEngine;
using System.Collections;


public class Pop : MonoBehaviour
{
    void OnMouseDown()
    {
        GameManager.Instance.Points += 1;
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }


    void Update()
    {
        if (GameManager.Instance.Points == 30)
        {
            print("Won Mini game!");
            GameManager.Instance.Suceed = true;
            GameManager.Instance.MiniGameDamage = 40;
            
            
            GameObject obj = GameObject.FindWithTag("Blur");

            BlurActive _BlurActive = obj.GetComponent<BlurActive>();
            
            _BlurActive.SlowMoOver = true;
            GameManager.Instance.Suceed = true;
            
            Destroy(gameObject);
        }
    }
}
