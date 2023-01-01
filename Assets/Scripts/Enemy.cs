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
    LivingEntity targetEntity;
    Material skinMaterial;

    Color originalColor;

    float attackDistanceThreshold = 0.5f; //���ݰŸ�
    float timeBetweenAttacks = 1;
    float damage = 1;

    float nextAttackTime;
    float myCollisionRadius;
    float targetCollisionRadius;

    bool hasTarget;

    protected override void Start()
    {
        base.Start();
        //�÷��̾� �±׸� ã�� �ü��ְ� ����
        pathfinder = GetComponent<NavMeshAgent>();
        skinMaterial = GetComponent<Renderer>().material;
        originalColor = skinMaterial.color;

        //���������� �÷��̾ ������ ��쿡�� �ڵ� ����
        if(GameObject.FindGameObjectWithTag ("Player") != null)
        {
            currentState = State.Chasing;
            hasTarget = true;

            target = GameObject.FindGameObjectWithTag("Player").transform;
            targetEntity = target.GetComponent<LivingEntity>();
            targetEntity.OnDeath += OnTargetDeath; //�̺�Ʈ ����

            myCollisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;

            StartCoroutine(UpdatePath());
        }
        
    }

    void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idle;
    }

    
    void Update()
    {
        if(hasTarget) {
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

        
    }

    IEnumerator Attack()
    {
        currentState = State.Attacking;
        pathfinder.enabled = false;

        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        //Ÿ�� ��ġ ��������
        Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius + attackDistanceThreshold / 2);
        

        float attackSpeed = 3f;
        float percent = 0;

        skinMaterial.color = Color.red;
        bool hasApplieDamage = false; //������� �����ϴ� ���ΰ�

        while(percent <= 1)
        {
            if(percent >= 0.5f && !hasApplieDamage)
            {
                hasApplieDamage = true;
                targetEntity.TakeDamage(damage);
            }
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
        //���Žð�
        float refreshRate = 0.25f;

        while(hasTarget)
        {
            if (currentState == State.Chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                //Ÿ�� ��ġ ��������
                Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold/2);
                if (!dead)
                {
                    pathfinder.SetDestination(targetPosition);
                }
            }
            yield return new WaitForSeconds(refreshRate); //1�ʸ��� ���� �ݺ�
        }
    }
}
