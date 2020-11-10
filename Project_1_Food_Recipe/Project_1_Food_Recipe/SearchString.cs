using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_project1
{
    class SearchString
    {
        private static readonly string[] VietnameseSigns = new string[] {
                "aAeEoOuUiIdDyY",

                "áàạảãâấầậẩẫăắằặẳẵ",

                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

                "éèẹẻẽêếềệểễ",

                "ÉÈẸẺẼÊẾỀỆỂỄ",

                "óòọỏõôốồộổỗơớờợởỡ",

                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

                "úùụủũưứừựửữ",

                "ÚÙỤỦŨƯỨỪỰỬỮ",

                "íìịỉĩ",

                "ÍÌỊỈĨ",

                "đ",

                "Đ",

                "ýỳỵỷỹ",

                "ÝỲỴỶỸ"
            };

        /// <summary>
        /// Hàm để xóa dấu tiếng việt đi thành không dấu
        /// </summary>
        /// <param name="str">Chuỗi có dấu</param>
        /// <returns>Trả về chuỗi không dấu</returns>
        public static string RemoveSign4VietnameseString(string str)

        {
            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi

            for (int i = 1; i < VietnameseSigns.Length; i++)

            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)

                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }

            return str;
        }

        /// <summary>
        /// Hàm parse từ chuỗi bình thường ra chuỗi để search
        /// </summary>
        /// <param name="str">Hàm muốn search</param>
        /// <returns>Trả về hàm không có dấu và viết thường hết</returns>
        public static string Parse(string str)
        {
            str = RemoveSign4VietnameseString(str);
            str = str.ToLower();
            return str;
        }

        /// <summary>
        /// Tính độ ưu tiên phục vụ tác vụ tìm kiếm
        /// </summary>
        /// <param name="searchedString">Chuỗi được user nhập vào để tìm kiếm</param>
        /// <param name="toSearchString">Chuỗi được so sánh từ dữ liệu</param>
        /// <returns>Độ ưu tiên(càng lớn càng được ưu tiên)</returns>
        public static int CalcPriority(string searchedString, string toSearchString)
        {
            int priority = 0;

            searchedString = Parse(searchedString); //dui ga sot cam
            if (!searchedString.Contains(" "))
            {
            }
            else
            {
                searchedString = " " + searchedString + " ";
            }

            toSearchString = Parse(toSearchString); //Đùi gà sốt cam
            var tokens = toSearchString.Split(new String[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < tokens.Length; i++)
            {
                string transformedSearchString;
                if (i == 0)
                {
                    if (!searchedString.Contains(" "))
                    {
                        transformedSearchString = tokens[i];
                    }
                    else
                    {
                        transformedSearchString = tokens[i] + " ";
                    }
                }
                else if (i == tokens.Length - 1)
                {
                    transformedSearchString = " " + tokens[i];
                }
                else
                {
                    transformedSearchString = " " + tokens[i] + " ";
                }

                if (searchedString.Length < transformedSearchString.Length)
                {
                    if (transformedSearchString.Contains(searchedString))
                    {
                        priority++;
                    }
                }
                else
                {
                    if (searchedString.Contains(transformedSearchString))
                    {
                        priority++;
                    }
                }
            }
            //foreach (var token in tokens)
            //{
            //    var spaceShiftedString = " " + token;
            //    var spacePushedString = token + " ";
            //    var spaceAddedString = " " + token + " ";
            //    if (searchedString.Contains(spaceShiftedString) || searchedString.Contains(spacePushedString) || searchedString.Contains(spaceAddedString))
            //    {
            //        priority++;
            //    }
            //}
            return priority;
        }

        /// <summary>
        /// Tính độ ưu tiên LỚN NHẤT phục vụ tác vụ tìm kiếm
        /// </summary>
        /// <param name="searchedString">Chuỗi được user nhập vào để tìm kiếm</param>
        /// <param name="toSearchString">Chuỗi được so sánh từ dữ liệu</param>
        /// <returns>Độ ưu tiên(càng lớn càng được ưu tiên)</returns>
        public static int CalcMaxPriority(string searchedString)
        {
            int maxPriority = 0;

            searchedString = Parse(searchedString);
            var tokens = searchedString.Split(new String[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            maxPriority = tokens.Length;
            return maxPriority;
        }

        /// <summary>
        /// Hàm để kiểm tra tìm kiếm
        /// </summary>
        /// <param name="searchedString">Chuỗi được user nhập vào để tìm kiếm</param>
        /// <param name="toSearchString">Chuỗi được so sánh từ dữ liệu</param>
        /// <returns>True nếu tìm thấy có từ trong chuõi, False nếu ngược lại</returns>
        public static bool CheckSearch(string searchedString, string toSearchString)
        {
            searchedString = Parse(searchedString);
            toSearchString = Parse(toSearchString);
            var tokens = searchedString.Split(new String[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var token in tokens)
            {
                if (toSearchString.Contains(token))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
