using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using akb.Entities.Interactions;
using akb.Entities.AI.Implementations.Astaroth;

public class AstarothDistanceCheck : MonoBehaviour
{
    AstarothNodeData _data;

    public AstarothDistanceCheck(AstarothNodeData nodeData)
    {
        this._data = nodeData;
    }

    public bool DistanceCheck()
    {
        if (!_data.GetCanMove() || _data.GetCanRotate())
        {
            return false;
        }
        bool currentRun = true;

        Vector3 targetPos = _data.GetTarget().transform.position;
        Vector3 agentPos = _data.GetAgentPosition();

        if((targetPos - agentPos).magnitude <= _data.GetMaxDistance())
        {
            Debug.Log("Charming");
        }
        else
        {
            Debug.Log("Spawning Circles");
        }

        return currentRun;
    }
}
