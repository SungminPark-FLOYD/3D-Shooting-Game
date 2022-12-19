using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    NavMeshAgent pathfinder;
    Transform target;
    protected override void Start()
    {
        base.Start();
        //플레이어 태그를 찾아 올수있게 설정
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(UpdatePath());
    }

    
    void Update()
    {
        
    }

    IEnumerator UpdatePath()
    {
        //갱신시간
        float refreshRate = 0.25f;

        while(target != null)
        {
            //타겟 위치 가져오기
            Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
            if (!dead)
            {
                pathfinder.SetDestination(targetPosition);
            }
            yield return new WaitForSeconds(refreshRate); //1초마다 루프 반복
        }
    }
}
