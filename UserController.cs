using MiniProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MiniProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            User user = new User();
            
            SqlConnection cn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            cn.ConnectionString = @"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=Thunder;Integrated Security=True";
            cn.Open();
            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from Users";
            SqlDataReader dr = cmd.ExecuteReader();
            List<User> objUser = new List<User>();

            while (dr.Read())
            {
                user.LogInName = (string)dr["LogInName"];
                user.Password = (string)dr["Password"];
                user.Phone = (string)dr["Phone"];
                user.FullName = (string)dr["FullName"];
                user.EmailId = (string)dr["EmailId"];
                user.CityId = (int)dr["CityId"];
                objUser.Add(user);
            }
            cn.Close();
            return View(objUser);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            User user = new User();

            SqlConnection cn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            cn.ConnectionString = @"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=Thunder;Integrated Security=True";
            cn.Open();
            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from Users";
            SqlDataReader dr = cmd.ExecuteReader();
            List<User> objUser = new List<User>();

            while (dr.Read())
            {
                user.LogInName = (string)dr["LogInName"];
                user.Password = (string)dr["Password"];
                user.Phone = (string)dr["Phone"];
                user.FullName = (string)dr["FullName"];
                user.EmailId = (string)dr["EmailId"];
                user.CityId = (int)dr["CityId"];
                objUser.Add(user);
            }
            cn.Close();
            return View(objUser);
          
        }

        // GET: User/Create
        public ActionResult Create()
        {
            User user = new User();
            SqlConnection cn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            cn.ConnectionString = @"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=Thunder;Integrated Security=True";
            cn.Open();
            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from City";
            SqlDataReader dr = cmd.ExecuteReader();
            List<SelectListItem> objList = new List<SelectListItem>();

            while (dr.Read())
            {
                City ct = new City();
                objList.Add(new SelectListItem { Text = (string)dr["CityName"], Value = dr["CityId"].ToString() });
            }


            user.Cities = objList;
            cn.Close();
            return View(user);
          
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                if (user.ConfirmPassword.Equals(user.Password))
                {

                    SqlConnection cn = new SqlConnection();
                    SqlCommand cmd = new SqlCommand();
                    cn.ConnectionString = @"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=Thunder;Integrated Security=True";
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "InsertSushant";

                    cmd.Parameters.AddWithValue("@LogInName", user.LogInName);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@FullName", user.FullName);
                    cmd.Parameters.AddWithValue("@EmailId", user.EmailId);
                    cmd.Parameters.AddWithValue("@CityId", user.CityId);
                    cmd.Parameters.AddWithValue("@Phone", user.Phone);
                    cmd.ExecuteNonQuery();

                    cn.Close();
                    return RedirectToAction("Index");
                }
                else
                {
                    
                    return RedirectToAction("Create");
                }              
            }
            catch(Exception e)
            {
                ViewBag.excep = e.Message;
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id=0)
        {
            
            SqlConnection cn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            cn.ConnectionString = @"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=Thunder;
                                    Integrated Security=True
                                    ";
            cn.Open();
           

            User user = new User();
            SqlCommand cmdUser = new SqlCommand();
            cmdUser.Connection = cn;
            cmdUser.CommandType = System.Data.CommandType.Text;
            cmdUser.CommandText = "select * from Users where UserId= " + id;

            SqlDataReader drnew = cmdUser.ExecuteReader();


            if (drnew.Read())
            {
                user.LogInName = (string)drnew["LogInName"];
                user.Password = (string)drnew["Password"];
                user.Phone = (string)drnew["Phone"];
                user.FullName = (string)drnew["FullName"];
                user.EmailId = (string)drnew["EmailId"];
                user.CityId = (int)drnew["CityId"];
            }

            drnew.Close();


            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from City";
            SqlDataReader dr = cmd.ExecuteReader();

                List<SelectListItem> objList = new List<SelectListItem>();

                while (dr.Read())
                {
                    City ct = new City();
                    objList.Add(new SelectListItem { Text = (string)dr["CityName"], Value = (string)dr["CityId"].ToString() });
                }
                user.Cities = objList;
            dr.Close();
                cn.Close();
            
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            try
            {
                SqlConnection cn = new SqlConnection();
                SqlCommand cmd = new SqlCommand();
                cn.ConnectionString = @"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=Thunder;Integrated Security=True";
                cn.Open();
                cmd.Connection = cn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "UpdateUser";

                cmd.Parameters.AddWithValue("@UserId", id);
                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@EmailId", user.EmailId);
                cmd.Parameters.AddWithValue("@CityId", user.CityId);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.ExecuteNonQuery();

                cn.Close();
                Session["item"] = user;
                return RedirectToAction("Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult Home()
        {
           
            
            User user = (User)Session["item"];
            return View(user);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            if (Request.Cookies["MyCookies"] != null)
            {
                Response.Cookies["MyCookies"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult LogIn(User user)
        {
            SqlConnection cn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            cn.ConnectionString = @"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=Thunder;Integrated Security=True";
            cn.Open();
            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "LogInUser";

            cmd.Parameters.AddWithValue("@LogInName", user.LogInName);
            cmd.Parameters.AddWithValue("@Password", user.Password);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                
                user.LogInName = (string)dr["LogInName"];
                user.Password = (string)dr["Password"];
                user.Phone = (string)dr["Phone"];
                user.FullName = (string)dr["FullName"];
                user.EmailId = (string)dr["EmailId"];
                user.CityId = (int)dr["CityId"];
                user.UserId = (int)dr["UserId"];
                if (user.IsActive == true)
                {
                    HttpCookie objcookies = new HttpCookie("MyCookies");
                    objcookies.Expires = DateTime.Now.AddMilliseconds(1000);
                    objcookies.Value = user.UserId.ToString();
                }
                cn.Close();

                Session["item"] = user;
                
                return RedirectToAction("Home");

            }
                cn.Close();
                return RedirectToAction("LogIn");
        }
    }
}
