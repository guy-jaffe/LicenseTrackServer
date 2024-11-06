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


    [HttpPost("register")]
    public IActionResult Register([FromBody] UsersDTO userDto)
    {
        try
        {
            HttpContext.Session.Clear(); //Logout any previous login attempt

            //Create model user class
            User modelsUser = userDto.GetModels();

            context.Users.Add(modelsUser);
            context.SaveChanges();

            //User was added!
            UsersDTO dtoUser = new UsersDTO(modelsUser);
            //dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.Id);
            return Ok(dtoUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }


}

