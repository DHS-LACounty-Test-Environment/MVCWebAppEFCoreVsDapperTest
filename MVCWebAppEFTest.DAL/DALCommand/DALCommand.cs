using Dapper;
using Mapster;
using MVCWebAppEFTest.DAL.Entities;
using System.Data;

namespace MVCWebAppEFTest.DAL.DALCommand
{
    public class DALCommand
    {
        private readonly IDbConnection _dbConnection;
        private readonly MVCWebAppEFTestContext _context;

        public DALCommand(IDbConnection dbConnection, MVCWebAppEFTestContext context)
        {
            _dbConnection = dbConnection;
            _context = context;
        }

        #region CREATE
        public int AddVisitorEFCore(Models.Visitor visitor)
        {
            var newVisitor = visitor.Adapt<Entities.Visitor>();
            _context.Visitors.Add(newVisitor);
            _context.SaveChanges();

            // add multiple records
            //_context.AddRange(newVisitors);

            return newVisitor.VisitorId;
        }


        public void AddVisitorDapper(Models.Visitor visitor)
        {
            var sql = @"
                        INSERT INTO [dbo].[Visitor]
                           ([FirstName]
                           ,[LastName]
                           ,[Age]
                           ,[Address]
                           ,[IsMinor]
                           ,[PatientMRN]
                           ,[PatientFIN]
                           ,[RelationshipId]
                           ,[VisitTypeId]
                           ,[CheckInDateTime]
                           ,[CheckOutDateTime]
                           ,[Comments]
                           ,[CreatedDate]
                           ,[CreatedBy]
                           ,[PatientLocation])
                        VALUES
                           (@FirstName
                           ,@LastName
                           ,@Age
                           ,@Address
                           ,@IsMinor
                           ,@PatientMRN
                           ,@PatientFin
                           ,@RelationshipId
                           ,@VisitTypeId
                           ,@CheckInDateTime
                           ,@CheckOutDateTime
                           ,@Comments
                           ,@CreatedDate
                           ,@CreatedBy
                           ,@PatientLocation)";

            _dbConnection.Execute(sql, visitor);
        }

        public void AddVisitorDapper2(Models.Visitor visitor)
        {
            _dbConnection.Execute("SP_InsertVisitor", new
            {
                FirstName = visitor.FirstName,
                visitor.LastName,
                visitor.Age,
                visitor.Address,
                visitor.IsMinor,
                visitor.PatientMrn,
                visitor.PatientFin,
                visitor.RelationshipId,
                visitor.VisitTypeId,
                visitor.CheckInDateTime,
                visitor.CheckOutDateTime,
                visitor.Comments,
                visitor.CreatedDate,
                visitor.CreatedBy,
                visitor.PatientLocation
            }, commandType: CommandType.StoredProcedure);


            // following will trigger error in run time
            //_dbConnection.Execute("SP_InsertVisitor", visitor, commandType: CommandType.StoredProcedure);

        }



        #endregion

        #region UPDATE

        public void UpdateVisitorEFCore(Models.Visitor visitor)
        {
            // Find database record
            var dbVisitor = _context.Visitors.Find(visitor.VisitorId);
            if (dbVisitor != null)
            {
                visitor.Adapt(dbVisitor);
                _context.SaveChanges();
            }
        }

        public void UpdateVisitorDapper(Models.Visitor visitor)
        {
            var sql = @"
                        UPDATE [dbo].[Visitor]
                           SET [FirstName] = @FirstName
                              ,[LastName] = @LastName
                              ,[Age] = @Age
                              ,[Address] = @Address
                              ,[IsMinor] = @IsMinor
                              ,[PatientMRN] = @PatientMRN
                              ,[PatientFIN] = @PatientFIN
                              ,[RelationshipId] = @RelationshipId
                              ,[VisitTypeId] = @VisitTypeId
                              ,[CheckInDateTime] = @CheckInDateTime
                              ,[CheckOutDateTime] = @CheckOutDateTime
                              ,[Comments] = @Comments
                              ,[CreatedDate] = @CreatedDate
                              ,[CreatedBy] = @CreatedBy
                              ,[PatientLocation] = @PatientLocation
                         WHERE VisitorId = @VisitorId
                        ";

            _dbConnection.Execute(sql, visitor);

        }

        public void UpdateVisitorNameEFCore(int visitorId, string firstName, string lastName)
        {
            var dbVisitor = _context.Visitors.Find(visitorId);
            if (dbVisitor != null)
            {
                dbVisitor.FirstName = firstName;
                dbVisitor.LastName = lastName;

                _context.SaveChanges();
            }
        }

        public void UpdateVisitorNameDapper(int visitorId, string firstName, string lastName)
        {
            var sql = @"
                        UPDATE [dbo].[Visitor]
                           SET [FirstName] = @firstName
                              ,[LastName] = @lastName
                         WHERE VisitorId = @visitorId
                        "
            ;

            _dbConnection.Execute(sql, new { visitorId, firstName, lastName });
        }

        #endregion

        #region DELETE

        public void DeleteVisitorEFCore(int visitorId)
        {
            var dbVisitor = _context.Visitors.Find(visitorId);
            if (dbVisitor != null)
            {
                _context.Remove(dbVisitor);
                _context.SaveChanges();

                // Delete multiple records
                //_context.RemoveRange(dbVisitors);
            }
        }

        public void DeleteVisitorDapper(int visitorId)
        {
            var sql = @"
                        DELETE [dbo].[Visitor]
                         WHERE VisitorId = @visitorId
                        ";

            _dbConnection.Execute(sql, new { visitorId });
        }

        #endregion


    }
}
