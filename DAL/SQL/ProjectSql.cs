namespace DAL.SQL
{
    public static class ProjectSql
    {
  

        public static string CreateProject = @"
         INSERT INTO [dbo].[Projects](ProjectName, StartTime,EndTime,ExpectedEndTime, PointsTotal,AddedPointsTotal, ProjectComplete, ProjectArchived,TimeIncrement) 
        OUTPUT INSERTED.ProjectId
        VALUES (@ProjectName, @StartTime, @EndTime, @ExpectedEndTime, @PointsTotal, @AddedPointsTotal, @ProjectComplete, @ProjectArchived, @TimeIncrement)
        ";

        public static string GetUserProject = @"        
         SELECT 
			   p.ProjectId,
               p.ProjectName,
			   p.StartTime as ProjectStartTime,
			   p.EndTime as ProjectEndTime,
			   p.ExpectedEndTime,
			   p.PointsTotal as ProjectPointsTotal,
			   p.AddedPointsTotal as ProjectAddedPoints,
			   p.ProjectComplete,
			   p.ProjectArchived,
			   p.TimeIncrement,
           
		  pc.ColumnId as pcCol,
         ct.ColumnId as timelogColId,
         ct.TaskId as linkTaskId,
           col.ColumnId,
           col.ColumnName,
		  col.PointsTotal as ColumnPointsTotal,
		  col.AddedPointsTotal as ColumnAddedPointsTotal,
           
            task.TaskId,
            task.TaskName,
            task.Comments,
		   task.PointsTotal,
		   task.AddedPointsTotal,
		   task.StartTime as taskStartTime,
		   task.EndTime as taskEndTime,
		   task.ExpectedEndTime as taskExpectedEndTime,
		   task.TaskDone,
		   task.TaskDeleted,
		   task.TaskArchived,
		   task.ExtensionReason,
		   task.AddedReason,
            
          
            time.TimeLogId,
            time.StartTime as timeStart,
            time.EndTime as timeEnd,
            time.TotalTime as timeTotal,
		   time.Billable,
		   time.Archived as timeArch,

			  
               u.UserId,
               u.UserName,
               u.Role,
			   u.Email,
			   u.Archived,
               taskUsers.UserId as taskUserId,
			   ut.TaskId as linkUserTaskId,
               taskUsers.UserName as taskUserName,
               taskUsers.Role as taskRole,
			   taskUsers.Email as taskEmail,
			   taskusers.Archived as taskArch,
			   ttl.TaskId as linkTimelogTaskId,
			   timelogUsers.UserName as timelogUserName,
			   timelogUsers.Role as timelogRole

			   

        INTO #tempTable
        
        FROM   [dbo].[Users] as u
		LEFT JOIN [dbo].[ProjectUsers] as pu
                   on pu.UserId = @userId
		LEFT JOIN [dbo].[Projects] as p
					on p.ProjectId = pu.ProjectId
		LEFT JOIN [dbo].[ProjectColumns] as pc
                   on pc.ProjectId = pu.ProjectId
		LEFT JOIN [dbo].[Users] as projectUsers
                   on projectUsers.UserId = pu.UserId
		LEFT JOIN [dbo].[Columns] as col
                   on col.ColumnId = pc.ColumnId
		LEFT JOIN [dbo].[ColumnTasks] as ct
                     on ct.ColumnId = col.ColumnId
		LEFT JOIN [dbo].[Tasks] as task
                     on ct.TaskId = task.TaskId
		LEFT JOIN [dbo].[UserTasks] as ut
                     on   task.TaskId = ut.TaskId
		LEFT JOIN [dbo].[Users] as taskUsers
                     on taskUsers.UserId = ut.UserId
		LEFT JOIN [dbo].[TasksTimeLogs] as ttl
                     on ttl.TaskId = task.TaskId
		LEFT JOIN [dbo].[TimeLogs] as time
                      on ttl.TimeLogId=time.TimeLogId
		LEFT JOIN [dbo].UserTimeLogs as utl
					  on utl.TimeLogId = ttl.TimeLogId
		LEFT JOIN [dbo].[Users] as timelogUsers
                     on utl.UserId = timelogUsers.UserId

		




        WHERE  u.UserId = @UserId 
        ORDER  BY pu.[ProjectId] 

		
  
		
		SELECT distinct TimeLogId, timeStart as StartTime, timeEnd as EndTime, timeTotal as TotalTime,Billable,timeArch,linkTimelogTaskId, timelogUserName, timelogRole FROM #tempTable
        SELECT distinct UserId, UserName, Role, Email, Archived FROM #tempTable
        SELECT distinct taskUserId as UserId, taskUserName as UserName, linkUserTaskId, taskEmail, taskArch, taskRole as Role FROM #tempTable
        SELECT distinct TaskId, TaskName, Comments,PointsTotal as Points,AddedPointsTotal as AddedPoints,taskStartTime as StartTime,taskEndTime as EndTime,taskExpectedEndTime as ExpectedEndTime,TaskDone,TaskDeleted,TaskArchived,ExtensionReason,AddedReason,timelogColId FROM #tempTable
        select distinct ColumnId,ColumnName, ColumnPointsTotal, ColumnAddedPointsTotal, ProjectId from #tempTable
        SELECT distinct ProjectId, ProjectName,ProjectStartTime,ProjectEndTime,ExpectedEndTime,ProjectPointsTotal,ProjectAddedPoints,ProjectComplete,ProjectArchived,TimeIncrement FROM #tempTable
       
        DROP TABLE #tempTable";
        public static string GetAdminProjects = @"        
         SELECT 
			   p.ProjectId,
               p.ProjectName,
			   p.StartTime as ProjectStartTime,
			   p.EndTime as ProjectEndTime,
			   p.ExpectedEndTime,
			   p.PointsTotal as ProjectPointsTotal,
			   p.AddedPointsTotal as ProjectAddedPoints,
			   p.ProjectComplete,
			   p.ProjectArchived,
			   p.TimeIncrement,
           
		  pc.ColumnId as pcCol,
         ct.ColumnId as timelogColId,
         ct.TaskId as linkTaskId,
           col.ColumnId,
           col.ColumnName,
		  col.PointsTotal as ColumnPointsTotal,
		  col.AddedPointsTotal as ColumnAddedPointsTotal,
           
            task.TaskId,
            task.TaskName,
            task.Comments,
		   task.PointsTotal,
		   task.AddedPointsTotal,
		   task.StartTime as taskStartTime,
		   task.EndTime as taskEndTime,
		   task.ExpectedEndTime as taskExpectedEndTime,
		   task.TaskDone,
		   task.TaskDeleted,
		   task.TaskArchived,
		   task.ExtensionReason,
		   task.AddedReason,
            
          
            time.TimeLogId,
            time.StartTime as timeStart,
            time.EndTime as timeEnd,
            time.TotalTime as timeTotal,
		   time.Billable,
		   time.Archived as timeArch,

			  
               u.UserId,
               u.UserName,
               u.Role,
			   u.Email,
			   u.Archived,
               taskUsers.UserId as taskUserId,
			   ut.TaskId as linkUserTaskId,
               taskUsers.UserName as taskUserName,
               taskUsers.Role as taskRole,
			   taskUsers.Email as taskEmail,
			   taskusers.Archived as taskArch,
			   ttl.TaskId as linkTimelogTaskId,
			   timelogUsers.UserName as timelogUserName,
			   timelogUsers.Role as timelogRole

			   

        INTO #tempTable
        
        FROM   [dbo].[Users] as u
		LEFT JOIN [dbo].[ProjectUsers] as pu
                   on pu.UserId = u.UserId
		LEFT JOIN [dbo].[Projects] as p
					on p.ProjectId = pu.ProjectId
		LEFT JOIN [dbo].[ProjectColumns] as pc
                   on pc.ProjectId = pu.ProjectId
		LEFT JOIN [dbo].[Users] as projectUsers
                   on projectUsers.UserId = pu.UserId
		LEFT JOIN [dbo].[Columns] as col
                   on col.ColumnId = pc.ColumnId
		LEFT JOIN [dbo].[ColumnTasks] as ct
                     on ct.ColumnId = col.ColumnId
		LEFT JOIN [dbo].[Tasks] as task
                     on ct.TaskId = task.TaskId
		LEFT JOIN [dbo].[UserTasks] as ut
                     on   task.TaskId = ut.TaskId
		LEFT JOIN [dbo].[Users] as taskUsers
                     on taskUsers.UserId = ut.UserId
		LEFT JOIN [dbo].[TasksTimeLogs] as ttl
                     on ttl.TaskId = task.TaskId
		LEFT JOIN [dbo].[TimeLogs] as time
                      on ttl.TimeLogId=time.TimeLogId
		LEFT JOIN [dbo].UserTimeLogs as utl
					  on utl.TimeLogId = ttl.TimeLogId
		LEFT JOIN [dbo].[Users] as timelogUsers
                     on utl.UserId = timelogUsers.UserId


        ORDER  BY pu.[ProjectId] 

		
  
		
		SELECT distinct TimeLogId, timeStart as StartTime, timeEnd as EndTime, timeTotal as TotalTime,Billable,timeArch,linkTimelogTaskId, timelogUserName, timelogRole FROM #tempTable
        SELECT distinct UserId, UserName, Role, Email, Archived FROM #tempTable
        SELECT distinct taskUserId as UserId, taskUserName as UserName, linkUserTaskId, taskEmail, taskArch, taskRole as Role FROM #tempTable
        SELECT distinct TaskId, TaskName, Comments,PointsTotal as Points,AddedPointsTotal as AddedPoints,taskStartTime as StartTime,taskEndTime as EndTime,taskExpectedEndTime as ExpectedEndTime,TaskDone,TaskDeleted,TaskArchived,ExtensionReason,AddedReason,timelogColId FROM #tempTable
        select distinct ColumnId,ColumnName, ColumnPointsTotal, ColumnAddedPointsTotal, ProjectId from #tempTable
        SELECT distinct ProjectId, ProjectName,ProjectStartTime,ProjectEndTime,ExpectedEndTime,ProjectPointsTotal,ProjectAddedPoints,ProjectComplete,ProjectArchived,TimeIncrement FROM #tempTable
       
        DROP TABLE #tempTable";

        public static string GetProject = @"
        --  DECLARE @ProjectId int = 1
        
         SELECT 
			   p.ProjectId,
               p.ProjectName,
			   p.StartTime,
			   p.EndTime,
			   p.ExpectedEndTime,
			   p.PointsTotal as ProjectPointsTotal,
			   p.AddedPointsTotal as ProjectAddedPoints,
			   p.ProjectComplete,
			   p.ProjectArchived,
			   p.TimeIncrement,
              
			  pc.ColumnId as pcCol,
       		  ct.ColumnId as timelogColId,
       		  ct.TaskId as linkTaskId,
              col.ColumnId,
              col.ColumnName,
			  col.PointsTotal as ColumnPointsTotal,
			  col.AddedPointsTotal as ColumnAddedPointsTotal,
              
               task.TaskId,
               task.TaskName,
               task.Comments,
			   task.PointsTotal,
			   task.AddedPointsTotal,
			   task.StartTime as taskStartTime,
			   task.EndTime as taskEndTime,
			   task.ExpectedEndTime as taskExpectedEndTime,
			   task.TaskDone,
			   task.TaskDeleted,
			   task.TaskArchived,
			   task.ExtensionReason,
			   task.AddedReason,
               
        	   ttl.TaskId as linkTimelogTaskId,
               time.TimeLogId,
               time.StartTime as timeStart,
               time.EndTime as timeEnd,
               time.TotalTime as timeTotal,
			   time.Billable,
			   time.Archived as timeArch,

			  
               projectUsers.UserId,
               projectUsers.UserName,
               projectUsers.Role,
			   projectUsers.Email,
			   projectUsers.Archived,

			   ut.TaskId as linkUserTaskId,
			   ut.UserId as Dauser,
			   taskUsers.UserId as taskUserId,
               taskUsers.UserName as taskUserName,
               taskUsers.Role as taskRole,
			   taskUsers.Email as taskEmail,
			   taskusers.Archived as taskArch

        INTO #tempTable
        
        FROM   [dbo].[Projects] as p
 
		LEFT JOIN [dbo].[ProjectColumns] as pc
                   on pc.ProjectId = @ProjectId
		LEFT JOIN [dbo].[ProjectUsers] as pu
                   on pu.ProjectId = @ProjectId
		LEFT JOIN [dbo].[Users] as projectUsers
                   on projectUsers.UserId = pu.UserId
		LEFT JOIN [dbo].[Columns] as col
                   on col.ColumnId = pc.ColumnId
		LEFT JOIN [dbo].[ColumnTasks] as ct
                     on ct.ColumnId = col.ColumnId
		LEFT JOIN [dbo].[Tasks] as task
                     on ct.TaskId = task.TaskId
		LEFT JOIN [dbo].[UserTasks] as ut
                     on   task.TaskId = ut.TaskId
		LEFT JOIN [dbo].[Users] as taskUsers
                     on taskUsers.UserId = ut.UserId
		LEFT JOIN [dbo].[TasksTimeLogs] as ttl
                     on ttl.TaskId = task.TaskId
		LEFT JOIN [dbo].[TimeLogs] as time
                      on ttl.TimeLogId=time.TimeLogId
		




        WHERE  p.[ProjectId] = @ProjectId
        ORDER  BY p.[ProjectId] 

		
  
		--SELECT DISTINCT * FROM #tempTable where #temptable.ProjectId = @ProjectId
		SELECT distinct TimeLogId, timeStart, timeEnd, timeTotal,Billable,timeArch,linkTimelogTaskId FROM #tempTable
        SELECT distinct UserId, UserName, Role, Email, Archived FROM #tempTable
        SELECT distinct taskUserId as UserId, taskUserName as UserName, taskRole, taskEmail, taskArch as Role, linkUserTaskId FROM #tempTable
        SELECT distinct TaskId, TaskName, Comments,PointsTotal,AddedPointsTotal,taskStartTime,taskEndTime,taskExpectedEndTime,TaskDone,TaskDeleted,TaskArchived,ExtensionReason,AddedReason,timelogColId FROM #tempTable
        select distinct ColumnId,ColumnName, ColumnPointsTotal, ColumnAddedPointsTotal from #tempTable
        SELECT distinct ProjectId, ProjectName,EndTime,ExpectedEndTime,ProjectPointsTotal,ProjectAddedPoints,ProjectComplete,ProjectArchived,TimeIncrement FROM #tempTable
       
        DROP TABLE #tempTable
        ";

        public static string UpdateProject = @"
        UPDATE [dbo].[Projects]
        SET [ProjectName] = @ProjectName
        , [StartTime] = @ProjectStartTime
        , [EndTime] = @ProjectEndTime
        , [ExpectedEndTime] = @ExpectedEndTime
        , [PointsTotal] = @PointsTotal
        , [AddedPointsTotal] = @AddedPoints
        , [ProjectComplete] = @ProjectComplete
        , [ProjectArchived] = @ProjectArchived
        , [TimeIncrement] = @TimeIncrement
        WHERE ProjectId = @ProjectId
        INSERT INTO [dbo].[UserTimeLogs](UserId, TimeLogId)
        OUTPUT INSERTED.UTLID
        SELECT @UserId, Id
        FROM @generated_timelog_key
        ";


        public static string CreateColumn = @"
        DECLARE @generated_column_key int;
        INSERT INTO [dbo].[Columns](ColumnName,PointsTotal,AddedPointsTotal)
        OUTPUT INSERTED.ColumnId
        VALUES(@ColumnName,@PointsTotal,@AddedPointsTotal)
        SET @generated_column_key = @@IDENTITY  
        INSERT INTO [dbo].[ProjectColumns](ProjectId, ColumnId)
        SELECT @ProjectId, @generated_column_key
        
        ";
        public static string GetUsers = @"
     SELECT TOP (1000) [UserId]
      ,[UserName]
      ,[Role]
      ,[Email]
      ,[Archived]
      ,[Password]
      ,[AccessToken]
  FROM [dbo].[Users]
        ";
        public static string UpdateColumn = @"
        UPDATE [dbo].[Columns]
        SET[ColumnName] = @ColumnName,
         [PointsTotal] = @PointsTotal,
         [AddedPointsTotal] = @AddedPointsTotal
        WHERE ColumnId = @ColumnId
        ";
        public static string CreateTask = @"
        DECLARE @generated_task_key int;
        INSERT INTO [dbo].[Tasks](TaskName, Comments, PointsTotal, AddedPointsTotal, StartTime, EndTime, ExpectedEndTime, TaskDone, TaskDeleted, TaskArchived, AddedReason, ExtensionReason)
        OUTPUT INSERTED.TaskId
        VALUES(@TaskName, @Comments, @PointsTotal, @AddedPointsTotal, @StartTime, @EndTime, @ExpectedEndTime, @TaskDone, @TaskDeleted, @TaskArchived, @AddedReason, @ExtensionReason)
        SET @generated_task_key = @@IDENTITY  
        INSERT INTO [dbo].[ColumnTasks](ColumnId, TaskId)
        SELECT @ColumnId, @generated_task_key
        ";
        public static string UpdateTask = @"
        UPDATE [dbo].[Tasks]
        SET [TaskName] = @TaskName
        ,[Comments] = @Comments
        ,[PointsTotal] = @PointsTotal
        ,[AddedPointsTotal] = @AddedPointsTotal
        ,[StartTime] = @StartTime
        ,[EndTime] = @EndTime
        ,[ExpectedEndTime] = @ExpectedEndTime
        ,[TaskDone] = @TaskDone
        ,[TaskDeleted] = @TaskDeleted
        ,[TaskArchived] = @TaskArchived
        ,[ExtensionReason] = @ExtensionReason
        ,[AddedReason] = @AddedReason
        WHERE TaskId = @TaskId
        ";
        public static string CreateUser = @"
        DECLARE @generated_user_key int;
        INSERT INTO [dbo].[Users](UserName, Role, Email, Password, AccessToken, Archived)
        OUTPUT INSERTED.UserId
        VALUES(@UserName, @Role, @Email,@Password, @AccessToken, @Archived)
        ";
        public static string UpdateUser = @"
      UPDATE [dbo].[Users]
        SET UserName = @UserName
        ,Role = @Role
        ,Email = @Email
        ,Password = @Password
        ,AccessToken = @AccessToken
        WHERE UserId = @UserId
        ";
        public static string CheckUser = @"
       SELECT [UserId]
      ,[UserName]
      ,[Role]
      ,[Email]
      ,[Archived]
      ,[Password]
      ,[AccessToken]
      FROM [dbo].[Users]
      WHERE [Email] = @Email
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
        public static string SetColumnTask = @"
        DECLARE @generated_task_key int;
        DECLARE @generated_column_key int;
        DELETE FROM [dbo].[ColumnTasks]
		WHERE TaskId = @TaskId 
        INSERT INTO [dbo].[ColumnTasks](ColumnId, TaskId)
        OUTPUT INSERTED.CTID
        VALUES(@ColumnId, @TaskId)

        ";
        public static string CreateTimeLog = @"
  		DECLARE @generated_timelog_key as TABLE(Id INT)

        INSERT INTO [dbo].[TimeLogs](StartTime, EndTime, TotalTime,Billable,Archived)
        OUTPUT INSERTED.TimeLogId INTO @generated_timelog_key
        SELECT StartTime, EndTime, TotalTime, Billable, Archived
        FROM @TimeLogs
     
        INSERT INTO [dbo].[UserTimeLogs](UserId, TimeLogId)
        OUTPUT INSERTED.UTLID
        SELECT @UserId, Id
        FROM @generated_timelog_key

		INSERT INTO [dbo].TasksTimeLogs(TaskId, TimeLogId)
		SELECT @TaskId, Id
        FROM @generated_timelog_key
        ";
        public static string UpdateTimeLog = @"
        UPDATE [dbo].[Timelogs]
        SET StartTime = @StartTime
        ,EndTime = @EndTime
        ,TotalTime = @TotalTime
        ,Billable = @Billable
        ,Archived = @Archived
        WHERE TimeLogId = @TimeLogId
        INSERT INTO [dbo].[UserTimeLogs](UserId, TimeLogId)
        OUTPUT INSERTED.UTLID
        SELECT @UserId, Id
        FROM @generated_timelog_key
        ";

        public static string MoveTask = @"UPDATE [dbo].[ColumnTasks]
   SET [ColumnId] = @ColumnId
      ,[TaskId] = @TaskId
 WHERE TaskId = @TaskId";

        public static string GetUserTasks = @"SELECT
		tasks.[TaskId]
      ,tasks.[TaskName]
      ,tasks.[Comments]
      ,tasks.[PointsTotal]
      ,tasks.[AddedPointsTotal]
      ,tasks.[StartTime]
      ,tasks.[EndTime]
      ,tasks.[ExpectedEndTime]
      ,tasks.[TaskDone]
      ,tasks.[TaskDeleted]
      ,tasks.[TaskArchived]
      ,tasks.[ExtensionReason]
      ,tasks.[AddedReason]
      ,tasks.[TrelloTaskId]
	  ,ut.UserId
	  INTO #tempTable
  FROM [dbo].[UserTasks]as ut
   LEFT JOIN [dbo].[Tasks] as tasks on tasks.TaskId = ut.TaskId

  select * from #tempTable
  where #tempTable.UserId = @UserId

   DROP TABLE #tempTable";
    }
}
