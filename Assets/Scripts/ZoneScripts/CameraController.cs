using UnityEngine;

public class CameraController : MonoBehaviour
{
    // ================================
    // Transform ของผู้เล่น → กล้องจะตามผู้เล่น
    // ================================
    public Transform player;

    // Transform ของโซนต่าง ๆ → ใช้กำหนดขอบเขตกล้อง
    public Transform[] zones;

    // ครึ่งความกว้างของโซน (ใช้ Clamp)
    public float zoneWidth = 8f;

    // ความเร็วการเคลื่อนกล้องแบบ Smooth
    public float smoothSpeed = 5f;

    // ดัชนีโซนปัจจุบัน
    private int currentZone = 0;

    // ======================================
    // LateUpdate: เรียกทุกเฟรมหลัง Update
    // เหมาะสำหรับกล้องเพื่อติดตามผู้เล่น
    // ======================================
    void LateUpdate()
    {
        // เก็บค่า Z ของกล้อง
        float zPos = transform.position.z;

        // กำหนด target X จากตำแหน่งผู้เล่น
        float targetX = player.position.x;

        // Clamp targetX ตามขอบเขตของโซนปัจจุบัน
        float minX = zones[currentZone].position.x - zoneWidth;
        float maxX = zones[currentZone].position.x + zoneWidth;
        targetX = Mathf.Clamp(targetX, minX, maxX);

        // สร้างตำแหน่งเป้าหมายของกล้อง → ใช้ Y และ Z เดิม
        Vector3 targetPos = new Vector3(targetX, transform.position.y, zPos);

        // เลื่อนกล้องแบบ Smooth ด้วย Lerp
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }

    // ======================================
    // GoToZone: เปลี่ยนโซนปัจจุบันของกล้อง
    // สามารถเรียกจาก Script อื่นเพื่อสั่งกล้องไปยังโซน
    // ======================================
    public void GoToZone(int zoneIndex)
    {
        currentZone = zoneIndex;
    }
}