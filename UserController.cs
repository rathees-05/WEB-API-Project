using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using WebApiProject.Models;

namespace WebApiProject.Controllers
{
    public class UserController : ApiController
    {
        public HttpResponseMessage Get()
        {
            List<UserDetail> userDetails = new List<UserDetail>();
            using (WebApiProjectEntities1 dbContext = new WebApiProjectEntities1())
            {
                userDetails = dbContext.UserDetails.ToList();
                if(userDetails.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please try again later");
                }
                return Request.CreateResponse(HttpStatusCode.OK, userDetails);

            }
        }
        public HttpResponseMessage Get(int id)
        {
            using (WebApiProjectEntities1 dbContext = new WebApiProjectEntities1())
            {
                var User = dbContext.UserDetails.FirstOrDefault(u => u.Id == id);
                if (User != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, User);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "User with ID" + id + "Not Found in Our Database");
                }
            }
        }
        public HttpResponseMessage Post(UserDetail userDetails )
        {
            using (WebApiProjectEntities1 dbContext = new WebApiProjectEntities1())
            {
                if(userDetails != null && ModelState.IsValid)
                {
                    dbContext.UserDetails.Add(userDetails);
                    dbContext.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.Created, userDetails);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Please Provide The Required Information");
                }
            }
        }
        public HttpResponseMessage Put(int id , UserDetail userDetails)
        {

            using (WebApiProjectEntities1 dbContext = new WebApiProjectEntities1())
            {
                var User = dbContext.UserDetails.FirstOrDefault(u => u.Id == id);
                if (User != null)
                {
                    User.User_Name = userDetails.User_Name;
                    User.Email = userDetails.Email;
                    User.Gender = userDetails.Gender;
                    User.Salary = userDetails.Salary;

                    dbContext.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, User);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "User With ID" + id + "Not Found in Our Database");
                }
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            using (WebApiProjectEntities1 dbContext = new WebApiProjectEntities1())
            {
                var User = dbContext.UserDetails.FirstOrDefault(u => u.Id == id);
                if(User != null)
                {
                    dbContext.UserDetails.Remove(User);
                    dbContext.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, User);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee With ID" + id + "Not Found in Our Database");
                }
            }
        }
    }
}
