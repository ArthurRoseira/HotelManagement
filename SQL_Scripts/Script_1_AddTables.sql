CREATE TABLE Quarto (
    QuartoId INT NOT NULL,
    TipoQuartoId INT NOT NULL,
    SituacaoQuartoId INT NOT NULL,
	PRIMARY KEY(QuartoId)
);

CREATE TABLE TipoQuarto (
    TipoQuartoId INT NOT NULL,
    Descricao VARCHAR (50) NOT NULL,
    Valor FLOAT NOT NULL,
	PRIMARY KEY(TipoQuartoId)
);

CREATE TABLE SituacaoQuarto (
    SituacaoQuartoId INT NOT NULL,
    Descricao VARCHAR (50) NOT NULL,
	PRIMARY KEY(SituacaoQuartoId)
);

CREATE TABLE Cliente (
    Cpf VARCHAR(11) NOT NULL,
    NomeCompleto VARCHAR (50) NOT NULL,
	Telefone VARCHAR(11) NULL,
	DataNascimento DATETIME NULL,
	Email VARCHAR(50) NULL,
	DataCriacao DATETIME NOT NULL,
	PRIMARY KEY(Cpf)
);


CREATE TABLE Reserva (
    ReservaId VARCHAR(11) NOT NULL,
	DataCriacao DATETIME NOT NULL,
	CheckIn DATETIME NOT NULL,
	CheckInStatus BIT NOT NULL,
	CheckOut DATETIME NOT NULL,
	CheckOutStatus BIT NOT NULL,
	Cpf VARCHAR(11) NOT NULL,
	HospedesJSON VARCHAR(1000) NULL,
	QuartoId INT NOT NULL,
	ValoresDiarias FLOAT NOT NULL,
	TaxasConsumo FLOAT NULL,
	ValorFinal FLOAT NULL
	PRIMARY KEY(ReservaId)
);

ALTER TABLE Quarto
ADD CONSTRAINT FK_TipoQuartoId FOREIGN KEY (TipoQuartoId) REFERENCES TipoQuarto(TipoQuartoId);

ALTER TABLE Quarto
ADD CONSTRAINT FK_SituacaoId FOREIGN KEY (SituacaoQuartoId) REFERENCES SituacaoQuarto(SituacaoQuartoId);

ALTER TABLE Reserva
ADD CONSTRAINT FK_CPF FOREIGN KEY (Cpf) REFERENCES Cliente(Cpf);

ALTER TABLE Reserva
ADD CONSTRAINT FK_QuartoId FOREIGN KEY (QuartoId) REFERENCES Quarto(QuartoId);
