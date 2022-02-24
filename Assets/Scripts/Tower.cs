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
        if(timer < attackRate) //Fix the bullet pile up bug
            timer += Time.deltaTime;
        if (enemies.Count > 0 && attackRate <= timer)
        {
            timer -= attackRate;
            Attack();
        }
    }

    void Attack()
    {
        //Fix NullReferenceException bug
        if (enemies[0] == null)
        {
            UpdateEnemy();
        }

        //Attack condition check
        if (enemies.Count > 0)
        {
            //Generate bullet
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, attackPosition.position, attackPosition.rotation);
            //Set the first enemy of the list as the priority target
            bullet.GetComponent<Bullet>().SetTarget(enemies[0].transform);
        }
        else
        {
            timer = attackRate; //Fix the idle bug after the enemy is destroyed
        }
    }

    void UpdateEnemy()
    {
        //Remove the null elements in the enemy list
        enemies.RemoveAll(e => e == null);
    }

    //Detection behavoir
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject); //Add a target when it enters the attack range
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject); //Remove a target when it exits the attack range
        }
    }

}
