using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int hp= 10;
    public GameObject ThroneHealth;
    //public void SubHealth(int value)
    //{
    //    Health -= value;
    //}

    public void TakeDamage(int damage)
    {
        if (hp <= 0) return;
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        GameObject effect = Instantiate(ThroneHealth, transform.position, transform.rotation) as GameObject;
        Destroy(effect, 1f);
        Destroy(this.gameObject);
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Health");
    //    if(collision.gameObject.tag == "Terminal")
    //    {
    //        SubHealth(10);
    //        GameObject p = GameObject.Find("12");
    //        Image img = p.GetComponent<Image>();
    //        img.fillAmount = 1;
    //        if(Health <=0)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}

}
