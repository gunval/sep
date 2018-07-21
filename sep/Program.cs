using System;
using System.IO;

namespace sep
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("パラメータが間違っています。");
                Console.WriteLine("　第一引数：入力ファイル名");
                Console.WriteLine("　第二引数：出力ファイル名");
                Console.WriteLine("　第三引数：削除する先頭からのオフセット（先頭にOxがつくと16進数表記）");
                return;
            }

            var inputfile = args[0];
            var outputfile = args[1];
            var offset = 0;
            
            if (args[2].StartsWith("0x"))
            {
                var temp = args[2].Replace("0x", "").Trim();
                offset = Convert.ToInt32(temp, 16);
            }
            else
            {
                var temp = args[2];
                offset = Convert.ToInt32(temp);
            }

            CopyFile(inputfile, outputfile, offset);
        }
        /// <summary>
        /// ファイルコピー
        /// </summary>
        /// <param name="input">入力ファイルパス</param>
        /// <param name="output">出力ファイルパス</param>
        /// <param name="offset">オフセット</param>
        /// 
        static private void CopyFile(string input, string output, int offset)
        {
            using (var instream = new FileStream(input, FileMode.Open))
            {
                var buffer = new byte[instream.Length];
                instream.Read(buffer, 0, buffer.Length);

                byte[] dist = new byte[buffer.Length - offset];
                Array.Copy(buffer, offset, dist, 0, dist.Length);

                using (var outstream = new FileStream(output, FileMode.Create))
                {
                    outstream.Write(dist, 0, dist.Length);
                }
            }
        }
    }
}
