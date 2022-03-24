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
    [SerializeField]private float startSpeed = 1f;

    //[HideInInspector]
    public float speed;
    [SerializeField]private int damage = 1;
    [SerializeField]private int reward = 20;
    [HideInInspector]public bool isDead = false; //To fix the kill reward stack bug
    private float freezeDuration = 3f;
    private bool frozen = false;
    private float stunDuration = 1f;
    private bool stunned = false;

    //Variables for the Enemy hp bar 
    private Slider hpSlider;

    //Variable for the death animation
    public Animator anim;

    //Build manager
    private BuildManager bdManager;

    // Start is called before the first frame update
    void Start()
    {
        speed = startSpeed;
        anim = GetComponent<Animator>();
        positions = Waypoints.positions;
        totalHP = hp;
        hpSlider = GetComponentInChildren<Slider>();
        bdManager = BuildManager.instance; //Cache the build manager
    }


    // Update is called once per frame
    void Update()
    {
        Move();

        //Freeze restoration
        if(frozen)
            freezeDuration -= Time.deltaTime;
        if (freezeDuration <= 0)
        {
            frozen = false;
            freezeDuration = 3f;
            speed = startSpeed;
        }

        //Stun restoration
        if (stunned)
            stunDuration -= Time.deltaTime;
        if(stunDuration <= 0)
        {
            stunned = false;
            stunDuration = 1f;
            speed = startSpeed;
        }
    }

    void Move()
    {
        //Move between the waypoints
        transform.LookAt(positions[index].position);
        transform.position = Vector3.MoveTowards(transform.position, positions[index].position,Time.deltaTime*speed);
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
        ThroneStat.hp -= damage; //Damage the throne
        GameObject.Destroy(this.gameObject); //Destroy the enemy when it reaches its destination
    }

    void OnDestroy()
    {
        EnemySpawner.countAliveEnemy--;
    }

    //Injury behavior
    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        //Let the enemy hp bar working.
        hpSlider.value = (float)hp / totalHP;
        if (hp <= 0 && !isDead)
        {
            Die();
            bdManager.ChangeMana(reward);
        }
    }
    //Slow
    public void Slow(float percentage)
    {
        speed = startSpeed *(1f - percentage);
    }

    //Freeze effect
    public void Freeze(float percentage)
    {
        frozen = true;
        Slow(percentage);
    }
    //private IEnumerator WaitForFreeze()
    //{
    //    yield return new WaitForSeconds(freezeDuration);
    //    Slow(0f); //Restore the move speed after freeze duration is over
    //}

    //Stun effect
    public void Stun()
    {
        stunned = true;
        Slow(1f); //Stun: slow by 100%
    }
    //private IEnumerator WaitForStun()
    //{
    //    yield return new WaitForSeconds(stunDuration);
    //    Slow(0f); //Restore the move speed after stun duration is over
    //}

    //Death behavior
    void Die()
    {
        speed = 0; //Freeze the enemy's position
        isDead = true;
        anim.SetBool("Death", true);
        this.gameObject.tag = "Dead";
        Destroy(this.gameObject, 3f);
    }
}
