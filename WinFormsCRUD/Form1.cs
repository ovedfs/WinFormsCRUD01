using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsCRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*if (PeopleDB.Test())
            {
                MessageBox.Show("Test ok");
            }
            else
            {
                MessageBox.Show("Test fail");
            }*/

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            dataGridView1.DataSource = PeopleDB.GetPeople();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            FormAdd form = new FormAdd();
            form.ShowDialog();
            RefreshGrid();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            int? id = GetId();

            if (id != null)
            {
                FormAdd form = new FormAdd(GetId());
                form.ShowDialog();
                RefreshGrid();
            }
        }

        private int? GetId()
        {
            try
            {
                return int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar este registro?", "Eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                int? id = GetId();

                if (id != null)
                {
                    PeopleDB.Delete((int)id);
                    RefreshGrid();
                }
            }
        }
    }
}
