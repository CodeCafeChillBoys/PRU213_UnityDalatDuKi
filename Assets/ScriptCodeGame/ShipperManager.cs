using UnityEngine;
using UnityEngine.UI;
using TMPro; // QUAN TRỌNG

public class ShipperManager : MonoBehaviour
{
    [Header("THÔNG SỐ NGƯỜI CHƠI")]
    public float tienHienCo = 0f; // Tiền trong ví
    public float doBenXe = 100f;  // Độ bền xe

    [Header("TRẠNG THÁI GIAO HÀNG")]
    public bool dangGiaoHang = false; // Biến kiểm tra đang rảnh hay bận

    [Header("GIAO DIỆN (Kéo thả vào đây)")]
    public TextMeshProUGUI textTienUI; // Kéo Text_TienMat vào đây
    public GameObject chuBayPrefab;    // Kéo Prefab chữ bay (FloatingText) vào đây
    public Transform viTriHienChu;     // Kéo cái Text UI hoặc vị trí trên đầu xe vào đây

    void Start()
    {
        // Cập nhật giao diện ngay khi vào game
        CapNhatTienUI();
    }

    // --- PHẦN 1: NHẬN VÀ TRẢ ĐƠN HÀNG (Gọi từ MissionZone) ---

    // Hàm gọi khi chạm vào điểm Lấy Hàng (A)
    public void NhanDonHang()
    {
        dangGiaoHang = true;
        Debug.Log("Shipper: Đã lấy hàng! Đang tìm nhà khách...");

        // Có thể thêm âm thanh "Ting" nhận đơn ở đây
    }

    // Hàm gọi khi chạm vào điểm Trả Hàng (B)
    // QUAN TRỌNG: Phải có (float tienThuong) để nhận tiền từ MissionZone
    public void HoanThanhDonHang(float tienThuong)
    {
        dangGiaoHang = false; // Trở về trạng thái rảnh
        tienHienCo += tienThuong; // Cộng tiền vào ví

        CapNhatTienUI(); // Cập nhật số mới lên màn hình

        // Hiện hiệu ứng chữ bay (+50k)
        TaoHieuUngBay(tienThuong, true);

        Debug.Log("Shipper: Giao thành công! Nhận được: " + tienThuong);
    }

    // --- PHẦN 2: XỬ LÝ VA CHẠM (Gọi từ AI Car) ---

    // Hàm này được gọi bởi Xe AI khi đâm trúng
    public void BiTruTien(float soTienMat)
    {
        tienHienCo -= soTienMat;
        if (tienHienCo < 0) tienHienCo = 0; // Không cho âm tiền

        CapNhatTienUI(); // Cập nhật số mới lên màn hình

        // Hiện hiệu ứng chữ bay (-20k) -> false nghĩa là bị trừ
        TaoHieuUngBay(soTienMat, false);
    }

    public void BiVaCham(float satThuong)
    {
        doBenXe -= satThuong;
        if (doBenXe < 0) doBenXe = 0;
        // Nếu có thanh máu (Slider) thì cập nhật ở đây
    }

    // --- PHẦN 3: CÁC HÀM HỖ TRỢ (UI & Hiệu ứng) ---

    void CapNhatTienUI()
    {
        if (textTienUI != null)
        {
            // Định dạng hiển thị: Ví dụ 150k
            textTienUI.text = "Ví: " + tienHienCo.ToString("F0") + "k";
        }
    }

    // Hàm tạo chữ bay (Đã nâng cấp để biết là Cộng hay Trừ)
    void TaoHieuUngBay(float soTien, bool laCongTien)
    {
        if (chuBayPrefab != null && viTriHienChu != null)
        {
            // 1. Sinh ra chữ mới
            GameObject chuMoi = Instantiate(chuBayPrefab, viTriHienChu.position, Quaternion.identity, viTriHienChu.parent);

            // 2. Lấy script trên chữ đó
            var script = chuMoi.GetComponent<FloatingText>();

            if (script != null)
            {
                // 3. Gửi lệnh: Số tiền bao nhiêu? Là cộng hay trừ?
                script.HienThiSoTien(soTien, laCongTien);
            }
        }
    }
}