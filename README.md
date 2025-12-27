# 📚 NexRead — Documentação do Projeto

## 1. Visão Geral
**NexRead** é uma plataforma de recomendação de livros focada em **descoberta inteligente de leituras**, utilizando preferências do usuário e similaridade de conteúdo para sugerir novas leituras de forma simples e personalizada.

O projeto foi concebido como um **side project** para explorar Angular (frontend), .NET (backend) e conceitos básicos de recomendação, com uma arquitetura clara e evolutiva.

---

## 2. Ideia do Produto
A maioria das plataformas de livros oferece recomendações genéricas e pouco explicáveis.  
O NexRead busca resolver isso com:

- recomendações personalizadas desde o primeiro uso
- UX simples e moderna
- base sólida para evolução futura (IA / ML)
- foco na jornada individual do leitor

---

## 3. Público-Alvo
- Leitores ocasionais ou frequentes  
- Pessoas que gostam de organizar leituras  
- Desenvolvedores/estudantes (no contexto do projeto)  

---

## 4. Stack Tecnológica

### Frontend
- Angular 20+
- Tailwind CSS
- Design System próprio (componentes básicos)

### Backend
- .NET (API REST)
- Entity Framework Core (Code First)
- Arquitetura em camadas

### Banco de Dados
- PostgreSQL (dados principais)
- Cache: IMemoryCache (MVP), com possibilidade de Redis no futuro

### APIs Externas
- Google Books API
- Open Library API

---

## 5. Visão do Produto Final (Futuro)
Funcionalidades planejadas fora do MVP:

- onboarding inteligente (gêneros, autores, humor)
- recomendações híbridas (conteúdo + comportamento)
- histórico completo de leitura
- avaliações e comentários
- explicação das recomendações
- uso de IA/NLP no recomendador
- dashboards de leitura

> ⚠️ Essas funcionalidades **não fazem parte do MVP**.

---

## 6. Escopo do MVP (POC)

O MVP valida a ideia central:
**buscar livros, salvar leituras e receber recomendações simples.**

### Status de Implementação do MVP

#### ✅ 6.1 Autenticação (Completo)
- ✅ Registro de usuários
- ✅ Login/Logout
- ✅ Refresh tokens
- ✅ Autenticação baseada em cookies
- ✅ Perfil do usuário

#### ✅ 6.2 Gerenciamento de Livros (Completo)
- ✅ CRUD completo de Books
- ✅ CRUD completo de Authors
- ✅ CRUD completo de Genres
- ✅ Relacionamentos N:N (BookAuthors, BookGenres)
- ✅ Validações com FluentValidation
- 🔄 Busca em APIs externas (estrutura criada, implementação pendente)
  - Interface IExternalBookApiClient definida
  - GoogleBooksClient preparado com TODOs
  - Ver: `NexRead.Infra/ExternalApis/README_GOOGLE_BOOKS.md`

#### ✅ 6.3 Detalhes do Livro (Completo via API)
Endpoint: `GET /api/books/{id}`
- ✅ Título, descrição, ISBN
- ✅ Autores (lista completa)
- ✅ Gêneros (lista completa)
- ✅ Imagem de capa (URL)
- ✅ Data de publicação, páginas, idioma
- ✅ Avaliação média

#### ✅ 6.4 Biblioteca do Usuário (Completo)
Endpoints: `POST/PUT/DELETE/GET /api/userlibrary`
- ✅ Estados: WantToRead (1), Reading (2), Read (3)
- ✅ Um livro por usuário (único)
- ✅ Alteração de status
- ✅ Listagem por status
- ✅ Constraint único (UserId + BookId)

#### ✅ 6.5 Recomendação Inteligente (Completo)
Endpoint: `GET /api/recommendations`
- ✅ Análise de gêneros dos livros lidos/lendo
- ✅ Busca de livros similares por gênero
- ✅ Ordenação por avaliação e relevância de gênero
- ✅ Fallback para top-rated quando sem biblioteca
- ✅ Exclusão de livros já na biblioteca

---

## 7. Fora do Escopo do MVP
- Avaliações escritas pelo usuário
- Comentários
- Histórico detalhado de leitura
- Recomendação por humor
- Algoritmos com embeddings / NLP
- Funcionalidades sociais
- Dark/Light mode
- Notificações

---

## 8. Modelagem de Dados (Resumo)

### Entidades do MVP
- `Users`
- `Books`
- `Authors`
- `Genres`
- `BookAuthors` (N:N)
- `BookGenres` (N:N)
- `UserLibrary` (status: Want / Reading / Read)

### Entidades Futuras
- `UserPreferences`
- `ReadingHistory`
- `UserReviews`
- `RecommendationLogs`

---

## 9. Estrutura do Backend (Proposta)

```text
/src
 ├── NexRead.Api
 ├── NexRead.Domain
 │    ├── Entities
 │    └── Enums
 ├── NexRead.Application
 │    ├── Services
 │    └── Interfaces
 └── NexRead.Infrastructure
      ├── Persistence
      └── ExternalApis
