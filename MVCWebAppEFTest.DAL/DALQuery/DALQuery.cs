using Dapper;
using Mapster;
using MVCWebAppEFTest.DAL.Entities;
using System.Data;

namespace MVCWebAppEFTest.DAL.DALQuery
{
    public class DALQuery
    {
        private readonly IDbConnection _dbConnection;
        private readonly MVCWebAppEFTestContext _context;

        public DALQuery(IDbConnection dbConnection, MVCWebAppEFTestContext context)
        {
            _dbConnection = dbConnection;
            this._context = context;
        }

        #region Query by PK
        public Models.Visitor GetVisitorEFCore(int visitorId)
        {
            return _context.Visitors.Find(visitorId)
                                   .Adapt<Models.Visitor>();

        }

        public Models.Visitor? GetVisitorDapper(int visitorId)
        {
            var sql = "SELECT * FROM Visitor WHERE VisitorId = @visitorId";
            return _dbConnection.QuerySingleOrDefault<Models.Visitor>(sql, new { visitorId });
        }
        #endregion

        #region Query by filters

        public Models.Visitor[] GetVisitorByNameAndAgeEFCore(string firstName, short age)
        {
            return _context.Visitors
               .Where(v => v.FirstName == firstName && v.Age == age)
               .ToArray()
               .Adapt<Models.Visitor[]>();

        }

        public Models.Visitor[] GetVisitorByNameAndAgeDapper(string firstName, string age)
        {

            var sql = "SELECT * FROM Visitor WHERE FirstName = @firstName and Age = @age";
            return _dbConnection.Query<Models.Visitor>(sql, new { firstName, age })
                             .ToArray();

        }

        #endregion

        #region Query all records

        public Models.Visitor[] GetAllVisitorsEFCore()
        {
            return _context.Visitors
                          .ToArray()
                          .Adapt<Models.Visitor[]>();
        }

        public Models.Visitor[] GetAllVisitorsDapper()
        {
            var sql = "SELECT * FROM Visitor";
            return _dbConnection.Query<Models.Visitor>(sql)
                             .ToArray();

        }

        #endregion

    }
}
