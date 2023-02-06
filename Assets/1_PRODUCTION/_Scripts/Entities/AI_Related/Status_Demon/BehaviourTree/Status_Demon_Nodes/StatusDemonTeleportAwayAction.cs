namespace AKB.Entities.AI.Implementations.Status_Demon
{
    public class StatusDemonTeleportAwayAction : INode
    {
        StatusDemonNodeData _data;

        float rangeToTeleportAway = 25f;

        public StatusDemonTeleportAwayAction(StatusDemonNodeData nodeData)
        {
            this._data = nodeData;
        }

        public INodeData GetNodeData()
        {
            return _data;
        }

        public bool Run()
        {
            _data.SetCanTeleport(false);
            _data.SetCanMove(false);

            _data.GetEnemyEntity().Initiate_TeleportAwayFromPlayer(_data.GetTarget().position, rangeToTeleportAway);
            return false;
        }
    }
}