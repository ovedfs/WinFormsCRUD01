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
    public partial class FormAdd : Form
    {
        private int? Id;

        public FormAdd(int? id = null)
        {
            InitializeComponent();

            Id = id;

            if (id != null)
            {
                LoadPerson((int)id);
            }
        }

        private void LoadPerson(int id)
        {
            Person person = PeopleDB.GetPeople(id);
            txtName.Text = person.Name;
            txtAge.Text = person.Age.ToString();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtName.Text) || String.IsNullOrEmpty(txtAge.Text))
            {
                MessageBox.Show("Todos los datos son obligatorios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Person person = new Person()
            {
                Name = txtName.Text,
                Age = Int32.Parse(txtAge.Text)
            };

            if (Id != null)
            {
                if (PeopleDB.Update(person, (int)Id) == -1)
                {
                    MessageBox.Show("No se pudo actualizar el registro, verifique", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                if (PeopleDB.Insert(person) == -1)
                {
                    MessageBox.Show("No se pudo guardar el registro, verifique", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            this.Close();
        }
    }
}
