
using LessonService.Core.Base.Exceptions;
using LessonService.Core.Interfaces;

namespace LessonService.Core.Base
{
    public enum TrainingLevel
    {
        Beginner = 1,
        Intermediate = 2,
        Advanced = 3,
        Expert = 4,
        Master = 5
    }

    public enum LessonType
    {
        None = 1,
        Freestyle = 2,
        Slalom = 3,
        GiantSlalom = 4,
        Downhill = 5,
        SuperG = 6
    }

    public enum LessonStatus
    {
        Scheduled = 1,
        InProgress = 2,
        Completed = 3,
        Canceled = 4
    }

    public class Lesson : Entity<Guid>, ILesson
    {
        public Lesson(Guid id, string name, string description, DateTime dateFrom, int duration, TrainingLevel trainingLevel, 
            LessonType lessonType, int maxStudents, Guid trainerId) : base (id)
        {
            Name = name;
            Description = description;
            DateFrom = dateFrom;
            Duration = duration;
            TrainingLevel = trainingLevel;
            LessonType = lessonType;
            LessonStatus = LessonStatus.Scheduled;
            MaxStudents = maxStudents;
            TrainerId = trainerId;
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime DateFrom { get; private set; }
        public int Duration { get; private set; }
        public TrainingLevel TrainingLevel { get; private set; }
        public LessonType LessonType { get; private set; }
        public int MaxStudents { get; private set; }

        public LessonStatus LessonStatus { get; private set; }
        public ICollection<LessonGroup> LessonGroups { get; } = new HashSet<LessonGroup>();
        public Guid TrainerId {get; private set; }

        public string GetName() => Name;
        public string GetDescription() => Description;
        public DateTime GetDateFrom() => DateFrom;
        public int GetDuration() => Duration;
        public TrainingLevel GetTrainingLevel() => TrainingLevel;
        public LessonType GetLessonType() => LessonType;
        public int GetMaxStudents() => MaxStudents;
        public LessonStatus GetLessonStatus() => LessonStatus;
        public void SetName(string value) => Name = value;
        public void SetDescription(string value) => Description = value;
        public void SetTrainingLevel(TrainingLevel value) => TrainingLevel = value;
        public void SetLessonType(LessonType value) => LessonType = value;
        public void SetMaxStudents(int value) => MaxStudents = value;
        public void ValidateLesson()
        {
            if (LessonStatus == LessonStatus.Scheduled)
                return;
            throw LessonStatus switch
            {
                LessonStatus.Completed => new LessonAlreadyCompletedException(this),
                LessonStatus.InProgress => new LessonInProgressException(this),
                LessonStatus.Canceled => new LessonCancelledException(this)
            };
        }
        public void EnrollStudent(Guid studentId)
        {
            if (LessonGroups.Count >= MaxStudents)
                throw new InvalidOperationException("Cannot enroll the student. Lesson is full.");

            ValidateLesson();
            LessonGroups.Add(new LessonGroup() { LessonId = this.Id, StudentId = studentId });
        }

        public void UnEnrollStudent(Guid studentId)
        {
            ValidateLesson();
            LessonGroups.Remove(new LessonGroup() { LessonId = this.Id, StudentId = studentId  });
        }

        public void Reschedule(DateTime dateFromValue, int durationValue)
        {
            if (DateFrom == dateFromValue)
                throw new InvalidOperationException(
                    "Cannot Reschedule Lesson. The current start date/time is equals the new one.");
            if (durationValue <= 1)
                throw new InvalidOperationException(
                    "Cannot Reschedule Lesson. The duration value must be grater than 1 min.");
            DateFrom = dateFromValue;
            Duration = durationValue;
        }

        public void CancelLesson()
        {
            ValidateLesson();
            LessonStatus = LessonStatus.Canceled;
        }

        public void CompleteLesson()
        {
            if (LessonStatus != LessonStatus.InProgress)
            {
                throw LessonStatus switch
                {
                    LessonStatus.Scheduled => new InvalidOperationException(
                        "Cannot Complete Lesson. Lesson is not started yet."),
                    LessonStatus.Completed => new InvalidOperationException(
                        "Cannot Complete Lesson. Lesson is completed already."),
                    LessonStatus.Canceled => new InvalidOperationException("Cannot Cancel Lesson. Lesson is canceled."),
                };
            }
            LessonStatus = LessonStatus.Completed;
        }
        public void StartLesson()
        {
            ValidateLesson();
            LessonStatus = LessonStatus.InProgress;
        }

        public void AssignTrainer(Guid trainerId)
        {
            ValidateLesson();
            TrainerId = trainerId;
        }
        public void RemoveTrainer()
        {
            if (TrainerId == Guid.Empty)
                throw new InvalidOperationException("Cannot Remove the trainer. The trainer is not assigned.");
            ValidateLesson();
            TrainerId = Guid.Empty;
        }
        public Lesson() : base(Guid.NewGuid())
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
            Description = string.Empty;
        }
        public Lesson(string name, string description ) : base(Guid.NewGuid())
        {
            Name = name;
            Description = description;
        }
    }
}
