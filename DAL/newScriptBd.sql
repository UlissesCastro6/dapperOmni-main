/*****************************DENUNCIA*************************************/
CREATE TABLE CIDADAO(
IdCidadao INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_CIDADAO PRIMARY KEY,
Email VARCHAR(35) NOT NULL,
Celular CHAR(11) NOT NULL,
Nome VARCHAR(30) NOT NULL,
Sobrenome VARCHAR(30) NOT NULL,
Cep CHAR(9) NOT NULL,
CPF CHAR(11) NOT NULL,
TituloEleitor CHAR(12) NOT NULL,
ZonaEleitor CHAR(3) NOT NULL,
Secao CHAR(4) NOT NULL,
ptCidadao INT NOT NULL
);
GO
CREATE TABLE FOTO_CIDADAO(
IdCidadao INT NOT NULL,
VbFotoCidadao IMAGE NOT NULL
);
ALTER TABLE FOTO_CIDADAO ADD CONSTRAINT FOTO_CIDADAO_PK PRIMARY KEY (IdCidadao)
GO
ALTER TABLE FOTO_CIDADAO ADD CONSTRAINT FOTO_CIDADAO_FK FOREIGN KEY (IdCidadao) REFERENCES CIDADAO(IdCidadao)
GO
CREATE TABLE TIPOS_DENUNCIA (
IdTipoDenuncia INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_TIPOS_DENUNCIA PRIMARY KEY,
dsTipoDenuncia VARCHAR(35) NOT NULL,
ptTipoDenuncia INT NOT NULL
);

GO
CREATE TABLE STATUS_DENUNCIA(
IdStatus INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_STATUS_DENUNCIA PRIMARY KEY,
DescricaoStatus VARCHAR(25) NOT NULL
);
GO
CREATE TABLE DENUNCIA(
IdDenuncia INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_DENUNCIA PRIMARY KEY,
IdCidadao INT NOT NULL,
IdTipoDenuncia INT NOT NULL,
IdStatus INT NOT NULL,
DataDenuncia DATE NOT NULL,
DescricaoDenuncia VARCHAR(8000)NOT NULL ,
localDenuncia VARCHAR(2000) NOT NULL,
pontosPrioridade INT NOT NULL
);
GO
ALTER TABLE DENUNCIA ADD CONSTRAINT DENUNCIA_FK01 FOREIGN KEY (IdTipoDenuncia) REFERENCES TIPOS_DENUNCIA(IdTipoDenuncia);
ALTER TABLE DENUNCIA ADD CONSTRAINT DENUNCIA_FK02 FOREIGN KEY (IdCidadao)      REFERENCES CIDADAO(IdCidadao);
ALTER TABLE DENUNCIA ADD CONSTRAINT DENUNCIA_FK03 FOREIGN KEY (IdStatus)       REFERENCES STATUS_DENUNCIA(IdStatus);
GO
CREATE TABLE IMAGEM_DENUNCIA(
IdDenuncia INT NOT NULL,
IdImagemDenuncia INT NOT NULL,
VbImagemDenuncia IMAGE NOT NULL
);
ALTER TABLE IMAGEM_DENUNCIA ADD CONSTRAINT IMAGEM_DENUNCIA_PK PRIMARY KEY (IdDenuncia, IdImagemDenuncia)
GO
ALTER TABLE IMAGEM_DENUNCIA ADD CONSTRAINT  IMAGEM_DENUNCIA_FK01 FOREIGN KEY (IdDenuncia) REFERENCES DENUNCIA(IdDenuncia)
GO
CREATE TABLE APONTAMENTOS(
IdDenuncia INT NOT NULL,
IdApontamento INT NOT NULL,
mtApontamentos SMALLDATETIME NOT NULL,
dsApontamentos VARCHAR(255) NOT NULL
);
ALTER TABLE APONTAMENTOS ADD CONSTRAINT APONTAMENTOS_PK PRIMARY KEY (IdDenuncia, IdApontamento)
GO
ALTER TABLE APONTAMENTOS ADD CONSTRAINT APONTAMENTOS_DF01 DEFAULT CURRENT_TIMESTAMP FOR mtApontamentos;
GO
ALTER TABLE APONTAMENTOS ADD CONSTRAINT APONTAMENTOS_FK01 FOREIGN KEY (IdDenuncia) REFERENCES  DENUNCIA(IdDenuncia)
GO
/***************************************PUBLICAÇÕES****************************************************/
CREATE TABLE TIPOS_PUBLICACOES (
IdTipoPublicacao INT IDENTITY(1,1) NOT NULL CONSTRAINT TIPOS_PUBLICACOES_PK PRIMARY KEY,
dsTipoDenuncia VARCHAR(25) NOT NULL
);
GO
CREATE TABLE PUBLICACOES (
IdPublicacao INT IDENTITY(1,1) NOT NULL CONSTRAINT PUBLICACOES_PK PRIMARY KEY,
IdTipoPublicacao INT NOT NULL,
ttPublicacao VARCHAR(50) NOT NULL,
txPublicacao VARCHAR(8000) NOT NULL,
inPublicacao VARCHAR(250) NOT NULL,
dtInicioExibicao DATETIME NOT NULL,
dtFimExibicao DATETIME NOT NULL,
snPublicacao INTEGER NOT NULL,
prPublicacao INTEGER NOT NULL
);
GO
ALTER TABLE PUBLICACOES ADD CONSTRAINT PUBLICACOES_FK01 FOREIGN KEY (IdTipoPublicacao) REFERENCES TIPOS_PUBLICACOES(IdTipoPublicacao);
GO
CREATE TABLE PUBLICACOES_IMAGENS (
IdPublicacao INT NOT NULL,
IdPublicacaoImagem INT NOT NULL,
vbPublicacaoImagem IMAGE NOT NULL
);
ALTER TABLE PUBLICACOES_IMAGENS ADD CONSTRAINT PUBLICACOES_IMAGENS_PK PRIMARY KEY (IdPublicacaoImagem)
GO
ALTER TABLE PUBLICACOES_IMAGENS ADD CONSTRAINT PUBLICACOES_IMAGENS_FK01 FOREIGN KEY (IdPublicacao) REFERENCES  PUBLICACOES(IdPublicacao)
GO


/*INSERTS
INSERT INTO CIDADAO (Email, Celular, Nome, Sobrenome, Cep, CPF, TituloEleitor, ZonaEleitor, Secao, ptCidadao)
VALUES('caio@gmail.com', '11954258562', 'Caio','Marinho', '02056020', '54412331202', '123456123456', '451', '541', 0)

INSERT INTO TIPOS_DENUNCIA (dsTipoDenuncia, ptTipoDenuncia) VALUES('Arvore em situacao de risco', 0)
INSERT INTO TIPOS_DENUNCIA (dsTipoDenuncia, ptTipoDenuncia) VALUES('Bueiro cheio', 0)

INSERT INTO STATUS_DENUNCIA (DescricaoStatus) VALUES('EM ABERTO')
INSERT INTO STATUS_DENUNCIA (DescricaoStatus) VALUES('FECHADO')

INSERT INTO DENUNCIA(IdCidadao, IdTipoDenuncia, IdStatus, DataDenuncia, DescricaoDenuncia, localDenuncia, pontosPrioridade)
VALUES(1, 1, 1, '2022-11-22', 'Testando insercao de denuncia pelo banco de dados', 'Sao Paulo', 0)

INSERT INTO TIPOS_PUBLICACOES(dsTipoDenuncia) VALUES('Anuncio')
INSERT INTO TIPOS_PUBLICACOES(dsTipoDenuncia) VALUES('Noticia')

INSERT INTO PUBLICACOES (IdTipoPublicacao, ttPublicacao, txPublicacao, inPublicacao, dtInicioExibicao, dtFimExibicao, snPublicacao, prPublicacao)
VALUES(2, 'Titulo da publicacao', 'Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. 
Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium 
quis, sem. Nulla consequat massa quis enim.', 'Teste teste teste teste teste', '2022-11-28', '2022-12-05', 0, 0)

*/