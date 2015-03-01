namespace Approve.PersistBase
{
    using Approve.EntityBase;
    using System;
    using System.Collections;

    public interface IBaseTools
    {
        IEBase ConstructEntity(EntityTypeEnum eType, object data, IDictionary Relation, ConstructTypeEnum ecType);
        string GetName(EntityTypeEnum eType);
    }
}

