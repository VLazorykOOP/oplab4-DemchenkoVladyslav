using Cursova.DBModels;
using Cursova.DisplayModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace Cursova
{
    public partial class MainWindow : Window
    {
        static SchoolDbContext _schoolDbContext;
        public MainWindow()
        {
            InitializeComponent();
            HideAllStackPanels();
            _schoolDbContext = new SchoolDbContext();
            _schoolDbContext.Database.EnsureCreated();
            selectedEntity.ItemsSource = _schoolDbContext.Model.GetEntityTypes()
                .Select(x => x.GetTableName())
                .Distinct()
                .ToList();
            selectedEntity.SelectedIndex = 0;
            if (_schoolDbContext.Schedules.Count() == 0)
            {
                List<string> days = (new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" }).ToList();
                foreach (string day in days)
                {
                    for (int i = 1; i <= 6; i++)
                        _schoolDbContext.Schedules.Add(new Schedule() { Day = day, Order = i });
                }
                _schoolDbContext.SaveChanges();
            }
        }
        private void selectedEntity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HideAllStackPanels();
            switch (selectedEntity.SelectedValue)
            {
                case "LessonTypes":
                    PrepareLessonTypePanel();
                    break;
                case "Reasons":
                    PrepareReasonPanel();
                    break;
                case "Teachers":
                    PrepareTeacherPanel();
                    break;
                case "Students":
                    PrepareStudentPanel();
                    break;
                case "Disciplines":
                    PrepareDisciplinePanel();
                    break;
                case "Schedules":
                    PrepareSchedulePanel();
                    break;
                case "Lessons":
                    PrepareLessonPanel();
                    break;
                case "Skips":
                    PrepareSkipPanel();
                    break;
            }
        }
        void HideAllStackPanels()
        {
            teacherPanel.Visibility = Visibility.Hidden;
            studentPanel.Visibility = Visibility.Hidden;
            skipPanel.Visibility = Visibility.Hidden;
            schedulePanel.Visibility = Visibility.Hidden;
            reasonPanel.Visibility = Visibility.Hidden;
            lessonTypePanel.Visibility = Visibility.Hidden;
            lessonPanel.Visibility = Visibility.Hidden;
            disciplinePanel.Visibility = Visibility.Hidden;
        }
        void PrepareTeacherPanel()
        {
            teacherPanel.Visibility = Visibility.Visible;
            TeacherId.Text = "";
            TeacherFullName.Text = "";
            teachersList.ItemsSource = _schoolDbContext.Teachers.Select(x => new DisplayTeacher() { Id = x.Id, FullName = x.FullName }).ToList();
            teachersList.SelectedIndex = 0;
        }
        void PrepareReasonPanel()
        {
            reasonPanel.Visibility = Visibility.Visible;
            ReasonId.Text = "";
            ReasonName.Text = "";
            reasonsList.ItemsSource = _schoolDbContext.Reasons.Select(x => new DisplayReason() { Id = x.Id, Name = x.Name }).ToList();
            reasonsList.SelectedIndex = 0;
        }
        void PrepareLessonTypePanel()
        {
            LessonTypeName.Text = "";
            lessonTypePanel.Visibility = Visibility.Visible;
            lessonTypesList.ItemsSource = _schoolDbContext.LessonTypes.Select(x => new DisplayLessonType() { Id = x.Id, Name = x.Name }).ToList();
            lessonTypesList.SelectedIndex = 0;
        }
        void PrepareStudentPanel()
        {
            studentPanel.Visibility = Visibility.Visible;
            StudentId.Text = "";
            StudentFullName.Text = "";
            studentsList.ItemsSource = _schoolDbContext.Students.Select(x => new DisplayStudent() { Id = x.Id, FullName = x.FullName }).ToList();
            studentsList.SelectedIndex = 0;
        }
        void PrepareDisciplinePanel()
        {
            disciplinePanel.Visibility = Visibility.Visible;
            DisciplineId.Text = "";
            DisciplineName.Text = "";
            disciplinesList.ItemsSource = _schoolDbContext.Disciplines.Select(x => new DisplayDiscipline() { Id = x.Id, Name = x.Name }).ToList();
            disciplinesList.SelectedIndex = 0;
        }
        void PrepareSchedulePanel()
        {
            schedulePanel.Visibility = Visibility.Visible;
            ScheduleId.Text = "";
            ScheduleDay.SelectedIndex = 0;
            ScheduleOrder.Text = "";
            schedulesList.ItemsSource = _schoolDbContext.Schedules.Select(x => new DisplaySchedule() { Id = x.Id, Day = x.Day, Order = x.Order }).ToList();
            schedulesList.SelectedIndex = 0;
        }
        void PrepareLessonPanel()
        {
            lessonPanel.Visibility = Visibility.Visible;
            LessonId.Text = "";
            LessonScheduleDay.Text = "";
            LessonScheduleOrder.Text = "";
            LessonRoom.Text = "";
            LessonDiscipline.ItemsSource = _schoolDbContext.Disciplines.Select(x => x.Name).ToList();
            LessonDiscipline.SelectedIndex = 0;
            LessonSchedule.ItemsSource = _schoolDbContext.Schedules.Select(x => x.Id.ToString() + " " + x.Day + " " + x.Order).ToList();
            LessonSchedule.SelectedIndex = 0;
            LessonTeacher.ItemsSource = _schoolDbContext.Teachers.Select(x => x.FullName).ToList();
            LessonTeacher.SelectedIndex = 0;
            LessonLessonType.ItemsSource = _schoolDbContext.LessonTypes.Select(x => x.Name).ToList();
            LessonLessonType.SelectedIndex = 0;
            lessonsList.ItemsSource = _schoolDbContext.Lessons.Include(x => x.Discipline).Include(x => x.LessonType).Include(x => x.Teacher).Include(x => x.Schedule).Select(x => new DisplayLesson()
            {
                Id = x.Id,
                Discipline = x.Discipline.Name,
                Order = x.Schedule.Order.ToString(),
                Room = x.Room,
                Day = x.Schedule.Day,
                Teacher = x.Teacher.FullName,
                LessonType = x.LessonType.Name
            }).ToList();
            lessonsList.SelectedIndex = 0;
        }
        void PrepareSkipPanel()
        {
            skipPanel.Visibility = Visibility.Visible;
            SkipLesson.ItemsSource = _schoolDbContext.Lessons.Select(x => x.Id).ToList();
            SkipReason.ItemsSource = _schoolDbContext.Reasons.Select(x => x.Name).ToList();
            SkipStudent.ItemsSource = _schoolDbContext.Students.Select(x => x.FullName).ToList();
            SkipId.Text = "";
            SkipStudent.SelectedIndex = 0;
            SkipLesson.SelectedIndex = 0;
            SkipReason.SelectedIndex = 0;
            SkipDate.Text = DateTime.Now.Date.ToString();
            skipsList.ItemsSource = _schoolDbContext.Skips.Include(x => x.Reason).Include(x => x.Student).Select(x => new DisplaySkip()
            {
                Id = x.Id,
                LessonId = x.LessonId.ToString(),
                Reason = x.Reason.Name,
                Date = x.Date,
                Student = x.Student.FullName
            }).ToList();
            skipsList.SelectedIndex = 0;
        }
        private void teachersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((teachersList.SelectedItem as DisplayTeacher) != null)
            {
                TeacherId.Text = (teachersList.SelectedItem as DisplayTeacher).Id.ToString();
                TeacherFullName.Text = (teachersList.SelectedItem as DisplayTeacher).FullName;
            }
        }
        private void reasonsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((reasonsList.SelectedItem as DisplayReason) != null)
            {
                ReasonId.Text = (reasonsList.SelectedItem as DisplayReason).Id.ToString();
                ReasonName.Text = (reasonsList.SelectedItem as DisplayReason).Name;
            }
        }
        private void studentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((studentsList.SelectedItem as DisplayStudent) != null)
            {
                StudentId.Text = (studentsList.SelectedItem as DisplayStudent).Id.ToString();
                StudentFullName.Text = (studentsList.SelectedItem as DisplayStudent).FullName;
            }
        }
        private void disciplinesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((disciplinesList.SelectedItem as DisplayDiscipline) != null)
            {
                DisciplineId.Text = (disciplinesList.SelectedItem as DisplayDiscipline).Id.ToString();
                DisciplineName.Text = (disciplinesList.SelectedItem as DisplayDiscipline).Name;
            }
        }
        private void schedulesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((schedulesList.SelectedItem as DisplaySchedule) != null)
            {
                ScheduleId.Text = (schedulesList.SelectedItem as DisplaySchedule).Id.ToString();
                ScheduleDay.SelectedItem = (schedulesList.SelectedItem as DisplaySchedule).Day;
                ScheduleOrder.Text = (schedulesList.SelectedItem as DisplaySchedule).Order.ToString();
            }
        }
        private void lessonTypesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((lessonTypesList.SelectedItem as DisplayLessonType) != null)
            {
                LessonTypeId.Text = (lessonTypesList.SelectedItem as DisplayLessonType).Id.ToString();
                LessonTypeName.Text = (lessonTypesList.SelectedItem as DisplayLessonType).Name;
            }
        }
        private void lessonsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((lessonsList.SelectedItem as DisplayLesson) != null)
            {
                Lesson l = _schoolDbContext.Lessons.Include(x => x.Schedule).Include(x => x.Teacher).Include(x => x.Discipline).Include(x => x.LessonType).First(x => x.Id == (lessonsList.SelectedItem as DisplayLesson).Id);
                LessonId.Text = l.Id.ToString();
                LessonSchedule.SelectedItem = LessonSchedule.ItemsSource.OfType<string>().FirstOrDefault(x => x.Contains(l.Schedule.Day + " " + l.Schedule.Order));
                LessonScheduleDay.Text = l.Schedule.Day;
                LessonScheduleOrder.Text = l.Schedule.Order.ToString();
                LessonRoom.Text = l.Room;
                LessonDiscipline.SelectedValue = l.Discipline.Name;
                LessonTeacher.SelectedValue = l.Teacher.FullName;
            }
        }
        private void skipsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((skipsList.SelectedItem as DisplaySkip) != null)
            {
                Skip s = _schoolDbContext.Skips.First(x => x.Id == (skipsList.SelectedItem as DisplaySkip).Id);
                SkipId.Text = s.Id.ToString();
                SkipStudent.SelectedItem = _schoolDbContext.Students.First(x => x.Id == s.StudentId).FullName;
                SkipReason.SelectedItem = _schoolDbContext.Reasons.First(x => x.Id == s.ReasonId).Name;
                SkipLesson.SelectedItem = s.LessonId.ToString();
                SkipDate.Text = s.Date.ToString();
            }
        }
        private void AddTeacher_Click(object sender, RoutedEventArgs e)
        {
            if (!_schoolDbContext.Teachers.Any(x => x.FullName == TeacherFullName.Text) && !string.IsNullOrEmpty(TeacherFullName.Text))
            {
                Teacher t = new Teacher() { FullName = TeacherFullName.Text };
                _schoolDbContext.Teachers.Add(t);
                _schoolDbContext.SaveChanges();
                _schoolDbContext.Entry(t).State = EntityState.Detached;
                teachersList.ItemsSource = _schoolDbContext.Teachers.Select(x => new DisplayTeacher() { Id = x.Id, FullName = x.FullName }).ToList();
            }
        }
        private void UpdateTeacher_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TeacherId.Text) && _schoolDbContext.Teachers.Any(x => x.Id == int.Parse(TeacherId.Text)) && !string.IsNullOrEmpty(TeacherFullName.Text))
            {
                Teacher t = _schoolDbContext.Teachers.First(x => x.Id == int.Parse(TeacherId.Text));
                t.FullName = TeacherFullName.Text;
                _schoolDbContext.Teachers.Update(new Teacher() { Id = int.Parse(TeacherId.Text), FullName = TeacherFullName.Text });
                _schoolDbContext.SaveChanges();
                _schoolDbContext.Entry(t).State = EntityState.Detached;
                teachersList.ItemsSource = _schoolDbContext.Teachers.Select(x => new DisplayTeacher() { Id = x.Id, FullName = x.FullName }).ToList();
            }
        }
        private void DeleteTeacher_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TeacherId.Text) && _schoolDbContext.Teachers.Any(x => x.Id == int.Parse(TeacherId.Text)))
            {
                _schoolDbContext.Teachers.Remove(_schoolDbContext.Teachers.First(x => x.Id == int.Parse(TeacherId.Text)));
                _schoolDbContext.SaveChanges();
                teachersList.ItemsSource = _schoolDbContext.Teachers.Select(x => new DisplayTeacher() { Id = x.Id, FullName = x.FullName }).ToList();
            }
        }
        private void AddReason_Click(object sender, RoutedEventArgs e)
        {
            if (!_schoolDbContext.Reasons.Any(x => x.Name == ReasonName.Text) && !string.IsNullOrEmpty(ReasonName.Text))
            {
                Reason r = new Reason() { Name = ReasonName.Text };
                _schoolDbContext.Reasons.Add(r);
                _schoolDbContext.SaveChanges();
                _schoolDbContext.Entry(r).State = EntityState.Detached;
                reasonsList.ItemsSource = _schoolDbContext.Reasons.Select(x => new DisplayReason() { Id = x.Id, Name = x.Name }).ToList();
            }
        }
        private void UpdateReason_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ReasonId.Text) && _schoolDbContext.Reasons.Any(x => x.Id == int.Parse(ReasonId.Text)) && !string.IsNullOrEmpty(ReasonName.Text))
            {
                Reason r = _schoolDbContext.Reasons.First(x => x.Id == int.Parse(ReasonId.Text));
                r.Name = ReasonName.Text;
                _schoolDbContext.Reasons.Update(r);
                _schoolDbContext.SaveChanges();
                _schoolDbContext.Entry(r).State = EntityState.Detached;
                reasonsList.ItemsSource = _schoolDbContext.Reasons.Select(x => new DisplayReason() { Id = x.Id, Name = x.Name }).ToList();
            }
        }
        private void DeleteReason_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ReasonId.Text) && _schoolDbContext.Reasons.Any(x => x.Id == int.Parse(ReasonId.Text)))
            {
                _schoolDbContext.Reasons.Remove(_schoolDbContext.Reasons.First(x => x.Id == int.Parse(ReasonId.Text)));
                _schoolDbContext.SaveChanges();
                reasonsList.ItemsSource = _schoolDbContext.Reasons.Select(x => new DisplayReason() { Id = x.Id, Name = x.Name }).ToList();
            }
        }
        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            if (!_schoolDbContext.Students.Any(x => x.FullName == StudentFullName.Text) && !string.IsNullOrEmpty(StudentFullName.Text))
            {
                Student s = new Student() { FullName = StudentFullName.Text };
                _schoolDbContext.Students.Add(s);
                _schoolDbContext.SaveChanges();
                _schoolDbContext.Entry(s).State = EntityState.Detached;
                studentsList.ItemsSource = _schoolDbContext.Students.Select(x => new DisplayStudent() { Id = x.Id, FullName = x.FullName }).ToList();
            }
        }
        private void UpdateStudent_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(StudentId.Text) && _schoolDbContext.Students.Any(x => x.Id == int.Parse(StudentId.Text)) && !string.IsNullOrEmpty(StudentFullName.Text))
            {
                Student s = _schoolDbContext.Students.First(x => x.Id == int.Parse(StudentId.Text));
                s.FullName = StudentFullName.Text;
                _schoolDbContext.Students.Update(s);
                _schoolDbContext.SaveChanges();
                _schoolDbContext.Entry(s).State = EntityState.Detached;
                studentsList.ItemsSource = _schoolDbContext.Students.Select(x => new DisplayStudent() { Id = x.Id, FullName = x.FullName }).ToList();
            }
        }
        private void DeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(StudentId.Text) && _schoolDbContext.Students.Any(x => x.Id == int.Parse(StudentId.Text)))
            {
                _schoolDbContext.Students.Remove(_schoolDbContext.Students.First(x => x.Id == int.Parse(StudentId.Text)));
                _schoolDbContext.SaveChanges();
                studentsList.ItemsSource = _schoolDbContext.Students.Select(x => new DisplayStudent() { Id = x.Id, FullName = x.FullName }).ToList();
            }
        }
        private void AddDiscipline_Click(object sender, RoutedEventArgs e)
        {
            if (!_schoolDbContext.Disciplines.Any(x => x.Name == DisciplineName.Text) && !string.IsNullOrEmpty(DisciplineName.Text))
            {
                Discipline d = new Discipline() { Name = DisciplineName.Text };
                _schoolDbContext.Disciplines.Add(d);
                _schoolDbContext.SaveChanges();
                _schoolDbContext.Entry(d).State = EntityState.Detached;
                disciplinesList.ItemsSource = _schoolDbContext.Disciplines.Select(x => new DisplayDiscipline() { Id = x.Id, Name = x.Name }).ToList();
            }
        }
        private void UpdateDiscipline_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(DisciplineId.Text) && !string.IsNullOrEmpty(DisciplineName.Text))
            {
                Discipline d = _schoolDbContext.Disciplines.AsNoTracking().First(x => x.Id == int.Parse(DisciplineId.Text));
                d.Name = DisciplineName.Text;
                _schoolDbContext.Disciplines.Update(d);
                _schoolDbContext.SaveChanges();
                _schoolDbContext.Entry(d).State = EntityState.Detached;
                disciplinesList.ItemsSource = _schoolDbContext.Disciplines.Select(x => new DisplayDiscipline() { Id = x.Id, Name = x.Name }).ToList();
            }
        }
        private void DeleteDiscipline_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(DisciplineId.Text) && _schoolDbContext.Disciplines.Any(x => x.Id == int.Parse(DisciplineId.Text)))
            {
                _schoolDbContext.Disciplines.Remove(_schoolDbContext.Disciplines.First(x => x.Id == int.Parse(DisciplineId.Text)));
                _schoolDbContext.SaveChanges();
                disciplinesList.ItemsSource = _schoolDbContext.Disciplines.Select(x => new DisplayDiscipline() { Id = x.Id, Name = x.Name }).ToList();
            }
        }
        private void AddSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (!_schoolDbContext.Schedules.Any(x => x.Day == ScheduleDay.Text && x.Order == int.Parse(ScheduleOrder.Text)) && !string.IsNullOrEmpty(ScheduleOrder.Text))
            {
                Schedule s = new Schedule() { Day = ScheduleDay.Text, Order = int.Parse(ScheduleOrder.Text) };
                _schoolDbContext.Schedules.Add(s);
                _schoolDbContext.SaveChanges();
                _schoolDbContext.Entry(s).State = EntityState.Detached;
                schedulesList.ItemsSource = _schoolDbContext.Schedules.Select(x => new DisplaySchedule() { Id = x.Id, Day = x.Day, Order = x.Order }).ToList();
            }
        }
        private void AddLessonType_Click(object sender, RoutedEventArgs e)
        {
            if (!_schoolDbContext.LessonTypes.Any(x => x.Name == LessonTypeName.Text) && !string.IsNullOrEmpty(LessonTypeName.Text))
            {
                LessonType lt = new LessonType() { Name = LessonTypeName.Text };
                _schoolDbContext.LessonTypes.Add(lt);
                _schoolDbContext.SaveChanges();
                _schoolDbContext.Entry(lt).State = EntityState.Detached;
                lessonTypesList.ItemsSource = _schoolDbContext.LessonTypes.Select(x => new DisplayLessonType() { Id = x.Id, Name = x.Name }).ToList();
            }
        }
        private void UpdateLessonType_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(LessonTypeId.Text) && _schoolDbContext.LessonTypes.Any(x => x.Id == int.Parse(LessonTypeId.Text)))
            {
                LessonType lt = _schoolDbContext.LessonTypes.First(x => x.Id == int.Parse(LessonTypeId.Text));
                lt.Name = LessonTypeName.Text;
                _schoolDbContext.LessonTypes.Update(lt);
                _schoolDbContext.SaveChanges();
                _schoolDbContext.Entry(lt).State = EntityState.Detached;
                lessonTypesList.ItemsSource = _schoolDbContext.LessonTypes.Select(x => new DisplayLessonType() { Id = x.Id, Name = x.Name }).ToList();
            }
        }
        private void DeleteLessonType_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(LessonTypeId.Text) && _schoolDbContext.LessonTypes.Any(x => x.Id == int.Parse(LessonTypeId.Text)))
            {
                _schoolDbContext.LessonTypes.Remove(_schoolDbContext.LessonTypes.First(x => x.Id == int.Parse(LessonTypeId.Text)));
                _schoolDbContext.SaveChanges();
                lessonTypesList.ItemsSource = _schoolDbContext.LessonTypes.Select(x => new DisplayLessonType() { Id = x.Id, Name = x.Name }).ToList();
            }
        }

        private void AddLesson_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int schId = int.Parse(LessonSchedule.SelectedItem.ToString().Split(" ")[0]);
                if (!_schoolDbContext.Lessons.Any(x => x.ScheduleId == schId && x.Room == LessonRoom.Text))
                {
                    Lesson l = new Lesson()
                    {
                        LessonTypeId = _schoolDbContext.LessonTypes.First(x => x.Name == LessonLessonType.SelectedItem.ToString()).Id,
                        Room = LessonRoom.Text,
                        ScheduleId = int.Parse(LessonSchedule.SelectedItem.ToString().Split(" ")[0]),
                        TeacherId = _schoolDbContext.Teachers.First(x => x.FullName == LessonTeacher.SelectedItem.ToString()).Id,
                        DisciplineId = _schoolDbContext.Disciplines.First(x => x.Name == LessonDiscipline.SelectedItem.ToString()).Id,
                    };
                    _schoolDbContext.Lessons.Add(l);
                    _schoolDbContext.SaveChanges();
                    _schoolDbContext.Entry(l).State = EntityState.Detached;
                    lessonsList.ItemsSource = _schoolDbContext.Lessons.Include(x => x.Discipline).Include(x => x.LessonType).Include(x => x.Teacher).Include(x => x.Schedule).Select(x => new DisplayLesson()
                    {
                        Id = x.Id,
                        Discipline = x.Discipline.Name,
                        Order = x.Schedule.Order.ToString(),
                        Room = x.Room,
                        Day = x.Schedule.Day,
                        Teacher = x.Teacher.FullName,
                        LessonType = x.LessonType.Name
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void UpdateLesson_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(LessonId.Text) &&
                _schoolDbContext.Lessons.Any(x => x.Id == int.Parse(LessonId.Text) &&
                !string.IsNullOrEmpty(LessonRoom.Text)))
            {
                Lesson l = _schoolDbContext.Lessons.First(x => x.Id == int.Parse(LessonId.Text));
                int schId = int.Parse(LessonSchedule.SelectedItem.ToString().Split(" ")[0]);

                l.LessonType = _schoolDbContext.LessonTypes.First(x => x.Name == LessonLessonType.SelectedItem.ToString());
                l.Room = LessonRoom.Text;
                l.ScheduleId = schId;
                l.Teacher = _schoolDbContext.Teachers.First(x => x.FullName == LessonTeacher.SelectedItem.ToString());
                l.Discipline = _schoolDbContext.Disciplines.First(x => x.Name == LessonDiscipline.SelectedItem.ToString());
                Lesson check = _schoolDbContext.Lessons.FirstOrDefault(x => x.ScheduleId == l.ScheduleId && x.Room == l.Room);
                if (check != null)
                {
                    if (l.Id == check.Id)
                    {
                        _schoolDbContext.Lessons.Update(l);
                        _schoolDbContext.SaveChanges();
                    }
                }
                else
                {
                    _schoolDbContext.Lessons.Update(l);
                    _schoolDbContext.SaveChanges();
                }
                _schoolDbContext.Entry(l.Teacher).State = EntityState.Detached;
                _schoolDbContext.Entry(l.Discipline).State = EntityState.Detached;
                _schoolDbContext.Entry(l.LessonType).State = EntityState.Detached;
                _schoolDbContext.Entry(l).State = EntityState.Detached;
                lessonsList.ItemsSource = _schoolDbContext.Lessons.Include(x => x.Discipline).Include(x => x.LessonType).Include(x => x.Teacher).Include(x => x.Schedule).Select(x => new DisplayLesson()
                {
                    Id = x.Id,
                    Discipline = x.Discipline.Name,
                    Order = x.Schedule.Order.ToString(),
                    Room = x.Room,
                    Day = x.Schedule.Day,
                    Teacher = x.Teacher.FullName,
                    LessonType = x.LessonType.Name
                }).ToList();
            }
        }
        private void DeleteLesson_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(LessonId.Text) && _schoolDbContext.Lessons.Any(x => x.Id.ToString() == LessonId.Text))
            {
                _schoolDbContext.Lessons.Remove(_schoolDbContext.Lessons.First(x => x.Id == int.Parse(LessonId.Text)));
                _schoolDbContext.SaveChanges();
                lessonsList.ItemsSource = _schoolDbContext.Lessons.Include(x => x.Discipline).Include(x => x.LessonType).Include(x => x.Teacher).Include(x => x.Schedule).Select(x => new DisplayLesson()
                {
                    Id = x.Id,
                    Discipline = x.Discipline.Name,
                    Order = x.Schedule.Order.ToString(),
                    Room = x.Room,
                    Day = x.Schedule.Day,
                    Teacher = x.Teacher.FullName,
                    LessonType = x.LessonType.Name
                }).ToList();
            }
        }
        private void AddSkip_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Skip s = new Skip()
                {
                    Date = ((DateTime)SkipDate.SelectedDate).Date,
                    StudentId = _schoolDbContext.Students.First(x => x.FullName == SkipStudent.SelectedItem.ToString()).Id,
                    LessonId = int.Parse(SkipLesson.SelectedItem.ToString()),
                    ReasonId = _schoolDbContext.Reasons.First(x => x.Name == SkipReason.SelectedItem.ToString()).Id
                };
                if (s.Date.DayOfWeek == (DayOfWeek)Enum.Parse(typeof(DayOfWeek), _schoolDbContext.Lessons.Include(x => x.Schedule).First(x => x.Id == s.LessonId).Schedule.Day, true))
                {
                    if (!_schoolDbContext.Skips.Any(x => x.LessonId == s.LessonId && x.StudentId == s.StudentId))
                    {
                        _schoolDbContext.Skips.Add(s);
                        _schoolDbContext.SaveChanges();
                        _schoolDbContext.Entry(s).State = EntityState.Detached;
                    }
                    else
                        MessageBox.Show("The skip is already in the database");
                }
                else
                {
                    MessageBox.Show("Wrong day of week in picked date");
                }
                skipsList.ItemsSource = _schoolDbContext.Skips.Include(x => x.Reason).Include(x => x.Student).Select(x => new DisplaySkip()
                {
                    Id = x.Id,
                    LessonId = x.LessonId.ToString(),
                    Reason = x.Reason.Name,
                    Date = x.Date,
                    Student = x.Student.FullName
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void UpdateSkip_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SkipId.Text) &&
               _schoolDbContext.Skips.Any(x => x.Id == int.Parse(SkipId.Text)))
            {
                Skip s = _schoolDbContext.Skips.First(x => x.Id == int.Parse(SkipId.Text));
                s.Lesson = _schoolDbContext.Lessons.First(x => x.Id.ToString() == SkipLesson.SelectedItem.ToString());
                s.Date = ((DateTime)SkipDate.SelectedDate).Date;
                s.Student = _schoolDbContext.Students.First(x => x.FullName == SkipStudent.SelectedItem.ToString());
                s.Reason = _schoolDbContext.Reasons.First(x => x.Name == SkipReason.SelectedItem.ToString());
                if (s.Date.DayOfWeek == (DayOfWeek)Enum.Parse(typeof(DayOfWeek), _schoolDbContext.Lessons.Include(x => x.Schedule).First(x => x.Id == s.LessonId).Schedule.Day, true))
                {
                    if (!_schoolDbContext.Skips.Any(x => x.LessonId == s.LessonId && x.StudentId == s.StudentId) ||
                        _schoolDbContext.Skips.First(x => x.LessonId == s.LessonId && x.StudentId == s.StudentId).Id == s.Id)
                    {
                        _schoolDbContext.Skips.Update(s);
                        _schoolDbContext.SaveChanges();
                    }
                }
                _schoolDbContext.Entry(s.Student).State = EntityState.Detached;
                _schoolDbContext.Entry(s.Reason).State = EntityState.Detached;
                _schoolDbContext.Entry(s.Lesson).State = EntityState.Detached;
                _schoolDbContext.Entry(s).State = EntityState.Detached;
                skipsList.ItemsSource = _schoolDbContext.Skips.Include(x => x.Reason).Include(x => x.Student).Select(x => new DisplaySkip()
                {
                    Id = x.Id,
                    LessonId = x.LessonId.ToString(),
                    Reason = x.Reason.Name,
                    Date = x.Date,
                    Student = x.Student.FullName
                }).ToList();
            }
        }
        private void DeleteSkip_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SkipId.Text) && _schoolDbContext.Skips.Any(x => x.Id.ToString() == SkipId.Text))
            {
                _schoolDbContext.Skips.Remove(_schoolDbContext.Skips.First(x => x.Id == int.Parse(SkipId.Text)));
                _schoolDbContext.SaveChanges();
                skipsList.ItemsSource = _schoolDbContext.Skips.Include(x => x.Reason).Include(x => x.Student).Select(x => new DisplaySkip()
                {
                    Id = x.Id,
                    LessonId = x.LessonId.ToString(),
                    Reason = x.Reason.Name,
                    Date = x.Date,
                    Student = x.Student.FullName
                }).ToList();
            }
        }
        private void LessonSchedule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string findId = LessonSchedule.SelectedItem.ToString().Split(" ")[0];
            LessonScheduleDay.Text = _schoolDbContext.Schedules.First(x => x.Id.ToString() == findId).Day;
            LessonScheduleOrder.Text = _schoolDbContext.Schedules.First(x => x.Id.ToString() == findId).Order.ToString();
        }
    }
}
