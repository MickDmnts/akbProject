using System.Collections;
using UnityEngine;

namespace akb.Projectiles
{
    public interface IRangedDemonSphere
    {
        public void StartLerping(Vector3 startPos, Vector3 bezierHint);
        IEnumerator LerpToTarget();
    }
}