using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public CameraController cam;
    public int zoneIndex;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cam.GoToZone(zoneIndex);
        }
    }
}
