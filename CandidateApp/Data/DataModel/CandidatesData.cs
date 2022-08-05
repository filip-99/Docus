using CandidateApp.Data.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateApp.Data.DataModel
{
    public class CandidatesData
    {
        private string ConnectionString =
"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Candidate;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Candidate> GetAllCandidates()
        {
            List<Candidate> listToReturn = new List<Candidate>();
            using (SqlConnection dataConnection = new SqlConnection(this.ConnectionString))
            {
                dataConnection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = dataConnection;
                command.CommandText = "SELECT * FROM Candidates";
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Candidate candidate = new Candidate();
                    candidate.Id = dataReader.GetInt32(0);
                    candidate.Name = dataReader.GetString(1);
                    candidate.EmailId = dataReader.GetString(2);
                    candidate.PhoneNumber = dataReader.GetString(3);
                    candidate.AdressLine1 = dataReader.GetString(4);
                    candidate.AdressLine2 = dataReader.GetString(5);
                    candidate.Active = dataReader.GetBoolean(6);
                    listToReturn.Add(candidate);
                }
            }
            return listToReturn;
        }
    }
}
