# 0001 – Monorepo Structure

## Context
We’re building a full-stack project with shared ownership across backend and frontend.

## Decision
Use a top-level monorepo with `frontend/`, `backend/`, and (potentially) `shared/`.

## Consequences
+ Easier to sync API types later
+ Unified CI/CD pipeline
- Slightly more setup overhead