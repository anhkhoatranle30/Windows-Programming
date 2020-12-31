using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake_Shop.MyUtils
{
    class MoveFiles
    {
        /// <summary>
        /// Hàm để chuyển hình ảnh đến thư mực được cấu hình sẵn
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="newFolderPath"></param>
        /// <returns>Trả về tên file</returns>
        public static string MoveImageToSpecifiedFolder(string sourceFilePath, string newFolderPath)
        {
            //string newFolderPath = AppDomain.CurrentDomain.BaseDirectory + "\\Images";
            if (!Directory.Exists(newFolderPath))
            {
                Directory.CreateDirectory(newFolderPath);
            }
            string newFilePath = Guid.NewGuid().ToString() + ".jpg";
            string newFullFilePath = newFolderPath + "\\" + newFilePath;
            File.Copy(sourceFilePath, newFullFilePath);

            return newFilePath;
        }

    }
}
