using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaNotasAluno
{
    public partial class Form1 : Form
    {
        private string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=sistema_notas;Data Source=DESKTOP-GLQ18K5";

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                string nomeCompleto = txtNomeCompleto.Text;
                string matricula = txtMatricula.Text;
                string cpf = txtCPF.Text;

                decimal nota1 = decimal.Parse(txtNota1.Text);
                decimal nota2 = decimal.Parse(txtNota2.Text);
                decimal nota3 = decimal.Parse(txtNota3.Text);
                decimal nota4 = decimal.Parse(txtNota4.Text);
                decimal media = (nota1 + nota2 + nota3 + nota4) / 4;
                string status = media < 5.5m ? "Reprovado" : media <= 6.0m ? "DP" : "Aprovado";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Insere o aluno e obtém o ID
                    string insertAlunoQuery = "INSERT INTO Aluno (NomeCompleto, Matricula, CPF) VALUES (@NomeCompleto, @Matricula, @CPF); SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(insertAlunoQuery, connection);
                    cmd.Parameters.AddWithValue("@NomeCompleto", nomeCompleto);
                    cmd.Parameters.AddWithValue("@Matricula", matricula);
                    cmd.Parameters.AddWithValue("@CPF", cpf);

                    int alunoId = Convert.ToInt32(cmd.ExecuteScalar());

                    // Insere as notas e o status
                    string insertNotaQuery = @"
                        INSERT INTO Nota (AlunoId, Nota1, Nota2, Nota3, Nota4, Status)
                        VALUES (@AlunoId, @Nota1, @Nota2, @Nota3, @Nota4, @Status)";
                    cmd = new SqlCommand(insertNotaQuery, connection);
                    cmd.Parameters.AddWithValue("@AlunoId", alunoId);
                    cmd.Parameters.AddWithValue("@Nota1", nota1);
                    cmd.Parameters.AddWithValue("@Nota2", nota2);
                    cmd.Parameters.AddWithValue("@Nota3", nota3);
                    cmd.Parameters.AddWithValue("@Nota4", nota4);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Aluno adicionado com sucesso!");
                CarregarDados();
            }
            catch (SqlException ex) when (ex.Number == 2627) // Erro de unicidade
            {
                MessageBox.Show("Erro: Matrícula ou CPF já cadastrado.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar aluno: " + ex.Message);
            }
        }

        private void CarregarDados()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(@"
                    SELECT Aluno.Id, Aluno.NomeCompleto, Aluno.Matricula, Aluno.CPF,
                           Nota.Nota1, Nota.Nota2, Nota.Nota3, Nota.Nota4, Nota.MediaFinal, Nota.Status
                    FROM Aluno
                    JOIN Nota ON Aluno.Id = Nota.AlunoId", connection);

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridAlunos.DataSource = dt;
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            CarregarDados();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridAlunos.SelectedRows.Count > 0)
                {
                    int alunoId = (int)dataGridAlunos.SelectedRows[0].Cells["Id"].Value;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Nota WHERE AlunoId = @AlunoId", connection);
                        cmd.Parameters.AddWithValue("@AlunoId", alunoId);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("DELETE FROM Aluno WHERE Id = @AlunoId", connection);
                        cmd.Parameters.AddWithValue("@AlunoId", alunoId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Aluno excluído com sucesso!");
                    CarregarDados();
                }
                else
                {
                    MessageBox.Show("Selecione um aluno para excluir.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir aluno: " + ex.Message);
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridAlunos.SelectedRows.Count > 0)
                {
                    int alunoId = (int)dataGridAlunos.SelectedRows[0].Cells["Id"].Value;
                    decimal nota1 = decimal.Parse(txtNota1.Text);
                    decimal nota2 = decimal.Parse(txtNota2.Text);
                    decimal nota3 = decimal.Parse(txtNota3.Text);
                    decimal nota4 = decimal.Parse(txtNota4.Text);
                    decimal media = (nota1 + nota2 + nota3 + nota4) / 4;
                    string status = media < 5.5m ? "Reprovado" : media <= 6.0m ? "DP" : "Aprovado";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string updateNotaQuery = @"
                            UPDATE Nota
                            SET Nota1 = @Nota1, Nota2 = @Nota2, Nota3 = @Nota3, Nota4 = @Nota4, Status = @Status
                            WHERE AlunoId = @AlunoId";
                        SqlCommand cmd = new SqlCommand(updateNotaQuery, connection);
                        cmd.Parameters.AddWithValue("@AlunoId", alunoId);
                        cmd.Parameters.AddWithValue("@Nota1", nota1);
                        cmd.Parameters.AddWithValue("@Nota2", nota2);
                        cmd.Parameters.AddWithValue("@Nota3", nota3);
                        cmd.Parameters.AddWithValue("@Nota4", nota4);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Aluno atualizado com sucesso!");
                    CarregarDados();
                }
                else
                {
                    MessageBox.Show("Selecione um aluno para atualizar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar aluno: " + ex.Message);
            }
        }

        private void dataGridAlunos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifique se o clique foi em uma linha válida (não no cabeçalho)
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridAlunos.Rows[e.RowIndex];

                // Preencha os TextBox com os dados da linha selecionada
                txtNomeCompleto.Text = row.Cells["NomeCompleto"].Value.ToString();
                txtMatricula.Text = row.Cells["Matricula"].Value.ToString();
                txtCPF.Text = row.Cells["CPF"].Value.ToString();
                txtNota1.Text = row.Cells["Nota1"].Value.ToString();
                txtNota2.Text = row.Cells["Nota2"].Value.ToString();
                txtNota3.Text = row.Cells["Nota3"].Value.ToString();
                txtNota4.Text = row.Cells["Nota4"].Value.ToString();
            }
        }
    }
}


