using UnityEngine;

public class NextLevel : MonoBehaviour
{
    // อ้างอิงไปยัง CameraController เพื่อสั่งกล้องไปโซนใหม่
    public CameraController cam;

    // ดัชนีโซนที่กล้องจะไปเมื่อผู้เล่นชน Trigger
    public int zoneIndex;

    // ======================================
    // OnTriggerEnter2D: เรียกเมื่อมี Collider อื่นชน Trigger
    // ตรวจสอบว่าเป็นผู้เล่นหรือไม่
    // ======================================
    void OnTriggerEnter2D(Collider2D other)
    {
        // ถ้า Collider เป็นผู้เล่น → สั่งกล้องไปโซนใหม่
        if (other.CompareTag("Player"))
        {
            cam.GoToZone(zoneIndex); // เรียกฟังก์ชันจาก CameraController (Abstraction)
        }
    }
}