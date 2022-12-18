namespace akb.Core.Managing.InRunUpdates
{
    [System.Serializable]
    public class SpearRunAdvancements : IAdvanceable
    {
        AdvancementTypes activeAdvancement = AdvancementTypes.None;

        public SpearRunAdvancements() { }

        public void SetActiveAdvancement(AdvancementTypes advancement) => activeAdvancement = advancement;

        public bool GetIsAdvancementActive(AdvancementTypes advancement)
        {
            return advancement == activeAdvancement;
        }

        public string GetActiveName()
        {
            return System.Enum.GetName(typeof(AdvancementTypes), activeAdvancement);
        }
    }
}