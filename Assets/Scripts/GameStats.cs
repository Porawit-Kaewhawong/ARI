public class GameStats
{
    // ใช้ private เพื่อซ่อนข้อมูล (encapsulation)
    private int score;

    // Public method สำหรับแก้ไขข้อมูลแบบปลอดภัย
    public void AddScore(int amount)
    {
        score += amount;
    }

    // Setter/Getter แบบควบคุม
    public int Score
    {
        get { return score; }     // อ่านค่าได้
        private set { score = value; }  // แต่เขียนจากข้างนอกไม่ได้
    }
}