﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercicio02Colaboradores
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if(lblId.Text == "0")
            {
                Inserir();
            }
            else
            {
                Alterar();
            }
        }

        private void Alterar()
        {
            Colaborador colaborador = new Colaborador();
            colaborador.Id = Convert.ToInt32(lblId.Text);
            colaborador.Nome = txtNome.Text;
            colaborador.Cpf = mtbCpf.Text;
            colaborador.Salario = Convert.ToDecimal(mtbSalario.Text.Replace("R$", ""));
            colaborador.Sexo = cbSexo.SelectedItem.ToString();
            colaborador.Cargo = cbCargo.SelectedItem.ToString();
            if (ckbSim.Checked)
            {
                colaborador.Programador = true;
            }
            else
            {
                colaborador.Programador = false;
            }

            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=T:\Documentos\Exercicios.mdf;Integrated Security=True;Connect Timeout=30";
            conexao.Open();

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = @"UPDATE colaboradores SET
nome = @NOME,
cpf = @CPF,
salario = @SALARIO,
sexo = @SEXO,
cargo = @CARGO,
programador = @PROGRAMADOR
WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", colaborador.Id);
            comando.Parameters.AddWithValue("@NOME", colaborador.Nome);
            comando.Parameters.AddWithValue("@CPF", colaborador.Cpf);
            comando.Parameters.AddWithValue("@SALARIO", colaborador.Salario);
            comando.Parameters.AddWithValue("@SEXO", colaborador.Sexo);
            comando.Parameters.AddWithValue("@CARGO", colaborador.Cargo);
            comando.Parameters.AddWithValue("@PROGRAMADOR", colaborador.Programador);
            comando.ExecuteNonQuery();
            conexao.Close();
            AtualizarTabela();
            LimparCampos();

            MessageBox.Show("Registro alterado com sucesso");

        }

        private void Inserir()
        {
            Colaborador colaborador = new Colaborador();
            colaborador.Nome = "";
            try
            {
                colaborador.Nome = txtNome.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Preenca adequadamente os campos");
                txtNome.Clear();
                txtNome.Focus();
                return;
            }

            colaborador.Cpf = "";
            try
            {
                colaborador.Cpf = mtbCpf.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Preenca adequadamente os campos");
                mtbCpf.Clear();
                mtbCpf.Focus();
                return;
            }

            colaborador.Salario = 0;
            try
            {
                colaborador.Salario = Convert.ToDecimal(mtbSalario.Text.Replace("R$", ""));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Preenca adequadamente os campos");
                mtbSalario.Clear();
                mtbSalario.Focus();
                return;
            }

            colaborador.Sexo = cbSexo.SelectedItem.ToString();
            if (cbSexo.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione o Sexo");
                cbSexo.Focus();
                cbSexo.DroppedDown = true;
                return;
            }

            colaborador.Cargo = cbCargo.SelectedItem.ToString();
            if (cbCargo.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione o Cargo");
                cbCargo.Focus();
                cbCargo.DroppedDown = true;
                return;
            }

            if (ckbSim.Checked)
            {
                colaborador.Programador = true;
            }
            else
            {
                colaborador.Programador = false;
            }

            //Desenvolvimento da tela de cadastro
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = @"";
            conexao.Open();

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = @"INSERT INTO colaboradores
(nome, cpf, salario, sexo, cargo, programador)
VALUES (@NOME, @CPF, @SALARIO, @SEXO, @CARGO, @PROGRAMADOR)";

            comando.Parameters.AddWithValue("@NOME", colaborador.Nome);
            comando.Parameters.AddWithValue("@CPF", colaborador.Cpf);
            comando.Parameters.AddWithValue("@SALARIO", colaborador.Salario);
            comando.Parameters.AddWithValue("@SEXO", colaborador.Sexo);
            comando.Parameters.AddWithValue("@CARGO", colaborador.Cargo);
            comando.Parameters.AddWithValue("@PROGRAMADOR", colaborador.Programador);
            comando.ExecuteNonQuery();
            MessageBox.Show("Registro criado com sucesso");
            LimparCampos();
            conexao.Close();
            AtualizarTabela();

        }

        private void LimparCampos()
        {
            lblId.Text = "0";
            txtNome.Clear();
            mtbCpf.Clear();
            mtbSalario.Clear();
            cbSexo.SelectedIndex = -1;
            cbCargo.SelectedIndex = -1;
        }

        private void AtualizarTabela()
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = @"";
            conexao.Open();

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = "SELECT * FROM colaboradores";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            dataGridView1.RowCount = 0;
            for(int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                Colaborador colaborador = new Colaborador();
                colaborador.Id = Convert.ToInt32(linha["id"]);
                colaborador.Nome = linha["nome"].ToString();
                colaborador.Cpf = linha["cpf"].ToString();
                colaborador.Salario = Convert.ToDecimal(linha["salario"]);
                colaborador.Sexo = linha["sexo"].ToString();
                colaborador.Cargo = linha["cargo"].ToString();
                colaborador.Programador = linha["programador"].ToString();
                dataGridView1.Rows.Add(new string[] { colaborador.Id.ToString(), colaborador.Nome, colaborador.Cpf, colaborador.Salario.ToString(), colaborador.Sexo, colaborador.Cargo, colaborador.Programador.ToString() });

            }
        }
    }
}
