using Dapper;
using System;
using System.Data;

namespace salao_app.Repository.Handlers
{
    public class TimeSpanHandler : SqlMapper.TypeHandler<TimeSpan>
    {
        public override void SetValue(IDbDataParameter parameter, TimeSpan value)
        {
            parameter.Value = value;
        }

        public override TimeSpan Parse(object value)
        {
            return TimeSpan.Parse(value.ToString());
        }
    }
}
