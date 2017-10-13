using UnityEngine;
using System.Collections;

// Moves enemy when seen by player
public class EnemyComponent : MonoBehaviour
{
    public float speed;
    public float healthAmount = 15.0f;
    public float mercyTime = 100;

    Rigidbody2D myRB;
    GameObject playerRef;
    EnemySpawner spawner;
    public SpriteRenderer rend;

    public GameManager.HealthCategory enemyType;
    bool hasSetColor;

    void Awake()
    {
        playerRef = GameManager.instance.PlayerRef;
        myRB = GetComponent<Rigidbody2D>();
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    private void OnEnable()
    {
        rend.color = new Color(.9f, .9f, .9f);
        hasSetColor = false;
        myRB.isKinematic = true;
        this.gameObject.tag = "Mercy";
    }

    public void SetEnemyType(GameManager.HealthCategory enemyType)
    {
        this.enemyType = enemyType;
        if (this.enemyType == GameManager.HealthCategory.Kick)
        {
            healthAmount = 35;
            speed = 2;
            speed = -speed;
        }
        else if (this.enemyType == GameManager.HealthCategory.Hats)
        {
            speed *= 2f;
        }
        else if (this.enemyType == GameManager.HealthCategory.Bass)
        {
            // Maybe Do stuff here?
        }
    }

    void SetEnemyColor()
    {
        if (this.enemyType == GameManager.HealthCategory.Kick)
        {
            rend.color = new Color(0, 0.69f, 0.87f);
        }
        else if (this.enemyType == GameManager.HealthCategory.Hats)
        {
            rend.color = new Color(0.24f, 0.87f, 0);
        }
        else if (this.enemyType == GameManager.HealthCategory.Bass)
        {
            rend.color = new Color(0.87f, 0.77f, 0);
        }
    }

    void FixedUpdate()
    {
        if (mercyTime <= 0)
        {
            float step = speed * Time.fixedDeltaTime;
            Vector3 nF3 = Vector3.MoveTowards(transform.position, playerRef.transform.position, step);
            Vector2 nextFrame = new Vector2(nF3.x, nF3.y);
            myRB.MovePosition(nextFrame);
        }

        //myRB.velocity = (new Vector2(playerRef.transform.position.x,
        //  playerRef.transform.position.y) - myRB.position) * speed * Time.fixedDeltaTime;

        if (myRB.transform.position.y < -13 || myRB.transform.position.y > 13 || myRB.transform.position.x < -19 || myRB.transform.position.x > 19)
        {
            spawner.RemoveEnemy(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if(mercyTime > 0)
        {
            mercyTime -= (Time.deltaTime * 2);
        }
        else
        {
            mercyTime = 0;
            if(hasSetColor == false)
            {
                SetEnemyColor();
                hasSetColor = true;
                myRB.isKinematic = false;
                this.gameObject.tag = "Enemy";
            }
        }
    }

    public void Kill(bool scores)
    {
        if(scores)
        {
            if (enemyType == GameManager.HealthCategory.Kick)
            {
                GameManager.instance.AddScore(15);
            }
            if (enemyType == GameManager.HealthCategory.Hats)
            {
                GameManager.instance.AddScore(10);
            }
            if (enemyType == GameManager.HealthCategory.Bass)
            {
                GameManager.instance.AddScore(7);
            }
            GameManager.instance.ChangeHealth(enemyType, healthAmount);
            SFXManager.instance.PlayKillSound(enemyType);
        }

        spawner.RemoveEnemy(this.gameObject);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (mercyTime <= 0)
        {
            if (collision.collider.gameObject.tag == "Bullet")
            {
                if (collision.collider.gameObject.GetComponent<BulletMovement>().bulletCategory == enemyType)
                {
                    Kill(true);
                }
            }

            if (collision.collider.gameObject.tag == "Player" && GameManager.instance.PlayerRef.GetComponent<PlayerMovemet>().IsDashing())
            {
                Kill(false);
            }

            // Kicks ignore collision with walls
            if (collision.gameObject.tag == "Wall" && enemyType == GameManager.HealthCategory.Kick)
            {
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.collider);
            }
        }
    }
}
