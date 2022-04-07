using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Variables for dmg system
    [SerializeField] private int damage = 50;
    [SerializeField] private float explosionRadius = 0f;

    //Variables for attack behavior
    [SerializeField] private float speed = 25f;
    [SerializeField] private float distance = 0.2f; //The distance between bullet and enemy
    [SerializeField] private float slowPercentage = 0.5f;
    [SerializeField] private bool isStunable = false;
    private Transform target;
    //VFX
    public GameObject hitEffectPrefab; 

    //Find target behavior
    public void SetTarget(Transform tar)
    {
        this.target = tar;
    }

    void Update()
    {
        //fix NullReferenceException bug
        if (target == null)
        {
            SelfDestroy(); //Destroy the bullet when enemy reaches the Throne
            return;
        }

        transform.LookAt(target.position); //Make the bullet face the target
        transform.Translate(Vector3.forward * speed * Time.deltaTime); //Make the bullet move to the target

        //The distance detection
        Vector3 dir = target.position - transform.position;
        if (dir.magnitude < distance)
        {
            //The target get damage when gets hit from bullet
            Hit();
        }
    }

    //Hit behavior
    void Hit()
    {
        if (!target.GetComponent<Enemy>().isDead)
        {
            if (!isStunable) //Check if the bullet is stunable
            { //Non-stunable
                if (explosionRadius > 0f) //AoE
                    Explode();
                else
                { //Not AoE
                    target.GetComponent<Enemy>().Freeze(slowPercentage);
                    Damage(target);
                }
            }
            else
            { //Stunable
                int rng = Random.Range(0, 10);
                if (rng == 8) //10% chance of stun
                    target.GetComponent<Enemy>().Stun();
                Damage(target);
            }

            GameObject effect = (GameObject)Instantiate(hitEffectPrefab, transform.position, transform.rotation);

            Destroy(effect, 1f); //Remove the effect after 1 sec
            SelfDestroy();
        }
        else
            SelfDestroy();
    }

    //Damage
    void Damage(Transform tar)
    {
        Enemy e = tar.GetComponent<Enemy>();

        if (e != null)
            e.TakeDamage(damage);
    }

    //Explode
    void Explode()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (explosionRadius >= Vector3.Distance(transform.position, enemy.transform.position))
            {
                enemy.GetComponent<Enemy>().Freeze(slowPercentage);
                Damage(enemy.transform);
            }        
        }
        //Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        //foreach (Collider c in colliders)
        //{
        //    if (c.CompareTag("Enemy"))
        //    {
        //        Damage(c.transform);
        //    }
        //}
    }

    //Self destroy behavior
    void SelfDestroy()
    {
        Destroy(this.gameObject, 0.1f); //Remove the bullet after 0.1 sec
    }

    //Collision detection between bullet and its target
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            Hit();
        }
    }

}
