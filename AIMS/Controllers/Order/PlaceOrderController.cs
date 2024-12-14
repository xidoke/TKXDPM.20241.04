using AIMS.Models.Entities;
using AIMS.Services;
using AIMS.Views.Order;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.Controllers.Order
{
    public class PlaceOrderController
    {
        private MediaService mediaService;
        public PlaceOrderController()
        {
            InitializeData();
            mediaService = new MediaService();
        }
        public List<CartItem> CartItems;
        public Dictionary<string, List<string>> districts = new Dictionary<string, List<string>>();
        public Dictionary<string, Dictionary<string, List<string>>> wards = new Dictionary<string, Dictionary<string, List<string>>>();
        public List<string> city = new List<string>()
        {
            "An Giang",
            "Bà Rịa - Vũng Tàu",
            "Bắc Giang",
            "Bắc Kạn",
            "Bạc Liêu",
            "Bắc Ninh",
            "Bến Tre",
            "Bình Định",
            "Bình Dương",
            "Bình Phước",
            "Bình Thuận",
            "Cà Mau",
            "Cần Thơ",
            "Cao Bằng",
            "Đà Nẵng",
            "Đắk Lắk",
            "Đắk Nông",
            "Điện Biên",
            "Đồng Nai",
            "Đồng Tháp",
            "Gia Lai",
            "Hà Giang",
            "Hà Nam",
            "Hà Nội",
            "Hà Tĩnh",
            "Hải Dương",
            "Hải Phòng",
            "Hậu Giang",
            "Hòa Bình",
            "Hưng Yên",
            "Khánh Hòa",
            "Kiên Giang",
            "Kon Tum",
            "Lai Châu",
            "Lâm Đồng",
            "Lạng Sơn",
            "Lào Cai",
            "Long An",
            "Nam Định",
            "Nghệ An",
            "Ninh Bình",
            "Ninh Thuận",
            "Phú Thọ",
            "Phú Yên",
            "Quảng Bình",
            "Quảng Nam",
            "Quảng Ngãi",
            "Quảng Ninh",
            "Quảng Trị",
            "Sóc Trăng",
            "Sơn La",
            "Tây Ninh",
            "Thái Bình",
            "Thái Nguyên",
            "Thanh Hóa",
            "Thừa Thiên Huế",
            "Tiền Giang",
            "TP Hồ Chí Minh",
            "Trà Vinh",
            "Tuyên Quang",
            "Vĩnh Long",
            "Vĩnh Phúc",
            "Yên Bái"
        };
        private void InitializeData()
        {
            districts.Add("Hà Nội", new List<string>() {
            "Ba Đình", "Hoàn Kiếm", "Tây Hồ", "Long Biên", "Cầu Giấy", "Đống Đa", "Hai Bà Trưng", "Hoàng Mai", "Thanh Xuân",
            "Sóc Sơn", "Đông Anh", "Gia Lâm", "Nam Từ Liêm", "Bắc Từ Liêm", "Mê Linh", "Hà Đông", "Sơn Tây", "Ba Vì", "Phúc Thọ",
            "Đan Phượng", "Hoài Đức", "Quốc Oai", "Thạch Thất", "Chương Mỹ", "Thanh Oai", "Thường Tín", "Phú Xuyên", "Ứng Hòa", "Mỹ Đức"
            });
            wards.Add("Hà Nội", new Dictionary<string, List<string>>()
            {
                // Quận Ba Đình
                { "Ba Đình", new List<string>() { "Cống Vị", "Điện Biên", "Đội Cấn", "Giảng Võ", "Kim Mã", "Liễu Giai", "Ngọc Hà", "Ngọc Khánh", "Nguyễn Trung Trực", "Phúc Xá", "Quán Thánh", "Thành Công", "Trúc Bạch", "Vĩnh Phúc" } },
                // Quận Hoàn Kiếm
                { "Hoàn Kiếm", new List<string>() { "Chương Dương", "Cửa Đông", "Cửa Nam", "Đồng Xuân", "Hàng Bạc", "Hàng Bài", "Hàng Bồ", "Hàng Bông", "Hàng Buồm", "Hàng Đào", "Hàng Gai", "Hàng Mã", "Hàng Trống", "Lý Thái Tổ", "Phan Chu Trinh", "Phúc Tân", "Trần Hưng Đạo", "Tràng Tiền" } },
                // Quận Tây Hồ
                { "Tây Hồ", new List<string>() { "Bưởi", "Nhật Tân", "Phú Thượng", "Quảng An", "Thụy Khuê", "Tứ Liên", "Xuân La", "Yên Phụ" } },
                // Quận Long Biên
                { "Long Biên", new List<string>() { "Bồ Đề", "Cự Khối", "Đức Giang", "Gia Thụy", "Giang Biên", "Long Biên", "Ngọc Lâm", "Ngọc Thụy", "Phúc Đồng", "Phúc Lợi", "Sài Đồng", "Thạch Bàn", "Thượng Thanh", "Việt Hưng" } },
                // Quận Cầu Giấy
                { "Cầu Giấy", new List<string>() { "Dịch Vọng", "Dịch Vọng Hậu", "Mai Dịch", "Nghĩa Đô", "Nghĩa Tân", "Quan Hoa", "Trung Hòa", "Yên Hòa" } },
                // Quận Đống Đa
                { "Đống Đa", new List<string>() { "Cát Linh", "Hàng Bột", "Khâm Thiên", "Khương Thượng", "Kim Liên", "Láng Hạ", "Láng Thượng", "Nam Đồng", "Ngã Tư Sở", "Ô Chợ Dừa", "Phương Liên", "Phương Mai", "Quang Trung", "Quốc Tử Giám", "Thịnh Quang", "Thổ Quan", "Trung Liệt", "Trung Phụng", "Trung Tự", "Văn Chương", "Văn Miếu" } },
                // Quận Hai Bà Trưng
                { "Hai Bà Trưng", new List<string>() { "Bách Khoa", "Bạch Đằng", "Bạch Mai", "Cầu Dền", "Đống Mác", "Đồng Nhân", "Đồng Tâm", "Lê Đại Hành", "Minh Khai", "Nguyễn Du", "Phạm Đình Hổ", "Phố Huế", "Quỳnh Lôi", "Quỳnh Mai", "Thanh Lương", "Thanh Nhàn", "Trương Định", "Vĩnh Tuy" } },
                // Quận Hoàng Mai
                { "Hoàng Mai", new List<string>() { "Đại Kim", "Định Công", "Giáp Bát", "Hoàng Liệt", "Hoàng Văn Thụ", "Lĩnh Nam", "Mai Động", "Tân Mai", "Thanh Trì", "Thịnh Liệt", "Trần Phú", "Tương Mai", "Vĩnh Hưng", "Yên Sở" } },
                // Quận Thanh Xuân
                { "Thanh Xuân", new List<string>() { "Hạ Đình", "Khương Đình", "Khương Mai", "Khương Trung", "Kim Giang", "Nhân Chính", "Phương Liệt", "Thanh Xuân Bắc", "Thanh Xuân Nam", "Thanh Xuân Trung", "Thượng Đình" } },
                // Huyện Sóc Sơn
                { "Sóc Sơn", new List<string>() { "Bắc Phú", "Bắc Sơn", "Đông Xuân", "Đức Hòa", "Hiền Ninh", "Hồng Kỳ", "Kim Lũ", "Mai Đình", "Minh Phú", "Minh Trí", "Nam Sơn", "Phú Cường", "Phú Minh", "Phù Linh", "Phù Lỗ", "Quang Tiến", "Tân Dân", "Tân Hưng", "Tân Minh", "Thanh Xuân", "Tiên Dược", "Trung Giã", "Việt Long", "Xuân Giang", "Xuân Thu" } },
                // Huyện Đông Anh
                { "Đông Anh", new List<string>() { "Bắc Hồng", "Cổ Loa", "Đại Mạch", "Đông Hội", "Dục Tú", "Hải Bối", "Kim Chung", "Kim Nỗ", "Liên Hà", "Mai Lâm", "Nam Hồng", "Nguyên Khê", "Tàm Xá", "Thụy Lâm", "Tiên Dương", "Uy Nỗ", "Vân Hà", "Vân Nội", "Việt Hùng", "Vĩnh Ngọc", "Võng La", "Xuân Canh", "Xuân Nộn" } },
                // Huyện Gia Lâm
                { "Gia Lâm", new List<string>() { "Bát Tràng", "Cổ Bi", "Đa Tốn", "Đặng Xá", "Đình Xuyên", "Dương Hà", "Dương Quang", "Dương Xá", "Kiêu Kỵ", "Kim Đức", "Kim Sơn", "Lệ Chi", "Ninh Hiệp", "Phù Đổng", "Phú Thị", "Trung Mầu", "Văn Đức", "Yên Thường", "Yên Viên" } },
                // Quận Nam Từ Liêm
                { "Nam Từ Liêm", new List<string>() { "Cầu Diễn", "Đại Mỗ", "Mễ Trì", "Mỹ Đình 1", "Mỹ Đình 2", "Phú Đô", "Phương Canh", "Tây Mỗ", "Trung Văn", "Xuân Phương" } },
                // Quận Bắc Từ Liêm
                { "Bắc Từ Liêm", new List<string>() { "Cổ Nhuế 1", "Cổ Nhuế 2", "Đông Ngạc", "Đức Thắng", "Liên Mạc", "Minh Khai", "Phú Diễn", "Phúc Diễn", "Tây Tựu", "Thượng Cát", "Thụy Phương", "Xuân Đỉnh", "Xuân Tảo" } },
                // Huyện Mê Linh
                { "Mê Linh", new List<string>() { "Chi Đông", "Chu Phan", "Đại Thịnh", "Hoàng Kim", "Kim Hoa", "Liên Mạc", "Mê Linh", "Tam Đồng", "Thạch Đà", "Thanh Lâm", "Tiền Phong", "Tiến Thắng", "Tiến Thịnh", "Tráng Việt", "Tự Lập", "Văn Khê", "Vạn Yên" } },
                // Quận Hà Đông
                { "Hà Đông", new List<string>() { "Biên Giang", "Đồng Mai", "Dương Nội", "Hà Cầu", "Kiến Hưng", "La Khê", "Mộ Lao", "Nguyễn Trãi", "Phú La", "Phú Lãm", "Phú Lương", "Phúc La", "Quang Trung", "Văn Quán", "Vạn Phúc", "Yên Nghĩa", "Yết Kiêu" } },
                // Thị xã Sơn Tây
                { "Sơn Tây", new List<string>() { "Cổ Đông", "Đường Lâm", "Kim Sơn", "Phú Thịnh", "Sơn Đông", "Sơn Lộc", "Trung Hưng", "Trung Sơn Trầm", "Xuân Khanh" } },
                // Huyện Ba Vì
                { "Ba Vì", new List<string>() { "Ba Trại", "Ba Vì", "Cẩm Lĩnh", "Cam Thượng", "Châu Sơn", "Chu Minh", "Cổ Đô", "Đông Quang", "Đồng Thái", "Khánh Thượng", "Minh Châu", "Minh Quang", "Phong Vân", "Phú Châu", "Phú Cường", "Phú Đông", "Phú Phương", "Phú Sơn", "Sơn Đà", "Tản Hồng", "Tản Lĩnh", "Thái Hòa", "Thuần Mỹ", "Thụy An", "Tiên Phong", "Tòng Bạt", "Vân Hòa", "Vạn Thắng", "Vật Lại", "Yên Bài" } },
                // Huyện Phúc Thọ
                { "Phúc Thọ", new List<string>() { "Cẩm Đình", "Hát Môn", "Hiệp Thuận", "Liên Hiệp", "Long Xuyên", "Ngọc Tảo", "Phúc Hòa", "Phụng Thượng", "Sen Phương", "Tam Hiệp", "Tam Thuấn", "Thanh Đa", "Thọ Lộc", "Thượng Cốc", "Tích Giang", "Trạch Mỹ Lộc", "Vân Hà", "Vân Nam", "Vân Phúc", "Võng Xuyên", "Xuân Phú" } },
                // Huyện Đan Phượng
                { "Đan Phượng", new List<string>() { "Đan Phượng", "Đồng Tháp", "Hạ Mỗ", "Hồng Hà", "Liên Hà", "Liên Hồng", "Liên Trung", "Phương Đình", "Song Phượng", "Tân Hội", "Tân Lập", "Thọ An", "Thọ Xuân", "Thượng Mỗ", "Trung Châu" } },
                // Huyện Hoài Đức
                { "Hoài Đức", new List<string>() { "An Khánh", "An Thượng", "Cát Quế", "Đắc Sở", "Di Trạch", "Đông La", "Đức Giang", "Đức Thượng", "Dương Liễu", "Kim Chung", "La Phù", "Lại Yên", "Minh Khai", "Sơn Đồng", "Song Phương", "Tiền Yên", "Vân Canh", "Vân Côn", "Yên Sở" } },
                // Huyện Quốc Oai
                { "Quốc Oai", new List<string>() { "Cấn Hữu", "Cộng Hòa", "Đại Thành", "Đồng Quang", "Đông Xuân", "Đông Yên", "Hòa Thạch", "Liệp Tuyết", "Nghĩa Hương", "Ngọc Liệp", "Ngọc Mỹ", "Phú Cát", "Phú Mãn", "Phượng Cách", "Sài Sơn", "Tân Hòa", "Tân Phú", "Thạch Thán", "Tuyết Nghĩa", "Yên Sơn" } },
                // Huyện Thạch Thất
                { "Thạch Thất", new List<string>() { "Bình Phú", "Bình Yên", "Cẩm Yên", "Canh Nậu", "Chàng Sơn", "Cần Kiệm", "Dị Nậu", "Đại Đồng", "Hạ Bằng", "Hương Ngải", "Hữu Bằng", "Kim Quan", "Lại Thượng", "Liên Quan", "Phú Kim", "Phùng Xá", "Tân Xã", "Thạch Hòa", "Thạch Xá", "Tiến Xuân", "Yên Bình", "Yên Trung" } },
                // Huyện Chương Mỹ
                { "Chương Mỹ", new List<string>() { "Chúc Sơn", "Xuân Mai", "Đại Yên", "Đông Phương Yên", "Đông Sơn", "Đồng Lạc", "Đồng Phú", "Hoà Chính", "Hoàng Diệu", "Hoàng Văn Thụ", "Hồng Phong", "Hợp Đồng", "Hữu Văn", "Lam Điền", "Mỹ Lương", "Nam Phương Tiến", "Ngọc Hòa", "Phú Nghĩa", "Phú Nam An", "Phụng Châu", "Quảng Bị", "Tân Tiến", "Tiên Phương", "Tốt Động", "Thanh Bình", "Thủy Xuân Tiên", "Thụy Hương", "Thượng Vực", "Trần Phú", "Trung Hòa", "Trường Yên", "Văn Võ" } },
                // Huyện Thanh Oai
                { "Thanh Oai", new List<string>() { "Kim Bài", "Bích Hòa", "Bình Minh", "Cao Dương", "Cao Viên", "Cự Khê", "Dân Hòa", "Đỗ Động", "Hồng Dương", "Kim An", "Kim Thư", "Liên Châu", "Mỹ Hưng", "Phương Trung", "Tam Hưng", "Tân Ước", "Thanh Cao", "Thanh Mai", "Thanh Thùy", "Thanh Văn", "Xuân Dương" } },
                // Huyện Thường Tín
                { "Thường Tín", new List<string>() { "Thường Tín", "Chương Dương", "Dũng Tiến", "Duyên Thái", "Hà Hồi", "Hiền Giang", "Hòa Bình", "Khánh Hà", "Hồng Vân", "Lê Lợi", "Liên Phương", "Minh Cường", "Nghiêm Xuyên", "Nguyễn Trãi", "Nhị Khê", "Ninh Sở", "Quất Động", "Tân Minh", "Thắng Lợi", "Thống Nhất", "Thư Phú", "Tiền Phong", "Tô Hiệu", "Tự Nhiên", "Vạn Điểm", "Văn Bình", "Văn Phú", "Văn Tự", "Vân Tảo" } },
                // Huyện Phú Xuyên
                { "Phú Xuyên", new List<string>() { "Phú Xuyên", "Phú Minh", "Bạch Hạ", "Châu Can", "Chuyên Mỹ", "Đại Thắng", "Đại Xuyên", "Hoàng Long", "Hồng Minh", "Hồng Thái", "Khai Thái", "Minh Tân", "Nam Phong", "Nam Tiến", "Nam Triều", "Phú Túc", "Phú Yên", "Phúc Tiến", "Phượng Dực", "Quang Lãng", "Quang Trung", "Sơn Hà", "Tân Dân", "Tri Thủy", "Tri Trung", "Văn Hoàng", "Vân Từ" } },
                // Huyện Ứng Hòa
                { "Ứng Hòa", new List<string>() { "Vân Đình", "Cao Thành", "Đại Cường", "Đại Hùng", "Đội Bình", "Đông Lỗ", "Đồng Tân", "Đồng Tiến", "Hoa Sơn", "Hoà Lâm", "Hoà Nam", "Hoà Xá", "Hồng Quang", "Kim Đường", "Liên Bạt", "Lưu Hoàng", "Minh Đức", "Mỹ Đức", "Phù Lưu", "Phương Tú", "Quảng Phú Cầu", "Sơn Công", "Tảo Dương Văn", "Trầm Lộng", "Trung Tú", "Trường Thịnh", "Vạn Thái", "Viên An", "Viên Nội" } },
                // Huyện Mỹ Đức
                { "Mỹ Đức", new List<string>() { "Đại Nghĩa", "An Mỹ", "An Phú", "An Tiến", "Bột Xuyên", "Đốc Tín", "Đồng Tâm", "Hồng Sơn", "Hợp Thanh", "Hợp Tiến", "Hùng Tiến", "Hương Sơn", "Lê Thanh", "Mỹ Thành", "Phù Lưu Tế", "Phúc Lâm", "Phùng Xá", "Thượng Lâm", "Tuy Lai", "Vạn Kim", "Xuy Xá" } }
            });
        }
        public async Task LoadItemsOrder()
        {
            PlaceOrderView.Instance.tempOrderItemBindingSource.Clear();
            if (CartItems.Count > 0)
            {
                foreach (var item in CartItems)
                {
                    if (item != null)
                    {
                        AIMS.Models.Entities.Media currentItem = await mediaService.GetMediaByIdAsync(item.media_id);
                        if (currentItem != null)
                        {
                            PlaceOrderView.Instance.tempOrderItemBindingSource.Add(new TempOrderItem()
                            {
                                mediaID = item.media_id,
                                quantity = item.quantity,
                                mediaName = currentItem.title,
                                price = currentItem.price * item.quantity,
                                isReady = currentItem.isEnough(item.quantity),
                                isSupportRushOrder = currentItem.rush_support
                            });
                        }
                    }
                }
            }
        }
        public int countItemIsNotEnough()
        {
            int count = 0;
            foreach (var item in GetOrderItems())
            {
                if (item != null && !item.isReady)
                    count++;
            }
            return count;
        }
        public List<TempOrderItem> GetOrderItems()
        {
            return PlaceOrderView.Instance.tempOrderItemBindingSource.Cast<TempOrderItem>().ToList();
        }
        public int countItemSupportedRushOrder()
        {
            int count = 0;
            foreach (var item in GetOrderItems())
            {
                if (item.isSupportRushOrder)
                    count++;
            }
            return count;
        }
        public string GetStringTotalMoneyFormat()
        {
            return string.Format("{0:N0}", GetTotalMoneyWithVAT());
        }
        public double GetTotalMoneyWithVAT()
        {
            double total = 0;
            foreach (var item in GetOrderItems())
                total += item.price;
            double vatAmount = total * 0.1;
            return total + vatAmount;
        }
    }
}
