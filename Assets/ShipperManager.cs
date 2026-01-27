using UnityEngine;
using UnityEngine.UI;
using TMPro; // QUAN TRỌNG

public class ShipperManager : MonoBehaviour
{
    [Header("THÔNG SỐ")]
    public float tienHienCo = 0f; // Thử sửa thành 100 để test
    public float doBenSua = 100f; // Code cũ

    [Header("GIAO DIỆN (Kéo thả vào đây)")]
    public TextMeshProUGUI textTienUI; // Kéo Text_TienMat vào đây
    public GameObject chuBayPrefab;    // Kéo file MauChuBay (màu xanh) vào đây
    public Transform viTriHienChu;     // Kéo Text_TienMat vào đây (để chữ bay ra từ ví)

    // --- Code Giữ Cho MissionZone Không Bị Lỗi ---
    public bool dangGiaoHang = false;
    public void NhanDonHang() { dangGiaoHang = true; }
    public void HoanThanhDonHang() { dangGiaoHang = false; }
    public void BiVaCham(float satThuong) { doBenSua -= satThuong; }
    // ---------------------------------------------

    void Start()
    {
        CapNhatTienUI();
    }

    // Hàm này được gọi bởi AI khi đâm trúng
    public void BiTruTien(float soTienMat)
    {
        tienHienCo -= soTienMat;
        if (tienHienCo < 0) tienHienCo = 0; // Không cho âm tiền

        CapNhatTienUI(); // Cập nhật số mới lên màn hình
        TaoHieuUngBay(soTienMat); // Gọi chữ bay lên
    }

    void CapNhatTienUI()
    {
        if (textTienUI != null)
        {
            textTienUI.text = "Ví: " + tienHienCo + "k";
        }
    }

    void TaoHieuUngBay(float soTien)
    {
        if (chuBayPrefab != null && viTriHienChu != null)
        {
            // Sinh ra chữ mới
            GameObject chuMoi = Instantiate(chuBayPrefab, viTriHienChu.position, Quaternion.identity, viTriHienChu.parent);

            // Set nội dung số tiền
            var script = chuMoi.GetComponent<FloatingText>();
            if (script != null) script.HienThiSoTien(soTien);
        }
    }
}