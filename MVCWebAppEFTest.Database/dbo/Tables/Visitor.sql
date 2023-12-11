CREATE TABLE [dbo].[Visitor] (
    [VisitorId]        INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]        VARCHAR (50)  NOT NULL,
    [LastName]         VARCHAR (50)  NULL,    
    [Age]         SMALLINT  NULL,    
    [DOB]         DATETIME  NULL,    
    [Address]       VARCHAR(120) NULL,
    [IsMinor]          BIT           NOT NULL,
    [PatientMRN]       VARCHAR (10)  NOT NULL,
    [PatientFIN]       VARCHAR (10)  NULL,
    [RelationshipId]   SMALLINT      NOT NULL,
    [VisitTypeId]      SMALLINT      NOT NULL,
    [CheckInDateTime]  DATETIME      NOT NULL,
    [CheckOutDateTime] DATETIME      NULL,
    [Comments]         VARCHAR (500) NULL,
    [CreatedDate]      DATETIME      NOT NULL,
    [CreatedBy]        VARCHAR (100) NULL,
    [PatientLocation]  VARCHAR (50)  NULL,
    CONSTRAINT [PK_Visitor] PRIMARY KEY CLUSTERED ([VisitorId] ASC)
);

