#  InvesTry

> A Secure & Scalable Multi-Model Crowdfunding Platform  
> Graduation Project – South Valley National University 
<p align="center">
  <a href="https://investry-project.vercel.app" target="_blank">
    🌐 Live Demo – Visit InvesTry Platform
  </a>
</p>

<p align="center">
  🔗 https://investry-project.vercel.app
</p>

---

#  Overview

**InvesTry** is a full-stack crowdfunding platform designed to connect founders with investors through a secure, transparent, and automated ecosystem.

The platform supports:

-  Reward-Based Funding  
-  Equity-Based Funding  
-  Mudarabah (Sharia-Compliant Profit Sharing)

Unlike traditional crowdfunding systems, InvesTry integrates:

 Escrow-based secure transactions  
 External KYC Provider integration  
 Automated reward & profit distribution  
 Full financial audit logging  

---

#  System Architecture

## 🔹 Frontend
- React.js
- Responsive Design (Mobile & Desktop)

## 🔹 Backend
- ASP.NET Core (.NET 8)
- RESTful APIs
- JWT Authentication
- Clean Architecture Pattern

## 🔹 Caching Layer (Redis)

- Integrated Redis Distributed Cache to improve performance and reduce database load.
- Implemented using a Clean Architecture-friendly MediatR Pipeline Behavior.
- Applied Cache-Aside pattern for efficient read optimization.

### Cached Queries:
- Get All Projects (Investor Feed)
- Get Project Details
- Get Founder Projects
- Get User Profile

### Strategy:
- Cache-Aside Pattern
- Key-based caching per entity/user
- Automatic cache invalidation on data changes (Create / Invest / Publish operations)

### Benefits:
- Faster API response times
- Reduced database load
- Improved scalability for read-heavy operations

## 🔹 Database
- SQL Server
- Entity Framework Core

## 🔹 Security Layer
- HTTPS
- Role-Based Authorization
- Escrow-Based Fund Handling
- External KYC Provider Integration

---

#  KYC Integration (Updated Implementation)

Instead of manual admin verification, InvesTry integrates with an **External KYC Provider API** to automate identity verification.

###  KYC Flow:

1. User submits identity information.
2. System sends verification request to KYC Provider.
3. Provider validates:
   - Identity Document
   - Selfie Match
   - AML Compliance
4. System receives verification status:
   -  Approved
   -  Rejected
   -  Pending
5. User account is automatically updated.

###  Benefits:

- Eliminates manual admin review
- Reduces fraud risk
- Ensures regulatory compliance
- Speeds up onboarding
- Production-ready identity validation

---

#  Core Features

##  User Management
- Secure Registration & Login
- JWT Authentication
- Role-Based Access (Founder / Investor / Admin)
- Email Notifications
- External KYC Verification

---

##  Campaign Management

### Founders Can:
- Create Reward, Equity, or Mudarabah Campaign
- Set Funding Goals & Duration
- Upload Media & Documents
- Define Reward Tiers / Equity Allocation / Profit Ratio
- Track Funding Progress
- View Investor Activity

---

##  Investment System

### Investors Can:
- Browse Approved Campaigns
- Invest Securely via Escrow
- Track Portfolio Performance
- View Ownership (Equity-Based)
- Receive Scheduled Profits (Mudarabah)
- Get Automatic Refunds if Campaign Fails

---

##  Escrow & Financial Management

- All investments locked in Escrow
- Automatic release upon campaign success
- Automatic refund upon failure
- Internal wallet system

---

##  Distribution Engine

### Reward-Based
- Automated reward scheduling
- Tier-based reward assignment

### Equity-Based
- Real-time ownership calculation
- Cap-table preview

### Mudarabah
- Capital return at completion
- Dynamic profit calculation engine

---

#  Admin Capabilities

- Monitor campaigns
- View transactions & audit logs
- Manage support tickets
- Monitor KYC status via provider integration

---

#  External Integrations

-  Payment Gateway API (stripe)
-  External KYC Provider API (Didit)
-  RESTful Service Architecture

---

# Docker Support

The project includes a complete Dockerized environment for simplified setup and deployment.

### Services
- ASP.NET Core API
- SQL Server
- Redis Cache


### Run with Docker

```bash
docker compose up --build
```

---

#  Installation

## Clone Repository

```bash
git clone https://github.com/AbdalrahmanOkeil/InvesTry.GD.git
cd InvesTry.GD
```
