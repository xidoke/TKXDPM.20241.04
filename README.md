# TKXDPM.20241.04
## Team member

| Name            | Role        |
|:----------------| :---------- |
| Lê Hà Anh Đức   | Team Leader |
| Phạm Đình Đô    | Member      |
| Phạm Quốc Đạt   | Member      |
| Lê Văn Tuấn Đạt | Member      |
| Đặng Đình Diệp  | Member      |
# Công nghệ sử dụng:
Cho phần mềm: Winform C# \
Server: Supabase, PostgreSQL
# Hướng dẫn sử dụng: 
Yêu cầu cài đặt: .NET Framework 4.8 Runtime\
File AIMS.exe theo đường dẫn bin/Debug/AIMS.exe\
Hướng dẫn build file exe: Mở file AIMS.csproj bằng Visual Studio Community 2022, sử dụng tổ hợp phím Ctrl + B để build ra phần mềm.
# Mô hình sử dụng để triển khai: Model - View - Controller
# Phân công công việc:
Lê Hà Anh Đức (20215351): Xây dựng, thiết kế giao diện Views, Entities, MediaService, MediaController, tạo Project, tạo kết nối tới Database, kết nối VNPay. \
Phạm Đình Đô (20200154): Tạo server, thiết kế và xây dựng database.\
Phạm Quốc Đạt (20215345): Xây dựng CartController.\
Lê Văn Tuấn Đạt (20215341): Xây dựng PlaceOrderController và OrderController.\
Đặng Đình Diệp (20183495): Thiết kế và hoàn thiện tài liệu.



## Report Content
<details>
  <summary>W2: 15/12/2024~21/12/2024 </summary>
<br>
<details>
<summary>Phạm Đình Đô</summary>
<br>
# Phân tích SOLID, Cohesion và Coupling

## 1. CartController.cs

### Vi phạm nguyên tắc SOLID:

- Single Responsibility Principle (SRP): CartController đang xử lý quá nhiều trách nhiệm như quản lý giỏ hàng, lưu/tải dữ liệu, cập nhật UI, và xử lý logic nghiệp vụ. Nên tách thành các lớp riêng biệt cho mỗi trách nhiệm.

- Open/Closed Principle (OCP): Lớp này không dễ dàng mở rộng mà không cần sửa đổi. Ví dụ, nếu muốn thêm một loại sản phẩm mới, có thể phải sửa đổi code hiện tại.

- Dependency Inversion Principle (DIP): CartController đang phụ thuộc trực tiếp vào các lớp cụ thể như CartView thay vì các interface.

### Vấn đề về Cohesion:

- Lớp này có cohesion thấp vì đang thực hiện nhiều chức năng không liên quan chặt chẽ với nhau như quản lý giỏ hàng, xử lý UI, và lưu trữ dữ liệu.

### Vấn đề về Coupling:

- High Coupling: CartController có coupling cao với CartView và các lớp khác, làm giảm tính module hóa và khó bảo trì.

## 2. MediaController.cs

### Vi phạm nguyên tắc SOLID:

- SRP: Tương tự CartController, MediaController đang xử lý quá nhiều trách nhiệm.
- OCP: Khó mở rộng để hỗ trợ các loại media mới mà không sửa đổi code hiện tại.
- DIP: Phụ thuộc trực tiếp vào các lớp cụ thể như MediaService thay vì abstraction.

### Vấn đề về Cohesion:

- Cohesion thấp do xử lý nhiều chức năng không liên quan chặt chẽ (quản lý media, xử lý UI, tải dữ liệu).

### Vấn đề về Coupling:

- Coupling cao với các lớp UI và service, làm giảm tính linh hoạt và khó thay đổi.

## 3. Cart.cs

### Vi phạm nguyên tắc SOLID:

- SRP: Cart đang xử lý cả logic nghiệp vụ và kiểm tra tính khả dụng của sản phẩm.
- OCP: Khó mở rộng để hỗ trợ các loại giỏ hàng khác nhau mà không sửa đổi lớp hiện tại.

### Vấn đề về Cohesion:

- Cohesion tương đối tốt, nhưng có thể cải thiện bằng cách tách biệt logic kiểm tra tính khả dụng của sản phẩm.

### Vấn đề về Coupling:

- Coupling với lớp CartMedia, có thể cải thiện bằng cách sử dụng interface.

## Đề xuất cải thiện:

1. Tách các trách nhiệm thành các lớp riêng biệt (ví dụ: CartService, CartRepository, CartViewModel).
2. Sử dụng các interface để giảm coupling và tăng tính linh hoạt.
3. Áp dụng nguyên tắc Dependency Injection để giảm sự phụ thuộc trực tiếp giữa các lớp.
4. Tạo các abstract class hoặc interface để hỗ trợ việc mở rộng trong tương lai mà không cần sửa đổi code hiện tại.
5. Tách logic nghiệp vụ khỏi các lớp controller và đưa vào các service riêng biệt.

Bằng cách áp dụng các nguyên tắc SOLID và cải thiện cohesion, coupling, code sẽ trở nên dễ bảo trì, mở rộng và tái sử dụng hơn.
</details>
</details>