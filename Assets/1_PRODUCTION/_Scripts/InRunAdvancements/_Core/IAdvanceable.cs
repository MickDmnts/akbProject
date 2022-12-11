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
        /// <returns></returns>
        string GetActiveName();
    }
}
