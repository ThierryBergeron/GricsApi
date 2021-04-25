using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleApi.Models;
using SimpleApi.Models.Repositories;
using SimpleApi.Services;

namespace SimpleApi.Helpers
{
    public class MockDataGenerator : IMockDataGenerator
    {
        private readonly ISchoolRepository _schoolRepo;
        private readonly ITeacherRepository _teacherRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly IReportCardRepository _reportCardRepo;

        public MockDataGenerator(ISchoolRepository schoolRepository, ITeacherRepository teacherRepository, IStudentRepository studentRepository, IReportCardRepository reportCardRepository)
        {
            _schoolRepo = schoolRepository;
            _teacherRepo = teacherRepository;
            _studentRepo = studentRepository;
            _reportCardRepo = reportCardRepository;
        }

        async public Task<bool> GenerateSchoolsAsync()
        {
            string[] schoolNames = { "henri-bourassa", "outremont", "vincent massey", "marie-anne", "jean-de-brebeuf", "westmount" };
            foreach (var name in schoolNames)
            {
                var number = new Random().Next(1000, 9999);
                var telephone = $"514-861-{number}";
                var school = new School { Name = name, TelephoneNumber = telephone };
                await _schoolRepo.AddAsync(school);
            }
            return true;
        }

        async public Task<bool> GenerateStudentsAsync()
        {
            int numberOfStudentsGeneratedPerTeacher = 28;
            var teachers = await _teacherRepo.GetAllAsync();

            // generate a class for each teacher
            foreach (var teacher in teachers)
            {
                for (var i = 0; i < numberOfStudentsGeneratedPerTeacher; i++)
                {
                    // generate a student
                    var tuple = GenerateNameAndSex();
                    var initialDate = new DateTime(2012, 9, 30);

                    var dob = GenerateRandomDob(initialDate);
                    var student = new Student
                    {
                        FirstName = tuple.FirstName,
                        LastName = tuple.LastName,
                        Sex = tuple.Sex,
                        SchoolId = teacher.SchoolId,
                        TeacherId = teacher.Id,
                        Dob = dob.ToUniversalTime()
                    };
                    var newStudent = await _studentRepo.AddAsync(student);

                    // generate a reportcard for the student
                    var reportCard = GenerateRandomReportCard(teacher.SchoolId, teacher.Id, newStudent.Id);
                    await _reportCardRepo.AddAsync(reportCard);
                }
            }
            return true;
        }

        async public Task<bool> GenerateTeachersAsync()
        {
            int numberOfTeacherGeneratedPerSchool = 6;
            var schools = await _schoolRepo.GetAllAsync();
            foreach (var school in schools)
            {
                for (var i = 0; i < numberOfTeacherGeneratedPerSchool; i++)
                {
                    var tuple = GenerateNameAndSex();
                    var teacher = new Teacher { FirstName = tuple.FirstName, LastName = tuple.LastName, Sex = tuple.Sex, SchoolId = school.Id };
                    await _teacherRepo.AddAsync(teacher);
                }
            }
            return true;
        }

        private (string FirstName, string LastName, string Sex) GenerateNameAndSex()
        {
            var maleFemale = new string[] { "M", "F" };
            var sexInt = new Random().Next(0, 2);

            var firstName = GenerateFirstName(sexInt);
            var lastName = GenerateLastName();

            return (firstName, lastName, maleFemale[sexInt]);
        }


        private string GenerateFirstName(int sex)
        {
            // 50 prenom feminin les plus populaires garcons et filles 2021
            var firstnameF = new string[] { "olivia", "alice", "emma", "charlie", "charlotte", "lea", "florence", "livia", "romy", "clara", "zoe", "beatrice", "chloe", "rosalie", "rose", "victoria", "mia", "juliette", "eva", "mila", "sofia", "maeva", "jade", "raphaelle", "jeanne", "julia", "ophelie", "amelia", "elizabeth", "camille", "leonie", "elena", "lexie", "flavie", "adele", "laurence", "sophia", "alicia", "eleonore", "anna", "oceane", "ellie", "gabrielle", "simone", "sarah", "elodie", "jasmine", "maelie", "lily", "billie" };
            var firstnameM = new string[] { "liam", "william", "noah", "thomas", "leo", "nathan", "edouard", "logan", "jacob", "arthur", "felix", "emile", "louis", "raphael", "arnaud", "charles", "alexis", "victor", "james", "theo", "benjamin", "samuel", "elliot", "adam", "antoine", "olivier", "nolan", "jayden", "henri", "milan", "gabriel", "zack", "jules", "laurent", "ethan", "lucas", "loic", "jackson", "theodore", "matheo", "eli", "eloi", "mathis", "xavier", "mayson", "zachary", "jake", "hubert", "ludovic", "leonard" };
            var index = new Random().Next(0, firstnameF.Length);
            return sex == 0 ? firstnameM[index] : firstnameF[index];
        }

        private string GenerateLastName()
        {
            // 100 noms de familles les plus populaires en ordre de frequences au quebec
            var lastnames = new string[] { "tremblay", "gagnon", "roy", "côté", "bouchard", "gauthier", "morin", "lavoie", "fortin", "gagné", "ouellet", "pelletier", "bélanger", "lévesque", "bergeron", "leblanc", "paquette", "girard", "simard", "boucher", "caron", "beaulieu", "cloutier", "dubé", "poirier", "fournier", "lapointe", "leclerc", "lefebvre", "poulin", "thibault", "st-pierre", "nadeau", "martin", "landry", "martel", "bédard", "grenier", "lessard", "bernier", "richard", "michaud", "hébert", "desjardins", "couture", "turcotte", "lachance", "parent", "blais", "gosselin", "savard", "proulx", "beaudoin", "demers", "perreault", "boudreau", "lemieux", "cyr", "perron", "dufour", "dion", "mercier", "bolduc", "bérubé", "boisvert", "langlois", "ménard", "therrien", "bilodeau", "plante", "blanchette", "dubois", "champagne", "paradis", "fortier", "arsenault", "dupuis", "gaudreault", "hamel", "houle", "villeneuve", "rousseau", "gravel", "thériault", "lemay", "robert", "allard", "deschênes", "giroux", "guay", "leduc", "boivin", "charbonneau", "lambert", "raymond", "vachon", "gilbert", "audet", "jean", "larouche" };
            var index = new Random().Next(0, lastnames.Length);
            return lastnames[index];
        }
        
        private DateTime GenerateRandomDob(DateTime initialDate, Random rnd)
        {
            var days = rnd.Next(0, 364);
            return initialDate.AddDays(days);
        }
        private DateTime GenerateRandomDob(DateTime initialDate)
        {
            var days = new Random().Next(0, 364);
            return initialDate.AddDays(days);
        }

        private int GenerateRandomGrade()
        {
            return new Random().Next(45, 99);
        }

        private List<string> GeneratePartialNameArray(string name)
        {
            List<string> partialNames = new List<string>();

            // front to back
            for (var i = 1; i <= name.Length; i++)
            {
                partialNames.Add(name.Substring(0, i));
            }

            // back to front
            //for (var i = name.Length - 1; i > 0; i--)
            //{
            //    partialNames.Add(name.Substring(i, name.Length - i));
            //}

            return partialNames;
        }

        private ReportCard GenerateRandomReportCard(string schoolId, string teacherId, string studentId)
        {
            return new ReportCard
            {
                Mathematique = GenerateRandomGrade(),
                Arts = GenerateRandomGrade(),
                Francais = GenerateRandomGrade(),
                Sciences = GenerateRandomGrade(),
                SchoolId = schoolId,
                TeacherId = teacherId,
                StudentId = studentId,
                Grade = 4
            };
        }
    }
}
