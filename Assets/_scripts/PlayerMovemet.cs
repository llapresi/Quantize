using UnityEngine;
using System.Collections;

public class PlayerMovemet : MonoBehaviour
{
    private Rigidbody2D myRB;
    SpriteRenderer rend;
    Color originalColor;
    ParticleSystem particles;
    public float MoveSpeed = 5;
    public float DashTime = 14;
    public float DashSpeed = 14;
    float currDashTime;
    bool dash;
    Camera c;
    Vector2 input;
    public WeaponSelect weapons;
    public float RespawnTime;
    bool isAlive;
    public AudioSource dieSFX;
    BeatRepeater spawnerRepeat;

    // Use this for initialization
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        c = Camera.main;
        input = new Vector2(0, 0);
        dash = false;
        originalColor = new Color(rend.color.r, rend.color.g, rend.color.b);
        particles = GetComponent<ParticleSystem>();
        isAlive = true;
        spawnerRepeat = GameObject.Find("EnemySpawner").GetComponent<BeatRepeater>();
    }

    void FixedUpdate()
    {
        // Limit input
        if (!(input.x > 0.1 || input.x < -0.1))
            input.x = 0;
        if (!(input.y > 0.1 || input.y < -0.1))
            input.y = 0;

        myRB.AddForce(input * MoveSpeed, ForceMode2D.Force);
        if(dash)
        {
            myRB.AddForce(input.normalized * DashSpeed, ForceMode2D.Impulse);
            dash = false;
        }
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if(currDashTime > 0)
        {
            particles.Emit(2);
            rend.color = new Color(1, 0, 0);
            //Physics2D.IgnoreLayerCollision(9, 10, true);
            currDashTime -= DashTime * Time.deltaTime;
        }
        else
        {
            rend.color = originalColor;
            //Physics2D.IgnoreLayerCollision(9, 10, false);
            currDashTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dash = true;
            currDashTime = 100;
        }

        Vector2 ptW = c.ScreenToWorldPoint(Input.mousePosition);
        float AngleRad = Mathf.Atan2(ptW.y - transform.position.y, ptW.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }

    public bool IsDashing()
    {
        if (currDashTime > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Die()
    {
        if (isAlive)
        {
            isAlive = false;
            GameManager.instance.PlayerLives -= 1;
            myRB.velocity = Vector2.zero;
            myRB.isKinematic = true;
            weapons.SetActive(false);
            rend.enabled = false;
            AudioManager.instance.Die();
            ParticleManager.instance.deathParticle.transform.position = this.transform.position;
            ParticleManager.instance.deathParticle.Emit(8);
            dieSFX.Play();
            spawnerRepeat.sampleOffset -= 84000;
            GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] mercyObjects = GameObject.FindGameObjectsWithTag("Mercy");
            for (int i = 0; i < enemyObjects.Length; i++)
            {
                enemyObjects[i].GetComponent<EnemyComponent>().Kill(false);
            }
            for (int i = 0; i < mercyObjects.Length; i++)
            {
                mercyObjects[i].GetComponent<EnemyComponent>().Kill(false);
            }
            if (GameManager.instance.PlayerLives > 0)
            {
                StartCoroutine(Respawn());
            }
            else
            {
                GameManager.instance.EndGame();
            }
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(RespawnTime);
        weapons.SetActive(true);
        rend.enabled = true;
        myRB.transform.position = new Vector3(0, 0, 0);
        myRB.isKinematic = false;
        isAlive = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && IsDashing() == false)
        {
            Die();
        }
    }
}