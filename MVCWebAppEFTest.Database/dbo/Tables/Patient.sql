CREATE TABLE [dbo].[Patient] (
    [PatientId] INT        IDENTITY(1,1) NOT NULL,
    [Name]      NCHAR (10) NOT NULL,
    [Age]      SMALLINT NOT NULL,
    CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED ([PatientId] ASC) 
);

