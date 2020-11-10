using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_project1
{
    class PathString

    {
        /// <summary>
        /// Chuyển đường dẫn TƯƠNG ĐỐI thành TUYỆT ĐỐI
        /// </summary>
        /// <param name="relative">đường dẫn tương đối</param>
        /// <returns>đường dẫn tuyệt đối tương ứng</returns>
        public static String toAbsolutePath(String relative)
        {
            String result;

            var path = new StringBuilder();
            path.Append(AppDomain.CurrentDomain.BaseDirectory);

            result = $"{path}{relative}";
            return result;
        }
        /// <summary>
        /// Hàm tách từ chuỗi địa chỉ file của hệ thống thành Đường dẫn tuyệt đối
        /// </summary>
        /// <param name="toParseString">đường dẫn hệ thống</param>
        /// <returns>Trả về đường dẫn tuyệt đối</returns>
        public static string ParseSystemPath(string toParseString)
        {
            // path : file: + /// + {absolutePath}
            var tokens = toParseString.Split(new String[] { "file:///" }, StringSplitOptions.RemoveEmptyEntries);
            var result = new StringBuilder();
            result.Append(tokens[0]);
            return result.ToString();
        }

        /// <summary>
        /// Chuyển đổi đường dẫn Youtube thông thường thành đường dẫn nhúng
        /// </summary>
        /// <param name="youtubeLink">Đường link video youtube</param>
        /// <returns>Đường dẫn nhúng youtube video vào ứng dụng</returns>
        public static string TransformToEmbedYoutubeLink(string youtubeLink)
        {
            var tokens = youtubeLink.Split(new string[] { "https://youtu.be/", "https://www.youtube.com/watch?v=", "?t=" }, StringSplitOptions.RemoveEmptyEntries);
            var result = tokens[0];
            result = "https://www.youtube.com/embed/" + result;
            return result;
        }
    }
}
