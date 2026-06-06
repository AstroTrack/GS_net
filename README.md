#  AstroTrack — API de Rastreamento de Frotas via Satélite

> **FIAP Global Solution 2026/1** · 2º Ano · Análise e Desenvolvimento de Sistemas · Turmas de Fevereiro

---

##  Integrantes do Grupo

| RM | Nome Completo | Turma |
|----|---------------|-------|
| RM563806 | Arthur Correia Delila | 2TDSPI |
| RM563732 | Gabriel Henrique Souza Goncalves | 2TDSPI |
| RM564112 | Jose Ricardo Pereira Iannuzzi | 2TDSPI |
| RM563210 | Rafael de Freitas Moraes | 2TDSPI |
| RM564928 | Rafael Pascotte Mercadante | 2TDSPI |

---

##  Links

| Recurso | Link |
|---------|------|
|  Deploy (API pública) | [Acessar API](_LINK_DO_DEPLOY_AQUI_) |
|  Swagger (documentação interativa) | [Acessar Swagger](_LINK_DO_DEPLOY_AQUI_) |
|  Vídeo de Demonstração (até 10 min) | [Assistir no YouTube](_LINK_DO_VIDEO_DEMONSTRACAO_AQUI_) |
|  Vídeo Pitch (até 3 min) | [Assistir no YouTube](_LINK_DO_VIDEO_PITCH_AQUI_) |
|  Repositório GitHub | [Acessar repositório](_LINK_DO_GITHUB_AQUI_) |

---

##  Sobre o Projeto

O **AstroTrack** é um sistema de rastreamento de frotas voltado para regiões sem cobertura de internet terrestre — as chamadas **"Zonas de Sombra"**. Utilizando infraestrutura de satélite como meio de comunicação, o sistema permite que gestores monitorem a localização e o status de veículos em tempo real, independentemente da disponibilidade de sinal 4G/5G.

A solução se conecta diretamente ao tema da **Economia Espacial** proposto pela Global Solution 2026/1, aplicando tecnologia espacial para resolver um problema real do agronegócio e da logística brasileira, contribuindo com o **ODS 9 — Indústria, Inovação e Infraestrutura**.

### Fluxo da solução
Um dispositivo IoT instalado no veículo coleta dados de localização GPS e status da carga. Esses dados são transmitidos via satélite para esta API, que os persiste e disponibiliza para gestores através de um painel de controle e aplicativo mobile.

---

##  Arquitetura do Projeto

```
AstroTrack/
├── Controllers/              # Endpoints HTTP (Auth, Cliente, Motorista, Veiculo, Viagem, Checkpoint)
├── Models/                   # Entidades mapeadas para o banco Oracle
│   ├── Cliente.cs
│   ├── Motorista.cs
│   ├── Veiculo.cs
│   ├── Viagem.cs
│   ├── Checkpoint.cs
│   └── UsuarioSistema.cs
├── Enums/                    # StatusCadastro, StatusVeiculo, StatusViagem
├── DTOs/
│   ├── Requests/             # Records de entrada com validações
│   └── Responses/            # Records de saída
├── Data/
│   ├── AppDbContext.cs       # Contexto EF Core com Fluent API
│   └── AppDbContextFactory.cs# Factory para design-time (migrations)
├── Repositories/
│   ├── Interfaces/           # IRepositories.cs — todas as interfaces
│   └── Implementations/      # Repositories.cs — todas as implementações
├── Services/
│   ├── Interfaces/           # IServices.cs — todas as interfaces
│   └── Implementations/      # Services.cs — todas as implementações
├── Mappers/
│   └── AstroTrackMapper.cs   # Mapeamento de entidades para DTOs
├── Exceptions/
│   ├── AppExceptions.cs      # NotFoundException, BadRequestException, ConflictException, UnauthorizedException
│   └── GlobalExceptionMiddleware.cs # Tratamento global de erros
└── Infrastructure/
    ├── JwtService.cs         # Geração e validação de tokens JWT
    └── CurrentUserService.cs # Serviço para obter usuário autenticado
```

**Padrões aplicados:**
- Layered Architecture (Controllers → Services → Repositories → Data)
- Repository Pattern com interfaces
- DTOs com C# Records
- Primary Constructors (.NET 9)
- Fluent API no EF Core
- Global Exception Middleware
- JWT Authentication com BCrypt
- Enums convertidos para string via `HasConversion<string>()`

---

##  Tecnologias Utilizadas

| Tecnologia | Versão |
|-----------|--------|
| .NET / ASP.NET Core | 9.0 |
| Entity Framework Core | 9.0 |
| Oracle.EntityFrameworkCore | 9.23.60 |
| BCrypt.Net-Next | 4.0.3 |
| Swashbuckle.AspNetCore (Swagger) | 7.3.1 |
| Microsoft.AspNetCore.Authentication.JwtBearer | 9.0.0 |
| System.IdentityModel.Tokens.Jwt | 8.3.1 |
| Banco de Dados | Oracle (FIAP) |

---

##  Modelagem do Banco de Dados

### Diagrama de Relacionamentos

```
AT_USUARIOS_SISTEMA
  └── email (PK), usuario, senha, status, data_criacao

AT_CLIENTES
  └── id_cliente (PK), nome, cnpj (UNIQUE), email (UNIQUE), telefone, status

AT_MOTORISTAS
  └── id_motorista (PK), nome, cpf (UNIQUE), cnh (UNIQUE), telefone, status

AT_VEICULOS
  └── id_veiculo (PK), placa (UNIQUE), modelo, marca, ano, status

AT_VIAGENS
  └── id_viagem (PK)
  └── id_cliente (FK → AT_CLIENTES)
  └── id_motorista (FK → AT_MOTORISTAS)
  └── id_veiculo (FK → AT_VEICULOS)
  └── origem, destino, data_inicio, data_fim, status, quilometragem_total

AT_CHECKPOINTS
  └── id_checkpoint (PK)
  └── id_viagem (FK → AT_VIAGENS, CASCADE DELETE)
  └── latitude, longitude, data_registro, botao_panico, porta_aberta
```

### Relacionamentos implementados
- `AT_VIAGENS` → `AT_CLIENTES` (N:1)
- `AT_VIAGENS` → `AT_MOTORISTAS` (N:1)
- `AT_VIAGENS` → `AT_VEICULOS` (N:1)
- `AT_CHECKPOINTS` → `AT_VIAGENS` (N:1 com CASCADE DELETE)

---

##  Como Executar Localmente

### Pré-requisitos
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Acesso à rede FIAP ou VPN (para o banco Oracle)
- [dotnet-ef](https://learn.microsoft.com/ef/core/cli/dotnet) instalado globalmente

### 1. Clonar o repositório
```bash
git clone _LINK_DO_GITHUB_AQUI_
cd AstroTrack
```

### 2. Configurar a connection string
Edite o arquivo `appsettings.json` com seu RM e senha Oracle da FIAP:
```json
{
  "ConnectionStrings": {
    "OracleConnection": "User Id=rmXXXXXX;Password=ddmmyyyy;Data Source=oracle.fiap.com.br:1521/ORCL"
  }
}
```

### 3. Instalar a ferramenta EF (se necessário)
```bash
dotnet tool install --global dotnet-ef
```

### 4. Aplicar a Migration
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 5. Rodar a API
```bash
dotnet run
```

### 6. Acessar o Swagger
Abra no navegador:
```
http://localhost:5000
```

---

##  Autenticação JWT

Todos os endpoints exceto `/api/auth/register` e `/api/auth/login` são protegidos por JWT.

**Fluxo de autenticação:**

1. `POST /api/auth/register` → registra o usuário e retorna o token
2. `POST /api/auth/login` → autentica e retorna o token
3. No Swagger, clique em **Authorize** e insira: `Bearer {seu_token}`
4. Todos os endpoints passam a funcionar normalmente

---

##  Endpoints da API

###  Autenticação
| Método | Rota | Descrição | Auth | HTTP |
|--------|------|-----------|------|------|
| POST | `/api/auth/register` | Registrar novo usuário | ❌ | 201 / 409 |
| POST | `/api/auth/login` | Login e obter token JWT | ❌ | 200 / 401 |

###  Clientes
| Método | Rota | Descrição | Auth | HTTP |
|--------|------|-----------|------|------|
| GET | `/api/clientes` | Listar todos os clientes | ✅ | 200 |
| GET | `/api/clientes/{id}` | Buscar cliente por ID | ✅ | 200 / 404 |
| POST | `/api/clientes` | Criar novo cliente | ✅ | 201 / 400 / 409 |
| PUT | `/api/clientes/{id}` | Atualizar cliente | ✅ | 200 / 400 / 404 / 409 |
| DELETE | `/api/clientes/{id}` | Remover cliente | ✅ | 204 / 404 |

###  Motoristas
| Método | Rota | Descrição | Auth | HTTP |
|--------|------|-----------|------|------|
| GET | `/api/motoristas` | Listar todos os motoristas | ✅ | 200 |
| GET | `/api/motoristas/{id}` | Buscar motorista por ID | ✅ | 200 / 404 |
| POST | `/api/motoristas` | Criar novo motorista | ✅ | 201 / 400 / 409 |
| PUT | `/api/motoristas/{id}` | Atualizar motorista | ✅ | 200 / 400 / 404 / 409 |
| DELETE | `/api/motoristas/{id}` | Remover motorista | ✅ | 204 / 404 |

###  Veículos
| Método | Rota | Descrição | Auth | HTTP |
|--------|------|-----------|------|------|
| GET | `/api/veiculos` | Listar todos os veículos | ✅ | 200 |
| GET | `/api/veiculos/{id}` | Buscar veículo por ID | ✅ | 200 / 404 |
| POST | `/api/veiculos` | Criar novo veículo | ✅ | 201 / 400 / 409 |
| PUT | `/api/veiculos/{id}` | Atualizar veículo | ✅ | 200 / 400 / 404 / 409 |
| DELETE | `/api/veiculos/{id}` | Remover veículo | ✅ | 204 / 404 |

###  Viagens
| Método | Rota | Descrição | Auth | HTTP |
|--------|------|-----------|------|------|
| GET | `/api/viagens` | Listar todas as viagens | ✅ | 200 |
| GET | `/api/viagens/status/{status}` | Filtrar viagens por status | ✅ | 200 |
| GET | `/api/viagens/{id}` | Buscar viagem por ID | ✅ | 200 / 404 |
| POST | `/api/viagens` | Criar nova viagem | ✅ | 201 / 400 / 409 |
| PUT | `/api/viagens/{id}` | Atualizar viagem | ✅ | 200 / 400 / 404 / 409 |
| DELETE | `/api/viagens/{id}` | Remover viagem | ✅ | 204 / 400 / 404 |

###  Checkpoints (Rastreamento GPS)
| Método | Rota | Descrição | Auth | HTTP |
|--------|------|-----------|------|------|
| GET | `/api/checkpoints` | Listar todos os checkpoints | ✅ | 200 |
| GET | `/api/checkpoints/viagem/{idViagem}` | Histórico de posições por viagem | ✅ | 200 / 404 |
| GET | `/api/checkpoints/{id}` | Buscar checkpoint por ID | ✅ | 200 / 404 |
| POST | `/api/checkpoints` | Registrar posição GPS / status IoT | ✅ | 201 / 400 / 404 |
| PUT | `/api/checkpoints/{id}` | Atualizar checkpoint | ✅ | 200 / 400 / 404 |
| DELETE | `/api/checkpoints/{id}` | Remover checkpoint | ✅ | 204 / 404 |

---

##  Exemplos de Requisição

### POST /api/auth/register
```json
{
  "usuario": "joao.silva",
  "email": "joao@astrotrack.com",
  "senha": "senha123"
}
```

### POST /api/auth/login
```json
{
  "email": "joao@astrotrack.com",
  "senha": "senha123"
}
```

### POST /api/clientes
```json
{
  "nome": "Agro Norte Ltda",
  "cnpj": "11222333000181",
  "email": "contato@agronorte.com",
  "telefone": "(11) 99999-0001",
  "status": "ATIVO"
}
```

### POST /api/motoristas
```json
{
  "nome": "Carlos Silva",
  "cpf": "52998224725",
  "cnh": "12345678901",
  "telefone": "(11) 98888-0002",
  "status": "ATIVO"
}
```

### POST /api/veiculos
```json
{
  "placa": "ABC1D23",
  "modelo": "Constellation 25.390",
  "marca": "Volkswagen",
  "ano": 2022,
  "status": "DISPONIVEL"
}
```

### POST /api/viagens
```json
{
  "idCliente": 1,
  "idMotorista": 1,
  "idVeiculo": 1,
  "origem": "Cuiabá, MT",
  "destino": "Santarém, PA",
  "dataInicio": "2026-06-10T08:00:00",
  "dataFim": null,
  "status": "EM_ANDAMENTO",
  "quilometragemTotal": 0.00
}
```

### POST /api/checkpoints
```json
{
  "idViagem": 1,
  "latitude": -12.5804,
  "longitude": -55.9119,
  "dataRegistro": "2026-06-10T10:30:00",
  "botaoPanico": 0,
  "portaAberta": 0
}
```

---

##  Regras de Negócio

| Regra | Comportamento |
|-------|--------------|
| Cliente inativo em viagem | `400 Bad Request` |
| Motorista inativo em viagem | `400 Bad Request` |
| Veículo em manutenção ou inativo em viagem | `400 Bad Request` |
| Motorista/veículo já em outra viagem EM_ANDAMENTO | `409 Conflict` |
| Viagem FINALIZADA sem data de fim | `400 Bad Request` |
| Data de fim anterior à data de início | `400 Bad Request` |
| Checkpoint em viagem FINALIZADA ou CANCELADA | `400 Bad Request` |
| Deletar viagem EM_ANDAMENTO | `400 Bad Request` |
| Veículo muda para EM_VIAGEM ao iniciar viagem | Automático |
| Veículo volta para DISPONIVEL ao finalizar/cancelar | Automático |
| CNPJ, CPF, CNH, placa, e-mail duplicados | `409 Conflict` |

---

##  Retornos HTTP

| Código | Situação |
|--------|----------|
| 200 | OK — listagem ou atualização com sucesso |
| 201 | Created — recurso criado com sucesso |
| 204 | No Content — recurso removido com sucesso |
| 400 | Bad Request — validação ou regra de negócio violada |
| 401 | Unauthorized — token ausente, inválido ou credenciais incorretas |
| 404 | Not Found — recurso não encontrado |
| 409 | Conflict — duplicidade de dados únicos |
| 500 | Internal Server Error — erro não tratado |

---

##  Exemplos de Testes

### Teste de erro — cliente inativo em viagem
```json
POST /api/viagens
{
  "idCliente": 3,
  "idMotorista": 1,
  "idVeiculo": 1,
  "origem": "São Paulo, SP",
  "destino": "Manaus, AM",
  "dataInicio": "2026-06-10T08:00:00",
  "dataFim": null,
  "status": "AGENDADA",
  "quilometragemTotal": 0.00
}
```
**Resposta esperada:** `400 Bad Request` — "Cliente precisa estar ativo para receber uma viagem"

### Teste de erro — placa duplicada
```json
POST /api/veiculos
{
  "placa": "ABC1D23",
  "modelo": "Outro Modelo",
  "marca": "Outra Marca",
  "ano": 2023,
  "status": "DISPONIVEL"
}
```
**Resposta esperada:** `409 Conflict` — "Já existe veículo cadastrado com esta placa"

### Teste de erro — checkpoint em viagem finalizada
```json
POST /api/checkpoints
{
  "idViagem": 1,
  "latitude": -5.0,
  "longitude": -52.0,
  "dataRegistro": "2026-06-12T10:00:00",
  "botaoPanico": 0,
  "portaAberta": 0
}
```
**Resposta esperada:** `400 Bad Request` — "Não é possível registrar checkpoints em viagens encerradas"

---

##  Valores aceitos para os Enums

### StatusCadastro (Clientes e Motoristas)
```
ATIVO | INATIVO | BLOQUEADO
```

### StatusVeiculo
```
DISPONIVEL | EM_MANUTENCAO | EM_VIAGEM | INATIVO
```

### StatusViagem
```
AGENDADA | EM_ANDAMENTO | FINALIZADA | CANCELADA
```

---

##  Como fazer o deploy (Render)

1. Suba o projeto para o GitHub
2. Acesse [render.com](https://render.com) e crie uma conta
3. Clique em **New → Web Service**
4. Conecte o repositório GitHub
5. Configure:
   - **Runtime:** Docker ou .NET
   - **Build Command:** `dotnet publish -c Release -o out`
   - **Start Command:** `dotnet out/AstroTrack.dll`
6. Adicione a variável de ambiente `ConnectionStrings__OracleConnection` com sua string de conexão
7. Clique em **Deploy**
8. Atualize os links no topo deste README

---

*Projeto desenvolvido para a Global Solution 2026/1 — FIAP*
