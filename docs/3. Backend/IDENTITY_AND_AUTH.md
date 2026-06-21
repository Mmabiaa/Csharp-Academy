# Identity and Authentication

C# Academy handles security using a combination of **ASP.NET Core Identity** and **Stateful JWT (JSON Web Tokens)**.

---

## 🔐 Identity Configuration

The platform uses `IdentityUser<int>` for the **User** entity. This provides out-of-the-box support for:
- Secure password hashing (BCrypt-based).
- User and Role management (Student, Teacher, Admin).
- Role-based policy enforcement.

---

## 🎫 JWT Workflow

Authentication is entirely stateless. We do not use server-side sessions.

1. **Login**: The user sends email/password to `/api/auth/login`.
2. **validation**: `AuthHandler` verifies credentials and retrieves the User's roles.
3. **Issuance**: `JwtTokenService` generates a signed token.
   - **Claims**: Includes `NameIdentifier` (UserID), `Email`, `Role`, and `Jti` (Unique ID).
   - **Expiration**: Defaulted to **24 hours**.
4. **Storage**: The frontend stores the token in `localStorage`.
5. **Authorization**: Every subsequent request includes the header:
   `Authorization: Bearer <token>`
6. **Verification**: The backend middleware validates the signature and populates `User.Identity` for the controller.

---

## 🌐 Google OAuth Integration

We support "Log in with Google" via the `google-login` endpoint.
- **Frontend**: Obtains an `idToken` from Google.
- **Backend Handlers**: Use the `GoogleJsonWebSignature` library to verify the token's authenticity. If the user doesn't exist, we auto-create a Student account with their Google profile info.

---

## 🛡️ Authorization Levels

- **Any Authenticated User**: Can access profiles and simple lessons.
- **Student Role**: Restricted to their own progress and public courses.
- **Teacher Role**: Can manage classrooms, create assignments, and grade submissions.
- **Admin Role**: Full system access, including global analytics and content management.

Role checking is enforced via the `[Authorize(Roles = "...")]` attribute on controllers.
