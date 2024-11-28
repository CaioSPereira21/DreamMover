# DreamMover

## 📖 Descrição do Projeto  
O **DreamMover** é uma aplicação desenvolvida em **C#** com o padrão **MVC**, utilizando **Vue.js** no front-end e **LINQ to Entities** para acesso e manipulação de dados. O objetivo principal do sistema é gerenciar os lutadores do jogo digital **M.U.G.E.N**, permitindo cadastrar lutadores, realizar lutas diretamente pela plataforma e coletar os resultados para ajustar os níveis dos personagens.

O sistema também oferece funcionalidades para consultar as últimas lutas realizadas, visualizar os rivais de cada lutador e monitorar o nível atual de cada personagem no jogo.

---

## 🚀 Funcionalidades  
- **Cadastro de Lutadores**  
  Registre novos lutadores para o jogo, fornecendo detalhes como nome, nível inicial e outras características.

- **Gerenciamento de Lutas**  
  Realize lutas diretamente pela plataforma integrada ao **M.U.G.E.N** e registre os resultados automaticamente.

- **Histórico de Lutas**  
  Consulte as últimas lutas realizadas, incluindo informações sobre os participantes, resultados e níveis atualizados.

- **Rivais de Lutadores**  
  Visualize os rivais mais frequentes de cada lutador com base no histórico de combates.

- **Sistema de Nível**  
  Acompanhe o progresso de cada lutador, com ajustes automáticos de nível baseados em seu desempenho.

---

## 🛠️ Tecnologias Utilizadas  
### Backend  
- **C#**  
- **ASP.NET MVC**  
- **LINQ to Entities**  
- **Entity Framework**  

### Frontend  
- **Vue.js**  
- **Bootstrap** (para estilização)  

### Banco de Dados  
- **SQL Server**  

---

## 🎮 Fluxo de Funcionamento  
1. **Cadastro de Lutadores**  
   O administrador registra os lutadores.  

2. **Realização de Lutas**  
   Através da plataforma, é possível iniciar lutas diretamente no **M.U.G.E.N**, com integração para capturar os resultados.  

3. **Processamento de Resultados**  
   Os dados das lutas são analisados e utilizados para atualizar automaticamente o nível dos lutadores com base em seu desempenho.  

4. **Consultas**  
   - Últimas Lutas: Exibe o histórico recente de batalhas realizadas.  
   - Rivais: Lista os oponentes mais enfrentados por cada lutador.  
   - Nível Atual: Mostra o progresso e a evolução de cada lutador.

---

## 🧑‍💻 Como Executar o Projeto  

### Pré-requisitos  
- **.NET SDK 6.0+**  
- **Node.js**  
- **SQL Server**  

### Passos  
1. Clone o repositório:  
   ```bash
   git clone https://github.com/seu-usuario/DreamMover.git
   cd DreamMover
   ```
2. Configure o banco de dados no arquivo `Web.config`.  

3. Restaure os pacotes do projeto backend:  
   ```bash
   dotnet restore
   ```  

4. Instale as dependências do frontend:  
   ```bash
   cd ClientApp
   npm install
   ```

5. Execute a aplicação:  
   ```bash
   dotnet run
   ```

6. Acesse o sistema em `http://localhost:5000`.

---

## 📈 Melhorias Futuras  
- Adicionar gráficos para visualização de estatísticas de desempenho dos lutadores.  
- Criar um sistema de torneios automatizado.  

---

## 🤝 Contribuições  
Contribuições são bem-vindas!  
- Faça um fork do repositório.  
- Crie uma branch para sua funcionalidade: `git checkout -b minha-nova-feature`.  
- Envie seu Pull Request.

---

## 📄 Licença  
Este projeto está licenciado sob a **MIT License**.  
Consulte o arquivo [LICENSE](./LICENSE) para mais detalhes.

--- 

Desenvolvido com 💻 por [Caio da Silva Pereira](https://github.com/CaioSPereira21).  
