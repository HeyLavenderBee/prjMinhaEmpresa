Create DataBase bd_MinhaEmpresa_2DS_MTEC_TA;
use bd_MinhaEmpresa_2DS_MTEC_TA;

CREATE TABLE tb_login (
     log_usuario varchar(15) primary key,
     log_senha varchar(8) not null,
     log_nome varchar(30) not null,
     log_ult_Atualizacao timestamp not null
);

Insert into tb_login(
log_usuario,log_senha,log_nome,log_ult_Atualizacao)
values
('isa','isa1234','Isabele Bolaños',CURRENT_TIMESTAMP );
select * from tb_login;
 Insert into tb_login (log_usuario,log_senha,log_nome,log_ult_Atualizacao)
        values ('lramos','lr509094','Lucio Ramos',CURRENT_TIMESTAMP );
insert into tb_produtos set Prod_Codigo = 1;


CREATE TABLE tb_produtos (
     Prod_Codigo VarChar(5) primary key,
     Prod_Descricao varchar(30) not null,
     Prod_Qtd_Estoq decimal(5,2) not null,
     Prod_Estoq_Min decimal(5,2) not null,
     Prod_Unidade char(2) not null,     
     Prod_Locacao varchar(3) not null,
     Prod_PrcCusto decimal(6,2) not null,
     Prod_Marg_Lucro decimal(4,2) not null,
     Prod_ult_Atualizacao timestamp DEFAULT CURRENT_TIMESTAMP
								   ON UPDATE CURRENT_TIMESTAMP
     ); 
  
     CREATE TABLE tb_clientes (
     Cli_RG varchar(15) primary key,
     Cli_Nome varchar(30) not null,
     Cli_Endereco varchar(40) not null,
     Cli_Cidade Varchar(20) not null,     
     Cli_Uf char(2) not null,
     Cli_fone varchar(10) not null,
     Cli_ult_Atualizacao timestamp DEFAULT CURRENT_TIMESTAMP
								   ON UPDATE CURRENT_TIMESTAMP
     ); 