using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MulakatProjesi.Models;

namespace MulakatProjesi
{
    public partial class Form1 : Form
    {
        TelefonEntitiesConnectionDb db = new TelefonEntitiesConnectionDb();
        public Form1()
        {
            InitializeComponent();
        }

        public void Yenile()
        {
            dataGridView1.DataSource = db.Rehber.ToList();
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Yenile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Rehber rehber = new Rehber();
                rehber.Ad = textBox1.Text;
                rehber.Soyad = textBox2.Text;
                rehber.Telefon = textBox3.Text;
                rehber.Mail = textBox4.Text;
                db.Rehber.Add(rehber);
                db.SaveChanges();
                Yenile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                Rehber rehber = db.Rehber.FirstOrDefault(x => x.id == id);
                rehber.Ad = textBox1.Text;
                rehber.Soyad = textBox2.Text;
                rehber.Telefon = textBox3.Text;
                rehber.Mail = textBox4.Text;
                db.SaveChanges();
                Yenile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                Rehber rehber = db.Rehber.FirstOrDefault(x => x.id == id);
                DialogResult dr = MessageBox.Show($"{dataGridView1.CurrentRow.Cells[1].Value.ToString()} {dataGridView1.CurrentRow.Cells[2].Value.ToString()} Kişini kalıcı olarak silmek istediğinizden emin misiniz?", "Kişi Silinecek", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dr == DialogResult.Yes)
                {
                    db.Rehber.Remove(rehber);
                    db.SaveChanges();
                }
                Yenile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = db.Rehber.Where(x => x.Ad.Contains(textBox5.Text) || 
                x.Soyad.Contains(textBox5.Text) || 
                x.Telefon.Contains(textBox5.Text)||
                x.Mail.Contains(textBox5.Text)
                ).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
