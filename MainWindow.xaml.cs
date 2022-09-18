using System;
using System.Collections.Generic;
using System.Windows;

namespace Encryptor_with_GUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            userseed_output.IsReadOnly = true;
        }

        public void ClearInputAfterEnc_Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            //
        }

        private void ClearInputAfterDec_Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            //
        }

        //шифровка
        public void FirstEncBtnClick(object sender, RoutedEventArgs e)
        {
            FreqDictGenerator(out Dictionary<char, int> freqdict, out List<char> freqsequence_list);
            string input_string = first_enc_input.Text;
            string output_encrypted = "";
            EncryptAs1(freqsequence_list, input_string, ref output_encrypted);
            first_enc_output.Text = LastSpaceCutter(output_encrypted);
            
            if (ClearInputAfterEnc_Checkbox.IsChecked == true)
            {
                first_enc_input.Clear();
            }
        }

        public void SecondEncBtnClick(object sender, RoutedEventArgs e)
        {
            List<char> positions_list = PositionsListGenerator();
            string input_string = second_enc_input.Text;
            string output_encrypted = "";
            EncryptAs2(positions_list, input_string, ref output_encrypted);
            second_enc_output.Text = LastSpaceCutter(output_encrypted);

            if (ClearInputAfterEnc_Checkbox.IsChecked == true)
            {
                second_enc_input.Clear();
            }
        }

        public void ThirdEncBtnClick(object sender, RoutedEventArgs e)
        {

            string input_string = third_enc_input.Text;
            //генерация межсимвольных значений
            int seed = SeedGenerator();
            //составление внутреннего seed с маркерами (!--n--!) и вывод пользовательского seed
            try
            {
                int ascii_shift = Convert.ToInt32(ascii_shift_input.Text);
                string seed_string = InternalSeedBuilder(seed);
                userseed_output.Text = UserSeedOut(seed);
                string output_encrypted = EncryptAs3(input_string, ascii_shift, seed_string);
                third_enc_output.Text = output_encrypted;

                if (ClearInputAfterEnc_Checkbox.IsChecked == true)
                {
                    third_enc_input.Clear();
                }
            }
            catch
            {
                ErrorMessage();
            }

        }


        public void FourthEncBtnClick(object sender, RoutedEventArgs e)
        {
            string output_encrypted = "";
            List<char> positions_list_4 = PositionsList2Generator();
            string input_string = fourth_enc_input.Text;
            EncryptAs2(positions_list_4, input_string, ref output_encrypted);
            fourth_enc_output.Text = LastSpaceCutter(output_encrypted);

            if (ClearInputAfterEnc_Checkbox.IsChecked == true)
            {
                fourth_enc_input.Clear();
            }
        }

        //дешифровка

        public void FirstDecBtnClick(object sender, RoutedEventArgs e)
        {
            FreqDictGenerator(out Dictionary<char, int> freqdict, out List<char> freqsequence_list);
            string input_encrypted = first_dec_input.Text;
            string output_string = DecryptAs1(freqsequence_list, input_encrypted);
            first_dec_output.Text = output_string;

            if (ClearInputAfterDec_Checkbox.IsChecked == true)
            {
                first_dec_input.Clear();
            }
        }

        public void SecondDecBtnClick(object sender, RoutedEventArgs e)
        {
            List<char> positions_list = PositionsListGenerator();
            string input_encrypted = second_dec_input.Text;
            string output_string = DecryptAs2(positions_list, input_encrypted);
            second_dec_output.Text = output_string;

            if (ClearInputAfterDec_Checkbox.IsChecked == true)
            {
                second_dec_input.Clear();
            }
        }

        public void ThirdDecBtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                char seed_char = Convert.ToChar(seedinput_dec.Text);     //введенный символ конвертится из string в char
                string seed_string = SeedStringBuilder(seed_char);
                string output_string = "";
                string input_encrypted = third_dec_input.Text;
                int ascii_shift = Convert.ToInt32(ascii_shift_input.Text);
                output_string = DecryptAs3(seed_string, output_string, input_encrypted, ascii_shift);
                third_dec_output.Text = output_string;

                if (ClearInputAfterDec_Checkbox.IsChecked == true)
                {
                    third_dec_input.Clear();
                }
            }

            catch
            {
                ErrorMessage();
            }
        }

        public void FourthDecBtnClick(object sender, RoutedEventArgs e)
        {
            List<char> positions_list_2 = PositionsList2Generator();
            string input_encrypted = fourth_dec_input.Text;
            string output_string = DecryptAs2(positions_list_2, input_encrypted);
            fourth_dec_output.Text = output_string;

            if (ClearInputAfterDec_Checkbox.IsChecked == true)
            {
                fourth_dec_input.Clear();
            }
        }
    }
}