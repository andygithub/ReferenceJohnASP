Project to explore a reference architecture with the following components: Entity Framework, Unity, Asp.net web forms, Glimpse, Log4net, ELMAH, AutoMapper.

Other items used: SQL Server for session, expect to use the AD membership provider for authentication and roles.

There is a custom generic repository over the entity framework database first model.

A database project is present for the SQL Server DML.

A custom caching implemntation is present leveraging Unity's interception capabilities.

A dependency was taken on EntityFramework.Extended ideally for Future and Audit capability.  Unfortunately Futures doesn't work within the logging and interception capabilities of EF 6.1 so it probably won't be used.  The audit capability was embedded in the unit of work implementation.

Outstanding items:

	more unit tests and documentation

	add cache provider using the standard .net framework library
	
	finish web api/angular ui project



