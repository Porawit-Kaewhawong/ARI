using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform[] zones;
    public float zoneWidth = 8f;
    public float smoothSpeed = 5f;

    private int currentZone = 0;

    void LateUpdate()
    {
        // เก็บค่า Z ของกล้อง
        float zPos = transform.position.z;

        // กำหนด target X จากผู้เล่น
        float targetX = player.position.x;

        // Clamp ตาม Zone
        float minX = zones[currentZone].position.x - zoneWidth;
        float maxX = zones[currentZone].position.x + zoneWidth;
        targetX = Mathf.Clamp(targetX, minX, maxX);

        // สร้าง target position → ใช้ Y ของกล้องเดิม
        Vector3 targetPos = new Vector3(targetX, transform.position.y, zPos);

        // เลื่อนกล้องแบบ Smooth
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }

    public void GoToZone(int zoneIndex)
    {
        currentZone = zoneIndex;
    }
}
