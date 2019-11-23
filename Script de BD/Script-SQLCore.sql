USE [CoreSQLServer]
GO
/****** Object:  Table [dbo].[User]    Script Date: 23/11/2019 8:03:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[user_nick] [varchar](250) NOT NULL,
	[user_password] [varchar](256) NOT NULL,
	[user_name] [varchar](250) NOT NULL,
	[user_createDate] [date] NOT NULL,
	[user_delete] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[USER.Get]    Script Date: 23/11/2019 8:03:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<RGAV>
-- Create date: <2019-11-22>
-- Description:	<Procedimiento para autenticar usuario>
-- =============================================
CREATE PROCEDURE [dbo].[USER.Get] 
	@Nick varchar(250),
	@Password varchar(256)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT [user_id] AS Id,[user_name] AS Name, user_createDate AS CreateDate
	FROM dbo.[User]
	WHERE user_nick = @Nick ANd user_password = @Password AND user_delete = 0
END
GO
/****** Object:  StoredProcedure [dbo].[USER.Insert]    Script Date: 23/11/2019 8:03:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:		<RGAV>
-- Create date: <2019-11-22>
-- Description:	<Procedimiento para insertar un nuevo usuario>
-- ===============================================================
CREATE PROCEDURE [dbo].[USER.Insert] 
	@Nick varchar(250),
	@Password varchar(256),
	@Name varchar(250),
	@Id int output
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[User]
           ([user_nick]
           ,[user_password]
           ,[user_name]
           ,[user_createDate]
           ,[user_delete])
     VALUES
           (@Nick
           ,@Password
           ,@Name
           ,GETDATE()
           ,0)

	SELECT @Id = SCOPE_IDENTITY();
  
END
GO
