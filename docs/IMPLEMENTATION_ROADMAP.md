# DineSmart Rebuild Roadmap (WinForms)

## Mục tiêu
Tái cấu trúc app theo 6 module nghiệp vụ và 5 giai đoạn triển khai.

## Trạng thái hiện tại
- Đã có login + phân quyền cơ bản + đặt món + màn hình bếp.
- Chưa tách rõ kiến trúc Core/Data/Services.
- Chưa có cơ chế đồng bộ chuẩn hoá (SignalR/polling service tách riêng).

## Giai đoạn 0 (đã khởi tạo trong commit này)
- Tạo nền tảng domain trong `DineSmart.Core` (entities, DTO, interfaces).
- Tạo `OrderService` trong `DineSmart.Services` để gom luồng nghiệp vụ đặt món.
- Tạo `PollingSyncService` làm nền đồng bộ realtime kiểu polling.

## Giai đoạn 1
- Cài đặt Repository thật trong `DineSmart.Data` (EF Core).
- Kết nối form hiện tại vào service layer thay vì gọi DbContext trực tiếp.
- Hoàn thiện CRUD danh mục và thực đơn cho module quản lý.

## Giai đoạn 2
- Refactor `frmDatMon` dùng `OrderService.CreateOrderAsync`.
- Refactor `frmBep` theo state machine đơn hàng: Chờ -> Đang làm -> Sẵn sàng.
- Bật polling 3 giây cho các màn hình cần realtime.

## Giai đoạn 3
- Tạo module phục vụ (notification + xác nhận đã mang món).
- Tạo module thu ngân (hoá đơn, thanh toán, in bill).

## Giai đoạn 4
- Báo cáo doanh thu ngày/tuần/tháng.
- CRUD thực đơn có ảnh.
- Phân quyền đầy đủ theo role.

## Giai đoạn 5
- Offline fallback và đồng bộ lại.
- Global exception handling.
- Đóng gói triển khai.
