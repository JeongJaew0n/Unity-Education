using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent nav;

    private EnemyHealth enemyHeatlh;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  //s는 모든 객체를 다 가져옴
        enemyHeatlh = GetComponent<EnemyHealth>();  //죽으면 더 이상 쫓아오지 않게 하기 위해
        nav = GetComponent<NavMeshAgent>();     //플레이어가 마음대로 돌아다닐 수 있게
    }

    private void Update()
    {
        if (enemyHeatlh.hp > 0)
        {
            transform.LookAt(player);
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }

}