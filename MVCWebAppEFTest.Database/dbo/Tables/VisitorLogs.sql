CREATE TABLE [dbo].[VisitorLogs]
(
	[VisitorLogsId] INT IDENTITY(1,1) NOT NULL , 
    [PatientId] INT NOT NULL, 
    [VisitorId] INT NOT NULL, 
    CONSTRAINT [PK_VisitorLogs] PRIMARY KEY ([VisitorLogsId]), 
    CONSTRAINT [FK_VisitorLogs_Patient] FOREIGN KEY ([PatientId]) REFERENCES [Patient]([PatientId]), 
    CONSTRAINT [FK_VisitorLogs_Visitor] FOREIGN KEY ([VisitorId]) REFERENCES [Visitor]([VisitorId]) 
)
