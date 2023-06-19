# MinsaitChallenge

A Aplicação foi construída utilizando DDD e possui as seguintes camadas:
 . API
 . Application
 . Domain
 . Security
 . Infra
 .Tests

 Para rodar a aplicação localmente será necessário ter SDK .NET 6 e o SQL SERVER instalado na máquina. além de alterar a ConnectionString dentro do arquivo appsettings.Development.json.(OBs) no SQL SERVER deve conter um Database com nome "Merchant"
 Pra rodar via container docker temos o Dockerfile da aplicação e o docker-compose com a api e o SQL SERVER, tbm tem um script .cmd que sobe um SQLSERVER com um Database já criado.

 A Aplicação contém autenticação Jwt, o usuário pré cadastrado para fazer o login e gerar e pegar o token gerado é:  
   email : admin@admin.com;
   senha : admin;

   Temos dois CRUDS implementados: Merchant e MerchantRelease,  o primeiro é relacionado a comerciantes e o segundo aos lançamentos do comerciante.
   Além do endpoint de login, tbm existe endpoint pra criar senha e alterar senha de um usúario.

   O endpoint api/v1/merchant/{merchantId}/release/consolidate traz todos os lançamentos a partir de um período com inicio e fim e com o saldo total dentro desse período.
