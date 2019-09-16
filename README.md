# VOTADOR 0.1

##Criando o banco

-Cria um banco em sua instância do PG chamado votador usando a ferramenta de sua preferência
-Rode o script database.sql disponível na raiz do projeto
-Configure o servidor(endereço, porta e usuário) em backend/appsettings.json

##Rodando o backend

- Abrir a solução no visual studio e iniciar o projeto padrão (votador)
- esse projeto é a API Rest que será consumida pelo backend

###Endpoints da API

[GET]
- /auditoria
lista os votos de todos os usuários, ordenando por qtd de votos
- /auth

[POST]
retorna um token, dada credenciais válidas para o login

[GET]
- /perfil
retorna uma lista com o ENUM de perfis, que definem as permissões que cada usuário possui

[GET|POST|PUT|DELETE]
- /recurso
usuários com o perfil *CtrlProducao* podem realizar operações CRUD dos recursos disponíveis para votação

[GET]
- /resultado
retorna uma lista os recursos e quantidade de votos

[GET|POST|PUT|DELETE]
- /usuario
usuários com o perfil *DeptPessoal* podem realizar operações CRUD dos usuários que terão acesso ao portal

[GET|POST]
- /voto
todos os usuários podem listar os recursos disponíveis para votação (ainda não votados)
todos os usuários podem votar em um recurso


##Frontend

Usando o bash de sua preferência, navegue até a pasta recipente do frontend (./front a partir da raiz do projeto)

Caso não possua o NodeJS e o NPM instalados em sua máquina, vc pode efetuar o download aqui:

-https://nodejs.org/en/download/

Rode os seguintes comandos para iniciar a aplicação:

- npm i (instala todas as dependências)
- npm run serve (inicia a aplicação em http://localhost:8080)

O script já trás um usuário padrão para acesso ao sistema:

	user: dp@alterdata.com.br
  senha: alterdata

A partir de agora vc consegue cadastrar os demais funcionários, recursos de acordo com o perfil de cada usuário.


em caso de dúvidas, entre em contato:

marcosviniciosneves@gmail.com
