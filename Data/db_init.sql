-- Criação da tabela 'pessoa'
CREATE TABLE IF NOT EXISTS Pessoa (
    ID SERIAL PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Cidade INT NOT NULL,
    Email VARCHAR(255),
    CEP VARCHAR(255),
    Endereco VARCHAR(255),
    Pais VARCHAR(255),
    Usuario VARCHAR(255),
    Telefone VARCHAR(255),
    Data_Nascimento DATE,
    Cargo_ID INT
);

-- Criação da tabela 'cargo'
CREATE TABLE IF NOT EXISTS Cargo (
    ID SERIAL PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Salario INT
);

-- Criação da tabela 'pessoa_salario'
CREATE TABLE IF NOT EXISTS Pessoa_Salario (
    ID INT,
    Nome VARCHAR(255),
    Salario INT
    
);

COPY Pessoa(Nome, Cidade, Email, CEP, Endereco, Pais, Usuario, Telefone, Data_Nascimento, Cargo_ID)
FROM '/data/Pessoas-Pessoa.csv'
DELIMITER ',' CSV HEADER;

COPY Cargo(Nome, Salario)
FROM '/data/Cargo-Plan1.csv'
DELIMITER ',' CSV HEADER;
