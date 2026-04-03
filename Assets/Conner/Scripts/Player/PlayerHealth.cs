using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int Health;
    private SpriteRenderer render;

    private Color defaultcolor;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        defaultcolor = render.color;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            Health -= 1;
            StartCoroutine(DamageEffect());
        }
        else if (other.CompareTag("Bullet"))
        {
            Health -= 1;
            StartCoroutine(DamageEffect());
        }
    }

    void Update()
    {
        if (Health <= 0)
        {
            print("Player dead!");
            SceneManager.LoadScene("GameOver");
        }
    }

    IEnumerator DamageEffect()
    {
        render.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        render.color = defaultcolor;
    }
}