using System.Collections.Generic;
using UnityEngine;

using akb.Entities.Interactions;

namespace akb.Entities.AI.Implementations.Astaroth
{
    public class AstarothAttackHandler : MonoBehaviour
    {
        [SerializeField] int attackDamage;

        BossAstaroth astarothEntity;

        private void Start()
        {
            astarothEntity = transform.root.GetComponent<BossAstaroth>();
        }

        public void InitiateAttack()
        {
            Debug.Log("I'm attacking what?");
        }
    }
}
