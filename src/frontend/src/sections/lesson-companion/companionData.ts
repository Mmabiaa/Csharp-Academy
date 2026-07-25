import type {
  LessonCompanionData,
  CompanionMiniQuiz,
} from "../../lib/api";

export type {
  LessonCompanionData,
  CompanionMiniQuiz,
};

const GREETING_KEYWORDS = ["hi", "hello", "hey", "greetings", "yo", "sup", "good morning", "good afternoon", "good evening"];
const ENCOURAGE_KEYWORDS = ["encourage", "motivate", "inspire", "i need help", "stuck", "struggling", "can't do", "cant do", "give up"];

export function getAnswerFromData(
  userMessage: string,
  data: LessonCompanionData,
  lessonTitle: string,
): string {
  if (!data) {
    return "I'm still loading your lesson data — give me a moment and try again!";
  }

  const msg = (userMessage || "").toLowerCase().trim();

  if (!msg) {
    return "Hey — I didn't catch that. Ask me anything about this lesson and I'll help!";
  }

  if (GREETING_KEYWORDS.some((g) => msg.includes(g))) {
    const greeting = pickRandom(data.greetings || []) || "Hi there!";
    const topic = (Object.keys(data.conceptExplanations || {})[0]) || lessonTitle;
    return `${greeting}\n\nRight now we're working on **${lessonTitle}** — I can explain **${topic}**, walk through example code, or quiz you to test your understanding. What would you like?`;
  }

  if (ENCOURAGE_KEYWORDS.some((k) => msg.includes(k))) {
    return pickRandom(data.encouragements || []) || "You've got this — one step at a time! 💪";
  }

  const qaMatches = (data.questionAnswers || []).filter((qa) =>
    (qa.keywords || []).some((kw) => msg.includes(kw.toLowerCase())),
  );
  const qaByScore = qaMatches.sort(
    (a, b) =>
      (b.keywords || []).filter((kw) => msg.includes(kw.toLowerCase())).length -
      (a.keywords || []).filter((kw) => msg.includes(kw.toLowerCase())).length,
  );
  if (qaByScore[0]) {
    return qaByScore[0].answer;
  }

  const msgWords = msg.split(/\s+/).filter((w) => w.length > 2);
  let bestConceptKey: string | null = null;
  let bestConceptScore = 0;
  for (const [key, text] of Object.entries(data.conceptExplanations || {})) {
    let score = 0;
    for (const word of msgWords) {
      if (key.toLowerCase().includes(word)) score += 3;
      if (text.toLowerCase().includes(word)) score += 1;
    }
    if (score > bestConceptScore) {
      bestConceptScore = score;
      bestConceptKey = key;
    }
  }
  if (bestConceptKey && bestConceptScore > 0) {
    return data.conceptExplanations[bestConceptKey];
  }

  const exampleHit = (data.examples || []).find((e) =>
    msg.includes(e.concept.toLowerCase()),
  );
  if (exampleHit) {
    return `**${exampleHit.concept}**\n\n💡 Analogy: ${exampleHit.analogy}\n\n\`\`\`csharp\n${exampleHit.codeSample}\n\`\`\``;
  }

  const concepts = Object.keys(data.conceptExplanations || {});
  const topics = concepts.length > 0
    ? concepts.map((c) => `• **${c}**`).join("\n")
    : "• General C# fundamentals";
  return `I'm your lesson tutor for **${lessonTitle}** and I focus on what we're studying. Try asking:\n\n${topics}\n\nOr click one of the suggested question buttons below. You can also switch to the 🧠 Quiz tab to test yourself, or the 🔊 Listen tab to have the lesson read aloud!`;
}

export function getSuggestedQuestions(data: LessonCompanionData | null): string[] {
  if (!data) return [];
  const questions: string[] = [];

  const topConcepts = Object.keys(data.conceptExplanations || {}).slice(0, 2);
  for (const key of topConcepts) {
    questions.push(`What is ${key}?`);
  }

  const qaCount = Math.min(2, (data.questionAnswers || []).length);
  for (let i = 0; i < qaCount; i++) {
    questions.push(data.questionAnswers[i].question);
  }

  if ((data.examples || []).length > 0) {
    questions.push(`Can you show me a ${data.examples[0].concept} example?`);
  }

  if ((data.miniQuizzes || []).length > 0) {
    questions.push("Quiz me on this lesson");
  }

  return questions.slice(0, 4);
}

function pickRandom<T>(arr: T[]): T | undefined {
  if (!arr || arr.length === 0) return undefined;
  return arr[Math.floor(Math.random() * arr.length)];
}
