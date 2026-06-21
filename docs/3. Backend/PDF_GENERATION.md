# PDF Generation (QuestPDF)

When a student completes a course, the system generates an official A4 Certificate.

---

## 🛠️ Implementation

- **Library**: `QuestPDF`. We chose this over older libraries (like iTextSharp) because of its modern fluent API and high-performance rendering.
- **Service**: `CertificatePdfService.cs` in the Infrastructure layer.

## 📄 Template Features

The certificate is generated as an A4 Landscape PDF containing:
- **Student Name**: Pulled from the `User` profile.
- **Course Title**: The name of the completed course.
- **Date of Issue**: The timestamp when the 100% completion threshold was met.
- **Verification ID**: A unique, cryptographically generated 8-character code.

## 🔗 Verification Workflow

1. **Generation**: The student sees a "Download Certificate" button on their completed course page.
2. **Download**: The backend generates the PDF on-the-fly and returns it with a `application/pdf` MIME type.
3. **Public Check**: A potential employer or teacher can go to the **Verify Page** and enter the code to see the digital verification record, ensuring the certificate hasn't been tampered with.
