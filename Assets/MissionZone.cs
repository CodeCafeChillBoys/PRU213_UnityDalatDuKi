using UnityEngine;

public class MissionZone : MonoBehaviour
{
    public enum LoaiZone { DiemLayHang, DiemTraHang }

    [Header("Cấu hình Nhiệm vụ")]
    public LoaiZone loaiZone;
    public float tienThuong = 50f;

    // --- BỎ PHẦN KHAI BÁO BIẾN DIEM TIEP THEO VÌ KHÔNG CẦN DÙNG NỮA ---
    // public GameObject diemTiepTheo; 
    // public GameObject diemKhoiDau;  

    // --- BỎ LUÔN HÀM START ---
    // (Vì không cần ẩn điểm B lúc đầu game nữa)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Tìm GameManager xịn
            ShipperManager shipper = FindFirstObjectByType<ShipperManager>();

            if (shipper != null)
            {
                XuLyNhiemVu(shipper);
            }
        }
    }

    void XuLyNhiemVu(ShipperManager shipper)
    {
        // TRƯỜNG HỢP 1: Tại điểm LẤY HÀNG (A)
        if (loaiZone == LoaiZone.DiemLayHang)
        {
            // Chỉ nhận đơn nếu đang rảnh tay
            if (!shipper.dangGiaoHang)
            {
                shipper.NhanDonHang();
                Debug.Log("Đã nhận đơn! Hãy chạy đến điểm giao hàng.");

                // --- ĐÃ XÓA LỆNH ẨN/HIỆN Ở ĐÂY ---
            }
        }
        // TRƯỜNG HỢP 2: Tại điểm TRẢ HÀNG (B)
        else if (loaiZone == LoaiZone.DiemTraHang)
        {
            // Chỉ trả hàng nếu đang có hàng
            if (shipper.dangGiaoHang)
            {
                shipper.HoanThanhDonHang(tienThuong);
                Debug.Log("Giao thành công! Có thể quay lại lấy hàng tiếp.");

                // --- ĐÃ XÓA LỆNH ẨN/HIỆN Ở ĐÂY ---
            }
        }
    }
}