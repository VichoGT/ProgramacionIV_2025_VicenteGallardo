using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public int dmg;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(dmg);
            Destroy(gameObject);
        }

        gameObject.SetActive(false);
    }

}
