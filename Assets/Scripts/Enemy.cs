using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    public enum State { Idle, Chasing, Attacking};
    State currentState;

    NavMeshAgent pathfinder;
    Transform target;
    Material skinMaterial;

    Color originalColor;

    float attackDistanceThreshold = 0.5f; //공격거리
    float timeBetweenAttacks = 1;

    float nextAttackTime;
    float myCollisionRadius;
    float targetCollisionRadius;

    protected override void Start()
    {
        base.Start();
        //플레이어 태그를 찾아 올수있게 설정
        pathfinder = GetComponent<NavMeshAgent>();
        skinMaterial = GetComponent<Renderer>().material;
        originalColor = skinMaterial.color;

        currentState = State.Chasing;
        target = GameObject.FindGameObjectWithTag("Player").transform;

        myCollisionRadius = GetComponent<CapsuleCollider>().radius;
        targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;

        StartCoroutine(UpdatePath());
    }

    
    void Update()
    {
        if (Time.time > nextAttackTime)
        {
            float sqrDisToTarget = (target.position - transform.position).sqrMagnitude;

            if (sqrDisToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2))
            {
                nextAttackTime = Time.time + timeBetweenAttacks;
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        currentState = State.Attacking;
        pathfinder.enabled = false;

        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        //타겟 위치 가져오기
        Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius + attackDistanceThreshold / 2);
        

        float attackSpeed = 3f;
        float percent = 0;

        skinMaterial.color = Color.red;

        while(percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;

            //float interpolation = (-percent * percent + percent) * 4;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
            yield return null;
        }
        skinMaterial.color = originalColor;
        currentState = State.Chasing;
        pathfinder.enabled = true;
    }

    IEnumerator UpdatePath()
    {
        //갱신시간
        float refreshRate = 0.25f;

        while(target != null)
        {
            if (currentState == State.Chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                //타겟 위치 가져오기
                Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold/2);
                if (!dead)
                {
                    pathfinder.SetDestination(targetPosition);
                }
            }
            yield return new WaitForSeconds(refreshRate); //1초마다 루프 반복
        }
    }
}
