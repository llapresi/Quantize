using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

    public float speed;
    public Rigidbody2D rb;
    public GameManager.HealthCategory bulletCategory;

    [SerializeField]
    private GameObject deathParticals;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), GameManager.instance.PlayerRef.GetComponent<Collider2D>());
    }

    public void Fire(Transform playerTransform)
    {
        rb.transform.position = playerTransform.position;
        rb.transform.rotation = playerTransform.rotation;
        rb.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag != "Player")
        {
                if (bulletCategory == GameManager.HealthCategory.Hats)
                {
                    ParticleManager.instance.EmittParticles(bulletCategory, collision.contacts[0].point, collision.contacts[0].normal);
                }
                else
                {
                    ParticleManager.instance.EmittParticles(bulletCategory, collision.contacts[0].point);
                }
        }

        gameObject.SetActive(false);
    }
}
