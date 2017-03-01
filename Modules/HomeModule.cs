using Nancy;
using System.Collections.Generic;

namespace Registrar
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
                return View["index.cshtml", ModelMaker()];
            };

            Post["/add-department"] = _ => {
                Department newDepartment = new Department(Request.Form["department-name"]);
                newDepartment.Save();
                return View["index.cshtml", ModelMaker()];
            };

            Post["/add-course"] = _ => {
                Course newCourse = new Course(Request.Form["course-name"], Request.Form["course-number"], Request.Form["department-id"]);
                newCourse.Save();
                return View["index.cshtml", ModelMaker()];
            };

            Get["/students"] = _ =>
            {
                Dictionary<string, object> model = ModelMaker();
                model.Add("Students", Student.GetAll());
                return View["students.cshtml", model];
            };

            Post["students/{id}"] = parameters =>
            {
                Student.Find(parameters.id).AddCourse(Request.Form["course-id"]);
                Dictionary<string, object> model = ModelMaker();
                model.Add("Students", Student.GetAll());
                return View["students.cshtml", model];
            };

            Patch["students/{id}"] = parameters =>
            {
                Student.Find(parameters.id).UpdateCourseGrade(Request.Form["course-id"], Request.Form["course-grade"]);
                Dictionary<string, object> model = ModelMaker();
                model.Add("Students", Student.GetAll());
                return View["students.cshtml", model];
            };

            Delete["students/{studentId}/course/{id}"] = parameters =>
            {
                Student.Find(parameters.studentId).DeleteCourse(parameters.id);
                Dictionary<string, object> model = ModelMaker();
                model.Add("Students", Student.GetAll());
                return View["students.cshtml", model];
            };

            Post["/add-student"] = _ => {
                Student newStudent = new Student(Request.Form["student-name"], Request.Form["enrollment-date"], Request.Form["department-id"]);
                newStudent.Save();
                Dictionary<string, object> model = ModelMaker();
                model.Add("Students", Student.GetAll());
                return View["students.cshtml", model];
            };
        }

        public static Dictionary<string, object> ModelMaker()
        {
            Dictionary<string, object> model = new Dictionary<string, object>{{"Departments", Department.GetAll()}, {"Courses", Course.GetAll()}};
            return model;
        }
    }
}
