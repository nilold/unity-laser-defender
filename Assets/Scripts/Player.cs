using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float movePadding = 1f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 15f;
    [SerializeField] float laserFiringPeriod = 0.25f;
    [SerializeField] float playerHealth = 600;
    Coroutine fireCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Use this for initialization
    void Start()
    {
        SetUpMoveBondaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Laser laser = collision.gameObject.GetComponent<Laser>();
        //if (laser && laser.tag == "Enemy") //not needed becouse iam using layers
        if (laser)
        {
            Hit(laser);
        }
    }

    private void Hit(Laser laser)
    {
        Debug.Log("PLAYER HIT!!!!!");
        playerHealth -= laser.Damage;

        if (playerHealth <= 0)
            Die();

    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            yield return new WaitForSeconds(laserFiringPeriod);
        }

    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        float newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        float newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }


    private void SetUpMoveBondaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + movePadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - movePadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + movePadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - movePadding;
    }
}
