# API de Controle de Estoque

Uma API RESTful desenvolvida em ASP.NET Core utilizando Entity Framework Core para o gerenciamento completo de um sistema de estoque. O projeto conta com operações de cadastro, listagem, filtragem por nome, edição e exclusão de produtos.

## 🚀 Tecnologias Utilizadas

* C# / .NET Core
* ASP.NET Core Web API
* Entity Framework Core (ORM)
* Banco de Dados Relacional (SQL Server / MySQL)

## 🛠️ Funcionalidades da API

* **Cadastro de Produtos (POST):** Permite registrar novos itens informando nome, categoria, preço e quantidade.
* **Listagem Geral (GET):** Retorna todos os produtos cadastrados no estoque.
* **Filtro por Nome (GET):** Realiza buscas parciais flexíveis por termos no nome do produto.
* **Edição de Produtos (PUT):** Atualiza as informações de um item existente buscando pelo ID.
* **Exclusão de Produtos (DELETE):** Remove um registro do banco de dados com base no ID fornecido e validação de existência.

## ⚙️ Como Executar o Projeto

1. Clone este repositório em sua máquina:
   
  ```bash
  git clone https://github.com/wesley-dcezzz/controleEstoque.git
 ```
2. Abra o projeto em sua IDE de preferência (como o Visual Studio ou Visual Studio Code).

3. Configure a string de conexão com o seu banco de dados no arquivo appsettings.json (você pode se basear no appsettings.example.json).

4. Abra o terminal na pasta raiz do projeto e execute as migrações para criar o banco de dados:
  ```bash
  dotnet ef database update
  ```
5. Inicie a aplicação:
  ```bash
  dotnet run
