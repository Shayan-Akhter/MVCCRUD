using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Test.Models;

namespace Test.Controllers
{
    public class StudentController : Controller
    {
        string connectionString = @"Data Source=DESKTOP-GG675F6;Initial Catalog=test;Integrated Security=True";
        [HttpGet]
        // GET: Student
        public ActionResult Index()
        {
            DataTable dtStudent = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from tbl_Student", sqlConnection);
                sqlDataAdapter.Fill(dtStudent);
            }
                return View(dtStudent);
        }
        
        [HttpGet]

        // GET: Student/Create
        public ActionResult Create()
        {
            return View(new StudentModel());
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(StudentModel studentModel)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string query = "insert into tbl_Student values (@studentName,@studentAge,@studentClass)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@studentName", studentModel.studentName);
                sqlCommand.Parameters.AddWithValue("@studentAge", studentModel.studentAge);
                sqlCommand.Parameters.AddWithValue("@studentClass", studentModel.studentClass);
                sqlCommand.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            StudentModel studentModel = new StudentModel();
            DataTable dtStudent = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string query = "Select * from tbl_Student where StudentID = @StudentID";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@StudentID", id);
                sqlDataAdapter.Fill(dtStudent);
            }

            if (dtStudent.Rows.Count == 1)
            {
                studentModel.studentId = Convert.ToInt32(dtStudent.Rows[0][0].ToString());
                studentModel.studentName = dtStudent.Rows[0][1].ToString();
                studentModel.studentAge = Convert.ToInt32(dtStudent.Rows[0][2].ToString());
                studentModel.studentClass = Convert.ToInt32(dtStudent.Rows[0][3].ToString());
                return View(studentModel);
            }
            else 
            {
                return RedirectToAction("index");
            }

        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(StudentModel studentModel)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string query = "update tbl_Student set StudentName = @studentName, StudentAge = @studentAge, StudentClass = @studentClass where StudentId = @StudentID";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@studentID", studentModel.studentId);
                sqlCommand.Parameters.AddWithValue("@studentName", studentModel.studentName);
                sqlCommand.Parameters.AddWithValue("@studentAge", studentModel.studentAge);
                sqlCommand.Parameters.AddWithValue("@studentClass", studentModel.studentClass);
                sqlCommand.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string query = "delete from tbl_Student where StudentId = @StudentID";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@studentID", id);
                sqlCommand.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        //// POST: Student/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
