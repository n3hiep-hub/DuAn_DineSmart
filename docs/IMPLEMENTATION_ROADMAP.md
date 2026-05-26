# DineSmart Rebuild Roadmap (WinForms)

## Mục tiêu
Tái cấu trúc app theo 6 module nghiệp vụ và 5 giai đoạn triển khai.

## Trạng thái hiện tại
- Đã có login + phân quyền cơ bản + đặt món + màn hình bếp.
- Đã có `Core` + `Services` cơ bản.
- Đã bắt đầu `Data` layer với repository triển khai trên `AppDbContext` hiện có.

## Giai đoạn 0 (✅ hoàn thành)
- Domain foundation trong `DineSmart.Core` (entities, DTO, interfaces).
- `OrderService` trong `DineSmart.Services` cho luồng đặt món.
- `PollingSyncService` cho đồng bộ realtime kiểu polling.

## Giai đoạn 1 (🔄 đang làm)
- ✅ Cài đặt repository thật trong `DineSmart.Data` (`MenuRepository`, `OrderRepository`, `TableRepository`).
- 🔄 Tích hợp dần form hiện tại vào service layer thay vì gọi DbContext trực tiếp.
- ⏳ Hoàn thiện CRUD danh mục và thực đơn cho module quản lý.

## Giai đoạn 2
- Refactor `frmDatMon` dùng `OrderService.CreateOrderAsync` toàn phần.
- Refactor `frmBep` theo state machine: Chờ -> Đang làm -> Sẵn sàng.
- Bật polling 3 giây cho các màn hình cần realtime.

## Giai đoạn 3
- Module phục vụ (notification + xác nhận đã mang món).
- Module thu ngân (hoá đơn, thanh toán, in bill).

## Giai đoạn 4
- Báo cáo doanh thu ngày/tuần/tháng.
- CRUD thực đơn có ảnh.
- Phân quyền đầy đủ theo role.

## Giai đoạn 5
- Offline fallback và đồng bộ lại.
- Global exception handling.
- Đóng gói triển khai.
