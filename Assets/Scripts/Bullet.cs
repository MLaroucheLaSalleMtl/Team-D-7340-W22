using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Variables for dmg system
    [SerializeField] private int damage = 0; //TO BE ADJUSTED

    //Variables for attack behavior
    [SerializeField] private float speed = 20;
    private Transform target;

    //VFX
    public GameObject hitEffectPrefab; //TO BE ADDED

    //Find target behavior
    public void SetTarget(Transform tar)
    {
        this.target = tar;
    }

    void Update()
    {
        transform.LookAt(target.position); //Make the bullet face the target
        transform.Translate(Vector3.forward * speed * Time.deltaTime); //Make the bullet move to the target
    }

    //Collision detection between bullet and its target
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            GameObject.Instantiate(hitEffectPrefab, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
