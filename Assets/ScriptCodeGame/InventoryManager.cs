using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Kéo 5 cái Slot (từ 1 đến 5) vào đây")]
    public Transform[] slots;

    [Header("Kéo cục màu xanh Pizza Prefab vào đây")]
    public GameObject pizzaPrefab;

    private bool[] isFull;

    // THÊM CÁI NÀY: Một danh sách để lưu chính xác miếng bánh nào đang nằm ở đâu
    private GameObject[] itemTrongO;

    void Start()
    {
        isFull = new bool[slots.Length];
        itemTrongO = new GameObject[slots.Length]; // Khởi tạo bộ nhớ
    }

    public void NhatPizzaVaoTui()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (isFull[i] == false)
            {
                isFull[i] = true;

                // Lệnh Instatiate đẻ ra miếng bánh, ĐỒNG THỜI lưu nó vào bộ nhớ luôn!
                itemTrongO[i] = Instantiate(pizzaPrefab, slots[i], false);

                Debug.Log("Đã nhét Pizza vào ô: " + slots[i].name);
                break;
            }
        }
    }

    public void XoaPizzaKhoiTui()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (isFull[i] == true)
            {
                // Kiểm tra xem bộ nhớ có đang giữ cái bánh nào không
                if (itemTrongO[i] != null)
                {
                    // Phá hủy CHÍNH XÁC cái bánh đó (không sợ xóa nhầm khung viền nữa)
                    Destroy(itemTrongO[i]);
                }

                isFull[i] = false; // Trả lại ô trống

                Debug.Log("Đã giao bánh! Ô " + slots[i].name + " đã trống rỗng.");
                break;
            }
        }
    }
}