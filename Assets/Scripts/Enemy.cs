using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform[] positions;
    private int index = 0;
    public float speed = 1;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        positions = Waypoints.positions;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (index > positions.Length - 1)
        {
            return;//TO DO: decrease HP when enemy hits the throne
        }
        //Move between the waypoints
        transform.LookAt(positions[index].position);
        transform.position = Vector3.MoveTowards(transform.position, positions[index].position,Time.deltaTime*speed);
        //transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        //Move to next waypoint when arrive one waypoint
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2f)
            index++;
        //Reach the throne
        if (index > positions.Length - 1)
        {
            ReachDestination();
        }
    }

    void ReachDestination()
    {
        GameObject.Destroy(this.gameObject);
    }
    void OnDestroy()
    {
        EnemySpawner.countAliveEnemy--;
    }
}
