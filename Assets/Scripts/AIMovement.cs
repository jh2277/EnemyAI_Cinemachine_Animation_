using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    private NavMeshAgent enemyAgent;
    private float dist; //적과 나 사이의 거리
    public float followThreshold; //어느 정도 까진 무시하고 어느 정도까진 따라가겠다는 임계점 설정.
    //애니메이터
    private Animator playerAnimator;

    [SerializeField]
    Transform targetTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        enemyAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(targetTransform.position, transform.position);
        if (dist < followThreshold) //공격 범위에 들어오면
        {
            enemyAgent.isStopped = false;
            enemyAgent.SetDestination(targetTransform.position);
            playerAnimator.SetBool("isRunning", true);
            playerAnimator.SetBool("isAttack", false);

            if (dist <= enemyAgent.stoppingDistance)
            {
                playerAnimator.SetBool("isAttack", true);
                playerAnimator.SetBool("isRunning", false);
            }
        }
        else
        {
            enemyAgent.isStopped=true;
            playerAnimator.SetBool("isRunning", true);

        }
        

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bullet")
        {
            playerAnimator.SetBool("isDead", true);
        }

    }
}
