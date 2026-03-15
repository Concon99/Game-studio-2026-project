using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int Health = 20; 

    public GameObject DamageEffect;

    public string visualDamage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            Health -= 1;
            visualDamage = "-1";
            float xOffset = Random.Range(-3f, 3f);
            float yOffset = Random.Range(-3f, 3);

            Vector3 spawnPos = transform.position + new Vector3(xOffset + 10, yOffset, 0);

            GameObject effect = Instantiate(DamageEffect, spawnPos, Quaternion.identity);
            
            DamgeEffect textScript = effect.GetComponent<DamgeEffect>();

            if (textScript != null)
            {
                textScript.ChangeText(visualDamage); // call the function
            }
        }

        if (other.CompareTag("PlayerAttackUltra"))
        {
            Health -= GameManager.Instance.MiniGameDamage;
            visualDamage = GameManager.Instance.MiniGameDamage.ToString();
            float xOffset = Random.Range(-3f, 3f);
            float yOffset = Random.Range(-3f, 3);

            Vector3 spawnPos = transform.position + new Vector3(xOffset + 10, yOffset, 0);

            GameObject effect = Instantiate(DamageEffect, spawnPos, Quaternion.identity);
            
            DamgeEffect textScript = effect.GetComponent<DamgeEffect>();

            if (textScript != null)
            {
                textScript.ChangeText(visualDamage); // call the function
            } 
        }
    }
    

    void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
