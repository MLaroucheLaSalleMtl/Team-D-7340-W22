using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Variables for dmg system
    [SerializeField] private int damage = 50; //TO BE ADJUSTED

    //Variables for attack behavior
    [SerializeField] private float speed = 25f;
    [SerializeField] private float distance = 1f; //The distance between bullet and enemy
    [SerializeField] private float slowPercentage = 0.5f;
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
            target.GetComponent<Enemy>().TakeDamage(damage);
            Hit();
        }
    }

    //Hit behavior
    void Hit()
    {
        target.GetComponent<Enemy>().Slow(slowPercentage);
        GameObject effect = (GameObject)Instantiate(hitEffectPrefab, transform.position, transform.rotation);
        Destroy(effect, 1f); //Remove the effect after 1 sec
        SelfDestroy();
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
