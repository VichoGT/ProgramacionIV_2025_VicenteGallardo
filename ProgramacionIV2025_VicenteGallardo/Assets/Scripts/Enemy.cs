using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 100;
    private int currentHealth;
    [Header("Gameplay")]
    public int scoreValue = 50;
    [SerializeField] private float speed = 3f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (player == null) return;

        // Movimiento hacia el jugador
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
        }
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

        TankGameManager manager = FindObjectOfType<TankGameManager>();

        if (manager != null)
        {
            manager.AddTime(5f);
        }
        Destroy(gameObject);
        AnalyticsManager.Instance.EnemyDefeated("Enemy");
    }

}
