using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Dapper;

namespace FinanceAllianx.Web.Api.Helpers
{
    [ExcludeFromCodeCoverage]     
    public class MySqlGuidTypeHandler : SqlMapper.TypeHandler<Guid>  
    {
        public override void SetValue(IDbDataParameter parameter, Guid value)
        {
            parameter.Value = value.ToString();  
        }  

        public override Guid Parse(object value)
        {
            return new Guid(string)value);       
        }     
    }   
}  



