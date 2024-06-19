using Dapper;
using System;
using System.Data;

namespace salao_app.Repository.Handlers
{
    public class DateTimeHandler : SqlMapper.TypeHandler<DateTime>
    {
        public override void SetValue(IDbDataParameter parameter, DateTime value)
        {
            parameter.Value = value;
        }

        public override DateTime Parse(object value)
        {
            return DateTime.Parse(value.ToString());
        }
    }
}
