# ClinicScheduler Web

> Sistema web de agendamento clínico desenvolvido em ASP.NET Core — otimizando o gerenciamento de consultas de forma simples e segura.

![Status](https://img.shields.io/badge/status-em%20desenvolvimento-yellow)
![Tecnologia](https://img.shields.io/badge/tecnologia-ASP.NET%20Core-blue)
![Linguagem](https://img.shields.io/badge/linguagem-C%23-purple)

---

## 📋 Sobre o Projeto

O **ClinicScheduler Web** é um sistema de agendamento clínico que permite gerenciar profissionais, pacientes e consultas de forma centralizada, com foco em segurança, praticidade e ausência de conflitos de horário.

---

## ✅ Funcionalidades

- Autenticação com senha protegida por hash SHA-256
- Cadastro e gerenciamento de profissionais
- Cadastro e busca de pacientes (por nome ou ID)
- Criação de agendas com intervalos configuráveis e pausa de almoço opcional
- Agendamento de pacientes em horários disponíveis
- Edição e exclusão de agendamentos
- Status de horários: Confirmado, Cancelado, Falta, Bloqueado
- Bloqueio de horários com descrição de motivo
- Validação contra duplicações e conflitos de horário

---

## 🛠️ Tecnologias

| Tecnologia        | Uso                         |
| ----------------- | --------------------------- |
| C# / ASP.NET Core | Backend e lógica de negócio |
| Razor Pages       | Interface web               |
| HTML + CSS        | Frontend                    |
| SQLite            | Banco de dados              |
| SHA-256           | Criptografia de senhas      |

---

## 🗂️ Documentação

A pasta `/docs` contém os artefatos técnicos do projeto:

| Arquivo                               | Descrição                                                     |
| ------------------------------------- | ------------------------------------------------------------- |
| `requisitos.md`                       | Levantamento completo de requisitos funcionais e de segurança |
| `diagrama-er.png`                     | Diagrama Entidade-Relacionamento do banco de dados            |
| `fluxograma-login.png`                | Fluxo de autenticação com SHA-256                             |
| `fluxograma-criacao-agenda.png`       | Fluxo de criação de agenda com geração automática de horários |
| `fluxograma-agendamento-paciente.png` | Fluxo de agendamento de paciente com validações               |

---

## 🗃️ Estrutura do Banco de Dados

| Tabela          | Descrição                                             |
| --------------- | ----------------------------------------------------- |
| `Usuario`       | Usuários do sistema com nível de acesso               |
| `Profissional`  | Profissionais vinculados às agendas                   |
| `Paciente`      | Pacientes cadastrados no sistema                      |
| `Agenda`        | Agendas por profissional com configuração de horários |
| `HorarioAgenda` | Horários gerados automaticamente por agenda           |
| `Agendamento`   | Vínculo entre paciente e horário                      |

---

## 🔒 Segurança

- Senhas armazenadas com hash SHA-256
- Proteção contra SQL Injection via parâmetros ORM
- Proteção contra XSS com escape de outputs
- Tokens Anti-Forgery (CSRF) do ASP.NET Core
- Todas as rotas exigem autenticação

---

## 📁 Estrutura do Projeto

```
ClinicScheduler_Web/
├── docs/
│   ├── requisitos.md
│   ├── diagrama-er.png
│   ├── fluxograma-login.png
│   ├── fluxograma-criacao-agenda.png
│   └── fluxograma-agendamento-paciente.png
├── Pages/
├── wwwroot/
├── Program.cs
└── README.md
```

---

## 👨‍💻 Autor

**Wendel** — Desenvolvedor C# | ADS + Pós-graduação em Banco de Dados

[![GitHub](https://img.shields.io/badge/GitHub-perfil-black)](https://github.com/seu-usuario)
