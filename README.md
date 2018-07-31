# Exemplo de CRUD usando Windows Forms com C# e ADO .NET

Para utilizar, criar um banco de dados e uma tabela usando a seguinte query:

CREATE TABLE [dbo].[agenda]
{
  [codigo] INT NOT NULL PRIMARY KEY,
  [nome] VARCHAR(35) NOT NULL,
  [email] VARCHAR(35) NULL,
  [telefone] VARCHAR(14) NULL
}

E utilizar a string de conexão deste banco na classe Dados.cs
