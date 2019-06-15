DROP TABLE colaboradores;
CREATE TABLE colaboradores (
id INT PRIMARY KEY IDENTITY(1,1),
nome VARCHAR(100),
cpf VARCHAR(14),
salario DECIMAL(8,2),
sexo VARCHAR(20),
cargo VARCHAR(50),
programador BIT
);

SELECT * FROM colaboradores;


