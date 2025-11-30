using UnityEngine;

public class PlayerMove2D : MonoBehaviour
{
    // ความเร็วในการเดินของผู้เล่น
    public float moveSpeed = 5f;

    // แรงกระโดดของผู้เล่น
    public float jumpForce = 7f;

    // ตัวเก็บ Rigidbody2D ของ Player
    private Rigidbody2D rb;

    // เก็บสถานะว่าตัวละครหันขวาอยู่หรือไม่
    private bool facingRight = true;

    [Header("Ground Check")]
    // จุดเช็คพื้นว่าจะยืนอยู่บนพื้นไหม
    public Transform groundCheck;

    // รัศมีของวงกลมเช็คพื้น
    public float checkRadius = 0.1f;

    // เลเยอร์ที่ถือว่าเป็นพื้น
    public LayerMask groundLayer;

    // สถานะว่าตัวละครยืนบนพื้นหรือไม่
    private bool isGrounded;


    // ======================================
    // Start: เรียกเมื่อเริ่มเกม
    // ใช้ดึง Rigidbody2D ของ Player มาเก็บไว้
    // ======================================
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // ======================================
    // Update: ทำงานทุกเฟรม
    // รวมการเดิน กระโดด และเช็คการพลิกตัว
    // ======================================
    void Update()
    {
        // เช็คว่าติดพื้นหรือยัง โดยสร้างวงกลมตรวจใต้เท้า
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        // รับค่าการเดินจากปุ่ม A/D หรือ ซ้าย/ขวา
        float move = Input.GetAxis("Horizontal");

        // เปลี่ยนความเร็วในแนวนอน
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        // ถ้าเดินไปขวาแต่ตัวหันซ้ายอยู่ → พลิกตัว
        if (move > 0 && !facingRight) Flip();
        // ถ้าเดินไปซ้ายแต่ตัวหันขวาอยู่ → พลิกตัว
        else if (move < 0 && facingRight) Flip();

        // กด Space และต้องยืนอยู่บนพื้น → กระโดด
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }


    // ======================================
    // Flip: สลับด้านตัวละคร
    // ทำโดยการคูณ scale.x ด้วย -1
    // ======================================
    void Flip()
    {
        facingRight = !facingRight; // เปลี่ยนสถานะว่าตัวหันทางไหน
        Vector3 scale = transform.localScale;
        scale.x *= -1;              // พลิกแกน X
        transform.localScale = scale;
    }


    // ======================================
    // OnDrawGizmosSelected:
    // วาด Gizmo วงกลมเพื่อเช็คพื้นใน Scene View
    // ช่วยให้เห็นตำแหน่ง groundCheck ตอนปรับค่าต่างๆ
    // ======================================
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }
}