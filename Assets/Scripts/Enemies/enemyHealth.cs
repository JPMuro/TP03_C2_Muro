using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;

    private int currentHealth;

    private Renderer enemyRenderer;
    private Color originalColor;

    void Start()
    {
        currentHealth = maxHealth;

        enemyRenderer = GetComponentInChildren<Renderer>();

        if (enemyRenderer != null)
        {
            originalColor = enemyRenderer.material.color;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log(gameObject.name + " recibió " + damage + " de daño.");

        if (enemyRenderer != null)
        {
            StopAllCoroutines();
            StartCoroutine(FlashRed());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashRed()
    {
        enemyRenderer.material.color = Color.red;

        yield return new WaitForSeconds(0.15f);

        enemyRenderer.material.color = originalColor;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}