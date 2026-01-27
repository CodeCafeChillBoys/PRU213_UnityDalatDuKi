using UnityEngine;
using TMPro; // Bắt buộc để dùng TextMeshPro

public class FloatingText : MonoBehaviour
{
    public float tocDoBay = 50f;
    public float thoiGianSong = 1.5f;

    void Start()
    {
        // Tự sát sau 1.5 giây
        Destroy(gameObject, thoiGianSong);
    }

    void Update()
    {
        // Bay lên trên
        transform.Translate(Vector3.up * tocDoBay * Time.deltaTime);
    }

    public void HienThiSoTien(float soTien)
    {
        // Sửa nội dung chữ
        GetComponent<TextMeshProUGUI>().text = "-" + soTien + "k";
    }
}