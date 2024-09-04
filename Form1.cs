using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lab_03
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Este método se usa para inicializar datos cuando se carga el formulario
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cadena = "Server=LAB1507-13\\SQLEXPRESS03; Database=Tecsup20203DB1; Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM Estudiantes", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al listar datos con DataTable: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cadena = "Server=LAB1507-13\\SQLEXPRESS03; Database=Tecsup20203DB1; Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT * FROM Estudiantes", connection);
                    SqlDataReader reader = command.ExecuteReader();

                    List<Estudiante> listaEstudiantes = new List<Estudiante>();

                    while (reader.Read())
                    {
                        Estudiante estudiante = new Estudiante
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = reader["Nombre"].ToString(),
                            Precio = Convert.ToDecimal(reader["Precio"])
                        };
                        listaEstudiantes.Add(estudiante);
                    }

                    reader.Close();

                    // Configura el DataGridView para mostrar la lista de estudiantes
                    ConfigureDataGridViewEstudiantes();
                    dataGridView1.DataSource = listaEstudiantes;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al listar datos con SqlDataReader: {ex.Message}");
            }
        }

        private void ConfigureDataGridViewEstudiantes()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "ID",
                Name = "Id"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nombre",
                HeaderText = "Nombre",
                Name = "Nombre"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Precio",
                HeaderText = "Precio",
                Name = "Precio"
            });
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Implementa la lógica para manejar el clic en la celda si es necesario
        }
    }

    // Clase Estudiante
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
    }
}
