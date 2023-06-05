public interface IRelativeDatabaseEntity<TOrigin, TRelated, TRelation> where TOrigin : class
                                                                       where TRelated : class
                                                                       where TRelation : Enum
{
    public TOrigin Origin { get; set; }

    public TRelated Related { get; set; }

    public TRelation Relation { get; set; }
}
