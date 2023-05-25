using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkOrderTracker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Get the transaction entry number from the textbox.
            int entryNumber = int.Parse(txtSearchTX.Text);

            // Connect to the database.
            string connectionString = "Data Source=localhost;Initial Catalog=WorkOrderTracker;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Execute a query to get the work order details.
                string query = @"
            SELECT
                w.Name,
                w.Description,
                t.EntryNumber,
                t.Description,
                ti.ItemName,
                ti.Quantity,
                ti.UnitPrice,
                ti.Total
            FROM dbo.WorkOrder w
            INNER JOIN dbo.WOTransaction t ON w.ID = t.WorkOrderID
            INNER JOIN dbo.WOTransactionItem ti ON t.ID = ti.WOTransactionID
            WHERE t.EntryNumber = @entryNumber;
        ";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@entryNumber", entryNumber);

                // Execute the query.
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Display the results in a grid.
                GridView gridView = new GridView();
                gridView.Dock = DockStyle.Fill;
                gridView.DataSource = reader;

                // Add the grid view to the form.
                this.Controls.Add(gridView);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }