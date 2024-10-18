using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField]
    private float Health;

    [SerializeField]
    private Transform PlayerPosition;

    // Start is called before the first frame update
    void Start()
    {
        if(Health == 0)
        {
            Health = 2;
        }
        Speed = 2;

        //PlayerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckHealthStatus();
        //WalkTowardsPlayer();
    }

    void CheckHealthStatus()
    {
        if(Health == 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    //walking towards player needs fixing
    void WalkTowardsPlayer()
    {
        var p = PlayerPosition.position;
        p.y = transform.position.y;
        this.transform.LookAt(p);
        this.transform.position = Vector3.MoveTowards(transform.position, PlayerPosition.position, Speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Weapon"))
        {
            Health -= 1;
        }
    }
}
