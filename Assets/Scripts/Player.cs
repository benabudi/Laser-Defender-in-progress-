using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // configuration parameters
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float pedding = 1f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFireingPeriod = 0.1f;

    Coroutine firingCoroutine;

    float xMin , yMin;
    float xMax , yMax;

	// Use this for initialization
	void Start () {
        SetUpMoveBoundaries();
	}

   
    // Update is called once per frame
    void Update () {
        Move();
        Fire();
	}

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(fireContinouosly());
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator fireContinouosly()
    {
        while(true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFireingPeriod);
        }
        
    }

    private void Move()
    {
        //get X Movment
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed; //getting the X value of the creen from unity
        var newXPos = Mathf.Clamp(transform.position.x + deltaX , xMin , xMax);

        //get Y Movment
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);


        transform.position = new Vector2(newXPos , newYPos);
        
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + pedding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - pedding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + pedding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - pedding;
    }

}
