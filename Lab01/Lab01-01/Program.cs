using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01_01
{
    class Program
    {
        /// <summary>
        /// Bài tập 1: Viết chương trình trò chơi đoán số được mô tả như sau:
        /// - Máy tính sẽ phát sinh ngẫu nhiên một số có 3 chữ số từ 100 đến 999
        /// - Người chơi sẽ đoán số này bằng cách nhập vào một số có 3 chữ số.
        /// - Sau mỗi lần đoán, máy tính sẽ phản hồi dựa trên số đã đoán của người chơi:
        ///     * Dấu '+' được sử dụng để chỉ ra rằng một chữ số trong số đoán của người chơi
        ///       là chính xác và nằm ở đúng vị trí tương ứng.
        ///     * Dấu '?' được sử dụng để chỉ ra rằng một chữ số trong số đoán của người chơi
        ///       máy tính đã phát sinh.
        ///     * Các vị trí còn lại sẽ không có phản hồi nào.Trường hợp có các số đoán có chữ
        ///       số trùng nhau thì máy tính vẫn phản hồi ở tất cả các vị trí số đã đoán.
        /// - Thông báo chiến thắng/thất bại sau 7 lần đoán tối đa ?
        /// </summary>
        static void Main()
        {
            // Hiển thị ra màn hình "=== Chuong trinh doan so ==="
            Console.WriteLine("=== Chuong trinh doan so ===");

            // Tạo một số nguyên ngẫu nhiên trong khoảng từ 100 đến 999
            Random random = new Random();
            int targetNumber = random.Next(100, 999);
            // Chuyển số thành dạng chuỗi
            string targetString = targetNumber.ToString();

            // <attempt>: Số lần dự đoán
            // <MAX_GUESS>: Số lần dự đoán tối đa
            int attempt = 1, MAX_GUESS = 7;

            // <guess>: Dự đoán của người chơi
            // <feedback>: Phản hồi của máy tính
            string guess, feedback = string.Empty;

            // Bắt đầu trò chơi
            while (feedback != "+++" && attempt <= MAX_GUESS)
            {
                // Nhận dự đoán từ người dùng
                Console.Write("Lan doan thu {0}: ", attempt);
                guess = Console.ReadLine();

                // Xử lý kết quả phản hồi của máy tính
                feedback = GetFeedback(targetString, guess);
                Console.WriteLine("Phan hoi tu may tinh: {0}\n", feedback);

                // Tăng số lần dự đoán lên 1
                attempt++;
            }

            Console.WriteLine("Nguoi choi da doan {0} lan. Tro choi ket thuc!", attempt - 1);
            if (attempt > MAX_GUESS)
                Console.WriteLine("Nguoi choi thua cuoc. So can doan la: {0}", targetNumber);
            else
                Console.WriteLine("Nguoi choi thang cuoc!", attempt);

            Console.ReadLine();
        }

        private static string GetFeedback(string target, string guess)
        {
            string feedback = string.Empty;
            for (int i = 0; i < target.Length; i++)
            {
                if (target[i] == guess[i]) // Đoán đúng số, đúng vị trí
                    feedback += "+";
                else if (target.Contains(guess[i])) // Đoán đúng số, sai vị trí
                    feedback += "?";
            }

            return feedback;
        }
    }
}
