CREATE TABLE consulta 
( 
	id_consulta			INT PRIMARY KEY			IDENTITY, 
	id_medico			INT, 
	id_cliente			INT, 
	data				DATE, 
	horario				TIME, 
	valor				DECIMAL(9, 2), 
	status				CHAR, 
	ativo				BIT						DEFAULT  1
); 

CREATE TABLE funcionario 
( 
    id_funcionario     INT PRIMARY KEY			IDENTITY, 
    id_endereco        INT, 
    id_cargo           INT, 
    nome               VARCHAR(255), 
    genero             CHAR, 
    cpf                CHAR(11), 
    data_de_nascimento DATE, 
    ddd_cel            CHAR(2), 
    celular            VARCHAR(11), 
    ddd_tel            CHAR(2), 
    telefone           VARCHAR(11), 
    email              VARCHAR(255), 
    salario            DECIMAL(9, 2), 
    data_de_admissao   DATE, 
    usuario            VARCHAR(255), 
    senha              VARCHAR(128), 
    ativo              BIT						DEFAULT  1 
); 

CREATE TABLE endereco 
( 
    id_endereco		   INT PRIMARY KEY			IDENTITY, 
    cep				   CHAR(8), 
    cidade			   VARCHAR(255), 
    uf				   CHAR(2), 
	bairro			   VARCHAR(255), 
    logradouro		   VARCHAR(255), 
    numero			   VARCHAR(5), 
    complemento		   VARCHAR(255), 
	ativo			   BIT						DEFAULT  1 
); 

CREATE TABLE cargo 
( 
    id_cargo			INT PRIMARY KEY			IDENTITY, 
    descricao			VARCHAR(255),
    ativo				BIT						DEFAULT  1 
);

CREATE TABLE forma_de_pagamento 
( 
    id_forma_pagamento INT PRIMARY KEY			IDENTITY, 
    descricao          VARCHAR(255), 
    ativo              BIT						DEFAULT  1 
); 

CREATE TABLE diferenca_caixa 
( 
    id_diferenca_caixa	INT	PRIMARY KEY			IDENTITY, 
    id_caixa			INT, 
    id_forma_pagamento	INT, 
    valor				DECIMAL(9, 2), 
    ativo				BIT						DEFAULT  1, 
    FOREIGN KEY(id_forma_pagamento) REFERENCES forma_de_pagamento (id_forma_pagamento) 
); 

CREATE TABLE receita 
( 
    id_receita			INT PRIMARY KEY			IDENTITY, 
    id_consulta			INT, 
    olho_esquerdo		INT, 
    olho_direito		INT, 
    ativo				BIT						DEFAULT  1, 
    FOREIGN KEY(id_consulta) REFERENCES consulta (id_consulta) 
); 

CREATE TABLE cliente 
( 
    id_cliente			INT PRIMARY KEY			IDENTITY, 
    id_endereco			INT, 
    nome				VARCHAR(255), 
    genero				CHAR, 
    cpf					CHAR(11), 
    data_de_nascimento	DATE, 
    ddd_cel				CHAR(2), 
    celular				VARCHAR(11), 
    ddd_tel				CHAR(2), 
    telefone			VARCHAR(11), 
    email				VARCHAR(255), 
    foto				VARBINARY(MAX), 
    ativo				BIT						DEFAULT  1, 
    FOREIGN KEY(id_endereco) REFERENCES endereco (id_endereco) 
); 

CREATE TABLE clinica 
( 
    id_clinica			 INT PRIMARY KEY			 IDENTITY, 
    razao_social		 VARCHAR(255), 
    nome_fantasia		 VARCHAR(255), 
    cnpj				 VARCHAR(14), 
    ie					 VARCHAR(9), 
    ddd_tel				 CHAR(2), 
    telefone			 VARCHAR(11), 
    ddd_cel				 CHAR(2), 
    celular				 VARCHAR(11), 
    email				 VARCHAR(255), 
    logo_clinica		 VARBINARY(MAX), 
    site				 VARCHAR(255)
); 

CREATE TABLE categoria 
( 
    id_categoria		INT PRIMARY KEY				IDENTITY, 
    descricao			VARCHAR(255), 
    ativo				BIT						DEFAULT  1 
); 

CREATE TABLE diagnostico 
( 
    id_diagnostico		INT PRIMARY KEY						IDENTITY, 
    id_categoria		INT, 
    esferico			DECIMAL(4, 2), 
    cilindro			DECIMAL(4, 2), 
    adicao				DECIMAL(4, 2), 
    eixo				DECIMAL(4, 2), 
    ativo				BIT									 DEFAULT  1, 
    FOREIGN KEY(id_categoria) REFERENCES categoria (id_categoria) 
); 

CREATE TABLE pagamento 
( 
    id_pagamento					 INT PRIMARY KEY			IDENTITY, 
    id_caixa						 INT, 
    id_consulta						 INT, 
    id_forma_pagamento				 INT, 
    data							 DATE, 
    valor							 DECIMAL(9, 2), 
    qtd_parcela						 TINYINT, 
    ativo							 BIT						DEFAULT  1, 
    FOREIGN KEY(id_consulta)		 REFERENCES consulta (id_consulta), 
    FOREIGN KEY(id_forma_pagamento)	 REFERENCES forma_de_pagamento (id_forma_pagamento) 
); 

CREATE TABLE caixa 
( 
    id_caixa							INT PRIMARY KEY			IDENTITY, 
    funcionario_abertura				INT, 
    funcionario_fechamento				INT, 
    valor_inicial						DECIMAL(9, 2), 
    data_abertura						DATETIME, 
    data_fechamento						DATETIME, 
    ativo								BIT						DEFAULT  1, 
    FOREIGN KEY(funcionario_abertura)	REFERENCES funcionario (id_funcionario), 
    FOREIGN KEY(funcionario_fechamento) REFERENCES funcionario (id_funcionario) 
); 

ALTER TABLE consulta 
  ADD FOREIGN KEY(id_medico)	 REFERENCES funcionario (id_funcionario);

ALTER TABLE consulta 
  ADD FOREIGN KEY(id_cliente)	 REFERENCES cliente (id_cliente);

ALTER TABLE funcionario 
  ADD FOREIGN KEY(id_endereco)	 REFERENCES endereco (id_endereco);

ALTER TABLE funcionario 
  ADD FOREIGN KEY(id_cargo)		 REFERENCES cargo (id_cargo);

ALTER TABLE diferenca_caixa 
  ADD FOREIGN KEY(id_caixa)		 REFERENCES caixa (id_caixa);

ALTER TABLE receita 
  ADD FOREIGN KEY(olho_esquerdo) REFERENCES diagnostico (id_diagnostico);

ALTER TABLE receita 
  ADD FOREIGN KEY(olho_direito)  REFERENCES diagnostico (id_diagnostico); 

ALTER TABLE pagamento 
  ADD FOREIGN KEY(id_caixa)	     REFERENCES caixa (id_caixa);



INSERT INTO forma_de_pagamento 
	(descricao)
VALUES
	('DINHEIRO'),
	('CHEQUE'),
	('CARTÃO DE DÉBITO'),
	('CARTÃO DE CRÉDITO'),
	('BOLETO'),
	('DEPÓSITO');

INSERT INTO categoria 
	(descricao)
VALUES
	('PRESBIOPIA'),
	('MIOPIA'),
	('HIPERMETROPIA'),
	('ASTIGMATISMO');

INSERT INTO cargo 
	(descricao)
VALUES
	('OFTALMOLOGISTA'),
	('RECEPCIONISTA'),
	('GERENTE')