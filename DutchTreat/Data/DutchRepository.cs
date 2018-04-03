using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("Get all products was called");

                return _ctx.Products
                    .OrderBy(p => p.Title)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            try
            {
                return _ctx.Products
                        .Where(p => p.Category == category)
                        .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get products by given category: {ex}");
                return null;
            }
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            try
            {
                if (includeItems)
                {
                    return _ctx.Orders
                        .OrderBy(p => p.Id)
                        .Include(o => o.Items)
                        .ThenInclude(i => i.Product)
                        .ToList();
                }

                return _ctx.Orders
                    .ToList();


            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all orders: {ex}");
                return null;
            }
        }

        public Order GetOrderById(int id)
        {
            try
            {
                return _ctx.Orders
                    .OrderBy(p => p.Id)
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefault(o => o.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all orders: {ex}");
                return null;
            }
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
