Project to explore a reference architecture with the following components: Entity Framework, Unity, Asp.net web forms, Glimpse, Log4net, ELMAH, AutoMapper.

Other items used: SQL Server for session, expect to use the AD membership provider for authentication and roles.

There is a custom generic repository over the entity framework database first model.

A database project is present for the SQL Server DML.

A custom caching implemntation is present leveraging Unity's interception capabilities.

Outstanding items:

	more unit tests and documentation

	add cache provider using the standard .net framework library
	
	finish web api/angular ui project



