using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : GameBehaviour
{
    Animator anim;

    public EnemyType myType;
    public float mySpeed;
    public float myHealth;
    public float myMaxHealth;
    public float attackRadius = 2;

    [Header("AI")]
    public PatrolType myPatrol;
    //public int patrolPoint = 0;            //Needed for linear patrol movement
    //public bool reverse = false;           //Needed for repeat patrol movement
    //public Transform startPos;             //Needed for repeat patrol movement
    //public Transform endPos;               //Needed for repeat patrol movement
    //public Transform moveToPos;
    NavMeshAgent agent;
    int currentWaypoint;
    float detectDistance = 10f;
    float detectTime = 5f;
    float attackDistance = 2f;

    [Header("Health Bar")]
    public Slider healthBarSlider;
    public TMP_Text healthBarText;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        Setup();
        SetupAI();
        transform.SetPositionAndRotation(transform.position, transform.rotation);
    }

    void Setup()
    {
        float healthModifier = 1;
        float speedModifier = 1;
        switch(_GM.difficulty)
        {
            case Difficulty.Easy:
                healthModifier = 1f;
                speedModifier = 1f;
                break;
            case Difficulty.Medium:
                healthModifier = 2f;
                speedModifier = 1.2f;
                break;
            case Difficulty.Hard:
                healthModifier = 3f;
                speedModifier = 1.5f;
                break;
            default:
                healthModifier = 1f;
                speedModifier = 1f;
                break;
        }

        switch(myType)
        {
            case EnemyType.OneHand:
                myHealth = 100f * healthModifier;
                mySpeed = 2f * speedModifier;
                myPatrol = PatrolType.Patrol;
                break;
            case EnemyType.TwoHand:
                myHealth = 200f * healthModifier;
                mySpeed = 1f * speedModifier;
                myPatrol = PatrolType.Patrol;
                break;
            case EnemyType.Archer:
                myHealth = 60f * healthModifier;
                mySpeed = 5f * speedModifier;
                myPatrol = PatrolType.Patrol;
                break;
        }
        myMaxHealth = myHealth;
        healthBarSlider.maxValue = myHealth;
        UpdateHealthBar();
    }

    void SetupAI()
    {
        currentWaypoint = UnityEngine.Random.Range(0, _EM.spawnPoints.Length);
        agent.SetDestination(_EM.spawnPoints[currentWaypoint].position);
        ChangeSpeed(mySpeed);
    }

    void ChangeSpeed(float _speed)
    {
        agent.speed = _speed;
    }

    IEnumerator Attack()
    {
        myPatrol = PatrolType.Attack;
        ChangeSpeed(0);
        PlayAnimation("Attack");
        yield return new WaitForSeconds(1.5f);
        ChangeSpeed(mySpeed);
        myPatrol = PatrolType.Chase;
    }

    private void Update()
    {
        //Always get the distance between the player and me
        float distToPlayer = Vector3.Distance(transform.position, _P.transform.position);

        if (distToPlayer <= detectDistance && myPatrol != PatrolType.Attack)
        {
            if (myPatrol != PatrolType.Chase)
            {
                myPatrol = PatrolType.Detect;
            }
        }

        //Set the animators Speed float value to that of my speed
        anim.SetFloat("Speed", mySpeed);

        switch (myPatrol)
        {
            case PatrolType.Patrol:
                //Get the distance between the player and the curent waypoint
                float distToWaypoint = Vector3.Distance(transform.position, _EM.spawnPoints[currentWaypoint].position);
                //If the distance is close enough, get a new waypoint
                if (distToWaypoint < 1)
                    SetupAI();
                //Reset the detect time
                detectTime = 5;
                break;

            case PatrolType.Detect:
                //Sets the destination to ourself, essentially stopping us
                agent.SetDestination(transform.position);
                //Stops our movement speed
                ChangeSpeed(0);
                //Decrement our detect time
                detectTime -= Time.deltaTime;               
                if (distToPlayer <= detectDistance)         
                {
                    myPatrol = PatrolType.Chase;
                    detectTime = 5;
                }
                if (detectTime <= 0)
                {
                    myPatrol = PatrolType.Patrol;
                    SetupAI();
                }
                break;

            case PatrolType.Chase:
                //Set the destination to that of the player
                agent.SetDestination(_P.transform.position);
                //Increse the speed of which to chase the chase the player
                ChangeSpeed(mySpeed * 2);
                //If the player gets outside our detect distance, go back to detect state
                if (distToPlayer > detectDistance)
                    myPatrol = PatrolType.Detect;
                //If we are close to the player, then attack
                if (distToPlayer <= attackDistance)
                    StartCoroutine(Attack());
                break;
        }
    }

    void UpdateHealthBar()
    {
        healthBarSlider.value = myHealth;
        healthBarText.text = myHealth + "/" + myMaxHealth;
    }
    
    public void Hit(int _damage)
    {
        myHealth -= _damage;
        UpdateHealthBar();
        if (myHealth <= 0)
        {
            Die();
        }
        else
        {
            PlayAnimation("Hit");
            GameEvents.ReportEnemyHit(this.gameObject);
        }
    }

    void Die()
    {
        GetComponent<Collider>().enabled = false;
        StopAllCoroutines();
        PlayAnimation("Die");
        ChangeSpeed(0);
        myPatrol = PatrolType.Die;
        GameEvents.ReportEnemyDie(this.gameObject);
    }

    void PlayAnimation(string _animation)
    {
        int randAnim = UnityEngine.Random.Range(1, 4);
        anim.SetTrigger(_animation + randAnim.ToString());
    }
}
