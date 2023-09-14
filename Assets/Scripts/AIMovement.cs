using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    private NavMeshAgent enemyAgent;
    private float dist; //���� �� ������ �Ÿ�
    public float followThreshold; //��� ���� ���� �����ϰ� ��� �������� ���󰡰ڴٴ� �Ӱ��� ����.
    //�ִϸ�����
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
        if (dist < followThreshold) //���� ������ ������
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
