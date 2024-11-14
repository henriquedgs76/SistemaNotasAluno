
---

# Sistema de Notas de Alunos

Bem-vindo ao **Sistema de Notas de Alunos**! Este projeto foi desenvolvido em C# com Windows Forms e SQL Server para ajudar professores a gerenciarem as notas dos alunos de forma prática e eficaz, oferecendo um CRUD completo para cadastro de alunos e inserção de notas, além do cálculo automático da média anual e verificação de aprovação.

## 📌 Funcionalidades do Projeto

1. **Cadastro de Alunos**: 
   - Permite que o professor insira dados dos alunos, incluindo nome completo, matrícula (única) e CPF (também único).
   
2. **Lançamento de Notas**:
   - Cada aluno pode ter suas notas inseridas em quatro bimestres. O sistema armazena essas notas e calcula a média automaticamente para facilitar a verificação de aprovação.

3. **Cálculo Automático da Média**:
   - O sistema calcula automaticamente a média das quatro notas inseridas e determina o status do aluno:
     - **Aprovado**: média acima de 6,0.
     - **Reprovado**: média abaixo de 5,5.
     - **DP** (Dependência): média entre 5,5 e 6,0.

4. **Listagem e Edição**:
   - Permite listar todos os alunos cadastrados no sistema em um `DataGridView`, onde o professor pode visualizar e selecionar qualquer aluno para editar as informações.
   
5. **Exclusão de Alunos**:
   - O sistema permite excluir o registro de qualquer aluno, removendo todas as informações associadas a ele, incluindo notas.

## 🚀 Tecnologias Utilizadas

- **C# com Windows Forms**: Interface gráfica para o sistema de notas.
- **SQL Server**: Banco de dados relacional para armazenar os registros de alunos e suas notas.
- **ADO.NET**: Utilizado para conectar o C# ao SQL Server e executar as operações de CRUD.

## 🛠️ Como Executar o Projeto

### Pré-Requisitos

- Visual Studio 2022 com suporte para projetos Windows Forms em C#.
- SQL Server (de preferência SQL Server 2022) e o Management Studio para gerenciamento do banco.
- Configuração da string de conexão para o SQL Server.

### Configuração do Banco de Dados

1. No SQL Server, crie um banco de dados chamado `sistema_notas`.
2. Crie a tabela `Alunos` com a seguinte estrutura:

```sql
CREATE TABLE Alunos (
    Id INT PRIMARY KEY IDENTITY,
    NomeCompleto NVARCHAR(100) NOT NULL,
    Matricula NVARCHAR(20) UNIQUE NOT NULL,
    CPF NVARCHAR(14) UNIQUE NOT NULL,
    Nota1 FLOAT,
    Nota2 FLOAT,
    Nota3 FLOAT,
    Nota4 FLOAT
);
```

3. **String de Conexão**: No código C#, configure a conexão com o banco de dados para a sua máquina. A string utilizada neste projeto é:
   ```csharp
   private string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=sistema_notas;Data Source=DESKTOP-GLQ18K5";
   ```

### Como Rodar

1. Clone o repositório.
2. Abra o projeto no Visual Studio 2022.
3. Compile o projeto e execute-o.
4. Utilize o sistema para cadastrar alunos, inserir notas e visualizar resultados.

## 📂 Explicação dos Arquivos e Funções

- **Form1.cs**: Arquivo principal que contém a interface do sistema e todos os métodos do CRUD.
  
### Funções Principais

1. **Cadastrar Aluno** (`btnCadastrar_Click`): 
   - Valida os dados de matrícula e CPF para garantir que não sejam duplicados.
   - Insere o novo aluno no banco de dados com as informações preenchidas nos campos de `TextBox`.
   
2. **Listar Alunos** (`CarregarDados` e `btnListar_Click`):
   - Carrega todos os registros da tabela `Alunos` e exibe no `DataGridView`.
   - Atualiza a listagem apenas quando o botão **Listar** é clicado, proporcionando uma atualização manual e evitando recarga automática.

3. **Exibir Dados no `DataGridView`** (`dataGridAlunos_CellClick`):
   - Quando um aluno é clicado no `DataGridView`, os dados desse aluno são carregados nos campos `TextBox`, permitindo edição direta.

4. **Editar Aluno** (`btnEditar_Click`):
   - Após modificar os campos de um aluno no formulário, o botão de edição permite que o professor atualize as informações diretamente no banco de dados.

5. **Excluir Aluno** (`btnExcluir_Click`):
   - Remove o aluno selecionado do banco de dados, com base no ID do aluno, e atualiza a listagem.

6. **Calcular Média e Determinar Status**:
   - A média é calculada automaticamente com base nas notas inseridas para cada aluno, e o status de **Aprovado**, **Reprovado** ou **DP** é determinado com base nos critérios de aprovação.

## 💡 Considerações Finais

Este sistema oferece uma solução prática para gerenciar e avaliar o desempenho dos alunos. Com ele, o professor pode cadastrar e gerenciar as informações dos alunos em um ambiente amigável e intuitivo. O projeto pode ser expandido para incluir novos recursos, como relatórios e gráficos de desempenho, para oferecer uma visão ainda mais completa.

Sinta-se à vontade para fazer modificações e aprimoramentos. Se tiver sugestões ou dúvidas, fique à vontade para abrir uma *issue* ou contribuir com o projeto!

---

