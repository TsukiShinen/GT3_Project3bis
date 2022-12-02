using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace AddonBehaviourTree
{
    public class IsEnemyInRange : Conditional
    {
        public SharedTank Tank;
        public SharedTank Target;

        public override TaskStatus OnUpdate()
        {
            if (Tank.Value.tankDetection.tanksInRange.Count <= 0) return TaskStatus.Failure;

            foreach(Tank enemyTank in Tank.Value.tankDetection.tanksInRange)
            {
                RaycastHit hit;

                var start = Tank.Value.transform.position + new Vector3(0, 1, 0);
                var end = enemyTank.transform.position + new Vector3(0, 1, 0);
                var dir = (end - start);

                if (Physics.Raycast(start, dir, out hit, Mathf.Infinity))
                {
                    if (!hit.collider.CompareTag("Tank")) continue;
                    
                    Tank.Value.tankMovement.ClearPath();
                    Target.Value = hit.collider.gameObject.GetComponentInParent<Tank>();
                    return TaskStatus.Success;
                }
            }
            return TaskStatus.Failure;
        }
    }
}
