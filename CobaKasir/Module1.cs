using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace CobaKasir
{
    internal class Module1
    {
        public SqlConnection conn;
        public SqlDataAdapter da;
        public SqlDataReader dr;
        public DataTable dt;

        public readonly string namaServer = "Data Source=DESKTOP-5UJ2CS8\\SQLEXPRESS;Initial Catalog=kasir;Integrated Security=True";

        public SqlCommand cmd;

        public void koneksi()
        {
            conn = new SqlConnection(namaServer);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        public void closeKoneksi()
        {
            conn.Close();
            cmd.Dispose();
        }

        public DataTable getData(string sql){
            koneksi();
            try
            {
                cmd = new SqlCommand(sql, conn);
                da = new SqlDataAdapter();
                dt = new DataTable();
                da.SelectCommand = cmd;
                da.Fill(dt);
                return dt;
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                closeKoneksi(); 
            }
        }

        public int getCount(string sql)
        {
            koneksi();
            try
            {
                cmd = new SqlCommand(sql, conn);
                da = new SqlDataAdapter();
                dt = new DataTable();
                da.SelectCommand = cmd;
                da.Fill(dt);
                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                closeKoneksi();
            }
        }
        
        public object getValue(string sql, string col)
        {
            koneksi();
            object value = null;
            try
            {
                cmd = new SqlCommand(sql, conn);
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    if (dr.IsDBNull(dr.GetOrdinal(col))){
                        return "";
                    }
                    else
                    {
                        value = dr[col];
                        return value;
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
            finally
            {
                closeKoneksi();
            }
        }

        public bool exc(string sql)
        {
            koneksi();
            try
            {
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                return true;
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                closeKoneksi();
            }
        }

        public bool adaKosong(GroupBox gb)
        {
            foreach (Control ct in gb.Controls)
            {
                if (ct is TextBox textBox && textBox.Text.Trim() == string.Empty)
                {
                    return true; 
                }
            }
            return false;
        }

        public void clearForm(GroupBox gb)
        {
            foreach (Control ct in gb.Controls)
            {
                if (ct is TextBox tx)
                {
                    tx.Text = "";
                }else if (ct is NumericUpDown np)
                {
                    np.Value = 0;
                }
                else if (ct is ComboBox cb)
                {
                    cb.SelectedIndex = 0;
                }
            }
        }

        public void onlyNumber(KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Back)
            {
                if (e.KeyChar < '0' || e.KeyChar > '9')
                {
                    e.Handled = true;
                }
            }
        }

        public bool dialogForm(string s)
        {
            DialogResult a = MessageBox.Show(s, "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }

    }

}
