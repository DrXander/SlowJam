using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 0.5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public GameObject laserPrefab;
    public Transform firePoint;
    public float timeBetweenShots = 0.5f;
    private float shotTimer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        

        Vector2 mousePos = Input.mousePosition;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 mouseDistance = mousePos - screenPoint;
        float angle = Mathf.Atan2(mouseDistance.y, mouseDistance.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (Input.GetMouseButton(0) && shotTimer <= 0f)
        {
            Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
            shotTimer = timeBetweenShots;
        }

        if (shotTimer > 0f)
        {
            shotTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}