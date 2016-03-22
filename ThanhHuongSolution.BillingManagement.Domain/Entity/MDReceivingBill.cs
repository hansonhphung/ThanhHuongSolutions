using System;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using ThanhHuongSolution.BillingManagement.Domain.Factory;

namespace ThanhHuongSolution.BillingManagement.Domain.Entity
{
    [BsonDiscriminator(BillType.RECEIVING_BILL)]
    public class MDReceivingBill : MDBaseBill
    {
        public long IncurredCost { get; set; }

        public long FinalTotalAmount { get; set; }

        public override async Task<T> Visit<T>(IGetModelVisitor<T> visitor)
        {
            return await visitor.Visit(this);
        }
    }
}
