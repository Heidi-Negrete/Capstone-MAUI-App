using AcademicAssistant.Repositories.Models;
using SQLite;
using System.Threading.Tasks;

namespace AcademicAssistant.Repositories
{
    public class SQLiteRepository : IRepository
    {
        private SQLiteAsyncConnection _database;
        private int _studentId;
        private readonly object _lock = new object();

        public SQLiteRepository()
        {
            // TEMPORARY FOR DEBUGGING
            if (File.Exists(Constants.DatabasePath))
            {
                File.Delete(Constants.DatabasePath);
            }
        }

        private async Task Init()
        {
            if (_database != null) return;

            lock (_lock)
            {
                if (_database == null)
                {
                    _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
                }
            }

            await _database.CreateTableAsync<Student>();
            await _database.CreateTableAsync<Term>();
            await _database.CreateTableAsync<Course>();
            await _database.CreateTableAsync<PerformanceAssessment>();
            await _database.CreateTableAsync<ObjectiveAssessment>();
            await _database.CreateTableAsync<Instructor>();

            if (_studentId == 0)
            {
                await _database.InsertAsync(new Student() { Name = "John Doe" });
                _studentId = (await GetStudent()).Id;
                await SeedData();
            }
        }

        private async Task SeedData()
        {
            Term term = new Term { Title = "Term One"};
            await AddTerm(term);

            Course course = new Course
            {
                Title = "Course One",
                InstructorName = "Anika Patel",
                InstructorEmail = "anika.patel@strimeuniversity.edu",
                InstructorPhone = "555-123-4567",
            };
            await AddCourse(term.Id, course);
        }

        private async Task CreateAssessments(int courseId)
        {
            await Init();
            await _database.InsertAsync(new ObjectiveAssessment { CourseId = courseId });
            await _database.InsertAsync(new PerformanceAssessment { CourseId = courseId });
        }

        public async Task<Student> GetStudent()
        {
            await Init();
            return await _database.Table<Student>().FirstOrDefaultAsync();
        }

        public async Task UpdateStudent(int id, Student student)
        {
            await Init();
            await _database.UpdateAsync(student);
        }

        public async Task<List<Term>> GetTerms()
        {
            await Init();
            return await _database.Table<Term>().ToListAsync();
        }

        public async Task DeleteTerm(int termId)
        {
            await Init();
            Term term = await GetTermById(termId);
            if (term != null)
            {
                var courses = await GetCoursesByTermId(termId);
                foreach (var course in courses)
                {
                    await DeleteCourse(course.Id);
                }
                await _database.DeleteAsync(term);
            }
        }

        public async Task AddTerm(Term? term = null)
        {
            await Init();
            if (term == null)
            {
                term = new Term();
            }
            term.StudentId = _studentId;
            await _database.InsertAsync(term);
        }

        public async Task<Term> GetTermById(int termId)
        {
            await Init();
            return await _database.Table<Term>().Where(t => t.Id == termId).FirstOrDefaultAsync();
        }

        public async Task<List<Course>> GetCoursesByTermId(int termId)
        {
            await Init();
            return await _database.Table<Course>().Where(c => c.TermId == termId).ToListAsync();
        }

        public async Task DeleteCourse(int courseId)
        {
            await Init();
            Course course = await GetCourseById(courseId);
            if (course != null)
            {
                var assessments = await GetAssessmentsByCourseId(courseId);
                foreach (var assessment in assessments)
                {
                    await _database.DeleteAsync(assessment);
                }
                Term term = await GetTermById(course.TermId);
                term.CourseCount--;
                await _database.UpdateAsync(term);
                await _database.DeleteAsync(course);
            }
        }

        public async Task AddCourse(int termId, Course? course = null)
        {
            await Init();
            Term term = await GetTermById(termId);
            if (term.CourseCount == term.MaxCourseCount)
            {
                throw new Exception("Max course count reached");
            }
            term.CourseCount++;
            await _database.UpdateAsync(term);

            if (course == null)
            {
                course = new Course();
            }
            course.TermId = termId;
            await _database.InsertAsync(course);

            await CreateAssessments(course.Id);
        }

        public async Task<Course> GetCourseById(int courseId)
        {
            await Init();
            return await _database.Table<Course>().Where(c => c.Id == courseId).FirstOrDefaultAsync();
        }

        public async Task<List<Assessment>> GetAssessmentsByCourseId(int courseId)
        {
            await Init();
            List<Assessment> assessments = new List<Assessment>();
            assessments.AddRange(await _database.Table<ObjectiveAssessment>().Where(a => a.CourseId == courseId).ToListAsync());
            assessments.AddRange(await _database.Table<PerformanceAssessment>().Where(a => a.CourseId == courseId).ToListAsync());
            return assessments;
        }

        public async Task<Assessment> GetAssessmentById(int assessmentId)
        {
            await Init();
            Assessment assessment = await _database.Table<PerformanceAssessment>().Where(a => a.Id == assessmentId).FirstOrDefaultAsync();
            if (assessment == null)
            {
                assessment = await _database.Table<ObjectiveAssessment>().Where(a => a.Id == assessmentId).FirstOrDefaultAsync();
            }
            return assessment;
        }

        public async Task<List<Course>> GetCourses()
        {
            await Init();
            return await _database.Table<Course>().ToListAsync();
        }

        public async Task<List<Assessment>> GetAssessments()
        {
            await Init();
            List<Assessment> assessments = new List<Assessment>();
            assessments.AddRange(await _database.Table<ObjectiveAssessment>().ToListAsync());
            assessments.AddRange(await _database.Table<PerformanceAssessment>().ToListAsync());
            return assessments;
        }

        public async Task UpdateTerm(int id, Term updatedTerm)
        {
            await Init();
            var term = await GetTermById(id);
            if (term != null)
            {
                term.Title = updatedTerm.Title;
                term.StartDate = updatedTerm.StartDate;
                term.EndDate = updatedTerm.EndDate;
                term.CourseCount = updatedTerm.CourseCount;
                term.MaxCourseCount = updatedTerm.MaxCourseCount;
                await _database.UpdateAsync(term);
            }
        }

        public async Task UpdateCourse(int id, Course updatedCourse)
        {
            await Init();
            var course = await GetCourseById(id);
            if (course != null)
            {
                course.Title = updatedCourse.Title;
                course.InstructorName = updatedCourse.InstructorName;
                course.InstructorEmail = updatedCourse.InstructorEmail;
                course.InstructorPhone = updatedCourse.InstructorPhone;
                await _database.UpdateAsync(course);
            }
        }
    }
}