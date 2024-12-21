using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMS.Models.Entities;
using Npgsql;

namespace AIMS.Services
{
    public class OrderService
    {
        private DatabaseConnect dbConnect = DatabaseConnect.gI();

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

                int affectedRows= await dbConnect.InsertDataAsync("OrderData", orderValues);

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

                List<OrderData> insertedOrders = await dbConnect.SelectDataAsync<OrderData>("OrderData", MapDataReaderToOrderData, whereClause, parameters);

                if (insertedOrders.Count == 0)
                {
                    throw new Exception("Failed to retrieve the newly inserted order.");
                }

                int orderId = insertedOrders[0].id;

                foreach (var orderMedia in orderMedias)
                {
                    orderMedia.orderID = orderId;

                    Dictionary<string, object> orderMediaValues = new Dictionary<string, object>
                    {
                        { "order_id", orderMedia.orderID },
                        { "media_id", orderMedia.mediaID },
                        { "quantity", orderMedia.quantity },
                        { "price", orderMedia.price }
                    };

                    await dbConnect.InsertDataAsync("OrderMedia", orderMediaValues);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the order.", ex);
            }
        }

        private OrderData MapDataReaderToOrderData(NpgsqlDataReader reader)
        {
            return new OrderData
            {
                id = reader.GetInt32(reader.GetOrdinal("id")),
                fullname = reader.IsDBNull(reader.GetOrdinal("fullname")) ? null : reader.GetString(reader.GetOrdinal("fullname")),
                province = reader.IsDBNull(reader.GetOrdinal("province")) ? null : reader.GetString(reader.GetOrdinal("province")),
                address = reader.IsDBNull(reader.GetOrdinal("address")) ? null : reader.GetString(reader.GetOrdinal("address")),
                phone = reader.IsDBNull(reader.GetOrdinal("phone")) ? null : reader.GetString(reader.GetOrdinal("phone")),
                userID = reader.IsDBNull(reader.GetOrdinal("userID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("userID")),
                userDeviceID = reader.IsDBNull(reader.GetOrdinal("userDeviceID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("userDeviceID")),
                shippingFee = reader.GetInt32(reader.GetOrdinal("shippingFee")),
                createAt = reader.GetDateTime(reader.GetOrdinal("createAt")),
                instructions = reader.IsDBNull(reader.GetOrdinal("instructions")) ? null : reader.GetString(reader.GetOrdinal("instructions")),
                type = reader.IsDBNull(reader.GetOrdinal("type")) ? null : reader.GetString(reader.GetOrdinal("type")),
                totalPrice = reader.GetInt32(reader.GetOrdinal("totalPrice")),
                status = reader.IsDBNull(reader.GetOrdinal("status")) ? null : reader.GetString(reader.GetOrdinal("status"))
            };
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

    }
}
