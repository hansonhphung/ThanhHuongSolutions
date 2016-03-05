using MongoDB.Bson.Serialization.Attributes;
using System.Threading.Tasks;
using ThanhHuongSolution.DeptManagement.Domain.Factory;

namespace ThanhHuongSolution.DeptManagement.Domain.Entity
{
    [BsonDiscriminator(DebtType.DEBT)]
    public class MDDebt : MDBaseDebt
    {
        public override async Task<T> Visit<T>(IGetModelVisitor<T> visitor)
        {
            return await visitor.Visit(this);
        }
    }
}
