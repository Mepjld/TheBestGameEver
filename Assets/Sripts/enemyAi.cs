using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAi : MonoBehaviour
{
    //все точки перемещения
    public List<Transform> targetPoints;
    private NavMeshAgent _navMeshAgent;

    public PlayerController player;
    private bool _isPlayerNoticed;
    public float viewAngle;


    // Ос
    void Start()
    {
        initComponentLinks();
        pickNewPatrolPoint();
    }

    void Update()
    {
        //погоня за игроком
        playerNoticedUpdate();
        chaseUpdate();

        //патрулирование
        patrolTargetUpdate();
    }


    //функция? метод? подпрограмма?
    //выбор случйной точки на старте игры
    private void pickNewPatrolPoint()
    {
        _navMeshAgent.destination = targetPoints[Random.Range(0, targetPoints.Count)].position;
    }

    //выбор случайной точки, псоле достижения первой, в случае если игрока не видно
    private void patrolTargetUpdate()
    {
        if (!_isPlayerNoticed && _navMeshAgent.remainingDistance == 0)
        {
            pickNewPatrolPoint();
        }
    }

    //Dada Dada
    private void initComponentLinks()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void playerNoticedUpdate()
    {
        var direction = player.transform.position - transform.position;
        _isPlayerNoticed = false;
        //Угол обзора
        if (Vector3.Angle(transform.forward, direction) < viewAngle)
        {
            //Провекра столкновения
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, direction, out hit))
            {
                //Проверка столкновения с игроком
                if (hit.collider.gameObject == player.gameObject)
                {
                    _isPlayerNoticed = true;
                }
            }
        }
    }

    private void chaseUpdate()
    {
        if (_isPlayerNoticed)
        {
            _navMeshAgent.destination = player.transform.position;
        }
    }
}

