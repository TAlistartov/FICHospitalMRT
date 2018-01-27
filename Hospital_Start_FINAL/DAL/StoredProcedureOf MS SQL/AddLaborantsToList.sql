USE [Hospital]
GO
/****** Object:  StoredProcedure [dbo].[AddLaborantsToList]    Script Date: 17.12.2016 12:03:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Alistratov,,Taras>
-- Create date: <23.07.2016,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[AddLaborantsToList] 
	(
		@Last_Name nvarchar(50),
		@First_Name nvarchar(50),
		@laborantUsed bit=null
	)
AS
BEGIN
	SET NOCOUNT OFF;
		-- Added a new Laborant
	INSERT INTO [Laborant] ([Laborant].[LastName], [Laborant].[FirstName], [Laborant].[UsedOrNot]) VALUES (@Last_Name,@First_Name,@laborantUsed)
	
END
