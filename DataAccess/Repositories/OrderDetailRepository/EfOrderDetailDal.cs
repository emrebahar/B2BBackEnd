using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Repositories.OrderDetailRepository;
using DataAccess.Context.EntityFramework;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.OrderDetailRepository
{
    public class EfOrderDetailDal : EfEntityRepositoryBase<OrderDetail, SimpleContextDb>, IOrderDetailDal
    {
        public async Task<List<OrderDetailDto>> GetListDto(int orderId)
        {
            using (var context = new SimpleContextDb())
            {
                var result = from OrderDetail in context.OrderDetails.Where(p => p.OrderId == orderId)
                             join product in context.Products on OrderDetail.ProductId equals product.Id
                             select new OrderDetailDto
                             {
                                 Id = OrderDetail.Id,
                                 OrderId = OrderDetail.OrderId,
                                 Price = OrderDetail.Price,
                                 ProductId = OrderDetail.ProductId,
                                 ProductName = product.Name,
                                 Quantity = OrderDetail.Quantity,
                                 Total = OrderDetail.Quantity * OrderDetail.Price
                             };
                return await result.ToListAsync();
            }
        }
    }
}
