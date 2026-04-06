# Requisitos do Sistema — ClinicScheduler Web

## Informações Gerais

- **Projeto:** ClinicScheduler Web
- **Tecnologia:** ASP.NET Core (Razor Pages) + C# + SQLite
- **Versão:** 1.0
- **Data:** 2026-04-06

---

## 1. Autenticação

- [ ] Login com senha protegida por hash SHA-256
- [ ] Sessão autenticada obrigatória para acessar qualquer página
- [ ] Proteção contra falhas de segurança (SQL Injection, XSS, CSRF)

---

## 2. Cadastro de Profissionais

- [ ] ID única gerada automaticamente
- [ ] Nome do profissional
- [ ] Não permitir cadastro duplicado (mesmo nome)

---

## 3. Gestão de Agenda

- [ ] Criar agenda vinculada a um profissional
- [ ] Definir data da agenda
- [ ] Definir horário de início e horário de fim
- [ ] Definir intervalo entre os horários (ex: 30 minutos)
- [ ] Opção de horário de almoço via checkbox
  - [ ] Se marcado, definir duração do almoço
- [ ] Botão de excluir agenda

---

## 4. Cadastro de Pacientes

- [ ] ID única gerada automaticamente
- [ ] Nome completo
- [ ] Idade
- [ ] Não permitir cadastro duplicado

---

## 5. Busca de Pacientes

- [ ] Campo de busca por nome
- [ ] Campo de busca por ID
- [ ] Resultados em tempo real ou via botão de pesquisa

---

## 6. Agendamento

- [ ] Duplo clique no horário abre janela de agendamento
- [ ] Botão alternativo para agendar no horário selecionado
- [ ] Selecionar paciente para vincular ao horário
- [ ] Não permitir agendar paciente em horário já ocupado
- [ ] Não permitir agendar o mesmo paciente duas vezes no mesmo dia

---

## 7. Edição e Exclusão de Agendamento

- [ ] Duplo clique em horário ocupado abre janela de edição
- [ ] Botão alternativo para alterar paciente no horário selecionado
- [ ] Botão para excluir paciente do horário

---

## 8. Status do Horário

Clique com botão direito no horário exibe menu de opções:

**Horário com paciente agendado:**

- [ ] Confirmado
- [ ] Cancelado
- [ ] Falta

**Horário sem paciente:**

- [ ] Bloquear horário
  - [ ] Campo de texto para motivo do bloqueio
  - [ ] Botão Confirmar e botão Cancelar
  - [ ] Se confirmado, exibe no horário: `Bloqueado — [motivo]`

---

## 9. Segurança

- [ ] Hash SHA-256 para senhas
- [ ] Proteção contra SQL Injection (uso de parâmetros/ORM)
- [ ] Proteção contra XSS (escape de outputs)
- [ ] Proteção contra CSRF (tokens Anti-Forgery do ASP.NET Core)
- [ ] Acesso restrito por autenticação em todas as rotas

---

## Regras de Negócio

| Regra | Descrição                                            |
| ----- | ---------------------------------------------------- |
| RN01  | Não permitir agendamento em horário já ocupado       |
| RN02  | Não permitir duplicação de pacientes no cadastro     |
| RN03  | Não permitir duplicação de profissionais no cadastro |
| RN04  | Horário bloqueado não pode receber agendamento       |
| RN05  | Toda ação requer usuário autenticado                 |
