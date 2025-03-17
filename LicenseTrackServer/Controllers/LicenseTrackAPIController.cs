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

            UsersDTO dtoUser = new UsersDTO(modelsUser, this.webHostEnvironment.WebRootPath);
            //Login suceed! now mark login in session memory!
            HttpContext.Session.SetString("loggedInUser", modelsUser.Email);

            Student? student = context.GetStudent(modelsUser.Id);
            if (student != null)
            {
                //dtoUser = new StudentDto(student);
                return Ok(new StudentDto(student, this.webHostEnvironment.WebRootPath));
            }
            else
            {
                Teacher? teacher = context.GetTeacher(modelsUser.Id);
                if (teacher != null)
                {
                    //To be added!
                    //dtoUser = new TeacherDto(teacher);
                    return Ok(new TeacherDto(teacher, this.webHostEnvironment.WebRootPath));

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
            StudentDto dtoUser = new StudentDto(modelsUser, this.webHostEnvironment.WebRootPath);
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
            TeacherDto dtoUser = new TeacherDto(modelsUser, this.webHostEnvironment.WebRootPath);
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
            StudentDto dtoUser = new StudentDto(modelsUser, this.webHostEnvironment.WebRootPath);
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
            TeacherDto dtoUser = new TeacherDto(modelsUser, this.webHostEnvironment.WebRootPath);
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
                TeacherDto dto = new TeacherDto(t, this.webHostEnvironment.WebRootPath);
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



    [HttpGet("getAvailableLessonSchedules")]
    public IActionResult GetAvailableLessonSchedules([FromQuery] int teacherId, [FromQuery] int month, [FromQuery] int year)
    {
        try
        {
            // טווח שעות העבודה של המורה
            var workStartTime = new TimeOnly(8, 0); // 08:00
            var workEndTime = new TimeOnly(19, 0);  // 19:00
            var lessonDuration = TimeSpan.FromMinutes(60); // אורך שיעור

            // יצירת טווח תאריכים לחודש המבוקש
            var startDate = new DateOnly(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            // שליפת כל השיעורים הקיימים של המורה בחודש זה
            var bookedLessons = context.Lessons
                .Where(l => l.InstructorId == teacherId && l.LessonDate >= startDate && l.LessonDate <= endDate)
                .Select(l => new { l.LessonDate, l.LessonTime })
                .ToList();

            // יצירת רשימת השעות הפנויות
            var availableSchedules = new List<LessonSchedule>();

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                for (var time = workStartTime; time < workEndTime; time = time.AddMinutes(lessonDuration.TotalMinutes))
                {
                    // בדיקה אם השעה כבר תפוסה
                    bool isBooked = bookedLessons.Any(l => l.LessonDate == date && l.LessonTime == time);

                    if (!isBooked)
                    {
                        availableSchedules.Add(new LessonSchedule
                        {
                            LessonDate = date,
                            LessonTime = time
                        });
                    }
                }
            }

            return Ok(availableSchedules);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("UploadProfileImage")]
    public async Task<IActionResult> UploadProfileImageAsync(IFormFile file)
    {
        //Check if who is logged in
        string? userEmail = HttpContext.Session.GetString("loggedInUser");
        if (string.IsNullOrEmpty(userEmail))
        {
            return Unauthorized("User is not logged in");
        }

        //Get model user class from DB with matching email. 
        User? user = context.GetUser(userEmail);
        //Clear the tracking of all objects to avoid double tracking
        context.ChangeTracker.Clear();

        if (user == null)
        {
            return Unauthorized("User is not found in the database");
        }


        //Read all files sent
        long imagesSize = 0;

        if (file.Length > 0)
        {
            //Check the file extention!
            string[] allowedExtentions = { ".png", ".jpg" };
            string extention = "";
            if (file.FileName.LastIndexOf(".") > 0)
            {
                extention = file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();
            }
            if (!allowedExtentions.Where(e => e == extention).Any())
            {
                //Extention is not supported
                return BadRequest("File sent with non supported extention");
            }

            //Build path in the web root (better to a specific folder under the web root
            string filePath = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{user.Id}{extention}";

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);

                if (IsImage(stream))
                {
                    imagesSize += stream.Length;
                }
                else
                {
                    //Delete the file if it is not supported!
                    System.IO.File.Delete(filePath);
                }

            }

        }

        UsersDTO dtoUser = new UsersDTO(user, this.webHostEnvironment.WebRootPath);
        dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.Id);
        return Ok(dtoUser);
    }

    //this function gets a file stream and check if it is an image
    private static bool IsImage(Stream stream)
    {
        stream.Seek(0, SeekOrigin.Begin);

        List<string> jpg = new List<string> { "FF", "D8" };
        List<string> bmp = new List<string> { "42", "4D" };
        List<string> gif = new List<string> { "47", "49", "46" };
        List<string> png = new List<string> { "89", "50", "4E", "47", "0D", "0A", "1A", "0A" };
        List<List<string>> imgTypes = new List<List<string>> { jpg, bmp, gif, png };

        List<string> bytesIterated = new List<string>();

        for (int i = 0; i < 8; i++)
        {
            string bit = stream.ReadByte().ToString("X2");
            bytesIterated.Add(bit);

            bool isImage = imgTypes.Any(img => !img.Except(bytesIterated).Any());
            if (isImage)
            {
                return true;
            }
        }

        return false;
    }

    //this function check which profile image exist and return the virtual path of it.
    //if it does not exist it returns the default profile image virtual path
    private string GetProfileImageVirtualPath(int userId)
    {
        string virtualPath = $"/profileImages/{userId}";
        string path = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{userId}.png";
        if (System.IO.File.Exists(path))
        {
            virtualPath += ".png";
        }
        else
        {
            path = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{userId}.jpg";
            if (System.IO.File.Exists(path))
            {
                virtualPath += ".jpg";
            }
            else
            {
                virtualPath = $"/profileImages/default.png";
            }
        }

        return virtualPath;
    }


    [HttpGet("GetFutureLessons")]
    public IActionResult GetFutureLessons()
    {
        try
        {
            // בדיקה אם המשתמש מחובר
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

            // אם המשתמש הוא סטודנט, נבצע חיפוש של שיעורים עתידיים
            Student? student = context.GetStudent(user.Id);
            if (student == null)
            {
                return Unauthorized("User is not a student!");
            }

            // חיפוש שיעורים עתידיים
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            List<Lesson> futureLessons = context.Lessons
                .Where(l => l.StudentId == student.Id && l.LessonDate >= currentDate)
                .OrderBy(l => l.LessonDate)
                .ThenBy(l => l.LessonTime)
                .ToList();

            // אם אין שיעורים עתידיים
            if (futureLessons.Count == 0)
            {
                return Ok("No future lessons found");
            }

            // יצירת רשימה של שיעורים כפי שצריך להחזיר
            List<LessonDto> lessonDtos = new List<LessonDto>();
            foreach (Lesson lesson in futureLessons)
            {
                LessonDto dto = new LessonDto(lesson);
                lessonDtos.Add(dto);
            }

            return Ok(lessonDtos);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



    [HttpGet("DeleteLesson")]
    public IActionResult DeleteLesson([FromQuery] int lessonsId)
    {
        try
        {

            //// בדיקה אם המשתמש מחובר
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

            //// אם המשתמש הוא סטודנט, נבצע חיפוש של שיעורים עתידיים
            Student? student = context.GetStudent(user.Id);
            if (student == null)
            {
                return Unauthorized("User is not a student!");
            }

            // חיפוש השיעור לפי ID
            var lesson = context.Lessons.FirstOrDefault(l => l.Id == lessonsId && l.StudentId == user.Id);

            // אם לא נמצא שיעור עם ID כזה, נחזיר תשובה שלא נמצא שיעור
            if (lesson == null)
            {
                return NotFound("Lesson not found");
            }

            // מחיקת השיעור מהמאגר
            context.Lessons.Remove(lesson);
            context.SaveChanges();

            return Ok("Lesson deleted successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }




}

