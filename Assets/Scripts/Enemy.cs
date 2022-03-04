using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //Variables for the parameters of the enemy
    public int hp = 100;
    private int totalHP;
    private Transform[] positions;
    private int index = 0;   
    public float speed = 1;
    public int damage = 1;

    //Variables for the Enemy hp bar 
    private Slider hpSlider;

    public Animator anim;

    //VFX of death behavior
    //[SerializeField] private GameObject deathEffect;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        positions = Waypoints.positions;
        totalHP = hp;
        hpSlider = GetComponentInChildren<Slider>();
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
            Debug.Log("Enemy entered!");
            ReachDestination();
        }
    }

    void ReachDestination()
    {
        //GameManager.Instance.Defeat();
        GameObject.Destroy(this.gameObject); //Destroy the enemy when it reaches its destination
    }

    void OnDestroy()
    {
        EnemySpawner.countAliveEnemy--;
    }

    //Injury behavior
    public void TakeDamage(int damage)
    {
        if (hp <= 0) return;
        hp -= damage;
        //Let the enemy hp bar working.
        hpSlider.value = (float)hp / totalHP;
        if (hp <= 0)
        {
            Die();
        }
    }
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Terminal"))
    //    {
    //        Debug.Log("Enemy entered!");
    //        other.GetComponent<Terminal>().TakeDamage(damage);
    //        ReachDestination();
    //    }
    //}




    //Death behavior
    void Die()
    {
        //GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, transform.rotation);
        //Destroy(effect, 1f); //Destory the effect after 1 sec
        speed = 0; //Freeze the enemy's position
        anim.SetBool("Death", true);
        this.gameObject.tag = "Dead";
        Destroy(this.gameObject, 3f);
    }
}
