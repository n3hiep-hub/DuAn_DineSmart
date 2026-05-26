using DuAn_DineSmart.DAL;
using DuAn_DineSmart.Models;
using DineSmart.Core.DTOs;
using DineSmart.Data.Adapters;
using DineSmart.Data.Repositories;
using DineSmart.Services.Orders;
using DineSmart.Services.Sync;
using System.Text.Json;

namespace DuAn_DineSmart.Forms
{
    public partial class frmDatMon : Form
    {
        private readonly DashboardDAL _dalBan = new();
        private readonly OrderService _orderService;
        private List<ThucDon> _dsMonAn = new();
        private readonly Dictionary<int, (ThucDon mon, int soLuong)> _gioMon = new();
        private int _maBanChon = -1;
        private string _filterDanhMuc = "Tất cả";
        private readonly string _offlinePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "offline-orders.json");

        public frmDatMon()
        {
            InitializeComponent();

            var dbFactory = new AppDbContextFactory();
            _orderService = new OrderService(
                new OrderRepository(dbFactory),
                new MenuRepository(dbFactory),
                new TableRepository(dbFactory),
                new NoOpNotificationService());
        }

        private void frmDatMon_Load(object sender, EventArgs e)
        {
            LoadBanAn();
            LoadThucDon();
            lbBanAn.SelectedIndexChanged += LbBanAn_Changed;
            btnGuiBep.Click += BtnGuiBep_Click;
            btnXoaTat.Click += (s, ev) => { _gioMon.Clear(); RefreshGio(); };
            TrySyncOfflineOrders();
        }

        private void LoadBanAn()
        {
            lbBanAn.Items.Clear();
            var dsBan = _dalBan.GetAllBan();
            foreach (var b in dsBan)
                lbBanAn.Items.Add($"{b.TenBan} – {b.TrangThai}");
            lbBanAn.Tag = dsBan;
        }

        private void LbBanAn_Changed(object? sender, EventArgs e)
        {
            if (lbBanAn.Tag is not List<BanAn> dsBan) return;
            int idx = lbBanAn.SelectedIndex;
            if (idx < 0) return;
            var ban = dsBan[idx];
            _maBanChon = ban.MaBan;
            lblBanHienTai.Text = $"{ban.TenBan} – {ban.TrangThai}";
            lblTitleGio.Text = $"GIỎ MÓN – {ban.TenBan}";
            _gioMon.Clear();
            RefreshGio();
        }

        private void LoadThucDon()
        {
            using var db = new AppDbContext();
            _dsMonAn = db.ThucDons.Where(m => m.TrangThai).ToList();

            pnlDanhMuc.Controls.Clear();
            var danhMucs = new[] { "Tất cả" }
                .Concat(_dsMonAn.Select(m => m.DanhMuc).Distinct())
                .ToList();

            int x = 12;
            foreach (var dm in danhMucs)
            {
                var btn = new Button
                {
                    Text = dm,
                    Size = new Size(100, 28),
                    Location = new Point(x, 6),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9),
                    Tag = dm
                };
                btn.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
                SetCatBtnStyle(btn, dm == _filterDanhMuc);
                btn.Click += (s, ev) =>
                {
                    _filterDanhMuc = btn.Tag?.ToString() ?? "Tất cả";
                    foreach (Control c in pnlDanhMuc.Controls)
                        if (c is Button b) SetCatBtnStyle(b, b.Tag?.ToString() == _filterDanhMuc);
                    RenderMonAn();
                };
                pnlDanhMuc.Controls.Add(btn);
                x += 108;
            }
            RenderMonAn();
        }

        private void SetCatBtnStyle(Button btn, bool active)
        {
            btn.BackColor = active ? Color.FromArgb(192, 57, 43) : Color.White;
            btn.ForeColor = active ? Color.White : Color.FromArgb(80, 80, 80);
        }

        private void RenderMonAn()
        {
            flpMonAn.Controls.Clear();
            var ds = _filterDanhMuc == "Tất cả"
                ? _dsMonAn
                : _dsMonAn.Where(m => m.DanhMuc == _filterDanhMuc).ToList();

            foreach (var mon in ds)
            {
                var card = new Panel
                {
                    Size = new Size(140, 90),
                    Margin = new Padding(6),
                    BackColor = Color.White,
                    Tag = mon
                };
                card.Paint += (s, e) =>
                    ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                        Color.FromArgb(220, 220, 220), ButtonBorderStyle.Solid);

                var lblTen = new Label { Text = mon.TenMon, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(50, 50, 50), Location = new Point(8, 8), Size = new Size(124, 32) };
                var lblGia = new Label { Text = mon.GiaTien.ToString("N0") + "đ", Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(192, 57, 43), Location = new Point(8, 40), AutoSize = true };
                var btnThem = new Button { Text = "+ Thêm", Size = new Size(124, 26), Location = new Point(8, 58), FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(192, 57, 43), ForeColor = Color.White, Font = new Font("Segoe UI", 8), Tag = mon };
                btnThem.FlatAppearance.BorderSize = 0;
                btnThem.Click += BtnThem_Click;

                card.Controls.AddRange(new Control[] { lblTen, lblGia, btnThem });
                flpMonAn.Controls.Add(card);
            }
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            if (_maBanChon < 0) { MessageBox.Show("Vui lòng chọn bàn trước!", "Thông báo"); return; }
            if (sender is Button btn && btn.Tag is ThucDon mon)
            {
                if (_gioMon.ContainsKey(mon.MaMon)) _gioMon[mon.MaMon] = (mon, _gioMon[mon.MaMon].soLuong + 1);
                else _gioMon[mon.MaMon] = (mon, 1);
                RefreshGio();
            }
        }

        private void RefreshGio()
        {
            lvGioMon.Items.Clear();
            decimal tong = 0;
            foreach (var kv in _gioMon)
            {
                var (mon, sl) = kv.Value;
                decimal thanh = mon.GiaTien * sl;
                tong += thanh;
                var item = new ListViewItem(mon.TenMon);
                item.SubItems.Add(sl.ToString());
                item.SubItems.Add(thanh.ToString("N0") + "đ");
                item.Tag = kv.Key;
                lvGioMon.Items.Add(item);
            }
            lblTongTien.Text = $"Tổng: {tong:N0}đ";
        }

        private async void BtnGuiBep_Click(object? sender, EventArgs e)
        {
            if (_maBanChon < 0) { MessageBox.Show("Vui lòng chọn bàn!"); return; }
            if (_gioMon.Count == 0) { MessageBox.Show("Giỏ món đang trống!"); return; }

            var request = new CreateOrderRequest
            {
                TableId = _maBanChon,
                Items = _gioMon.Values.Select(v => new CreateOrderItemRequest { MenuItemId = v.mon.MaMon, Quantity = v.soLuong }).ToList()
            };

            try
            {
                int maDon = await _orderService.CreateOrderAsync(request);
                MessageBox.Show($"Đã gửi bếp thành công! Mã đơn: #{maDon}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Không thể gửi bếp: {ex.Message}", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex) when (IsOfflineFallbackCandidate(ex))
            {
                SaveOfflineOrder(request);
                MessageBox.Show("Mất kết nối DB. Đơn đã được lưu offline và sẽ đồng bộ lại khi có mạng.", "Offline fallback", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _gioMon.Clear();
            RefreshGio();
            LoadBanAn();
        }

        private static bool IsOfflineFallbackCandidate(Exception ex)
        {
            for (Exception? current = ex; current != null; current = current.InnerException)
            {
                if (current is TimeoutException || current is IOException)
                {
                    return true;
                }

                string exceptionName = current.GetType().Name;
                if (exceptionName.Contains("SqlException", StringComparison.OrdinalIgnoreCase) ||
                    exceptionName.Contains("DbException", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private void SaveOfflineOrder(CreateOrderRequest request)
        {
            var list = LoadOfflineOrders();
            list.Add(request);
            File.WriteAllText(_offlinePath, JsonSerializer.Serialize(list));
        }

        private List<CreateOrderRequest> LoadOfflineOrders()
        {
            if (!File.Exists(_offlinePath)) return new List<CreateOrderRequest>();
            try
            {
                return JsonSerializer.Deserialize<List<CreateOrderRequest>>(File.ReadAllText(_offlinePath)) ?? new List<CreateOrderRequest>();
            }
            catch
            {
                return new List<CreateOrderRequest>();
            }
        }

        private async void TrySyncOfflineOrders()
        {
            var list = LoadOfflineOrders();
            if (list.Count == 0) return;

            var remain = new List<CreateOrderRequest>();
            var hasInvalidOrder = false;
            foreach (var req in list)
            {
                try
                {
                    await _orderService.CreateOrderAsync(req);
                }
                catch (InvalidOperationException)
                {
                    hasInvalidOrder = true;
                }
                catch (Exception ex) when (IsOfflineFallbackCandidate(ex))
                {
                    remain.Add(req);
                }
                catch
                {
                    hasInvalidOrder = true;
                }
            }

            if (remain.Count == 0)
            {
                if (File.Exists(_offlinePath)) File.Delete(_offlinePath);
                string message = hasInvalidOrder
                    ? "Đã đồng bộ các đơn offline hợp lệ. Một số đơn lỗi dữ liệu đã bị bỏ qua."
                    : "Đã đồng bộ thành công các đơn offline.";
                MessageBox.Show(message, "Đồng bộ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                File.WriteAllText(_offlinePath, JsonSerializer.Serialize(remain));
            }
        }
    }
}
