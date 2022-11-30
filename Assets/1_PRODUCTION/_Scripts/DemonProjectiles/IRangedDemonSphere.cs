using System.Collections;
using UnityEngine;

namespace AKB.Projectiles
{
    public interface IRangedDemonSphere
    {
        public void StartLerping(Vector3 startPos, Vector3 bezierHint);
        IEnumerator LerpToTarget();
    }
}