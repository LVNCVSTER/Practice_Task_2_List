using System;
using System.IO;

namespace List
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input;
            string output;

            using (StreamReader sw = new StreamReader("INPUT.TXT"))
            {
                input = sw.ReadToEnd().Split('\n');
            }

            output = Solve(input);

            using (StreamWriter sw = new StreamWriter("OUTPUT.TXT"))
            {
                sw.Write(output);
            }
        }
        static string Solve(string[] get)
        {
            string[] arrStr = get[1].Split(' ');
            // Список номеров страниц
            int[] arrInt = new int[arrStr.Length];

            for (int i = 0; i < arrInt.Length; i++)
            {
                arrInt[i] = int.Parse(arrStr[i]);
            }

            // Список уникальных номеров
            int[] arrIntUniq = new int[arrInt.Length];
            int iUniq = 0;
            bool isZero = false;

            // Добавление в список уникальных чисел
            foreach (var num in arrInt)
            {
                // Если повторяется 0 или повторяется какое-то другое число
                if ((num == 0 && !isZero) || !Array.Exists(arrIntUniq, el => el == num))
                {
                    arrIntUniq[iUniq] = num;
                    iUniq++;

                    if (num == 0)
                    {
                        isZero = true;
                    }
                }
            }

            Array.Resize(ref arrIntUniq, iUniq);

            Array.Sort(arrIntUniq);

            string output = "";

            for (int i = 0; i < arrIntUniq.Length; i++)
            {
                // Кол-во подряд увеличивающихся на 1 чисел
                int cToDel = 0;
                for (int j = i; j < arrIntUniq.Length - 1; j++)
                {
                    // Если второе число не увеличено на 1
                    if (arrIntUniq[j] != arrIntUniq[j + 1] - 1)
                    {
                        break;
                    }

                    cToDel++;
                }

                // Если кол-во повторяющихся чисел >= 2 и (это не единтсвенные числа или предпоследнее число <= -10,
                // т.е. имеет 3 знака или предпоследнее число >= 100, т.е. имеет 3 знака)
                if (cToDel >= 2 && (!(cToDel == 2 && i + cToDel != arrIntUniq.Length) ||
                                    arrIntUniq[i + cToDel - 1] <= -10 || arrIntUniq[i + cToDel - 1] >= 100))
                {
                    // Заменяем числа на число из начала подпоследовательности, ..., число из конца подпоследовательности
                    output += arrIntUniq[i] + ", ..., " + arrIntUniq[i + cToDel] +
                              (i + cToDel != arrIntUniq.Length - 1 ? ", " : "");
                    i += cToDel;
                }
                // Иначе добавляем число как оно есть
                else
                {
                    output += arrIntUniq[i] + (i != arrIntUniq.Length - 1 ? ", " : "");
                }
            }


            return output;
        }
    }
}
