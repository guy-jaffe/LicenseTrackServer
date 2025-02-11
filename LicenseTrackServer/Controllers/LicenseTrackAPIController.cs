using LicenseTrackServer.DTO;
using LicenseTrackServer.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api")]
[ApiController]
public class LicenseTrackAPIController : ControllerBase
{
    //a variable to hold a reference to the db context!
    private LicenseTrackDbContext context;
    //a variable that hold a reference to web hosting interface (that provide information like the folder on which the server runs etc...)
    private IWebHostEnvironment webHostEnvironment;
    //Use dependency injection to get the db context and web host into the constructor
    public LicenseTrackAPIController(LicenseTrackDbContext context, IWebHostEnvironment env)
    {
        this.context = context;
        this.webHostEnvironment = env;
    }

    [HttpGet]
    [Route("TestServer")]
    public ActionResult<string> TestServer()
    {
        return Ok("Server Responded Successfully");
    }


    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginInfoDTO loginDto)
    {
        try
        {
            HttpContext.Session.Clear(); //Logout any previous login attempt

            //Get model user class from DB with matching email. 
            User? modelsUser = context.GetUser(loginDto.UserEmail);

            //Check if user exist for this email and if password match, if not return Access Denied (Error 403) 
            if (modelsUser == null || modelsUser.Pass != loginDto.UserPassword)
            {
                return Unauthorized();
            }

            UsersDTO dtoUser = new UsersDTO(modelsUser);
            //Login suceed! now mark login in session memory!
            HttpContext.Session.SetString("loggedInUser", modelsUser.Email);

            Student? student = context.GetStudent(modelsUser.Id);
            if (student != null)
            {
                //dtoUser = new StudentDto(student);
                return Ok(new StudentDto(student));
            }
            else
            {
                Teacher? teacher = context.GetTeacher(modelsUser.Id);
                if (teacher != null)
                {
                    //To be added!
                    //dtoUser = new TeacherDto(teacher);
                    return Ok(new TeacherDto(teacher));

                }
            }
            

            
            //dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.Id);
            return Ok(dtoUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }


    [HttpPost("studentRegister")]
    public IActionResult StudentRegister([FromBody] StudentDto userDto)
    {
        try
        {
            HttpContext.Session.Clear(); //Logout any previous login attempt

            //Create model user class
            Student modelsUser = userDto.GetModels();

            context.Students.Add(modelsUser);
            context.SaveChanges();

            //User was added!
            StudentDto dtoUser = new StudentDto(modelsUser);
            //dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.Id);
            return Ok(dtoUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPost("teacherRegister")]
    public IActionResult TeacherRegister([FromBody] TeacherDto userDto)
    {
        try
        {
            HttpContext.Session.Clear(); //Logout any previous login attempt

            //Create model user class
            Teacher modelsUser = userDto.GetModels();

            context.Teachers.Add(modelsUser);
            context.SaveChanges();

            //User was added!
            TeacherDto dtoUser = new TeacherDto(modelsUser);
            //dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.Id);
            return Ok(dtoUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPost("updateStudend")]
    public IActionResult UpdateStudent([FromBody] StudentDto userDto)
    {
        try
        {
            HttpContext.Session.Clear(); //Logout any previous login attempt

            //Create model user class
            Student modelsUser = userDto.GetModels();

            context.Students.Update(modelsUser);
            context.SaveChanges();

            //User was added!
            StudentDto dtoUser = new StudentDto(modelsUser);
            //dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.Id);
            return Ok(dtoUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }


    [HttpPost("updateTeacher")]
    public IActionResult UpdateTeacher([FromBody] TeacherDto userDto)
    {
        try
        {
            HttpContext.Session.Clear(); //Logout any previous login attempt

            //Create model user class
            Teacher modelsUser = userDto.GetModels();

            context.Teachers.Update(modelsUser);
            context.SaveChanges();

            //User was added!
            TeacherDto dtoUser = new TeacherDto(modelsUser);
            //dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.Id);
            return Ok(dtoUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpGet("GetTeachers")]
    public IActionResult GetTeachers()
    {
        try
        {
            //Check if user is logged in
            string? email = HttpContext.Session.GetString("loggedInUser");
            if (email == null)
            {
                return Unauthorized("User is not logged in");
            }
            User? user = context.GetUser(email);

            if (user == null) 
            {
                return Unauthorized("User does not exist!");
            }

            Student? student = context.GetStudent(user.Id);
            if (student == null)
            {
                return Unauthorized("User is not a student!");
            }


            //Create list of teacher
            List<Teacher> teachers = context.GetTeachers(user.City);
            //Return techrs as dto
            List<TeacherDto> list = new List<TeacherDto>();
            foreach (Teacher t in teachers) 
            {
                TeacherDto dto = new TeacherDto(t);
                list.Add(dto);
            }
            
            
            return Ok(list);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }


    [HttpPost("addLesson")]
    public IActionResult AddLesson([FromBody] LessonDto lessonDto)
    {
        try
        {
            //Check if user is logged in
            string? email = HttpContext.Session.GetString("loggedInUser");
            if (email == null)
            {
                return Unauthorized("User is not logged in");
            }
            User? user = context.GetUser(email);

            if (user == null)
            {
                return Unauthorized("User does not exist!");
            }
            //Create model lesson class
            Lesson modelLesson = lessonDto.GetModels();

            context.Lessons.Add(modelLesson);
            context.SaveChanges();

            //User was added!
            //LessonDto dtoLesson = new LessonDto(modelLesson);
            
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }



    //[HttpPost("getLessonSchedules")]
    //public IActionResult GetLessonSchedules([FromBody] int teacherId, int month, int year)
    //{
    //    try
    //    {
    //        // חישוב תאריך התחלה וסיום של החודש המבוקש
    //        var startDate = new DateOnly(year, month, 1);
    //        var endDate = startDate.AddMonths(1).AddDays(-1);

    //        // שליפת השיעורים של המורה בטווח התאריכים המבוקש
    //        var lessons = context.Lessons
    //            .Where(l => l.InstructorId == teacherId && l.LessonDate >= startDate && l.LessonDate <= endDate)
    //            .ToList();

    //        // המרת השיעורים לפורמט של LessonSchedule
    //        var lessonSchedules = lessons.Select(l => new LessonSchedule
    //        {
    //            LessonDate = l.LessonDate.Value,
    //            LessonTime = l.LessonTime.Value
    //        }).ToList();

    //        // מחזירים את התוצאה כ-Ok
    //        return Ok(lessonSchedules);
    //    }
    //    catch (Exception ex)
    //    {
    //        // אם קרתה שגיאה, מחזירים תשובת שגיאה
    //        return BadRequest(ex.Message);
    //    }
    //}

}

