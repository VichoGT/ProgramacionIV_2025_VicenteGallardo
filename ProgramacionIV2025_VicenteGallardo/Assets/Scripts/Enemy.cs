using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public int scoreValue = 50;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerScoreManager.Instance.AddScore(scoreValue); // aqui dependiendo del puntaje del enemigo el jugadoor recibira x cantidad de puntajes
        Destroy(gameObject);
    }

}
