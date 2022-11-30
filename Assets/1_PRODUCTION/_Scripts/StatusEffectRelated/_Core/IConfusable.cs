namespace AKB.Entities.Interactions
{
    public interface IConfusable
    {
        bool IsConfused();

        void ApplyConfusedInteraction();
        void RemoveConfusedInteraction();
    }
}
