namespace Approve.PersistBase
{
    using Approve.RuleBase;
    using System;

    public class CacheBase
    {
        private RBase rBase = null;

        private CacheBase(RBase rBase)
        {
            this.rBase = rBase;
        }
    }
}

