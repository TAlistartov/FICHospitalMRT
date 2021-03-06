USE [Hospital]
GO
/****** Object:  StoredProcedure [dbo].[UpdateSelectedLaborant]    Script Date: 27.01.2018 18:11:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Batch submitted through debugger: SQLQuery7.sql|7|0|C:\Users\Pavlog_3\AppData\Local\Temp\~vs1C9A.sql
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[UpdateSelectedLaborant]
(
	@laborantId int,
	@laborantLastName nvarchar(50),
	@laborantFirstName nvarchar(50),
	@laborantUsed bit=null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;
Update [Laborant] set LastName=@laborantLastName, FirstName=@laborantFirstName, UsedOrNot=@laborantUsed  Where Id=@laborantId
END
