using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMS.Models.Entities;
using Npgsql;

namespace AIMS.Services.impl
{
    public class OrderMediaService : BaseService<OrderMediaService, OrderMedia>, IOrderMediaService
    {
        public async Task InsertOrderMediaAsync(OrderMedia orderMedia, int orderId)
        {
            orderMedia.orderID = orderId;

            Dictionary<string, object> orderMediaValues = new Dictionary<string, object>
                    {
                        { "order_id", orderMedia.orderID },
                        { "media_id", orderMedia.mediaID },
                        { "quantity", orderMedia.quantity },
                        { "price", orderMedia.price }
                    };

            await InsertDataAsync(orderMediaValues);
        }

        protected override void Map(NpgsqlDataReader reader, OrderMedia orderMedia)
        {
            orderMedia.orderID = reader.IsDBNull(reader.GetOrdinal("order_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("order_id"));
            orderMedia.mediaID = reader.IsDBNull(reader.GetOrdinal("media_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("media_id"));
            orderMedia.quantity = reader.IsDBNull(reader.GetOrdinal("price")) ? 0 : reader.GetInt32(reader.GetOrdinal("price"));
            orderMedia.price = reader.IsDBNull(reader.GetOrdinal("quantity")) ? 0 : reader.GetInt32(reader.GetOrdinal("quantity"));
        }
    }
}
