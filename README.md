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

### Funcionalidades do MVP

#### 6.1 Autenticação básica
- Login simples
- Identificação do usuário no sistema

#### 6.2 Busca de livros
- Busca por título ou autor
- Consumo de APIs externas
- Cache local dos livros no banco

#### 6.3 Página de detalhes do livro
- Capa
- Título
- Autor(es)
- Gêneros
- Descrição
- Avaliação média (quando disponível)
- Ação: adicionar à biblioteca

#### 6.4 Biblioteca do usuário
- Estados:
  - Want to Read
  - Reading
  - Read
- Um livro por estado, por usuário

#### 6.5 Recomendação simples (v1)
- Baseada em:
  - gêneros dos livros da biblioteca
  - avaliação média
- Regras determinísticas (sem ML avançado)

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
