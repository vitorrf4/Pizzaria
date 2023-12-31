# Pizzaria AC
Aplicação fullstack que simula o funcionamento de uma pizzaria, desenvolvido utilizando C# no backend e Angular no frontend. Projeto acadêmico realizado para a disciplina de Desenvolvimento de Software Visual.

## Funcionalidades
### Backend (C#)
**Gerenciamento das Entidades**: O sistema permite a criação, atualização e remoção de objetos das classes pertinentes ao sistema, como clientes, sabores, endereços e pedidos.

**Integração com Banco de Dados:** Com o auxílio do Entity Framework, as informações registradas são persistidas em um banco de dados integrado (SQLite), dispensando de configurações independente do ambiente em que o programa é utilizado. 

### Frontend (Angular)
**Área do Cliente**: O site possue páginas onde os usuários podem visualizar as informações relevantes a sua conta, como endereço, e seus pedidos realizados.

**Realização de Pedidos**: Os usuários podem visualizar o cardápio, selecionar itens e personalizar suas pizzas por tamanho, sabores e quantidades, de acordo com suas preferências. Além de pizzas, o cardápio também inclue acompanhamentos.

**Carrinho Dinâmico:** O sistema possue um carrinho que pode ser gerenciado pelo usuário, com a adição e remoção de itens, visualização da quantidade de itens diretamente no seu icone e cálculo automático do preço do pedido.

## Como Iniciar
**_Requisitos_**
- .NET Core para o backend.
- Node.js e Angular CLI para o frontend.

**_Configuração_:**
- **Backend**: Na pasta raíz do projeto execute os seguintes comandos no seu terminal para mudar de pasta e iniciar o programa:

  `cd backend`

  `dotnet run`

- **Frontend**: Na pasta raíz do projeto execute os seguintes comandos no seu terminal para mudar de pasta e iniciar o programa:

  `cd frontend`

  `ng serve`

**_Navegador_**
- Abra seu navegador e acesse http://localhost:4200 para interagir com o sistema.

