using UnityEngine;
using System.Collections;


public class EnemyHealth : MonoBehaviour
{
    public int Health = 20; 

    public GameObject DamageEffect;

    public string visualDamage;
    
    public float pushAmount = 2f;     // how high it goes
    public float pushDuration = 0.3f; // how long it takes

    private bool isMoving = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            Health -= 1;
            visualDamage = "-1";
            float xOffset = Random.Range(-3f, 3f);
            float yOffset = Random.Range(-3f, 3);

            Vector3 spawnPos = transform.position + new Vector3(xOffset + 11, yOffset, 0);

            GameObject effect = Instantiate(DamageEffect, spawnPos, Quaternion.identity);
            
            DamgeEffect textScript = effect.GetComponent<DamgeEffect>();
            
            textScript.ChangeText(visualDamage);
        }

        if (other.CompareTag("PlayerAttackUltra"))
        {
            Health -= GameManager.Instance.MiniGameDamage;
            GameManager.Instance.MiniGameDamage *= -1;
            visualDamage = GameManager.Instance.MiniGameDamage.ToString();
            float xOffset = Random.Range(-3f, 3f);
            float yOffset = Random.Range(-3f, 3);

            Vector3 spawnPos = transform.position + new Vector3(xOffset + 11, yOffset, 0);

            GameObject effect = Instantiate(DamageEffect, spawnPos, Quaternion.identity);
            
            DamgeEffect textScript = effect.GetComponent<DamgeEffect>();

            if (textScript != null)
            {
                textScript.ChangeText(visualDamage); // call the function
            } 
        }
        
        
        
        if (other.CompareTag("Pushback") && !isMoving)
        {
            StartCoroutine(SmoothPush());
        }
    }
    
    IEnumerator SmoothPush()
    {
        isMoving = true;

        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + Vector3.up * pushAmount;

        float time = 0f;

        while (time < pushDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, time / pushDuration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }
    void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
