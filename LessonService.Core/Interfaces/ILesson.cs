using LessonService.Core.Base;

namespace LessonService.Core.Interfaces
{
    public interface ILesson
    {
        void ValidateLesson();
        void EnrollStudent(Guid studentId);
        void UnEnrollStudent(Guid studentId);
        void AssignTrainer(Guid trainerId);
        void RemoveTrainer();
        void Reschedule(DateTime dateFrom, int duration);
        void CancelLesson();
        void CompleteLesson();
        void StartLesson();
        void SetName(string value);
        void SetDescription(string value);
        void SetTrainingLevel(TrainingLevel value);
        void SetLessonType(LessonType value);
        void SetMaxStudents(int value);
        string GetName();
        string GetDescription();
        TrainingLevel GetTrainingLevel();
        LessonType GetLessonType();
        int GetMaxStudents();
    }
}
