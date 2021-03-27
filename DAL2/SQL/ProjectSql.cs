namespace Project_Chronos_Backend.DAL.SQL
{
    public static class ProjectSql
    {
        public static string CreateProjectAndTasks = @"
        DECLARE @generated_project_key INT;
        -- CREATE ONE PROJECT, OUTPUT CREATED PK INTO @generated_project_key
        INSERT INTO [dbo].[Projects](ProjectName) 
        VALUES (@ProjectName)
        SET @generated_project_key = @@IDENTITY  

		-- CREATE MULTIPLE TASKS, OUPUT EACH PK INTO @generated_task_keys
        INSERT INTO [dbo].Users(UserName, Role) 
        --OUTPUT inserted.UserId INTO @generated_user_keys
        SELECT UserName, Role
        FROM @Users
        
        
		-- CREATE MULTIPLE COLUMNS,  OUPUT EACH PK INTO @generated_column_keys
		INSERT INTO [dbo].[Columns](ColumnName) 
        --OUTPUT inserted.ColumnId INTO @generated_column_keys
        SELECT ColumnName
        FROM @Columns

        -- CREATE MULTIPLE TASKS, OUPUT EACH PK INTO @generated_task_keys
        INSERT INTO [dbo].[Tasks](TaskName, Comments) 
        --OUTPUT inserted.TaskId INTO @generated_task_keys
        SELECT TaskName, Comments
        FROM @Tasks

		-- CREATE MULTIPLE Timelogs, OUPUT EACH PK INTO @generated_timelog_keys
        INSERT INTO [dbo].[TimeLogs](StartTime, EndTime, TotalTime) 
       -- OUTPUT inserted.TimeLogId INTO @generated_timelog_keys
        SELECT StartTime, EndTime, TotalTime
        FROM  @TimeLogs
        SELECT @generated_project_key
        ";

        public static string CreatLinks = @"
        --DECLARE @generated_project_key INT;
        --DECLARE @generated_column_keys AS [dbo].[IdType];


        INSERT INTO [dbo].[ColumnTasks](Columnid, TaskId) 
        SELECT Columnid, TaskId
        FROM  @ColumnTasks
     
		INSERT INTO [dbo].[UserTimeLogs](UserId, TimeLogId) 
        SELECT UserId, TimeLogId
        FROM  @UserTimeLogs

		 INSERT INTO [dbo].[ProjectUsers](ProjectId, UserId) 
        SELECT ProjectId, UserId
        FROM  @ProjectUsers

		 INSERT INTO [dbo].[TasksTimeLogs](TaskId, TimeLogId) 
        SELECT TaskId, TimeLogId
        FROM  @TaskTimeLogs

		INSERT INTO [dbo].[UserTasks](UserId, TaskId) 
        SELECT UserId, TaskId
        FROM  @UserTasks

		INSERT INTO [dbo].[ProjectColumns](ProjectId, ColumnId)
        SELECT ProjectId, ColumnId
        FROM @ProjectColumns
        ";

        public static string GetProjectAndTasks = @"
        SELECT p.[ProjectId]
        ,p.[ProjectName]
	    ,t.TaskId
	    ,t.TaskName
        FROM [dbo].[Projects] as p

        INNER JOIN ProjectTasks as pt ON p.ProjectId = pt.ProjectId
        INNER JOIN Tasks as t on t.TaskId = pt.TaskId

        WHERE p.ProjectId IN (@ProjectIds)
        ";


        public static string CreateProject = @"
         INSERT INTO [dbo].[Projects](ProjectName) 
        OUTPUT INSERTED.ProjectId
        VALUES (@ProjectName)
        ";

        public static string GetProject = @"
       SELECT *
        --[dbo].[Projects].[ProjectId]
        --,[ProjectName]

        FROM [dbo].[Projects]
		LEFT JOIN [dbo].[ProjectColumns] as pc on pc.ProjectId = @ProjectId
		LEFT JOIN [dbo].[Columns] as col on col.ColumnId = pc.ColumnId
		LEFT JOIN [dbo].[ColumnTasks] as ct on ct.ColumnId = col.ColumnId
		LEFT JOIN [dbo].[Tasks] as task on task.TaskId = ct.TaskId
		LEFT JOIN [dbo].[TasksTimeLogs] as ttl on ttl.TaskId = task.TaskId
		LEFT JOIN [dbo].[TimeLogs] as time on time.TimeLogId = ttl.TimeLogId
		LEFT JOIN [dbo].[ProjectUsers] as pu on pu.ProjectId = @ProjectId
		LEFT JOIN [dbo].[UserTasks] as ut on ut.TaskId = task.TaskId
		LEFT JOIN [dbo].[Users] as users on users.UserId = pu.UserId | ut.UserId
		WHERE [dbo].[Projects].[ProjectId] = @ProjectId
		
		
        ";

        public static string CreateColumn = @"
        DECLARE @generated_column_key int;
        INSERT INTO [dbo].[Columns](ColumnName)
        OUTPUT INSERTED.ColumnId
        VALUES(@ColumnName)
        SET @generated_column_key = @@IDENTITY  
        INSERT INTO [dbo].[ProjectColumns](ProjectId, ColumnId)
        SELECT @ProjectId, @generated_column_key
        
        ";
        public static string CreateTask = @"
        DECLARE @generated_task_key int;
        INSERT INTO [dbo].[Tasks](TaskName, Comments)
        OUTPUT INSERTED.TaskId
        VALUES(@TaskName, @Comments)
        SET @generated_task_key = @@IDENTITY  
        INSERT INTO [dbo].[ColumnTasks](ColumnId, TaskId)
        SELECT @ColumnId, @generated_task_key
        
        ";
        public static string CreateUser = @"
        DECLARE @generated_user_key int;
        INSERT INTO [dbo].[Users](UserName, Role)
        OUTPUT INSERTED.UserId
        VALUES(@UserName, @Role)
        ";
        public static string SetTaskUser = @"
        DECLARE @generated_user_key int;
        DECLARE @generated_task_key int;
        INSERT INTO [dbo].[UserTasks](UserId, TaskId)
        OUTPUT INSERTED.UTID
        VALUES(@UserId, @TaskId)
        ";
        public static string SetProjectUser = @"
        DECLARE @generated_user_key int;
        DECLARE @generated_project_key int;
        INSERT INTO [dbo].[ProjectUsers](UserId, ProjectId)
        OUTPUT INSERTED.PUID
        VALUES(@UserId, @ProjectId)
        ";
        public static string CreateTimeLog = @"
		DECLARE @generated_timelog_key int

        INSERT INTO [dbo].[TimeLogs](StartTime, EndTime, TotalTime)
        SELECT StartTime, EndTime, TotalTime
        FROM @TimeLogs
     
		SET @generated_timelog_key = @@IDENTITY

        INSERT INTO [dbo].[UserTimeLogs](UserId, TimeLogId)
        OUTPUT INSERTED.UTLID
        VALUES(@UserId, @generated_timelog_key)
        ";
    }
}
