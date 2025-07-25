# 🔐 Authentication & Authorization Checklist for Full-Stack Apps (.NET + SPA + Mobile)

## ✅ Identity Providers & Protocols
- [ ] Choose a primary identity provider (e.g., Azure AD, Auth0, IdentityServer)
- [ ] Use OpenID Connect (OIDC) for authentication
- [ ] Use OAuth 2.0 for authorization and token flows
- [ ] Prefer Authorization Code Flow with PKCE for web and mobile apps
- [ ] Register each client (SPA, mobile, server) with appropriate grant types and scopes

---

## 🏛️ IdentityServer (Self-Hosted Identity Provider)
- [ ] Use IdentityServer if a self-hosted IdP is needed
- [ ] Integrate with ASP.NET Core Identity for user management
- [ ] Configure client settings securely (grant types, secrets, redirect URIs)
- [ ] Enable token cleanup jobs
- [ ] Use `IClientStore`, `IResourceStore`, and `IProfileService` for dynamic/extensible config (if needed)

---

## 👤 ASP.NET Core Identity (User Management)
- [ ] Use Identity for internal user store and login flows
- [ ] Scaffold UI for registration, login, 2FA, password reset, etc.
- [ ] Hash passwords with PBKDF2 or Argon2 (default is strong)
- [ ] Configure lockout thresholds and user validation rules

---

## 🔗 External Logins & Social Auth
- [ ] Support external logins (e.g., Google, GitHub, Microsoft)
- [ ] Use `.AddOpenIdConnect()` or `.AddOAuth()` in `Startup.cs`
- [ ] Store external tokens (access/refresh) securely via `UserManager` tokens
- [ ] Detect and link multiple external logins to the same user
- [ ] Prevent duplicate account creation for the same email

---

## 🧾 Claims Management & Identity Transformation
- [ ] Normalize claims from multiple identity providers
- [ ] Use `OnTokenValidated` or `OnCreatingTicket` to enrich or transform identity
- [ ] Add role/permission claims from DB or other sources
- [ ] Map external claims (e.g., `given_name`, `email`) to internal format

---

## 🍪 Authentication Schemes & Cookie Flows
- [ ] Use named authentication schemes for external providers and main app cookie
- [ ] Use intermediate cookies during external provider sign-in callbacks
- [ ] Configure cookie settings securely:
  - [ ] `HttpOnly = true`
  - [ ] `Secure = true`
  - [ ] `SameSite = Lax` or `Strict` (depending on app)
  - [ ] Short expiration + sliding expiration
- [ ] Reissue cookie on principal changes

---

## 🔐 Token Security (Access & Refresh Tokens)
- [ ] Use short-lived access tokens (5–15 mins)
- [ ] Store refresh tokens securely:
  - [ ] Server-side only for web (via BFF or cookie-based auth)
  - [ ] Secure storage (e.g., Keychain/Keystore) for mobile
- [ ] Rotate refresh tokens on use (sliding expiration)
- [ ] Revoke refresh tokens on logout or compromise
- [ ] Validate all tokens (issuer, audience, signature, expiration)

---

## 🧰 Backend API Protection (ASP.NET Core)
- [ ] Use JWT Bearer middleware (`AddAuthentication().AddJwtBearer(...)`)
- [ ] Validate `iss`, `aud`, `exp`, and `sub` in access tokens
- [ ] Use policies/roles with `[Authorize(Policy="...")]`
- [ ] Consider `scope`-based authorization with `RequireScope("api.read")`
- [ ] Rate-limit sensitive endpoints (e.g., login, password reset)
- [ ] Support token introspection if using reference tokens

---

## 🔐 Authorization (Policies & Claims)
- [ ] Use `[Authorize]`, `[Authorize(Roles="Admin")]`, and policy-based checks
- [ ] Define policies in `Startup.cs` using custom claims or requirements
- [ ] Protect resources based on user identity, tenant, roles, etc.
- [ ] Support fine-grained access control via scopes or permission claims

---

## 🔁 Backend-for-Frontend (BFF) Pattern
- [ ] Use BFF server to proxy API calls and manage sessions
- [ ] Store tokens securely server-side and expose minimal data to frontend
- [ ] Use anti-forgery tokens to protect POST/PUT requests
- [ ] Same-origin SPA + BFF setup preferred for CSRF protection

---

## 🌐 SPA Authentication (React / Angular / Blazor WASM)
- [ ] Use Authorization Code Flow with PKCE (via MSAL, oidc-client, etc.)
- [ ] Secure local storage: avoid long-lived access tokens in browser
- [ ] Use silent refresh with caution (iframe or refresh token rotation)
- [ ] Avoid storing refresh tokens in the browser unless absolutely required
- [ ] Validate all tokens before use
- [ ] Confirm SDK behavior for token renewal, storage, and revocation

---

## 📱 Mobile App Authentication
- [ ] Use Authorization Code Flow with PKCE
- [ ] Store tokens securely (Keychain on iOS, Keystore on Android)
- [ ] Handle token refresh automatically
- [ ] Support logout by clearing tokens and revoking refresh token
- [ ] Avoid embedding secrets in mobile apps

---

## 🚪 Logout & Session Management
- [ ] Support local logout (clear cookie or tokens)
- [ ] Support federated logout (OIDC `end_session_endpoint`)
- [ ] Revoke refresh/access tokens on logout
- [ ] Invalidate session identifiers on logout and token rotation
- [ ] Support global sign-out if required by business logic

---

## 🧪 Testing & Validation
- [ ] Test login flows for each identity provider
- [ ] Test session expiration and auto-logout
- [ ] Test refresh token flow and token expiration edge cases
- [ ] Use automated integration tests for authentication/authorization logic
- [ ] Conduct periodic penetration tests and threat modeling

---

## 📈 Monitoring & Alerts
- [ ] Log authentication and authorization events
- [ ] Alert on abnormal login patterns (e.g., IP changes, high failure rates)
- [ ] Monitor token issuance and usage trends
- [ ] Integrate with SIEM or logging platforms (e.g., Serilog + Seq/Splunk)

---

## 🧪 Secrets & Configuration
- [ ] Store client secrets securely (environment variables, key vaults)
- [ ] Avoid secrets in source control
- [ ] Rotate secrets regularly
- [ ] Use managed identity or workload identity for cloud-hosted apps

---

## 🌐 Web Hardening
- [ ] Enforce HTTPS (redirect + HSTS)
- [ ] Enable security headers:
  - [ ] `Content-Security-Policy`
  - [ ] `X-Frame-Options`
  - [ ] `X-Content-Type-Options`
  - [ ] `Referrer-Policy`
- [ ] Apply rate limiting to login/registration endpoints
- [ ] Protect against CSRF (via anti-forgery tokens in BFF or SameSite cookies)