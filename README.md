# ComicsAPI
Web API .Net core para consulta de dados de personagens de histórias em quadrinho

Instruções

1) Utilizando o Microsoft SQL Server Management Studio, conecte ao seu Database Server local. Execute então os scripts contidos no arquivo ComicsDatabaseScript.sql -> Essa ação criará o banco de dados usados nessa API (pode ser necessário ajustar o diretório em que o banco de dados será criado);

2) Após baixar os arquivos desse repositório para uma pasta local em seu computado, usando o Visual Studio 2019, faça o build da solution ComicsAPI.sln;

3) Após a conclusão do build, alterar o arquivo ComicsAPI/appsettings.json -> DefaultConnection - configurar uma string de conexão válida, informando corretamente nome do servidor, Database (comics), user e password.

Ao executar, na página exibida, pode-se testar os endpoints da API utilizando Swagger.
    
