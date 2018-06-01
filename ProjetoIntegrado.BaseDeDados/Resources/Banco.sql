CREATE TABLE consulta 
( 
	id_consulta				INT PRIMARY KEY			IDENTITY, 
	id_medico				INT						NOT NULL, 
	id_cliente				INT						NOT NULL, 
	id_convenio				INT						NULL, 
	numero_procedimento		VARCHAR(255)			NULL,
	forma_de_atendimento	CHAR					NOT NULL,
	data					DATE					NOT NULL, 
	horario					TIME					NOT NULL, 
	valor					DECIMAL(9, 2)			NOT NULL, 
	status_pagamento 		CHAR					NOT NULL, 
	tipo_de_consulta		CHAR					NULL,
	observacao				VARCHAR(500)			NULL,
	tipo_de_cancelamento	BIT						NULL,
	retorno					BIT						NULL,
	ativo					BIT						NOT NULL DEFAULT  1
); 

CREATE TABLE funcionario 
( 
    id_funcionario     INT PRIMARY KEY			IDENTITY, 
    id_endereco        INT						NULL, 
    id_cargo           INT						NOT NULL, 
    nome               VARCHAR(255)				NOT NULL, 
    genero             CHAR						NOT NULL, 
    cpf                CHAR(11)					NULL, 
    data_de_nascimento DATE						NULL,	 
    ddd_cel            CHAR(2)					NULL, 
    celular            VARCHAR(11)				NULL, 
    ddd_tel            CHAR(2)					NULL, 
    telefone           VARCHAR(11)				NULL, 
    email              VARCHAR(255)				NULL, 
    salario            DECIMAL(9, 2)			NULL, 
    data_de_admissao   DATE						NULL, 
    usuario            VARCHAR(255)				NULL, 
    senha              VARCHAR(128)				NULL, 
    ativo              BIT						NOT NULL DEFAULT  1 
); 

CREATE TABLE endereco 
( 
    id_endereco		   INT PRIMARY KEY			IDENTITY, 
    cep				   CHAR(8)					NULL, 
    cidade			   VARCHAR(255)				NULL, 
    uf				   CHAR(2)					NUll, 
	bairro			   VARCHAR(255)				NULL, 
    logradouro		   VARCHAR(255)				NULL, 
    numero			   VARCHAR(5)				NULL, 
    complemento		   VARCHAR(255)				NULL, 
	ativo			   BIT						NOT NULL DEFAULT  1 
); 

CREATE TABLE cargo 
( 
    id_cargo			INT PRIMARY KEY			IDENTITY, 
    descricao			VARCHAR(255)			NOT NULL,
    ativo				BIT						NOT NULL DEFAULT  1 
);

CREATE TABLE forma_de_pagamento 
( 
    id_forma_pagamento INT PRIMARY KEY			IDENTITY, 
    descricao          VARCHAR(255)				NOT NULL, 
    ativo              BIT						NOT NULL DEFAULT  1 
); 


CREATE TABLE receita 
( 
    id_receita				INT PRIMARY KEY			IDENTITY, 
    id_consulta				INT						NOT NULL, 
    olho_esquerdo_longe		INT						NOT NULL, 
    olho_direito_longe		INT						NOT NULL, 
    olho_esquerdo_perto		INT						NOT NULL, 
    olho_direito_perto		INT						NOT NULL, 
    ativo					BIT						NOT NULL DEFAULT  1, 
    FOREIGN KEY(id_consulta) REFERENCES consulta (id_consulta) 
); 

CREATE TABLE cliente 
( 
    id_cliente			INT PRIMARY KEY			IDENTITY, 
    id_endereco			INT						NOT NULL, 
    nome				VARCHAR(255)			NOT NULL, 
    genero				CHAR					NOT NULL, 
    cpf					CHAR(11)				NULL, 
    data_de_nascimento	DATE					NULL, 
    ddd_cel				CHAR(2)					NULL, 
    celular				VARCHAR(11)				NULL, 
    ddd_tel				CHAR(2)					NULL, 
    telefone			VARCHAR(11)				NULL, 
    email				VARCHAR(255)			NULL, 
	ativo				BIT						NOT NULL DEFAULT  1
    FOREIGN KEY(id_endereco) REFERENCES endereco (id_endereco) 
); 

CREATE TABLE clinica 
( 
    id_clinica			 INT PRIMARY KEY		IDENTITY, 
    id_endereco		 	 INT					NULL,
    razao_social		 VARCHAR(255)			NULL, 
    nome_fantasia		 VARCHAR(255)			NULL, 
    cnpj				 VARCHAR(14)			NULL, 
    ie					 VARCHAR(32)			NULL, 
    ddd_tel				 CHAR(2)				NULL, 
    telefone			 VARCHAR(11)			NULL, 
    ddd_cel				 CHAR(2)				NULL, 
    celular				 VARCHAR(11)			NULL, 
    email				 VARCHAR(255)			NULL, 
    logo_clinica		 VARBINARY(MAX)			NULL, 
    site				 VARCHAR(255)			NULL,
    FOREIGN KEY(id_endereco) REFERENCES endereco (id_endereco)
); 


CREATE TABLE diagnostico 
( 
    id_diagnostico		INT PRIMARY KEY			IDENTITY, 
    esferico			DECIMAL(9, 2)			NULL, 
    cilindro			DECIMAL(9, 2)			NULL, 
    adicao				DECIMAL(9, 2)			NULL, 
    eixo				DECIMAL(9, 2)			NULL, 
    ativo				BIT					    NOT  NULL DEFAULT  1
); 

CREATE TABLE pagamento 
( 
    id_pagamento					 INT PRIMARY KEY			IDENTITY, 
    id_caixa						 INT						NOT NULL, 
    id_consulta						 INT						NOT NULL, 
    id_forma_pagamento				 INT						NOT NULL, 
    data							 DATE						NOT NULL, 
    valor							 DECIMAL(9, 2)				NOT NULL, 
    qtd_parcela						 TINYINT					NOT NULL, 
    ativo							 BIT						NOT NULL DEFAULT  1, 
    FOREIGN KEY(id_consulta)		 REFERENCES consulta (id_consulta), 
    FOREIGN KEY(id_forma_pagamento)	 REFERENCES forma_de_pagamento (id_forma_pagamento) 
); 

CREATE TABLE caixa 
( 
    id_caixa							INT PRIMARY KEY			IDENTITY, 
    funcionario_abertura				INT						NOT NULL, 
    funcionario_fechamento				INT						NULL, 
    valor_inicial						DECIMAL(9, 2)			NOT NULL, 
    data_abertura						DATETIME				NOT NULL, 
    data_fechamento						DATETIME				NULL, 
	diferenca							DECIMAL(9, 2)			NULL,
	caixa_aberto						CHAR					NOT NULL DEFAULT 'V',
    ativo								BIT						NOT NULL DEFAULT  1, 
    FOREIGN KEY(funcionario_abertura)	REFERENCES funcionario (id_funcionario), 
    FOREIGN KEY(funcionario_fechamento) REFERENCES funcionario (id_funcionario) 
);

CREATE TABLE caixa_saida 
(	
	id_caixa_saida						INT	PRIMARY KEY			IDENTITY,
	id_caixa							INT						NOT NULL,
	descricao							VARCHAR(255)			NOT NULL,
	valor								DECIMAL(9,2)			NOT NULL,
	data								DATETIME				DEFAULT GETDATE(),
	ativo								BIT						NOT NULL DEFAULT 1
);

CREATE TABLE despesa
(
	id_despesa							INT PRIMARY	KEY			IDENTITY,
	descricao							VARCHAR(255)			NOT NULL,
	valor								DECIMAL(9, 2)			NOT NULL,
	data								DATE					NOT NULL,
	ativo								BIT						NOT NULL DEFAULT 1
);

CREATE TABLE convenio
(
	id_convenio							INT PRIMARY KEY			IDENTITY,
	nome								VARCHAR(255),
	ativo								BIT						NOT NULL DEFAULT 1
);

CREATE TABLE material
(
	id_material				INT				PRIMARY	KEY		IDENTITY,
	descricao				VARCHAR(255)	NOT NULL,
	valor					DECIMAL(9,2)	NOT NULL,
	ativo					BIT				NOT NULL		DEFAULT 1
);

CREATE TABLE cirurgia
(
	id_cirurgia				INT				PRIMARY	KEY		IDENTITY,
	id_consulta				INT,
	local					VARCHAR(255),
	valor_medico			DECIMAL(9,2),
	ativo					BIT				NOT NULL		DEFAULT 1
);

CREATE TABLE material_cirurgia
(
	id_material_cirurgia	INT				PRIMARY	KEY		IDENTITY,
	id_cirurgia				INT,
	id_material				INT,
	quantidade				TINYINT,
	valor_unitario			DECIMAL(9,2),
	ativo					BIT				NOT NULL		DEFAULT 1
);

CREATE TABLE equipe_cirurgia
(	
	id_equipe_cirurgia		INT				PRIMARY	KEY		IDENTITY,
	id_cirurgia				INT,
	id_funcionario			INT,
	funcao					VARCHAR(255),
	ativo					BIT				NOT NULL		DEFAULT 1
);

ALTER TABLE consulta 
  ADD FOREIGN KEY(id_medico)	 REFERENCES funcionario (id_funcionario);

ALTER TABLE consulta 
  ADD FOREIGN KEY(id_cliente)	 REFERENCES cliente (id_cliente);

ALTER TABLE funcionario 
  ADD FOREIGN KEY(id_endereco)	 REFERENCES endereco (id_endereco);

ALTER TABLE clinica 
  ADD FOREIGN KEY(id_endereco)	 REFERENCES endereco (id_endereco);

ALTER TABLE funcionario 
  ADD FOREIGN KEY(id_cargo)		 REFERENCES cargo (id_cargo);

ALTER TABLE receita 
  ADD FOREIGN KEY(olho_esquerdo_longe) REFERENCES diagnostico (id_diagnostico);

ALTER TABLE receita 
  ADD FOREIGN KEY(olho_direito_longe)  REFERENCES diagnostico (id_diagnostico); 

ALTER TABLE receita 
  ADD FOREIGN KEY(olho_esquerdo_perto) REFERENCES diagnostico (id_diagnostico);

ALTER TABLE receita 
  ADD FOREIGN KEY(olho_direito_perto)  REFERENCES diagnostico (id_diagnostico)

INSERT INTO forma_de_pagamento 
	(descricao)
VALUES
	('DINHEIRO'),
	('CHEQUE'),
	('CARTÃO DE DÉBITO'),
	('CARTÃO DE CRÉDITO'),
	('BOLETO'),
	('DEPÓSITO');

INSERT INTO cargo 
	(descricao)
VALUES
	('OFTALMOLOGISTA'),
	('RECEPCIONISTA'),
	('GERENTE');

INSERT INTO funcionario
	(usuario, nome, senha, genero, id_cargo)
VALUES
	('ADMINISTRADOR', 'ADMINISTRADOR', 'A/gVrESDv+E=', 1, 3);

INSERT INTO convenio
	(nome)
VALUES
	('ALLIANZ'),
	('AMAFRESP'),
	('AMIL'),
	('AMIL ONE'),
	('AMIL PLANOS'),
	('BRADESCO SAÚDE S.A.'),
	('CABESP'),
	('CARE PLUS'),
	('CASSI'),
	('CENTRAL NACIONAL UNIMED (CNU)'),
	('EMBRATEL'),
	('EMGEPRON'),
	('FSFX - USIMINAS'),
	('FUNDAÇÃO CESP '),
	('GAMA'),
	('GOLDEN CROSS'),
	('LIFE EMPRESARIAL'),
	('LINCX'),
	('MAPFRE SAÚDE'),
	('MEDIAL'),
	('MEDISERVICE'),
	('NOTRE DAME'),
	('PETROBRÁS'),
	('PORTO SEGURO'),
	('SAÚDE CAIXA'),
	('SEGUROS UNIMED'),
	('SUL AMÉRICA'),
	('UNIMED')