using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Encryptor_with_GUI
{
    public partial class MainWindow : Window
    {
        #region First methods
        public static char[] EncryptAs1(List<char> freqsequence_list, string input_string, ref string output_encrypted)
        {
            char[] inputChar_arr = input_string.ToCharArray();
            for (int x = 0; x < input_string.Length; x++)
            {
                int index_of = freqsequence_list.IndexOf(inputChar_arr[x]);
                output_encrypted = output_encrypted + index_of + " ";
            }

            return inputChar_arr;
        }

        public static string DecryptAs1(List<char> freqsequence_list, string input_encrypted)
        {
            string input_index_int = input_encrypted.Replace(",", string.Empty);
            int[] inputIndex_arr = new int[input_index_int.Length];

            try
            {
                inputIndex_arr = input_index_int.Split(' ').Select(int.Parse).ToArray();
                string output_string = "";
                for (int i = 0; i < inputIndex_arr.Length; i++)//повторяется, пока не закончатся символы в строке
                {
                    for (int j = 0; j < freqsequence_list.Count; j++)//перебирает все символы в словаре
                    {
                        if (inputIndex_arr[i] == j)
                        {
                            string output_str = Convert.ToString(freqsequence_list[j]);
                            output_string += output_str;
                        }
                    }
                }
                return output_string;
            }

            catch
            {
                return "Пожалуйста, заполните окно ввода";
            }
        }

        public static void FreqDictGenerator(out Dictionary<char, int> freqdict, out List<char> freqsequence_list)
        {
            string freqsequence_string = AbbcccStringBuilder();//построение частотной строки
            freqdict = SymbolRepeatCounter(freqsequence_string);//пересчет вхождений каждого символа в частотную строку
            //перемещение ключей(символов) в порядке увеличения числа вхождений в массив(теперь кол-во вхождений элемента == индекс этого же элемента в массиве)
            freqsequence_list = FreqSequenceListBuilder(freqdict);
        }

        public static List<char> FreqSequenceListBuilder(Dictionary<char, int> freqdict)//Dictionary<KEY, VALUE>
        {
            List<char> freqsequence_list = new()
            {
                (char)10060//заполнение нулевого индекса листа, подгонка индексации к частоте вхождения
            };
            freqsequence_list = freqdict.Keys.ToList();
            return freqsequence_list;
        }

        public static Dictionary<char, int> SymbolRepeatCounter(string freqsequence_string)
        {
            char alphabet_char;
            Dictionary<char, int> freqdict = new();
            foreach (char ch in freqsequence_string)
            {
                alphabet_char = ch;
                if (freqdict.ContainsKey(alphabet_char))
                    freqdict[alphabet_char]++;
                else
                    freqdict.Add(alphabet_char, 1);
            }
            return freqdict;
        }

        public static string AbbcccStringBuilder()//создание строки повторяющихся символов для дальнейшего частотного анализа (способ №1)
        {
            string symbol_string = "";
            int repeat = 0;
            for (int symbol = 32; symbol <= 126; symbol++, repeat++)//с 32 по 126 символ(латиница+символы)
            {
                for (int i = repeat; i >= 0; i--) symbol_string = symbol_string.Insert(symbol_string.Length, Convert.ToString(Convert.ToChar(symbol)));
            }
            //пропуск неподдерживаемых консолью символов
            for (int symbol = 1040; symbol <= 1103; symbol++, repeat++)//с 1040 по 1103 символ(кириллица, оба регистра)
            {
                for (int i = repeat; i >= 0; i--) symbol_string = symbol_string.Insert(symbol_string.Length, Convert.ToString(Convert.ToChar(symbol)));
            }

            return symbol_string;
        }

        #endregion

        #region Second methods
        public static char[] EncryptAs2(List<char> dictionary, string input_string, ref string output_encrypted)
        {
            char[] inputChar_arr = input_string.ToCharArray();

            for (int x = 0; x < input_string.Length; x++)
            {
                int index_of = dictionary.IndexOf(inputChar_arr[x]);
                output_encrypted = output_encrypted + index_of + " ";
            }
            return inputChar_arr;
        }

        public static string DecryptAs2(List<char> positions_list, string input_encrypted)
        {
            input_encrypted = input_encrypted.Replace(",", string.Empty);
            int[] inputIndex_arr = new int[input_encrypted.Length];
            try
            {
                inputIndex_arr = input_encrypted.Split(' ').Select(int.Parse).ToArray();
                string output_string = "";
                for (int i = 0; i < inputIndex_arr.Length; i++)//повторяется, пока не закончатся символы в строке
                {
                    for (int j = 0; j < positions_list.Count; j++)//перебирает все символы в словаре
                    {
                        if (inputIndex_arr[i] == j)
                        {
                            string output_str = Convert.ToString(positions_list[j]);
                            output_string += output_str;
                        }
                    }
                }
                return output_string;
            }

            catch
            {
                return "Пожалуйста, заполните окно ввода";
            }
        }

        public static List<char> PositionsListGenerator()//создание листа символов для дальнейшего вывода индексов символов
        {
            return new()
            {
                't','h','e','q','u','i','c','k','b','r','o','w','n','f','x','j','m','p','s','v','l','a','z','y','d','g',
                'T','H','E','Q','U','I','C','K','B','R','O','W','N','F','X','J','M','P','S','V','L','A','Z','Y','D','G',
                'с','ъ','е','ш','ь','ж','щ','ё','э','т','и','х','м','я','г','к','ф','р','а','н','ц','у','з','б','л','о','д','в','ы','п','й','ч','ю',
                'С','Ъ','Е','Ш','Ь','Ж','Щ','Ё','Э','Т','И','Х','М','Я','Г','К','Ф','Р','А','Н','Ц','У','З','Б','Л','О','Д','В','Ы','П','Й','Ч','Ю',
                ' ',',','!','.','?','-','_','<','>','[',']','{','}','+','=','$','@','%',':','(',')','"','1','2','3','4','5','6','7','8','9','0'
            };
        }

        #endregion

        #region Third methods

        public static string EncryptAs3(string input_string, int ascii_shift, string seed_string)
        {
            byte[] ascii_bytes = Encoding.UTF8.GetBytes(input_string);//получение байтов символов в кодировке ASCII (латиница)
            string output_encrypted = "";
            foreach (int ascii_bytes_of_element in ascii_bytes)
            {
                string encrypted_symbol = (ascii_bytes_of_element - ascii_shift + seed_string);//байтовое значение символа-сдвиг+добавление подстроки seed
                output_encrypted += encrypted_symbol;
            }

            return output_encrypted;
        }

        public static string DecryptAs3(string seed_string, string output_string, string input_encrypted, int ascii_shift)
        {
            string input_encrypted_without_seed = input_encrypted.Replace(seed_string, " ");
            //
            List<int> int_chars_list = new();
            string[] output_arr = input_encrypted_without_seed.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in output_arr)
            {
                if (!int.TryParse(s, out int temp))
                {
                    ErrorMessage();
                }

                else
                {
                    int_chars_list.Add(temp);
                }
            }
            foreach (int i in int_chars_list)
            {
                string output_str = Convert.ToString((char)(i + ascii_shift));
                output_string += output_str;
            }

            return output_string;
        }

        public static string SeedStringBuilder(char seed_char)
        {
            int seed_char_int = (int)seed_char;                 //далее извлекается код символа
            string seed_string = "!-" + seed_char_int + "-!";   //и строится полноценная строка с маркерами начала\конца seed'a
            return seed_string;
        }

        public static int SeedGenerator()//генератор численного seed
        {
            Random random = new();
            int seed = random.Next(33, 125);//диапазон кодов символов, доступных пользователю для ввода одной клавишей
            return seed;
        }

        public static string UserSeedOut(int seed)//вывод пользовательского seed(для копирования)
        {
            string userseed_string_output = Convert.ToString((char)seed);
            return userseed_string_output;
        }

        public static string InternalSeedBuilder(int seed)//построение внутреннего seed для построения итоговой строки
        {
            return "!-" + seed.ToString() + "-!";
        }

        #endregion

        #region Fourth methods

        public static List<char> PositionsList2Generator()//создание листа символов(в алфавитном порядке) для дальнейшего вывода индексов символов
        {
            return new()
            {
                'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
                'а','б','в','г','д','е','ё','ж','з','и','й','к','л','м','н','о','п','р','с','т','у','ф','х','ц','ч','ш','щ','ъ','ы','ь','э','ю','я',
                'А','Б','В','Г','Д','Е','Ё','Ж','З','И','Й','К','Л','М','Н','О','П','Р','С','Т','У','Ф','Х','Ц','Ч','Ш','Щ','Ъ','Ы','Ь','Э','Ю','Я',
                '1','2','3','4','5','6','7','8','9','0',' ',',','!','.','?','-','_','<','>','[',']','{','}','+','=','$','@','%',':','(',')','"',
            };
        }

        #endregion

        public static string LastSpaceCutter(string output_encrypted)
        {
            try
            {
                output_encrypted = output_encrypted.Remove(output_encrypted.LastIndexOf(' '), 1);
                return output_encrypted;
            }

            catch
            {
                return "Пожалуйста, заполните окно ввода";
            }
        }

        public static void ErrorMessage()//вывод сообщения об ошибке для случая ввода некорректного значения
        {
            MessageBox.Show("Ошибка. Пожалуйста, заполните все поля");
        }
        
    }
}