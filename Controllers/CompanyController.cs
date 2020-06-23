using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class CompanyController : Controller
    {
        [System.Web.Mvc.Route(Name = "CreateCompany")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage CreateCompany([FromUri]string companyId, [FromUri]string companyName)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            Guid _companyId = Guid.Parse(companyId);
            if (!string.IsNullOrEmpty(companyName))
            {
                Company company = new Company(_companyId, companyName);
                System.Diagnostics.Debug.WriteLine($"Company: {company.RetrieveCompany(_companyId)}");
                if (company.RetrieveCompany(_companyId).CompanyId.Equals(Guid.Empty))
                {
                    company.CreateCompany(company);
                    httpResponseMessage.Content = new StringContent("Successfully created company", System.Text.Encoding.UTF8, "application/json");
                    httpResponseMessage.StatusCode = HttpStatusCode.OK;
                }
                else
                {

                    httpResponseMessage.Content = new StringContent("Failed to add company: it already exists",System.Text.Encoding.UTF8,"application/json");
                    httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
                }
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to create user", System.Text.Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;
        }
        [System.Web.Mvc.Route(Name = "RetrieveCompany")]
        [System.Web.Mvc.HttpGet]
        public Company RetrieveCompany([FromUri]string id) => new Company().RetrieveCompany(Guid.Parse(id));

        [System.Web.Mvc.Route(Name = "RetrieveCompanies")]
        [System.Web.Mvc.HttpGet]
        public string RetrieveCompanies()
        {
            System.Diagnostics.Debug.WriteLine("Was invoked");
            JavaScriptSerializer javaScriptSerialiser = new JavaScriptSerializer();
            StringBuilder companyStringBuilder = new StringBuilder();
            List<Company> companyList = new Company().RetrieveAllCompanies();

            companyList.ForEach(company => {
                companyStringBuilder.Append(javaScriptSerialiser.Serialize(company));
                companyStringBuilder.Append("\n");
            });
            
            return companyStringBuilder.ToString();
        }
        
        [System.Web.Mvc.Route(Name = "UpdateCompany")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage UpdateCompany([FromUri]Guid id, [FromUri]string companyName)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            if (!string.IsNullOrEmpty(companyName))
            {

                new Company().UpdateCompany(new Company(id, companyName));
                httpResponseMessage.Content = new StringContent("Successfully updated user", System.Text.Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to update user", System.Text.Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;
        }
        [System.Web.Mvc.Route(Name = "DeleteCompany")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage DeleteCompany([FromUri]Guid id)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            new Company().DeleteCompany(id);
            httpResponseMessage.Content = new StringContent("Successfully deleted user",System.Text.Encoding.UTF8,"application/json");
            httpResponseMessage.StatusCode = HttpStatusCode.OK;
            return httpResponseMessage;
            
        }
    }
}