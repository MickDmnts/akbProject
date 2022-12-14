namespace AKB.Core.Managing.InRunUpdates
{
    /// <summary>
    /// An interface for better handling of the games' in-run advancements.
    /// </summary>
    public interface IAdvanceable
    {
        /// <summary>
        /// Get the active advancement name represented as a string.
        /// </summary>
        string GetActiveName();

        /// <summary>
        /// Set the active advancement of this handler to the passed enum type.
        /// </summary>
        /// <param name="advancement"></param>
        void SetActiveAdvancement(AdvancementTypes advancement);
    }
}
