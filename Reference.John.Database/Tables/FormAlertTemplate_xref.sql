CREATE TABLE [dbo].[FormAlertTemplate_xref]
(
	[FormId] INT NOT NULL , 
    [AlertTemplateId] INT NOT NULL,
	[IsActive] INT NOT NULL DEFAULT 1, 
	[StartDate] DATETIME NOT NULL, 
	[EndDate] DATETIME Not NULL, 
	[DateCreated] DATETIME NOT NULL DEFAULT GETDATE() , 
	[LastChangeUser] NVARCHAR(50) NOT NULL, 
	[LastChangeDate] DATETIME NOT NULL, 
	[ClientToken] UNIQUEIDENTIFIER NOT NULL DEFAULT newsequentialid(), 
	[RowVersion] ROWVERSION NULL, 
    [EntityId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    PRIMARY KEY ([FormId], [AlertTemplateId]), 
    CONSTRAINT [FK_FormAlertTemplate_xref_FormSImpleZero] FOREIGN KEY ([FormId]) REFERENCES [FormSimpleZero]([Id]) ,
	CONSTRAINT [FK_FormAlertTemplate_xref_AlertTemplate] FOREIGN KEY ([AlertTemplateId]) REFERENCES [AlertTemplate]([AlertTemplateId]) ,
)
