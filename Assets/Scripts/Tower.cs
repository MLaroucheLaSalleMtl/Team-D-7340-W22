using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //The list of targets
    public List<GameObject> enemies = new List<GameObject>();

    //Variables for attack behavior
    [SerializeField] private float attackRate = 0.5f; //attack per sec
    [SerializeField] private float timer = 0;
    public GameObject bulletPrefab;
    public Transform attackPosition;

    void Start()
    {
        timer = attackRate; //Fix the attack delay bug
    }

    void Update()
    {
        if(timer < attackRate)
            timer += Time.deltaTime;
        if (enemies.Count > 0 && attackRate <= timer)
        {
            timer -= attackRate;
            Attack();
        }
    }

    void Attack()
    {
        //Generate bullet
        GameObject bullet = GameObject.Instantiate(bulletPrefab, attackPosition.position, attackPosition.rotation);
        //Set the first enemy of the list as the priority target
        bullet.GetComponent<Bullet>().SetTarget(enemies[0].transform);
    }

    //Detection behavoir
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Add(other.gameObject); //Add a target when it enters the attack range
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Remove(other.gameObject); //Remove a target when it exits the attack range
        }
    }

}
