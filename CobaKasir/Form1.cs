using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CobaKasir
{
    public partial class Form1 : Form
    {
        Module1 mod = new Module1();
        string id = "0";
        bool aksi = false;  


        public Form1()
        {
            InitializeComponent();
        }

        public void awal()
        {
            dataGridView1.DataSource = mod.getData("select * from transaksi where pembeli like '%" + TextBox1.Text + "%'");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Penjual";
            dataGridView1.Columns[2].HeaderText = "Barang";
            dataGridView1.Columns[3].HeaderText = "Harga";
            dataGridView1.Columns[4].HeaderText = "Jumlah";
            dataGridView1.Columns[5].HeaderText = "Total";
            GroupBox1.Enabled = true;
            GroupBox2.Enabled = false;
            GroupBox3.Enabled = true;
            id = "0";
            aksi = false;
        }

        public void buka() 
        {
            GroupBox1.Enabled = false;
            GroupBox2.Enabled = true;
            GroupBox3.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            awal();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            mod.clearForm(GroupBox2);
            buka(); 
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (id == "0")
            {
                MessageBox.Show("Pilih data dulu..");
            }
            else
            {
                aksi = true;
                buka();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (id == "0")
            {
                MessageBox.Show("Pilih data dulu..");
            }
            else
            {
                if (mod.dialogForm("Apakah anda yakin ingin menghapus data ini..?"))
                {
                    var sql = "delete from transaksi where id =" + id;
                    mod.exc(sql);
                    mod.clearForm(GroupBox2);
                    awal();
                }
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            awal();
            mod.clearForm(GroupBox2);
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            awal();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)        
            {
                TextBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                TextBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                TextBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                NumericUpDown1.Value = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                TextBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

                id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void TextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            mod.onlyNumber(e);
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            if (TextBox4.Text == "")
            {
                TextBox5.Text = "0";
                TextBox4.Text = "0";
            }
            else
            {
                //TextBox5.Text = double.Parse(TextBox4.Text) * NumericUpDown1.Value;
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (mod.adaKosong(GroupBox2))
            {
                MessageBox.Show("Ada data kosong");
            }
            else
            {
                string sql;
                if (!aksi)
                {
                    sql = "insert into transaksi values ('" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + NumericUpDown1.Value + "','" + TextBox5.Text + "')";
                    //MessageBox.Show(sql);
                    mod.exc(sql);
                    mod.clearForm(GroupBox2);
                    MessageBox.Show("Data berhasil ditambah");
                    awal();
                }
                else
                {
                    sql = "update transaksi set pembeli = '"+ TextBox2.Text +"',barang = '"+ TextBox3.Text +"',harga = '"+ TextBox4.Text +"',jumlah = '"+ NumericUpDown1.Value +"',total = '"+ TextBox5.Text +"' where id = "+id;
                    //MessageBox.Show(sql);
                    mod.exc(sql);
                    mod.clearForm(GroupBox2);
                    MessageBox.Show("Data berhasil diubah");
                    awal();
                }
            }
        }
    }
}
