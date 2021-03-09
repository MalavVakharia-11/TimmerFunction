using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Net.Http;
using System.Net;
using FirstLiveDemo.Models;
using Microsoft.Azure.WebJobs.Host;

namespace FirstLiveDemo
{
    public static class InsertData
    {
        [FunctionName("InsertData")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("Received new account request");
            HttpResponseMessage response;
            try
            {
                // string sqlConnection = ConfigurationManager.AppSettings["AzureWebJobsStorage"];
                //.ConnectionStrings["dbEntities"];

                 //string cs = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
                //var test = Environment.GetEnvironmentVariable("dbEntities");
                var student = await req.Content.ReadAsAsync<Student>();
                if (student != null)
                {
                    log.Info("Saving to database");
                    using (var db = new StudentDbContext())
                    {
                        db.Students.Add(student);
                        await db.SaveChangesAsync();
                    }
                    response = req.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    var errorMessage = "Failed to parse user";
                    log.Error(errorMessage);
                    response = req.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
                response = req.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
            return response;
        }
    }
}
