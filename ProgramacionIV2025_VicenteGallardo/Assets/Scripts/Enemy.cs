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

    void Update()
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
       
        // Sumar puntaje al jugador
        PlayerScoreManager.Instance.AddScore(scoreValue);

        // Sumar +5 segundos al tiempo del TankGameManager
        TankGameManager manager = FindObjectOfType<TankGameManager>();
      
        if (manager != null)
        {
            manager.AddTime(5f);
        }

        // Destruir enemigo
       Destroy(gameObject);
       AnalyticsManager.Instance.EnemyDefeated("Enemy"); 
    }
}

