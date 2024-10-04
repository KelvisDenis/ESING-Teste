# Projeto - Desafio Técnico ESIG Group

## Descrição do Projeto
Este projeto foi desenvolvido como parte do processo seletivo para o **ESIG Group**, com o objetivo de avaliar habilidades técnicas e estilo de codificação. A tarefa consiste no desenvolvimento de uma aplicação **ASP.NET Web Forms** que manipula um conjunto de dados fornecidos e implementa funcionalidades de cálculo de salários e um CRUD (Create, Read, Update, Delete) para a tabela de pessoas.

A aplicação conecta-se a um banco de dados **PostgreSQL** e processa os dados das tabelas **pessoa** e **cargo**, preenchendo uma tabela adicional chamada **pessoa_salario** com as informações calculadas.

## Estrutura do Banco de Dados
O banco de dados **PostgreSQL** utilizado no projeto contém as seguintes tabelas:

### Tabela: Pessoa
- **ID** (SERIAL PRIMARY KEY)
- **Nome** (TEXT NOT NULL)
- **Cidade** (TEXT NOT NULL)
- **Email** (TEXT)
- **CEP** (TEXT)
- **Endereço** (TEXT)
- **País** (TEXT)
- **Usuário** (TEXT)
- **Telefone** (TEXT)
- **Data_Nascimento** (DATE)
- **Cargo_ID** (INT)

### Tabela: Cargo
- **ID** (SERIAL PRIMARY KEY)
- **Nome** (TEXT NOT NULL)
- **Salário** (INT NOT NULL)

### Tabela: Pessoa_Salario
- **ID** (SERIAL PRIMARY KEY)
- **Nome** (TEXT)
- **Salário** (INT)

> **Observação:** Essas tabelas são automaticamente criadas e populadas pelo código da aplicação, não sendo necessária qualquer configuração manual adicional.

## Funcionalidades Implementadas

1. **Listagem de Salários:**
   - A aplicação exibe uma listagem dos salários das pessoas, que é preenchida automaticamente a partir dos dados presentes nas tabelas **pessoa** e **cargo**. A tabela **pessoa_salario** é atualizada com base nesses dados.

2. **Cálculo/Recalcular Salários:**
   - A aplicação oferece uma funcionalidade para calcular ou recalcular os salários das pessoas, atualizando a tabela **pessoa_salario**. O cálculo é executado de forma assíncrona para otimizar o desempenho.

3. **Processamento Assíncrono:**
   - A implementação do cálculo dos salários foi feita de forma assíncrona, utilizando boas práticas em **C#**, garantindo eficiência durante o processamento de grandes volumes de dados.

4. **CRUD de Pessoa:**
   - A aplicação inclui um CRUD completo para a tabela **Pessoa**, permitindo criar, ler, atualizar e excluir registros diretamente na interface.

## Como Executar o Projeto

### Pré-requisitos
- **PostgreSQL**
- **.NET SDK**
- **Visual Studio** de preferência a versão 2019.

### Passos para Executar

1. **Instalar o PostgreSQL:**
   - Baixe e instale o PostgreSQL na sua máquina local a partir do [site oficial](https://www.postgresql.org/download/).

2. **Configuração do Projeto:**
   - Clone o repositório do projeto no GitHub.
   - Abra a pasta CRUD e selecione projeto CRUD.csproj no **Visual Studio**.
   - Certifique-se de que a string de conexão no arquivo **web.config** **>** **connectionStrings** esteja configurada corretamente    para apontar para seu banco de dados PostgreSQL local.


3. **Execução do Projeto:**
   - Esteja com o projeto **CRUD** selecionado.
   - No **Visual Studio**, vá para **Compilar**  **>** **Limpar** **Solução**.
   - Em seguida vá **Compilar**  **>** **Recompilar** **Solução**
   - Execute a aplicação diretamente do **Visual Studio** clicando em **IIS** **Express**.
   - O banco de dados será automaticamente configurado, e as tabelas necessárias serão criadas.
   - Navegue até a interface da aplicação pelo navegador para visualizar a listagem de salários e utilizar as funções de CRUD.
   - Caso dê algum erro, repita novamente os mesmos passos com o projeto CRUD.csproj selecionado no **Gerenciador** **de** **Soluções**.


## Considerações Finais
Este projeto foi desenvolvido com foco em:
- Conexão e manipulação de dados em um banco de dados relacional (**PostgreSQL**).
- Implementação de uma aplicação web utilizando **ASP.NET Web Forms**.
- Processamento de dados assíncrono em **C#** para melhor desempenho.
- Implementação de um **CRUD** para a tabela de pessoas.

---

Desenvolvido por Denis Kelvis.
