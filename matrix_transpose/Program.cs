using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrix_transpose
{
    class Program
    {
        static void Main(string[] args)
        {
            var input_file = "Input_4000_6000.bin";
            var output = "Output_6000_4000.bin";
            byte[] fileBytes = System.IO.File.ReadAllBytes(input_file);
            byte[] outputBytes = GetTranspose(fileBytes);

            
            File.WriteAllBytes(output, outputBytes);
        }

        private static byte[] GetTranspose(byte[] fileBytes)
        {
            var outputBytes = new byte[24000000];
            int colCount = 6000;
            int rowCount = 4000;
            
            int index = 0;
            for (int row = 0; row < colCount; row++)
            {
                for (int col = 0; col < rowCount; col++)
                {
                    var kCnt = row + colCount * col;
                    outputBytes[index++] = fileBytes[kCnt];
                }
            }

            //Parallel.For(0, colCount,
            //       index_i =>
            //       {
            //           Parallel.For(0, rowCount,
            //              index_j =>
            //              {
            //                  outputBytes[index++] = fileBytes[index_i + colCount * index_j];
            //              });
            //       });

            return outputBytes;
        }
    }
}
