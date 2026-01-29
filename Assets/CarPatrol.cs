using UnityEngine;
using UnityEngine.AI;

public class CarPatrol : MonoBehaviour
{
    [Header("Cấu hình di chuyển")]
    public Transform diemA;
    public Transform diemB;

    [Header("Cấu hình Phạt Tiền")]
    public int soTienPhat = 100; // Số tiền muốn trừ
    // Kéo script quản lý tiền của bạn vào đây (nếu có), ví dụ: GameManager
    // public GameManager gameManager; 

    private NavMeshAgent agent;
    private Transform diemDenHienTai;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Bắt đầu chạy đến điểm A
        diemDenHienTai = diemA;
        if (diemA != null) agent.SetDestination(diemA.position);
    }

    void Update()
    {
        // Logic tuần tra: Đến nơi thì đổi chiều
        if (!agent.pathPending && agent.remainingDistance < 2.0f)
        {
            DoiMucTieu();
        }
    }

    void DoiMucTieu()
    {
        if (diemDenHienTai == diemA) diemDenHienTai = diemB;
        else diemDenHienTai = diemA;

        if (diemDenHienTai != null) agent.SetDestination(diemDenHienTai.position);
    }

    // --- PHẦN TRỪ TIỀN NẰM Ở ĐÂY ---
    private void OnTriggerEnter(Collider other)
    {
        // 1. Kiểm tra xem có đâm trúng Nhân vật (Player) không?
        if (other.CompareTag("Player"))
        {
            Debug.Log("Đâm trúng người chơi! Đang gọi trừ tiền...");

            // 2. Tìm cái ông quản lý tiền (ShipperManager) trong game
            // (Lệnh này sẽ tìm bất kỳ vật thể nào có gắn script ShipperManager)
            ShipperManager manager = FindFirstObjectByType<ShipperManager>();

            // 3. Nếu tìm thấy thì phạt tiền
            if (manager != null)
            {
                manager.BiTruTien(soTienPhat); // Gọi hàm trừ tiền bên kia
                Debug.Log("Đã trừ thành công: " + soTienPhat);
            }
            else
            {
                Debug.LogError("LỖI: Không tìm thấy ShipperManager trong Scene!");
            }

            // 4. (Tùy chọn) Đẩy lùi người chơi ra cho giống thật
            Rigidbody rbNguoiChoi = other.GetComponent<Rigidbody>();
            if (rbNguoiChoi != null)
            {
                Vector3 huongDay = (other.transform.position - transform.position).normalized;
                rbNguoiChoi.AddForce(huongDay * 500f + Vector3.up * 200f);
            }
        }
    }
}