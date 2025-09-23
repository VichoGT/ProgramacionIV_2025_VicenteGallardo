using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float dmg;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }
}
