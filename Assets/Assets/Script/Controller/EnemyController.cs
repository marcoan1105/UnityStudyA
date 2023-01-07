using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float loodRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    CharacterCombatController combat;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.Instance.Player.transform;
        combat = GetComponent<CharacterCombatController>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= loodRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance) {

                CharacterStatsController targetStats = target.GetComponent<CharacterStatsController>();

                if (targetStats != null)
                {
                    combat.Attack(targetStats);
                }
                
                FaceTarget();
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, loodRadius);
    }
}
