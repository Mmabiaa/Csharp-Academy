using CsharpAcademy.Application.Common.Interfaces;
using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Lessons.Queries;

public record GetLessonCompanionDataQuery(int LessonId) : IRequest<LessonCompanionDataDto?>;

public class GetLessonCompanionDataQueryHandler : IRequestHandler<GetLessonCompanionDataQuery, LessonCompanionDataDto?>
{
    private readonly ILessonRepository _lessonRepository;
    private readonly ICodingExerciseRepository _exerciseRepository;
    private readonly ITutorialStepRepository _tutorialRepository;

    public GetLessonCompanionDataQueryHandler(
        ILessonRepository lessonRepository,
        ICodingExerciseRepository exerciseRepository,
        ITutorialStepRepository tutorialRepository)
    {
        _lessonRepository = lessonRepository;
        _exerciseRepository = exerciseRepository;
        _tutorialRepository = tutorialRepository;
    }

    public async Task<LessonCompanionDataDto?> Handle(
        GetLessonCompanionDataQuery request,
        CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetByIdAsync(request.LessonId, cancellationToken);
        if (lesson is null) return null;

        var exercises = await _exerciseRepository.GetByLessonIdAsync(lesson.Id, cancellationToken);
        var tutorialSteps = await _tutorialRepository.GetByLessonIdAsync(lesson.Id, cancellationToken);

        var haystack = (lesson.Title + " " + lesson.Content + " " + lesson.BestPractices + " " +
                        lesson.VoiceSummary + " " + string.Join(" ", tutorialSteps.Select(s => s.Title + " " + s.Content)))
            .ToLowerInvariant();

        var result = new LessonCompanionDataDto();

        foreach (var g in LessonCompanionKnowledgeBase.Greetings)
            result.Greetings.Add(g);
        foreach (var c in LessonCompanionKnowledgeBase.Celebrations)
            result.Celebrations.Add(c);
        foreach (var e in LessonCompanionKnowledgeBase.Encouragements)
            result.Encouragements.Add(e);

        foreach (var concept in LessonCompanionKnowledgeBase.ConceptExplanations)
        {
            var keywords = concept.Key.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (keywords.Any(k => haystack.Contains(k.ToLowerInvariant())))
            {
                var firstKey = keywords[0];
                result.ConceptExplanations.TryAdd(firstKey, concept.Value);
            }
        }

        foreach (var qa in LessonCompanionKnowledgeBase.QuestionAnswers)
        {
            if (qa.Keywords.Any(k => haystack.Contains(k.ToLowerInvariant())))
            {
                result.QuestionAnswers.Add(new CompanionQaDto
                {
                    Keywords = qa.Keywords,
                    Question = qa.Question,
                    Answer = qa.Answer,
                });
            }
        }

        foreach (var ex in LessonCompanionKnowledgeBase.Examples)
        {
            if (haystack.Contains(ex.Concept.ToLowerInvariant()))
            {
                result.Examples.Add(new CompanionExampleDto
                {
                    Concept = ex.Concept,
                    Analogy = ex.Analogy,
                    CodeSample = ex.CodeSample,
                });
            }
        }

        foreach (var q in LessonCompanionKnowledgeBase.MiniQuizzes)
        {
            if (q.Topics.Any(t => haystack.Contains(t.ToLowerInvariant())))
            {
                result.MiniQuizzes.Add(new CompanionMiniQuizDto
                {
                    Question = q.Question,
                    Options = q.Options,
                    CorrectIndex = q.CorrectIndex,
                    Explanation = q.Explanation,
                });
            }
        }

        foreach (var ex in exercises)
        {
            result.PracticeHints.Add(new CompanionPracticeHintDto
            {
                ExerciseTitle = ex.Title,
                Hints = new List<string>
                {
                    ex.Hint,
                    "Read the instructions one sentence at a time and write down in plain English what each part means.",
                    $"Try writing a really small version first — just enough code to get the {ex.Title.ToLowerInvariant()} to compile.",
                },
            });
        }

        if (!result.Greetings.Any())
        {
            result.Greetings.AddRange(LessonCompanionKnowledgeBase.Greetings);
        }
        if (!result.QuestionAnswers.Any())
        {
            result.QuestionAnswers.AddRange(LessonCompanionKnowledgeBase.QuestionAnswers
                .Take(5)
                .Select(q => new CompanionQaDto
                {
                    Keywords = q.Keywords,
                    Question = q.Question,
                    Answer = q.Answer,
                }));
        }
        if (!result.ConceptExplanations.Any())
        {
            foreach (var c in LessonCompanionKnowledgeBase.ConceptExplanations.Take(10))
            {
                var firstKey = c.Key.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[0];
                result.ConceptExplanations.TryAdd(firstKey, c.Value);
            }
        }
        if (!result.MiniQuizzes.Any())
        {
            result.MiniQuizzes.AddRange(LessonCompanionKnowledgeBase.MiniQuizzes
                .Take(4)
                .Select(q => new CompanionMiniQuizDto
                {
                    Question = q.Question,
                    Options = q.Options,
                    CorrectIndex = q.CorrectIndex,
                    Explanation = q.Explanation,
                }));
        }
        if (!result.Examples.Any())
        {
            result.Examples.AddRange(LessonCompanionKnowledgeBase.Examples
                .Take(3)
                .Select(e => new CompanionExampleDto
                {
                    Concept = e.Concept,
                    Analogy = e.Analogy,
                    CodeSample = e.CodeSample,
                }));
        }

        return result;
    }
}
