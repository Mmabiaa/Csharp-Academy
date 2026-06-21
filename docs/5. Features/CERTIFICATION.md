# Certification System

C# Academy provides tangible proof of learning through dynamic PDF certificates.

---

## 🏆 Earning a Certificate

A certificate is automatically enabled for download when:
1. All lessons of a course are marked as completed.
2. All mandatory quizzes for that course have been passed.
3. The platform-calculated `CompletionPercentage` for that enrollment reaches **100%**.

---

## 📄 Generation (QuestPDF)

The generation is handled server-side in `CertificatePdfService.cs`:
- **Font**: Uses standard browser-safe sans-serif fonts for accessibility.
- **Branding**: Includes the official C# Academy logo and a "Verified Graduate" seal.
- **Metadata**: Encodes the Student ID and Course ID into the unique verification code.

---

## 🔗 Verification and Sharing

Each certificate has a permanent **Verification URL**:
`https://academy.com/certificates/v/ABC-123-XYZ`

- **Public Access**: This page does not require a login.
- **Authenticity**: It acts as a digital ledger entry. If a student tries to Photoshop their name onto a PDF, the verification code check will reveal the original earner's name, preventing credential fraud.

---

## 🎖️ Badges
Earning your first certificate also unlocks the **"Graduate"** platinum badge on the user's profile.
