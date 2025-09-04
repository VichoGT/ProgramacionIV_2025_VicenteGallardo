using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float dmg;
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
