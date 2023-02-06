namespace AKB.Entities.AI.Implementations.Status_Demon
{
    public class StatusDemonAOEReset : INode
    {
        StatusDemonNodeData _data;

        public StatusDemonAOEReset(StatusDemonNodeData nodeData)
        {
            this._data = nodeData;
        }

        public INodeData GetNodeData()
        {
            return _data;
        }

        public bool Run()
        {
            _data.SetCanTeleport(true);
            _data.SetCanMove(true);
            return false;
        }
    }
}