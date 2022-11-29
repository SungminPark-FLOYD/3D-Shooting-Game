using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    NavMeshAgent pathfinder;
    Transform target;
    void Start()
    {
        //�÷��̾� �±׸� ã�� �ü��ְ� ����
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(UpdatePath());
    }

    
    void Update()
    {
        
    }

    IEnumerator UpdatePath()
    {
        //���Žð�
        float refreshRate = 0.25f;

        while(target != null)
        {
            //Ÿ�� ��ġ ��������
            Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
            pathfinder.SetDestination(targetPosition);
            yield return new WaitForSeconds(refreshRate); //1�ʸ��� ���� �ݺ�
        }
    }
}