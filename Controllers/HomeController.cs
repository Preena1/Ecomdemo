using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using Ecomdemo.Models;


namespace Ecomdemo.Controllers
{



public class HomeController : Controller
{

// <-------Note connection string and password to database---------->
private string constr = "server=localhost;port=3306;uid=ifundiuser;" +
     "pwd=ifundiuser123; database=shopalot_db;charset=utf8;sslmode=none;AllowPublicKeyRetrieval=True";

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


   public IActionResult Index()
    {
       if (GlobalModel.firstname != null)
       {

            ViewBag.Name = GlobalModel.firstname;
       }
        return View();
    }
    public IActionResult About()
    {
        return View();
    }
    public IActionResult Product()
    {
        return View();
    }
        public IActionResult Cart()
    {
        return View();
    }
         public IActionResult Checkout()
    {
        return View();
    }
        public IActionResult Order()
    {
        return View();
    }
        public IActionResult Contact()
    {
        return View();
    }

         public IActionResult Terms()
    {
        return View();
    }

         public IActionResult FAQ()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }
   public IActionResult Shipping()
    {
        return View();
    }
   
      [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
public IActionResult Register()
        {
            return View();
        }

public IActionResult Login()
        {
            return View();
        }


     [HttpPost] 
        public ActionResult Register(UserModel obj)
        {
            ViewBag.firstname = obj.firstname;
            ViewBag.lastname = obj.lastname;
            ViewBag.email = obj.email;
            ViewBag.password = obj.password;
 // <-------Will build encoding password process in future development---------->
            // string enPassword = obj.EncodePasswordToBase64(obj.password);
            // string dePassword = obj.DecodeFrom64(enPassword);
 
            // ViewBag.enPassword = enPassword;
            // ViewBag.dePassword = dePassword;
            MySqlConnection conn = new MySqlConnection(constr);
 
            using (conn)
            {
                try
                {
                    List <UserModel> user = new List<UserModel>();
                    conn.Open();
                    MySqlCommand command =  conn.CreateCommand();
                    command.CommandType=System.Data.CommandType.Text;
 
                    string? fname = obj.firstname; 
                    string? lname = obj.lastname; 
                    string? email = obj.email;
                    string? password = obj.password;
                   // reader.Close();
                   Console.WriteLine("fname: " + fname);
                    command.CommandText=$"Insert into Userlogin(firstname, lastname, email, password) values('{fname}','{lname}','{email}','{password}');";
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Close();
                     command.CommandText=$"Select * From Userlogin;";
                    conn.Close();
                    System.Console.WriteLine("Connection is : " + conn.State.ToString() + Environment.NewLine);
                    // return StatusCode(200);
                }catch(MySql.Data.MySqlClient.MySqlException ex){
                    System.Console.WriteLine("Error: " + ex.Message.ToString());
                    conn.Close();
                    System.Console.WriteLine("Connection is : " + conn.State.ToString() + Environment.NewLine);
                    return StatusCode(500);
                }finally {
                    //System.Console.WriteLine("Press any key to Exit...");
                    //
                    //Console.ReadKey();
                }
            // if (chkAddon != null)
            //     ViewBag.Addon = "Selected";
            // else
            //     ViewBag.Addon = "Not Selected";
 
            return View(obj);
            // return View("Login", obj);
            }
         } 

[HttpPost]
    public IActionResult Login(loginModel obj)
   {
        MySqlConnection conn = new MySqlConnection(constr);
        using (conn)
        {
             var emailid = obj.email;
             var pword = obj.password;
    // <-------Added in emailid and password match to gain access and login---------->
            string query = $"SELECT firstname, email, password FROM userlogin where email = '{emailid}' and password ='{pword}';";
    
            using (MySqlCommand cmd = new MySqlCommand(query))
            {
                cmd.Connection = conn;
                conn.Open();
                using (MySqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        // obj.password = string.Format("{0:S}", sdr["password"]);
               // <-------Used a Global Model to pull in first name for welcome login---------->          
                        GlobalModel.firstname = string.Format("{0:S}", sdr["firstname"]);
                        if (string.Compare(obj.password,string.Format("{0:S}", sdr["password"]),false)==0)
                            return RedirectToAction("Index");
                        else
                            return View("Error");                        
                    }
                }
                conn.Close();
            }
        }        
        return View(obj);
    }

  }
}


