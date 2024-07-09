using Microsoft.VisualBasic;
using System.Globalization;
using static System.Windows.Forms.AxHost;

namespace AutomatTheory
{
    public partial class Menu : Form
    {
        private enum State { S, F, E, A, B, C, D, G, H, I, J, K, L, M, N, O, P, Q, R, T, U, V, E1, C1 ,D1 , H1, A1, G1, B1 };
        int nowCharPos = 0;
        string[,] identificators = new string[100, 2];
        string[,] constants = new string[100, 2];
        string Result = "";
        string Constants = "";
        string Identificators = "";
        public Menu()
        {
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            textBox1.ForeColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == string.Empty)
            {
                Result = "Ошибка аргумента: задана пустая строка.";
                MessageBox.Show(Result);
                return;
            }
            if (textBox1.Text[textBox1.TextLength - 1] != ';' && ((textBox1.Text[textBox1.TextLength - 2] != 'd') || (textBox1.Text[textBox1.TextLength - 2] != 'D')) && ((textBox1.Text[textBox1.TextLength - 3] != 'n') || (textBox1.Text[textBox1.TextLength - 3] != 'N')) && ((textBox1.Text[textBox1.TextLength - 4] != 'e') || (textBox1.Text[textBox1.TextLength - 4] != 'E')))
            {
                Result = "Синтаксическая ошибка: отсутствует ключевое слово END с завершающим символом \";\"";
                textBox1.Focus();
                textBox1.SelectionStart = textBox1.TextLength;
                textBox1.SelectionLength = 0;
                MessageBox.Show(Result);
                return;
            }

            Result = string.Empty;
            Identificators = "";
            Constants = string.Empty;

            string stroke = textBox1.Text;
            int length = stroke.Length;

            char nowChar;
            nowCharPos = 0;

            State state = State.S;

            identificators = new string[100, 2];


            constants = new string[100, 2];


            int check = 0;
            int indexIdent = 0;
            int indexConst = 0;

            try
            {
                while (state != State.F && state != State.E && nowCharPos < length)
                {
                    nowChar = stroke[nowCharPos];

                    switch (state)
                    {
                        case State.S:
                            if (nowChar == ' ')
                                state = State.S;
                            else if (string.Join("", nowChar, stroke[nowCharPos + 1], stroke[nowCharPos + 2]).ToLower() == "if ")
                            {
                                nowCharPos += 2;
                                state = State.A;
                            }
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидалось ключевое слово \"IF \"");
                            }
                            break;

                        case State.A:
                            if (nowChar == ' ')
                                state = State.A;
                            else if ((nowChar >= 'A' && nowChar <= 'Z')
                                     || (nowChar >= 'a' && nowChar <= 'z'))
                            {
                                identificators[indexIdent, 0] += nowChar;
                                identificators[indexIdent, 1] = Convert.ToString(nowCharPos);
                                state = State.B;
                            }
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: идентификатор должен начинаться с латинской буквы.");
                            }
                            break;
                        case State.A1:
                            if (nowChar == ' ')
                                state = State.A1;
                            else if ((nowChar >= 'A' && nowChar <= 'Z')
                                     || (nowChar >= 'a' && nowChar <= 'z'))
                            {
                                identificators[indexIdent, 0] += nowChar;
                                identificators[indexIdent, 1] = Convert.ToString(nowCharPos);
                                state = State.G1;
                            }
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: идентификатор должен начинаться с латинской буквы.");
                            }
                            break;

                        case State.B:
                            if ((nowChar >= 'A' && nowChar <= 'Z')
                                     || (nowChar >= 'a' && nowChar <= 'z')
                                     || (nowChar >= '0' && nowChar <= '9'))
                            {
                                identificators[indexIdent, 0] += nowChar;

                                state = State.B;
                            }
                            else if (nowChar == '>' || nowChar == '<' || nowChar == '=')
                            {
                                indexIdent++;
                                state = State.C;
                            }
                            else if (nowChar == ' ')
                            {
                                indexIdent++;
                                state = State.D;
                            }
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидалось продолжение идентификатора (латинская буква или цифра) или знаки сравнения (>, <, =) или пробел");
                            }
                            break;
                        case State.G1:
                            if ((nowChar >= 'A' && nowChar <= 'Z')
                                     || (nowChar >= 'a' && nowChar <= 'z')
                                     || (nowChar >= '0' && nowChar <= '9'))
                            {
                                identificators[indexIdent, 0] += nowChar;
                                state = State.G1;
                            }
                            else if (nowChar == '>' || nowChar == '<' || nowChar == '=')
                            {
                                indexIdent++;
                                state = State.B1;
                            }
                            else if (nowChar == ' ')
                            {
                                indexIdent++;
                                state = State.D;
                            }
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидалось продолжение идентификатора (буква или цифра) или знаки сравнения (<, >, =) или пробел");
                            }
                            break;

                        case State.C:
                            if (nowChar == ' ')
                                state = State.C;
                            else if ((nowChar >= 'A' && nowChar <= 'Z')
                                  || (nowChar >= 'a' && nowChar <= 'z'))
                            {
                                identificators[indexIdent, 0] += nowChar;
                                identificators[indexIdent, 1] = Convert.ToString(nowCharPos);
                                indexIdent++;
                                state = State.H;
                            }
                            else if (nowChar == '0' && nowCharPos + 1 < length && (stroke[nowCharPos + 1] < '0' || stroke[nowCharPos + 1] > '9'))
                            {
                                constants[indexConst, 0] += nowChar;
                                constants[indexConst, 1] = Convert.ToString(nowCharPos);
                                state = State.G;
                            }
                            else if (nowChar >= '1' && nowChar <= '9')
                            {
                                constants[indexConst, 0] += nowChar;
                                constants[indexConst, 1] = Convert.ToString(nowCharPos);
                                state = State.G;
                            }
                            else if (nowChar == '-')
                            {
                                if (nowCharPos + 1 < length && (stroke[nowCharPos + 1] >= '1' && stroke[nowCharPos + 1] <= '9'))
                                {
                                    constants[indexConst, 0] += "-" + stroke[nowCharPos + 1];
                                    constants[indexConst, 1] = Convert.ToString(nowCharPos);
                                    indexConst++;
                                    nowCharPos++;
                                    state = State.G;
                                }
                                else
                                {
                                    state = State.E;
                                    throw new Exception("Синтаксическая ошибка: ожидалась цифра");
                                }
                            }
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидалось начало идентификатора (буква) или константы ");
                            }
                            break;
                        case State.B1:
                            if (nowChar == ' ')
                                state = State.B1;
                            else if ((nowChar >= 'A' && nowChar <= 'Z')
                                  || (nowChar >= 'a' && nowChar <= 'z'))
                            {
                                identificators[indexIdent, 0] += nowChar;
                                identificators[indexIdent, 1] = Convert.ToString(nowCharPos);
                                state = State.H;
                            }
                            else if (nowChar == '0' && nowCharPos + 1 < length && (stroke[nowCharPos + 1] < '0' || stroke[nowCharPos + 1] > '9'))
                            {
                                constants[indexConst, 0] += nowChar;
                                constants[indexConst, 1] = Convert.ToString(nowCharPos);
                                state = State.D1;
                            }
                            else if (nowChar >= '1' && nowChar <= '9')
                            {
                                constants[indexConst, 0] += nowChar;
                                constants[indexConst, 1] = Convert.ToString(nowCharPos);
                                state = State.D1;
                            }
                            else if (nowChar == '.')
                            {
                                constants[indexConst, 0] += nowChar;
                                constants[indexConst, 1] = Convert.ToString(nowCharPos);
                                state = State.D1;
                            }
                            else if (nowChar == '-')
                            {
                                if (nowCharPos + 1 < length && (stroke[nowCharPos + 1] >= '1' && stroke[nowCharPos + 1] <= '9'))
                                {
                                    constants[indexConst, 0] += "-" + stroke[nowCharPos + 1];
                                    constants[indexConst, 1] = Convert.ToString(nowCharPos);
                                    nowCharPos++;
                                    state = State.D1;
                                }
                                else
                                {
                                    state = State.E;
                                    throw new Exception("Синтаксическая ошибка: ожидалась цифра");
                                }
                            }
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидалось начало идентификатора (буква) или константы");
                            }
                            break;
                        case State.D:
                            if (nowChar == ' ')
                                state = State.D;
                            else if (nowChar == '>' || nowChar == '<' || nowChar == '=')
                                state = State.C;
                            else if ((length - nowCharPos - 1 >= 5) && (string.Join("", nowChar, stroke[nowCharPos + 1], stroke[nowCharPos + 2], stroke[nowCharPos + 3], stroke[nowCharPos + 4]).ToLower() == "then "))
                            {
                                nowCharPos += 4;
                                state = State.J;
                            }
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидался знак сравнения (<, >, =) или ключевое слово \"THEN \" или пробел");
                            }
                            break;

                        case State.G:
                            if (nowChar >= '0' && nowChar <= '9')
                            {
                                constants[indexConst, 0] += nowChar;
                                state = State.G;
                            }
                            else if (nowChar == ' ')
                            {
                                indexConst++;
                                state = State.I;
                            }
                            else if (nowChar == '.')
                            {
                                constants[indexConst, 0] += nowChar;
                                state = State.G;
                            }
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидалась цифра или пробел");
                            }
                            break;
                        case State.D1:
                            {
                                if (nowChar >= '0' && nowChar <= '9')
                                {
                                    constants[indexConst, 0] += nowChar;
                                    state = State.D1;
                                }
                                else if (nowChar == ' ')
                                {
                                    indexConst++;
                                    state = State.I;
                                }
                                else if (nowChar == '.')
                                {
                                    constants[indexConst, 0] += nowChar;
                                    state = State.D1;
                                }
                                else
                                {
                                    state = State.E;
                                    throw new Exception("Синтаксическая ошибка: ожидалась цифра или пробел");
                                }
                                break;
                            }
                        case State.H:
                            if ((nowChar >= 'A' && nowChar <= 'Z')
                             || (nowChar >= 'a' && nowChar <= 'z')
                             || (nowChar >= '0' && nowChar <= '9'))
                            {
                                identificators[indexIdent, 0] += nowChar;
                                indexIdent++;
                                state = State.H;
                            }
                            else if (nowChar == ' ')
                                state = State.I;
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидалось продолжение идентификатора или пробел");
                            }

                            break;

                        case State.I:
                            if (nowChar == ' ')
                                state = State.I;
                            else if ((length - nowCharPos - 1 >= 5) && (string.Join("", nowChar, stroke[nowCharPos + 1], stroke[nowCharPos + 2], stroke[nowCharPos + 3], stroke[nowCharPos + 4]).ToLower() == "then "))
                            {
                                nowCharPos += 4;
                                state = State.J;
                            }
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидалось ключевое слово \"THEN \"");
                            }
                            break;

                        case State.J:
                            if (nowChar == ' ')
                            {

                                state = State.J;
                            }
                            else if (check == 0)
                            {
                                if ((nowChar >= 'A' && nowChar <= 'Z')
                                      || (nowChar >= 'a' && nowChar <= 'z'))
                                {
                                    identificators[indexIdent, 0] += nowChar;
                                    identificators[indexIdent, 1] = Convert.ToString(nowCharPos);
                                    check++;
                                    state = State.K;
                                }
                            }
                            else if (check == 1)
                            {
                                if ((nowChar >= 'A' && nowChar <= 'Z')
                                  || (nowChar >= 'a' && nowChar <= 'z'))
                                {
                                    identificators[indexIdent, 0] += nowChar;
                                    identificators[indexIdent, 1] = Convert.ToString(nowCharPos);
                                    state = State.K;
                                }
                            }

                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: идентификатор должен начинаться с латинской буквы");
                            }
                            break;

                        case State.K:
                            if ((nowChar >= 'A' && nowChar <= 'Z')
                             || (nowChar >= 'a' && nowChar <= 'z')
                             || (nowChar >= '0' && nowChar <= '9'))
                            {
                                identificators[indexIdent, 0] += nowChar;
                                state = State.K;
                            }
                            else if (nowChar == ' ')
                            {
                                indexIdent++;
                                state = State.L;
                            }
                            else if ((length - nowCharPos - 1 >= 2) && (string.Join("", nowChar, stroke[nowCharPos + 1]) == ":="))
                            {
                                indexIdent++;
                                nowCharPos++;
                                state = State.M;
                            }
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидалось продолжение идентификатора (латинская буква или цифра) или оператор присваивания \":=\"");
                            }
                            break;

                        case State.L:
                            if (nowChar == ' ')
                                state = State.L;
                            else if ((length - nowCharPos - 1 >= 2) && (string.Join("", nowChar, stroke[nowCharPos + 1]) == ":="))
                            {
                                nowCharPos++;
                                state = State.M;
                            }
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидался оператор присваивания \":=\"");
                            }

                            break;

                        case State.M:
                            if (nowChar == ' ')
                                state = State.M;
                            else if ((nowChar >= 'A' && nowChar <= 'Z')
                                  || (nowChar >= 'a' && nowChar <= 'z'))
                            {
                                identificators[indexIdent, 0] += nowChar;
                                identificators[indexIdent, 1] = Convert.ToString(nowCharPos);
                                state = State.O;
                            }
                            else if (nowChar == '0' && nowCharPos + 1 < length && (stroke[nowCharPos + 1] < '0' || stroke[nowCharPos + 1] > '9'))
                            {
                                constants[indexConst, 0] += nowChar;
                                constants[indexConst, 1] = Convert.ToString(nowCharPos);
                                state = State.N;
                            }
                            else if (nowChar >= '1' && nowChar <= '9')
                            {
                                constants[indexConst, 0] += nowChar;
                                constants[indexConst, 1] = Convert.ToString(nowCharPos);
                                state = State.N;
                            }
                            else if (nowChar == '-')
                            {
                                if (nowCharPos + 1 < length && (stroke[nowCharPos + 1] >= '1' && stroke[nowCharPos + 1] <= '9'))
                                {
                                    constants[indexConst, 0] += "-" + stroke[nowCharPos + 1];
                                    constants[indexConst, 1] = Convert.ToString(nowCharPos);
                                    nowCharPos++;
                                    state = State.N;
                                }
                                else
                                {
                                    state = State.E;
                                    throw new Exception("Синтаксическая ошибка: ожидалась цифра");
                                }
                            }
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидалось начало идентификатора (латинская буква) или константы");
                            }
                            break;

                        case State.N:
                            if (nowChar >= '0' && nowChar <= '9')
                            {
                                constants[indexConst, 0] += nowChar;
                                state = State.N;
                            }
                            else if (nowChar == '.')
                            {
                                constants[indexConst, 0] += nowChar;
                                state = State.N;
                            }
                            else if (nowChar == ' ')
                            {
                                indexConst++;
                                state = State.P;
                            }
                            else if (string.Join("", nowChar, stroke[nowCharPos + 1], stroke[nowCharPos + 2], ";").ToLower() == "end;")
                            {
                                indexConst++;
                                state = State.F;
                            }
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидалась цифра или ключевое слово \"END;\" или знак \".\"");
                            }
                            break;
                        case State.H1:
                            if (nowChar >= '0' && nowChar <= '9')
                            {
                                constants[indexConst, 0] += nowChar;
                                indexConst++;
                                state = State.H1;
                            }
                            else if (nowChar == ' ')
                                state = State.P;
                            else if (string.Join("", nowChar, stroke[nowCharPos + 1], stroke[nowCharPos + 2], ";").ToLower() == "end;")
                                state = State.F;
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидалась цифра или ключевое слово \"END;\" или знак \".\"");
                            }
                            break;
                        case State.O:
                            if ((nowChar >= 'A' && nowChar <= 'Z')
                             || (nowChar >= 'a' && nowChar <= 'z')
                             || (nowChar >= '0' && nowChar <= '9'))
                            {
                                identificators[indexIdent, 0] += nowChar;
                                indexIdent++;
                                state = State.O;
                            }
                            else if (nowChar == ' ')
                                state = State.P;
                            else if (nowChar == ';')
                                state = State.F;
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидалось продолжение идентификатора (буква или цифра) или завершающий символ \";\"");
                            }
                            break;

                        case State.P:
                            if (nowChar == ' ')
                                state = State.P;
                            else if ((length - nowCharPos - 1 >= 5) && (string.Join("", nowChar, stroke[nowCharPos + 1], stroke[nowCharPos + 2], stroke[nowCharPos + 3], stroke[nowCharPos + 4]).ToLower() == "else "))
                            {
                                nowCharPos += 4;
                                state = State.Q;
                            }
                            else if (string.Join("", nowChar, stroke[nowCharPos + 1], stroke[nowCharPos + 2], ";").ToLower() == "end;")
                                state = State.F;
                            else if ((length - nowCharPos - 1 >= 7) && (string.Join("", nowChar, stroke[nowCharPos + 1], stroke[nowCharPos + 2], stroke[nowCharPos + 3], stroke[nowCharPos + 4], stroke[nowCharPos + 5]).ToLower() == "elsif "))
                            {
                                nowCharPos = nowCharPos + 5;
                                state = State.A1;
                            }
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидалось ключевое слово \"ELSE \" или \"ELSIF \" или ключевое слово \" END; \" ");
                            }
                            break;

                        case State.Q:
                            if (nowChar == ' ')
                                state = State.Q;
                            else if ((nowChar >= 'A' && nowChar <= 'Z')
                                  || (nowChar >= 'a' && nowChar <= 'z'))
                            {
                                identificators[indexIdent, 0] += nowChar;
                                identificators[indexIdent, 1] = Convert.ToString(nowCharPos);
                                state = State.R;
                            }
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: идентификатор должен начинаться с латинской буквы");
                            }
                            break;

                        case State.R:
                            if ((nowChar >= 'A' && nowChar <= 'Z')
                             || (nowChar >= 'a' && nowChar <= 'z')
                             || (nowChar >= '0' && nowChar <= '9'))
                            {
                                identificators[indexIdent, 0] += nowChar;
                                state = State.R;
                            }
                            else if (nowChar == ' ')
                            {
                                indexIdent++;
                                state = State.T;
                            }
                            else if ((length - nowCharPos - 1 >= 2) && (string.Join("", nowChar, stroke[nowCharPos + 1]) == ":="))
                            {
                                indexIdent++;
                                nowCharPos++;
                                state = State.U;
                            }
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидалось продолжение идентификатора (латинская буква или цифра от 0 до 9) или оператор присваивания \":=\"");
                            }
                            break;

                        case State.T:
                            if (nowChar == ' ')
                                state = State.T;
                            else if ((length - nowCharPos - 1 >= 2) && (string.Join("", nowChar, stroke[nowCharPos + 1]) == ":="))
                            {
                                nowCharPos++;
                                state = State.U;
                            }
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидался оператор присваивания \":=\"");
                            }
                            break;

                        case State.U:
                            if (nowChar == ' ')
                                state = State.U;
                            else if ((nowChar >= 'A' && nowChar <= 'Z')
                                  || (nowChar >= 'a' && nowChar <= 'z'))
                            {
                                identificators[indexIdent, 0] += nowChar;
                                identificators[indexIdent, 1] = Convert.ToString(nowCharPos);
                                state = State.E1;
                            }
                            else if (nowChar == '0' && nowCharPos + 1 < length && (stroke[nowCharPos + 1] < '0' || stroke[nowCharPos + 1] > '9'))
                            {
                                constants[indexConst, 0] += nowChar;
                                constants[indexConst, 1] = Convert.ToString(nowCharPos);
                                state = State.V;
                            }
                            else if (nowChar >= '1' && nowChar <= '9')
                            {
                                constants[indexConst, 0] += nowChar;
                                constants[indexConst, 1] = Convert.ToString(nowCharPos);
                                state = State.V;
                            }
                            else if (nowChar == '-')
                            {
                                if (nowCharPos + 1 < length && (stroke[nowCharPos + 1] >= '1' && stroke[nowCharPos + 1] <= '9'))
                                {
                                    constants[indexConst, 0] += "-" + stroke[nowCharPos + 1];
                                    constants[indexConst, 1] = Convert.ToString(nowCharPos);
                                    nowCharPos++;
                                    state = State.V;
                                }
                                else
                                {
                                    state = State.E;
                                    throw new Exception("Синтаксическая ошибка: ожидалась цифра");
                                }
                            }
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидалось начало идентификатора (латинская буква) или константы");
                            }
                            break;

                        case State.V:
                            if (nowChar >= '0' && nowChar <= '9')
                            {
                                constants[indexConst, 0] += nowChar;
                                state = State.V;
                            }
                            else if (nowChar == ';')
                            {
                                indexConst++;
                                state = State.F;
                            }
                            else if (nowChar == ' ')
                            {
                                indexConst++;
                                state = State.C1;
                            }
                            else if (nowChar == '.')
                            {
                                constants[indexConst, 0] += nowChar;
                                state = State.V;
                            }
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидалась цифра константы или завершающий символ \";\"");
                            }
                            break;

                        case State.E1:
                            if ((nowChar >= 'A' && nowChar <= 'Z')
                             || (nowChar >= 'a' && nowChar <= 'z')
                             || (nowChar >= '0' && nowChar <= '9'))
                            {
                                identificators[5, 0] += nowChar;
                                state = State.E1;
                            }
                            else if (nowChar == ';')
                                state = State.F;
                            else if (nowChar == ' ')
                                state = State.C1;
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидалось продолжение идентификатора (латинская буква или цифра) или завершающий символ \";\"");
                            }
                            break;

                        case State.C1:
                            if (nowChar == ' ')
                                state = State.C1;
                            else if (string.Join("", nowChar, stroke[nowCharPos + 1], stroke[nowCharPos + 2], ";").ToLower() == "end;")
                                state = State.F;
                            else
                            {
                                state = State.E;
                                throw new Exception("Синтаксическая ошибка: ожидалось ключевое слово \"END;\"");
                            }
                            break;
                    }
                    nowCharPos++;
                }

                for (int i = 0; i < 10; i++)
                {
                    if (identificators[i, 0] == null) continue;

                    if (identificators[i, 0].Length > 8)
                    {
                        nowCharPos = Convert.ToInt16(identificators[i, 1]);
                        throw new Exception($"Семантическая ошибка: длина идентификатора \"{identificators[i, 0]}\" не может быть больше 8");
                    }

                    if (identificators[i, 0].ToLower() == "if" || identificators[i, 0].ToLower() == "then" || identificators[i, 0].ToLower() == "else")
                    {
                        nowCharPos = Convert.ToInt16(identificators[i, 1]);
                        throw new Exception($"Семантическая ошибка: ключевое слово не может быть идентификатором");
                    }
                }

                for (int i = 0; i < 10; i++)
                {
                    if (constants[i, 0] == null)
                        continue;

                    string b = constants[i, 0];
                    double d = Convert.ToDouble("0.99", CultureInfo.InvariantCulture);
                    double a = Convert.ToDouble(b, CultureInfo.InvariantCulture);
                    if (a is double)
                    {
                        if (a < -32768 || a > 32767)
                        {
                            nowCharPos = Convert.ToInt16(constants[i, 1]);
                            throw new Exception($"Семантическая ошибка: константа \"{constants[i, 0]}\" не входит в диапазон [-32768;32767]");
                        }
                    }
                    else
                    {

                        throw new Exception($"Некорректная константа: \"{constants[i, 0]}\"");
                    }
                }

                if (nowCharPos < length)
                    throw new Exception("Ошибок не обнаружено.");

                Result = "Ошибок не обнаружено.\nЦепочка принадлежит языку.";
                MessageBox.Show(Result);

            }
            catch (Exception ex)
            {
                string[] message = new string[100];
                message[0] = ex.Message;
                Result = message[0];

                textBox1.Focus();
                textBox1.SelectionStart = nowCharPos;
                MessageBox.Show(Result);
            }

        }

        private void ShowSemantics_Click(object sender, EventArgs e)
        {

            if (Result == "")
            {
                MessageBox.Show("Сначала нужно сделать анализ.", "Сначала сделайте анализ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Constants = "";
            Identificators = "";

            List<string> SettedIdentificators = new List<string>();

            for (int i = 0; i < 10; i++)
                if (identificators[i, 0] != null) SettedIdentificators.Add(identificators[i, 0]);

            SettedIdentificators = SettedIdentificators.Distinct().ToList();

            foreach (string identificator in SettedIdentificators)
                Identificators += identificator + "\n";

            List<string> SettedConstants = new List<string>();

            for (int i = 0; i < 10; i++)
                if (constants[i, 0] != null) SettedConstants.Add(constants[i, 0]);

            SettedConstants = SettedConstants.Distinct().ToList();

            foreach (string constant in SettedConstants)
                Constants += constant + "\n";
            new Semantics(Identificators, Constants).ShowDialog();
        }
    }
}