using UnityEngine;

public class MissionZone : MonoBehaviour
{
    public bool laDiemGiaoHang = false; // Tích vào nếu đây là điểm Đích
    public ShipperManager quanLyGame; // Kéo GameManager vào đây

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!laDiemGiaoHang && !quanLyGame.dangGiaoHang)
            {
                quanLyGame.NhanDonHang(); // Chạm điểm A -> Nhận đơn
            }
            else if (laDiemGiaoHang && quanLyGame.dangGiaoHang)
            {
                quanLyGame.HoanThanhDonHang(); // Chạm điểm B -> Xong đơn
            }
        }
    }
}