using Domain;
using System;
using System.Data.Entity;

namespace Data.Helpers
{
    public static class StateHelper
    {
        public static EntityState ConvertState(State state)
        {
            switch (state)
            {
                case State.Added:
                    return EntityState.Added;
                case State.Deleted:
                    return EntityState.Deleted;
                case State.Modified:
                    return EntityState.Modified;
                default:
                    return EntityState.Unchanged;
            }
        }
    }
}