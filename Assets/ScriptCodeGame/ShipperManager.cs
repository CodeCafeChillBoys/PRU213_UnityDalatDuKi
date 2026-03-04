using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShipperManager : MonoBehaviour
{
    [Header("THÔNG SỐ NGƯỜI CHƠI")]
    public float tienHienCo = 0f;
    public float doBenXe = 100f;
    private float thoiGianHoiPhuc = 0;

    [Header("TRẠNG THÁI GIAO HÀNG")]
    public bool dangGiaoHang = false;

    [Header("GIAO DIỆN (Kéo thả vào đây)")]
    public TextMeshProUGUI textTienUI;
    public GameObject chuBayPrefab;
    public Transform viTriHienChu;
    public Canvas mainCanvas;
    public InventoryManager tuiDo;

    // --- THÊM DÒNG NÀY ĐỂ KẾT NỐI VỚI HỆ THỐNG NHIỆM VỤ ---
    public MissionManager heThongNhiemVu;

    void Start()
    {
        CapNhatTienUI();
    }

    public void NhanDonHang()
    {
        dangGiaoHang = true;
        Debug.Log("Shipper: Đã lấy hàng! Đang tìm nhà khách...");

        if (tuiDo != null)
        {
            tuiDo.NhatPizzaVaoTui();
        }

        // --- GỌI HỆ THỐNG: Bật mũi tên và hiện Khách Hàng ---
        if (heThongNhiemVu != null)
        {
            heThongNhiemVu.TrangThaiDiGiaoHang();
        }
    }

    public void HoanThanhDonHang(float tienThuong)
    {
        dangGiaoHang = false;
        tienHienCo += tienThuong;

        CapNhatTienUI();
        TaoHieuUngBay(tienThuong, true);

        Debug.Log("Shipper: Giao thành công! Nhận được: " + tienThuong);

        if (tuiDo != null)
        {
            tuiDo.XoaPizzaKhoiTui();
        }

        // --- GỌI HỆ THỐNG: Tắt mũi tên và tạo Cửa hàng mới ---
        if (heThongNhiemVu != null)
        {
            heThongNhiemVu.TrangThaiChoNhanDon();
        }
    }

    public void BiTruTien(float soTienMat)
    {
        if (Time.time < thoiGianHoiPhuc) return;

        tienHienCo -= soTienMat;
        if (tienHienCo < 0) tienHienCo = 0;

        CapNhatTienUI();
        TaoHieuUngBay(soTienMat, false);

        thoiGianHoiPhuc = Time.time + 5f;
        Debug.Log("Bị trừ tiền! Đang bất tử trong 5s...");
    }

    public void BiVaCham(float satThuong)
    {
        doBenXe -= satThuong;
        if (doBenXe < 0) doBenXe = 0;
    }

    void CapNhatTienUI()
    {
        if (textTienUI != null)
        {
            // Đã sửa lại lỗi text = text = ở đây
            textTienUI.text = tienHienCo.ToString("N0") + " VND";
        }
    }

    void TaoHieuUngBay(float soTien, bool laCongTien)
    {
        if (chuBayPrefab != null && textTienUI != null)
        {
            GameObject textMoi = Instantiate(chuBayPrefab, textTienUI.transform.parent);
            textMoi.transform.position = textTienUI.transform.position;
            textMoi.transform.localPosition += new Vector3(0, 50, 0);

            FloatingText scriptText = textMoi.GetComponent<FloatingText>();
            if (scriptText != null)
            {
                scriptText.HienThiSoTien(soTien, laCongTien);
            }
        }
    }
}