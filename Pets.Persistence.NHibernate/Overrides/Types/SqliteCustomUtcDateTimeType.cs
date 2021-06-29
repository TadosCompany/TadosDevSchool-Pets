namespace Pets.Persistence.NHibernate.Overrides.Types
{
    using System;
    using global::NHibernate.Type;

    
    
    public class SqliteCustomUtcDateTimeType : UtcDateTimeType
    {
        // protected override DateTime AdjustDateTime(DateTime dateValue)
        // {
        //     return base.AdjustDateTime(dateValue);
        // }
        
        protected override DateTime AdjustDateTime(DateTime dateValue)
        {
            if (dateValue.Kind == DateTimeKind.Local)
                dateValue = dateValue.ToUniversalTime();
            
            return dateValue;
        }
    }
}