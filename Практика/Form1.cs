using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Практика
{
    public partial class Form1 : Form
    {
        public DataGridView dgv;
        public Form1()
        {
            InitializeComponent();
            SetRoundedShape(panel1, 30);
            SetRoundedShape(panel2, 30);
            SetRoundedShape(panel3, 30);
            label10.Text = "";
            timer1.Enabled = true;
            timer1.Interval = 1000;
            fontDialog1.ShowColor = true;
            // расширенное окно для выбора цвета
            colorDialog1.FullOpen = true;
            // установка начального цвета для colorDialog
            colorDialog1.Color = this.BackColor;
            string dbPath = openFileDialog1.FileName;
            dgv = new DataGridView();
            List<string> wordsList = new List<string> {"Аллерголог","Аллерголог-иммунолог","Ангионевролог","Ангиохирург","Андролог","Андролог-эндокринолог","Анестезиолог","Апитерапевт","АритмологАроматерапевт","АртрологБактериолог","Бальнеолог","Валеолог","Венеролог","Вертебролог","Вирусолог","Врач по спортивной медицине","Врач скорой помощи","Врач ультразвуковой диагностики","Врач функциональной диагностики","Врач ЭКО","Врач-эпилептолог","Гастроэнтеролог","Гематолог","Генетик","Гепатолог","Гериатр (геронтолог)","Гинеколог","Гинеколог-онколог","Гинеколог-перинатолог","Гинеколог-эндокринолог","Гирудотерапевт","Гистолог","Гомеопат","Дерматовенеролог","Дерматолог","Детский гинеколог","Детский невропатолог","Детский хирург","Диабетолог","Диетолог","Иглорефлексотерапевт","Иммунолог","Имплантолог","Инфекционист","Кардиолог","Кардиохирург","Кинезиолог","КЛД (лаборант)","Комбустиолог","Косметолог-дерматолог","Курортолог","Логопед","ЛФК-врач","Маммолог","Миколог","Микрохирург","Нарколог","Натуротерапевт","Невропатолог","Нейрохирург","Неонатолог","Нефролог","Окулист","Онколог","Онкоуролог","Ортопед","Остеопат","Отоларинголог (ЛОР)","Паразитолог","Пародонтолог","Педиатр","Пластический хирург","Подолог","Проктолог (колопроктолог)","Профпатолог","Психиатр","Психиатр-нарколог","Психоаналитик","Психолог","Психоневролог","Психотерапевт","Пульмонолог","Радиолог","Реабилитолог","Реаниматолог","Ревматолог (кардиоревматолог)","Рентгенолог","Рефлексотерапевт","Сексолог","Сексопатолог", };
            comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection autoCompleteSource = new AutoCompleteStringCollection();
            // Заполняем коллекцию словами из списка
            autoCompleteSource.AddRange(wordsList.ToArray());
            comboBox1.AutoCompleteCustomSource = autoCompleteSource;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "регистратура12DataSet1.Роль". При необходимости она может быть перемещена или удалена.
            this.рольTableAdapter.Fill(this.регистратура12DataSet1.Роль);

            try
            {
                db db = new db();
                db.openconn();
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand($"SELECT Врачи.Фамилия+' '+ LEFT(Врачи.Имя,1)+'.'+LEFT(Врачи.Отчество,1)+'.' as [Доктор], Врачи.Кабинет,Роль.Наименование, Врачи.ПН, Врачи.ВТ, Врачи.СР, Врачи.ЧТ, Врачи.ПТ\r\nFROM            Врачи INNER JOIN\r\n                         Роль ON Врачи.[Код роли] = Роль.[Код роли]", db.getconn());
                adapter.SelectCommand = command;
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                label10.ForeColor = Color.Red;
                Console.ForegroundColor = ConsoleColor.Red;
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }
        static void SetRoundedShape(Control control, int radius)
        {
            try
            {
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddLine(radius, 0, control.Width - radius, 0);
                path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
                path.AddLine(control.Width, radius, control.Width, control.Height - radius);
                path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
                path.AddLine(control.Width - radius, control.Height, radius, control.Height);
                path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
                path.AddLine(0, control.Height - radius, 0, radius);
                path.AddArc(0, 0, radius, radius, 180, 90);
                control.Region = new Region(path);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
        }
        bool a = true;
        int x, y;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //    Form2 frm = new Form2();
            //frm.dataGridView2.Rows = dataGridView1.Rows;
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                DataGridView dataGridView = dataGridView1;
                int rowHeight = dataGridView.RowTemplate.Height;
                int columnWidth;
                int height = rowHeight;
                // Печать заголовка таблицы
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    columnWidth = dataGridView.Columns[i].Width;
                    e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(0, height, columnWidth, rowHeight));
                    e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, height, columnWidth, rowHeight));
                    e.Graphics.DrawString(dataGridView.Columns[i].HeaderText, dataGridView.Font, Brushes.Black, new RectangleF(0, height, columnWidth, rowHeight));
                    height += rowHeight;
                }
                // Печать данных таблицы
                for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
                {
                    height += rowHeight;
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        columnWidth = dataGridView.Columns[j].Width;
                        e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, height, columnWidth, rowHeight));
                        e.Graphics.DrawString(dataGridView.Rows[i].Cells[j].FormattedValue.ToString(), dataGridView.Font, Brushes.Black, new RectangleF(0, height, columnWidth, rowHeight));
                    }
                }
            }
            catch (Exception ex)
            {
                label10.ForeColor = Color.Red;
                Console.ForegroundColor = ConsoleColor.Red;
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                PrintDocument printDocument = new PrintDocument();
                printDialog.Document = printDocument;
                printDocument.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument.Print();
                }
            }
            catch (Exception ex)
            {
                label10.ForeColor = Color.Red;
                Console.ForegroundColor = ConsoleColor.Red;
                MessageBox.Show("ошибка печати: " + ex.Message);
            }
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            panel1.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            maskedTextBox1.Clear();
            maskedTextBox2.Clear();
            maskedTextBox3.Clear();
            maskedTextBox4.Clear();
            maskedTextBox5.Clear();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel1.Hide();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel2.Hide();
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            a = true;
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!a)
            {
                Panel mPanel = (Panel)sender;
                mPanel.Left += e.X - x;
                mPanel.Top += e.Y - y;
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Panel mPanel = (Panel)sender;
            // стартовая позиция
            x = e.X;
            y = e.Y;
            a = false;//переменная , нажата ли кнопка мыши
        }
        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            a = true;
        }
        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (!a)
            {
                Panel mPanel = (Panel)sender;
                mPanel.Left += e.X - x;
                mPanel.Top += e.Y - y;
            }
        }
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            Panel mPanel = (Panel)sender;
            // стартовая позиция
            x = e.X;
            y = e.Y;
            a = false;//переменная , нажата ли кнопка мыши
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            try
            {
                db db = new db();
                db.openconn();
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand($"SELECT Врачи.[Код врача], Врачи.Фамилия+' '+ LEFT(Врачи.Имя,1)+'.'+LEFT(Врачи.Отчество,1)+'.' as [Доктор], Врачи.Кабинет,Роль.Наименование, Врачи.ПН, Врачи.ВТ, Врачи.СР, Врачи.ЧТ, Врачи.ПТ\r\nFROM            Врачи INNER JOIN\r\n                         Роль ON Врачи.[Код роли] = Роль.[Код роли]", db.getconn());
                adapter.SelectCommand = command;
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                label10.ForeColor = Color.Red;
                Console.ForegroundColor = ConsoleColor.Red;
                MessageBox.Show("ошибка удаления записи: " + ex.Message);
            }
        }
        //private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        //{
        //    if (e.RowIndex == -1)
        //    {
        //        return;
        //    }
        //    if (e.RowIndex % 2 == 0 || (e.RowIndex - 1) % 2 == 0)
        //    {
        //        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(95, 211, 255);
        //        return;
        //    }
        //    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(0, 69, 255);
        //}
        public void toolStripButton5_Click(object sender, EventArgs e)
        {
            Form2 form3 = new Form2();
            form3.Show();
        }
        public static Color TableColor { get; set; }
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            panel3.Show();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label10.Text = DateTime.Now.ToLongTimeString();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            panel3.Hide();
        }
        private void panel3_MouseUp(object sender, MouseEventArgs e)
        {
            a = true;
        }
        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (!a)
            {
                Panel mPanel = (Panel)sender;
                mPanel.Left += e.X - x;
                mPanel.Top += e.Y - y;
            }
        }
        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            Panel mPanel = (Panel)sender;
            // стартовая позиция
            x = e.X;
            y = e.Y;
            a = false;//переменная , нажата ли кнопка мыши
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // установка шрифта
            dataGridView1.Font = fontDialog1.Font;
            // установка цвета шрифта
            dataGridView1.ForeColor = fontDialog1.Color;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // установка цвета формы
            panel1.BackColor = colorDialog1.Color;
            panel2.BackColor = colorDialog1.Color;
            panel3.BackColor = colorDialog1.Color;
        }
        public void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    Color color1 = colorDialog1.Color;
                    if (colorDialog1.ShowDialog() == DialogResult.OK)
                    {
                        Color color2 = colorDialog1.Color;
                        int rowIndex = 0;
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (rowIndex % 2 == 0)
                            {
                                row.DefaultCellStyle.BackColor = color1;
                            }
                            else
                            {
                                row.DefaultCellStyle.BackColor = color2;
                            }
                            rowIndex++;
                        }
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (rowIndex % 2 == 0)
                            {
                                row.DefaultCellStyle.BackColor = color1;
                            }
                            else
                            {
                                row.DefaultCellStyle.BackColor = color2;
                            }
                            rowIndex++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                label10.ForeColor = Color.Red;
                Console.ForegroundColor = ConsoleColor.Red;
                MessageBox.Show("Произошла ошибка, интерфейс не изменен: " + ex.Message);
            }
        }
        public void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Access Database (*.db;*.accdb)|*.db;*.accdb|SQLite Database (*.sqlite)|*.sqlite|All Files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = openFileDialog1.FileName;
                string filePath = openFileDialog1.FileName;
                // Продолжайте работу с файлом базы данных
                MessageBox.Show("Выбран файл: " + filePath);
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridView1.ClearSelection();
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            //try
            //{
            //    using (SQLiteConnection conn = new SQLiteConnection($"Data Source =C:/db/raspisanie.db;Version =3"))
            //    {
            //        conn.Open();
            //        string comText = "INSERT INTO doctors (Кабинет, Доктор, Специалист, Понедельник, Вторник, Среда, Четверг, Пятница) VALUES (@cab, @name, @spec, @mo, @tu, @we, @su, @fr)";
            //        SQLiteCommand command = new SQLiteCommand
            //        {
            //            Connection = conn,
            //            CommandText = comText
            //        };
            //        command.Parameters.AddWithValue("@cab", numericUpDown1.Text);
            //        command.Parameters.AddWithValue("@name", textBox1.Text);
            //        command.Parameters.AddWithValue("@spec", textBox2.Text);
            //        command.Parameters.AddWithValue("@mo", maskedTextBox1.Text);
            //        command.Parameters.AddWithValue("@tu", maskedTextBox2.Text);
            //        command.Parameters.AddWithValue("@we", maskedTextBox3.Text);
            //        command.Parameters.AddWithValue("@su", maskedTextBox4.Text);
            //        command.Parameters.AddWithValue("@fr", maskedTextBox5.Text);
            //        textBox1.Clear();
            //        textBox2.Clear();
            //        maskedTextBox1.Clear();
            //        maskedTextBox2.Clear();
            //        maskedTextBox3.Clear();
            //        maskedTextBox4.Clear();
            //        maskedTextBox5.Clear();
            //        numericUpDown1.Value = 0;
            //        command.ExecuteNonQuery();
            //        MessageBox.Show("запись создана");
            //        command.Connection = conn;
            //        command.CommandText = "SELECT * FROM doctors";
            //        command.ExecuteNonQuery();
            //        DataTable dt = new DataTable();
            //        SQLiteDataAdapter adap = new SQLiteDataAdapter(command);
            //        adap.Fill(dt);
            //        dataGridView1.DataSource = dt;
            //        panel1.Hide();
            //        panel2.Hide();
            //        this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    label10.ForeColor = Color.Red;
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    MessageBox.Show("ошибка создания записи: " + ex.Message);
            //}
        }
    }
}
