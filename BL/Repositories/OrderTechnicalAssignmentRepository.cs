using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface IOrderTechnicalAssignmentRepository
    {  }

    public class OrderTechnicalAssignmentRepository :  Repository<OrderTechnicalAssignment>, IOrderTechnicalAssignmentRepository
    {
        public OrderTechnicalAssignmentRepository(DBContext ctx) : base(ctx)
        {  }
    }
}
