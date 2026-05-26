# 📦 StockFlow

Sistema de estoque com gerenciamento de produtos, categorias, fornecedores e pedidos.  
O projeto segue uma arquitetura em camadas (Clean Architecture simplificada).

---

## 🧱 Estrutura do Projeto

- **Estoque.API** → API REST (Controllers e endpoints)
- **Estoque.Application** → Regras de negócio (Services)
- **Estoque.Domain** → Entidades do sistema (Models)
- **Estoque.Infrastructure** → Acesso ao banco de dados (Entity Framework Core)
- **Estoque (MVC)** → Interface do usuário (Views)

---

## 🗄️ Banco de Dados (Entity Framework Core)

O banco é gerenciado automaticamente via **Migrations**.

---

## 📦 Migrations

    dotnet ef migrations add NomeDaMigration --project Estoque.Infrastructure --startup-project Estoque.API

## ✔ Aplicar migrations no banco

    dotnet ef database update --project Estoque.Infrastructure --startup-project Estoque.API
