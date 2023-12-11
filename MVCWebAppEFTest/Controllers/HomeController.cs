using Microsoft.AspNetCore.Mvc;
using MVCWebAppEFTest.DAL.DALCommand;
using MVCWebAppEFTest.DAL.DALQuery;
using MVCWebAppEFTest.DAL.DataAccessDapper;
using MVCWebAppEFTest.DAL.Entities;
using MVCWebAppEFTest.Models;
using System;
using System.Diagnostics;

namespace MVCWebAppEFTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DALQuery _dalQuery;
        private readonly DALCommand _dalCommand;

        public HomeController(ILogger<HomeController> logger, ISqlConnectionFactory connectionFactory, MVCWebAppEFTestContext context)
        {
            _logger = logger;

            this._dalQuery = new DALQuery(connectionFactory.Create(), context);
            this._dalCommand = new DALCommand(connectionFactory.Create(), context);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult EFCreate()
        {
            var visitor = CreateRandomVisitor();
            int visitorId = _dalCommand.AddVisitorEFCore(visitor);

            return View("Create", visitorId);
        }

        public IActionResult DapperCreate()
        {
            var visitor = CreateRandomVisitor();
            _dalCommand.AddVisitorDapper2(visitor);

            return View("Create", 0);
        }

        public IActionResult EFUpdate(int id)
        {
            Models.Visitor visitor = null;

            if (id > 0)
            {
                visitor = CreateRandomVisitor();
                visitor.VisitorId = id;

                _dalCommand.UpdateVisitorEFCore(visitor);
            }

            return View("Update", visitor);
        }

        public IActionResult DapperUpdate(int id)
        {
            Models.Visitor visitor = null;

            if (id > 0)
            {
                visitor = CreateRandomVisitor();
                visitor.VisitorId = id;

                _dalCommand.UpdateVisitorDapper(visitor);
            }

            return View("Update", visitor);
        }

        public IActionResult EFDelete(int id)
        {
            _dalCommand.DeleteVisitorEFCore(id);
            return View("Delete");
        }

        public IActionResult DapperDelete(int id)
        {
            _dalCommand.DeleteVisitorDapper(id);
            return View("Delete");
        }

        private Models.Visitor CreateRandomVisitor()
        {
            var random = new Random();
            Models.Visitor visitor = new Models.Visitor
            {
                FirstName = GenerateRandomString(),
                LastName = GenerateRandomString(),
                Age = (short)random.Next(1, 100),
                Address = GenerateRandomString(),
                IsMinor = random.Next(0, 2) == 1,
                PatientMrn = GenerateRandomString(),
                PatientFin = GenerateRandomString(),
                RelationshipId = (short)random.Next(1, 10),
                VisitTypeId = (short)random.Next(1, 5),
                CheckInDateTime = DateTime.Now.AddHours(-random.Next(1, 100)),
                CheckOutDateTime = random.Next(0, 2) == 1 ? DateTime.Now : (DateTime?)null,
                Comments = GenerateRandomString(),
                CreatedDate = DateTime.Now.AddHours(-random.Next(1, 100)),
                CreatedBy = GenerateRandomString(),
                PatientLocation = GenerateRandomString()
            };

            return visitor;
        }

        private string GenerateRandomString()
        {
            return Guid.NewGuid().ToString("N").Substring(0, new Random().Next(5, 11));
        }




        public IActionResult EFRead(int id)
        {
            var visitors = new List<Models.Visitor>();
            if (id == 0)
            {
                visitors.AddRange(_dalQuery.GetAllVisitorsEFCore());
            }
            else
            {
                visitors.Add(_dalQuery.GetVisitorEFCore(id));
            }
            return View("Read", visitors.ToArray());
        }

        public IActionResult DapperRead(int id)
        {
            var visitors = new List<Models.Visitor>();
            if (id == 0)
            {
                visitors.AddRange(_dalQuery.GetAllVisitorsDapper());
            }
            else
            {
                visitors.Add(_dalQuery.GetVisitorDapper(id));
            }
            return View("Read", visitors.ToArray());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}