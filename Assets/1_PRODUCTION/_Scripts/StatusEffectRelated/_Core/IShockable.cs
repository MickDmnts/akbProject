namespace AKB.Entities.Interactions
{
    public interface IShockable
    {
        bool IsGettingShocked();

        void InflictShockInteraction();
        void RemoveShockInteraction();
    }
}