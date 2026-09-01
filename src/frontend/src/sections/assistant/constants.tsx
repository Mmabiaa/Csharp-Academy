export const LOGO = (
  <img
    src="/images/logo.png"
    alt=""
    aria-hidden="true"
    width={28}
    height={28}
    style={{ objectFit: "contain", display: "block" }}
  />
);

export const SUGGESTED_QUESTIONS = [
  "What is a class in C#?",
  "How do loops work?",
  "Explain async/await",
  "What are generics?",
];

export interface AssistantMessage {
  role: "user" | "assistant";
  content: string;
}
