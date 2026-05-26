-- ============================================================
--  DineSmart – MySQL Database Setup
--  Server  : localhost:3306
--  Database: DineSmart
--  User    : root / (no password)
--
--  Cách dùng:
--    mysql -u root < DineSmart_Database.sql
--  hoặc mở file này trong MySQL Workbench / HeidiSQL rồi Execute All.
--
--  Tài khoản mặc định (mật khẩu đều là: 123456)
--    admin      – Quản lý
--    nv01       – Nhân viên
--    bep01      – Bếp
--    thungan01  – Thu ngân
-- ============================================================

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ------------------------------------------------------------
-- 1. DATABASE
-- ------------------------------------------------------------
CREATE DATABASE IF NOT EXISTS `DineSmart`
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE `DineSmart`;

-- ------------------------------------------------------------
-- 2. TABLES
-- ------------------------------------------------------------

CREATE TABLE IF NOT EXISTS `BanAn` (
  `MaBan`     INT          NOT NULL AUTO_INCREMENT,
  `TenBan`    LONGTEXT     NOT NULL,
  `TrangThai` LONGTEXT     NOT NULL,
  PRIMARY KEY (`MaBan`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `NguoiDung` (
  `MaNguoiDung` INT      NOT NULL AUTO_INCREMENT,
  `TenDangNhap` LONGTEXT NOT NULL,
  `MatKhau`     LONGTEXT NOT NULL,
  `VaiTro`      LONGTEXT NOT NULL,
  `TrangThai`   TINYINT(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`MaNguoiDung`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `ThucDon` (
  `MaMon`     INT          NOT NULL AUTO_INCREMENT,
  `TenMon`    LONGTEXT     NOT NULL,
  `DanhMuc`   LONGTEXT     NOT NULL,
  `GiaTien`   DECIMAL(18,2) NOT NULL,
  `TrangThai` TINYINT(1)   NOT NULL DEFAULT 1,
  PRIMARY KEY (`MaMon`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `DonHang` (
  `MaDonHang` INT          NOT NULL AUTO_INCREMENT,
  `MaBan`     INT          NOT NULL,
  `SoMon`     INT          NOT NULL DEFAULT 0,
  `TrangThai` LONGTEXT     NOT NULL,
  `TongTien`  DECIMAL(18,2) NOT NULL DEFAULT 0,
  `ThoiGian`  DATETIME(6)  NOT NULL,
  PRIMARY KEY (`MaDonHang`),
  KEY `IX_DonHang_MaBan` (`MaBan`),
  CONSTRAINT `FK_DonHang_BanAn_MaBan`
    FOREIGN KEY (`MaBan`) REFERENCES `BanAn` (`MaBan`)
    ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `ChiTietDonHang` (
  `MaChiTiet`  INT          NOT NULL AUTO_INCREMENT,
  `MaDonHang`  INT          NOT NULL,
  `MaMon`      INT          NOT NULL,
  `SoLuong`    INT          NOT NULL DEFAULT 1,
  `DonGia`     DECIMAL(18,2) NOT NULL,
  PRIMARY KEY (`MaChiTiet`),
  KEY `IX_ChiTietDonHang_MaDonHang` (`MaDonHang`),
  KEY `IX_ChiTietDonHang_MaMon`     (`MaMon`),
  CONSTRAINT `FK_ChiTietDonHang_DonHang_MaDonHang`
    FOREIGN KEY (`MaDonHang`) REFERENCES `DonHang` (`MaDonHang`)
    ON DELETE CASCADE,
  CONSTRAINT `FK_ChiTietDonHang_ThucDon_MaMon`
    FOREIGN KEY (`MaMon`) REFERENCES `ThucDon` (`MaMon`)
    ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ------------------------------------------------------------
-- 3. SEED DATA – Bàn ăn (10 bàn)
-- Bàn 01-02 đang có khách để test workflow
-- ------------------------------------------------------------
INSERT INTO `BanAn` (`TenBan`, `TrangThai`) VALUES
  ('Bàn 01', 'Có khách'),
  ('Bàn 02', 'Có khách'),
  ('Bàn 03', 'Đặt trước'),
  ('Bàn 04', 'Trống'),
  ('Bàn 05', 'Trống'),
  ('Bàn 06', 'Trống'),
  ('Bàn 07', 'Trống'),
  ('Bàn 08', 'Trống'),
  ('Bàn 09', 'Trống'),
  ('Bàn 10', 'Trống');

-- ------------------------------------------------------------
-- 4. SEED DATA – Thực đơn (14 món, 4 danh mục)
-- ------------------------------------------------------------
INSERT INTO `ThucDon` (`TenMon`, `DanhMuc`, `GiaTien`, `TrangThai`) VALUES
  -- Khai vị
  ('Salad rau trộn',    'Khai vị',      45000, 1),
  ('Súp bí đỏ',         'Khai vị',      35000, 1),
  ('Chả giò chiên',     'Khai vị',      55000, 1),
  -- Món chính
  ('Cơm sườn nướng',    'Món chính',    85000, 1),
  ('Bún bò Huế',        'Món chính',    75000, 1),
  ('Phở bò tái',        'Món chính',    70000, 1),
  ('Cơm gà chiên mắm',  'Món chính',    90000, 1),
  -- Tráng miệng
  ('Bánh flan',         'Tráng miệng',  30000, 1),
  ('Chè đậu xanh',      'Tráng miệng',  25000, 1),
  ('Kem dừa',           'Tráng miệng',  35000, 1),
  -- Đồ uống
  ('Nước cam tươi',     'Đồ uống',      30000, 1),
  ('Trà đá',            'Đồ uống',      10000, 1),
  ('Cà phê sữa đá',     'Đồ uống',      35000, 1),
  ('Sinh tố bơ',        'Đồ uống',      45000, 1);

-- ------------------------------------------------------------
-- 5. SEED DATA – Tài khoản
--    Mật khẩu "123456" đã mã hóa BCrypt (cost=11)
-- ------------------------------------------------------------
SET @pwd = '$2a$11$WGXDDzmyjDJ9ITA6015oFeG9BNedC36IXVGDFaP5FCp1Akh2veDVS';

INSERT INTO `NguoiDung` (`TenDangNhap`, `MatKhau`, `VaiTro`, `TrangThai`) VALUES
  ('admin',     @pwd, 'Quản lý',  1),
  ('nv01',      @pwd, 'Nhân viên',1),
  ('bep01',     @pwd, 'Bếp',      1),
  ('thungan01', @pwd, 'Thu ngân', 1);

-- ------------------------------------------------------------
-- 6. SAMPLE ORDERS – để test full workflow ngay
--    Bàn 01: đơn đang làm (bếp đang nấu)
--    Bàn 02: đơn chờ phục vụ (bếp xong, chờ nhân viên mang ra)
-- ------------------------------------------------------------

-- Đơn #1: Bàn 01 – Đang làm
INSERT INTO `DonHang` (`MaBan`, `SoMon`, `TrangThai`, `TongTien`, `ThoiGian`) VALUES
  (1, 3, 'Đang làm', 0, NOW() - INTERVAL 15 MINUTE);

-- Chi tiết đơn #1
INSERT INTO `ChiTietDonHang` (`MaDonHang`, `MaMon`, `SoLuong`, `DonGia`) VALUES
  (1, 4, 1, 85000),   -- Cơm sườn nướng x1
  (1, 6, 1, 70000),   -- Phở bò tái x1
  (1, 12, 1, 10000);  -- Trà đá x1

-- Đơn #2: Bàn 02 – Chờ phục vụ (bếp đã xong, nhân viên cần mang ra)
INSERT INTO `DonHang` (`MaBan`, `SoMon`, `TrangThai`, `TongTien`, `ThoiGian`) VALUES
  (2, 2, 'Chờ phục vụ', 0, NOW() - INTERVAL 30 MINUTE);

-- Chi tiết đơn #2
INSERT INTO `ChiTietDonHang` (`MaDonHang`, `MaMon`, `SoLuong`, `DonGia`) VALUES
  (2, 5, 1, 75000),   -- Bún bò Huế x1
  (2, 11, 2, 30000);  -- Nước cam tươi x2

-- ------------------------------------------------------------
-- 7. SAMPLE – Đơn đã hoàn thành (để test Dashboard & Báo cáo)
--    Giả sử Bàn 05 vừa trả tiền
-- ------------------------------------------------------------

-- Tạm thời set Bàn 05 về Trống (đã xử lý ở INSERT BanAn)
-- Đơn hoàn thành hôm nay
INSERT INTO `DonHang` (`MaBan`, `SoMon`, `TrangThai`, `TongTien`, `ThoiGian`) VALUES
  (5, 4, 'Hoàn thành', 235000, NOW() - INTERVAL 2 HOUR);

INSERT INTO `ChiTietDonHang` (`MaDonHang`, `MaMon`, `SoLuong`, `DonGia`) VALUES
  (3, 7, 2, 90000),   -- Cơm gà chiên mắm x2
  (3, 13, 2, 35000);  -- Cà phê sữa đá x2

-- Đơn hôm qua (để Báo cáo có dữ liệu nhiều ngày)
-- Salad rau trộn 45k + Bún bò Huế 75k = 120k
INSERT INTO `DonHang` (`MaBan`, `SoMon`, `TrangThai`, `TongTien`, `ThoiGian`) VALUES
  (6, 2, 'Hoàn thành', 120000, NOW() - INTERVAL 1 DAY);

INSERT INTO `ChiTietDonHang` (`MaDonHang`, `MaMon`, `SoLuong`, `DonGia`) VALUES
  (4, 1, 1, 45000),   -- Salad rau trộn x1
  (4, 5, 1, 75000);   -- Bún bò Huế x1

SET FOREIGN_KEY_CHECKS = 1;

-- ============================================================
-- XONG! Trạng thái sau khi import:
--
--   Bàn 01 – Có khách  → đơn #1 đang bếp nấu    (test frmBep)
--   Bàn 02 – Có khách  → đơn #2 chờ phục vụ     (test frmNhanVienBep)
--   Bàn 03 – Đặt trước → chưa có đơn            (test đặt trước)
--   Bàn 04-10 – Trống  → sẵn sàng nhận khách
--
--   Dashboard: doanh thu hôm nay = 235.000đ (đơn #3)
--   Báo cáo  : có dữ liệu 2 ngày (đơn #3 hôm nay, đơn #4 hôm qua)
-- ============================================================
