using System;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using ThanhHuongSolution.BillingManagement.Domain.Interface;
using ThanhHuongSolution.BillingManagement.Domain.Factory;

namespace ThanhHuongSolution.BillingManagement.Domain.Entity
{
    [BsonDiscriminator(BillType.BILL)]
    public class MDBill : MDBaseBill
    {
        public override async Task<T> Visit<T>(IGetModelVisitor<T> visitor)
        {
            return await visitor.Visit(this);
        }
    }
}
