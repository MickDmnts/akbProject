﻿namespace akb.Entities.Interactions
{
    public interface IStunnable
    {
        bool IsAlreadyStunned();

        void InflictStunnedInteraction();
        void RemoveStunnedInteraction();
    }
}
