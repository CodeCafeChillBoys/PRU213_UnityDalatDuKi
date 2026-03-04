using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [Header("--- GIAI ĐOẠN 1: TÌM CỬA HÀNG LẤY BÁNH ---")]
    public GameObject npcLayHang; // Kéo TramNhanPizza vào đây
    public Transform[] danhSachViTriLayHang; // Kéo các điểm cửa hàng có thể đứng

    [Header("--- GIAI ĐOẠN 2: TÌM KHÁCH GIAO BÁNH ---")]
    public GameObject khachHang; // Kéo DiemGiaoHang vào đây
    public Transform[] danhSachNhaKhach; // Kéo các điểm khách có thể đứng

    [Header("--- LA BÀN ---")]
    public ChiDuong muiTenChiDuong; // Kéo MuiTenLaBan vào đây

    void Start()
    {
        // Vừa vào game: Chưa có đơn -> Ẩn mũi tên, chờ người chơi tự tìm đến cửa hàng
        TrangThaiChoNhanDon();
    }

    public void TrangThaiChoNhanDon()
    {
        // 1. TẮT mũi tên và TẮT khách hàng
        if (muiTenChiDuong != null) muiTenChiDuong.gameObject.SetActive(false);
        if (khachHang != null) khachHang.SetActive(false);

        // 2. Random vị trí Cửa hàng và BẬT nó lên để người chơi tới lấy
        if (danhSachViTriLayHang.Length > 0 && npcLayHang != null)
        {
            int viTri = Random.Range(0, danhSachViTriLayHang.Length);
            npcLayHang.transform.position = danhSachViTriLayHang[viTri].position;
            npcLayHang.SetActive(true);
            Debug.Log("Cửa hàng đã mở tại: " + danhSachViTriLayHang[viTri].name);
        }
    }

    public void TrangThaiDiGiaoHang()
    {
        // Đã khóa lệnh tắt cửa hàng, bây giờ nó sẽ luôn hiện!
        // if (npcLayHang != null) npcLayHang.SetActive(false); 

        // 2. Random vị trí Khách hàng và BẬT nó lên
        if (danhSachNhaKhach.Length > 0 && khachHang != null)
        {
            int viTri = Random.Range(0, danhSachNhaKhach.Length);
            khachHang.transform.position = danhSachNhaKhach[viTri].position;
            khachHang.SetActive(true);
            Debug.Log("Khách hàng đang chờ ở: " + danhSachNhaKhach[viTri].name);
        }

        // 3. BẬT mũi tên lên và trỏ nó vào Khách hàng
        if (muiTenChiDuong != null)
        {
            muiTenChiDuong.gameObject.SetActive(true);
            muiTenChiDuong.mucTieu = khachHang.transform;
        }
    }
}