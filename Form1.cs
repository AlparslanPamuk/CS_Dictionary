using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;    // access veri tabanı kodlarını çağırmamızı sağlar

namespace CS_Dictionary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        OleDbConnection connection = new OleDbConnection("Provider=Microsoft.JET.OleDb.4.0;Data Source=" 
                                                         + Application.StartupPath + "\\db_dictionary.mdb");

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand addCommand = new OleDbCommand("insert into engturk (english,turkish) values('" + textBox1.Text + "','" +
                textBox2.Text + "')",connection);
            addCommand.ExecuteNonQuery(); // ekle komutunun tanımlandığı sorguyu çalıştır, sonuçları veri tabanına işle.
            connection.Close();
            MessageBox.Show("Word has been added to DataBase.", "DataBase Movements");
            textBox1.Clear();
            textBox2.Clear();
            try
            {

            }
            catch (Exception explanation)
            {
                MessageBox.Show(explanation.Message, "DataBase Movements");
                connection.Close();    //---------------hata olursa yuardaki bağlantı kapatma çalışmayacak o yüzdenburda kapatıyoruz-----------
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                connection.Open();
                OleDbCommand updateCommand = new OleDbCommand("update engturk set turkish= '" + textBox2.Text + "' where english = '"
                    + textBox1.Text + "'", connection);
                updateCommand.ExecuteNonQuery();// güncelle komutunun tanımlandığı sorguyu çalıştır, sonuçları veri tabanına işle.
                connection.Close();
                MessageBox.Show("Word has been updated on DataBase.", "DataBase Movements");
                textBox1.Clear();
                textBox2.Clear();
            }
            catch (Exception explanation)
            {
                MessageBox.Show(explanation.Message, "DataBase Movements");
                connection.Close();    //---------------hata olursa yuardaki bağlantı kapatma çalışmayacak o yüzdenburda kapatıyoruz-----------
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand deletecommand = new OleDbCommand("delete from engturk where english = '" + textBox1.Text + "'", connection);
                deletecommand.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Word has been deleted from DataBase", "DataBase Movements");
                textBox1.Clear();
                textBox2.Clear();
            }
            catch (Exception explanation)
            {
            MessageBox.Show(explanation.Message, "Databese Movements");
                connection.Close();
                
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                connection.Open();
                OleDbCommand searchcommand = new OleDbCommand("select english,turkish from engturk where english like'" +
                    textBox1.Text + "%'", connection);
                // ---------------girilen harfle ilgili her kaydı getirir-------------
                OleDbDataReader read = searchcommand.ExecuteReader();
                //------------ veri tabanından veri okumayı gerçekleştirir, okuma işlemi gerçekleştirir |Tablodaki kayıtların okunmasını sağlıyoruz|---------------
                while (read.Read()) //------Eğer herhangibi kayıt bulunmuşsa: ----------
                {
                    listBox1.Items.Add(read["english"].ToString() + "=" + read["turkish"].ToString()); // bir kelime aranıp bulunmuşsa listbox 1'e ingilizce kaydın değerini yazdırır,----
                    //-----------Eşittir'den sonra türkçe karşılığını yazdırır. ---------------
                }
                connection.Close();
            }
            catch (Exception explanation)
            {
                MessageBox.Show(explanation.Message, "DataBase Movements");
                connection.Close();
            }
        }
    }
}
