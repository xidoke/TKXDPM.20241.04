using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AIMS.Models.Entities;
using AIMS.Services.impl;
using Npgsql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AIMS.Services
{
    public class OrderService : BaseService<OrderService, OrderData>, IOrderService
    {
        private readonly IOrderMediaService orderMediaService;

        public OrderService()
        {

        }

        public OrderService(IOrderMediaService orderMediaService)
        {
            this.orderMediaService = orderMediaService;
        }

        public async Task CreateOrderAsync(OrderData orderData, List<OrderMedia> orderMedias)
        {
            try
            {
                Dictionary<string, object> orderValues = new Dictionary<string, object>
                {
                    { "fullname", ConvertToUtf8(orderData.fullname) },
                    { "city", ConvertToUtf8(orderData.province) },
                    { "address", ConvertToUtf8(orderData.address) },
                    { "phone", orderData.phone },
                    { "shipping_fee", orderData.shippingFee },
                    { "instructions", orderData.instructions },
                    { "type", orderData.type },
                    { "total_price", orderData.totalPrice },
                    { "status", orderData.status },
                    { "user_id", orderData.userID },
                    { "user_device_id", orderData.userDeviceID }
                };

                int affectedRows= await InsertDataAsync(orderValues);

                if (affectedRows == 0)
                {
                    throw new Exception("Order insertion failed. No rows were affected.");
                }

                string whereClause = "fullname = @fullname AND createAt = @createAt";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "fullname", orderData.fullname },
                    { "created_at", orderData.createAt }
                };

                List<OrderData> insertedOrders = await SelectDataAsync(whereClause, parameters);

                if (insertedOrders.Count == 0)
                {
                    throw new Exception("Failed to retrieve the newly inserted order.");
                }

                int orderId = insertedOrders[0].id;

                foreach (var orderMedia in orderMedias)
                {
                    await orderMediaService.InsertOrderMediaAsync(orderMedia, orderId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the order.", ex);
            }
        }

        private string ConvertToUtf8(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            byte[] bytes = Encoding.UTF8.GetBytes(input);
            return Encoding.UTF8.GetString(bytes);
        }

        protected override void Map(NpgsqlDataReader reader, OrderData orderData)
        {
            orderData.id = reader.GetInt32(reader.GetOrdinal("id"));
            orderData.fullname = reader.IsDBNull(reader.GetOrdinal("fullname")) ? null : reader.GetString(reader.GetOrdinal("fullname"));
            orderData.province = reader.IsDBNull(reader.GetOrdinal("province")) ? null : reader.GetString(reader.GetOrdinal("province"));
            orderData.address = reader.IsDBNull(reader.GetOrdinal("address")) ? null : reader.GetString(reader.GetOrdinal("address"));
            orderData.phone = reader.IsDBNull(reader.GetOrdinal("phone")) ? null : reader.GetString(reader.GetOrdinal("phone"));
            orderData.userID = reader.IsDBNull(reader.GetOrdinal("userID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("userID"));
            orderData.userDeviceID = reader.IsDBNull(reader.GetOrdinal("userDeviceID")) 
                ? (int?)null : reader.GetInt32(reader.GetOrdinal("userDeviceID"));
            orderData.shippingFee = reader.GetInt32(reader.GetOrdinal("shippingFee"));
            orderData.createAt = reader.GetDateTime(reader.GetOrdinal("createAt"));
            orderData.instructions = reader.IsDBNull(reader.GetOrdinal("instructions")) 
                ? null : reader.GetString(reader.GetOrdinal("instructions"));
            orderData.type = reader.IsDBNull(reader.GetOrdinal("type")) ? null : reader.GetString(reader.GetOrdinal("type"));
            orderData.totalPrice = reader.GetInt32(reader.GetOrdinal("totalPrice"));
            orderData.status = reader.IsDBNull(reader.GetOrdinal("status")) ? null : reader.GetString(reader.GetOrdinal("status"));
        }
    }
}
