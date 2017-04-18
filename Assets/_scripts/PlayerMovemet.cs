using UnityEngine;
using System.Collections;

public class PlayerMovemet : MonoBehaviour
{
    private Rigidbody2D myRB;
    public float MoveSpeed = 5;
    Camera c;
    Vector2 input;

    // Use this for initialization
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        c = Camera.main;
        input = new Vector2(0, 0);
    }

    void FixedUpdate()
    {
        myRB.AddForce(input * MoveSpeed, ForceMode2D.Force);
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        Vector2 ptW = c.ScreenToWorldPoint(Input.mousePosition);
        float AngleRad = Mathf.Atan2(ptW.y - transform.position.y, ptW.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }
}