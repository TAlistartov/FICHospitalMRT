USE [Hospital]
GO
/****** Object:  StoredProcedure [dbo].[AddNewStudy]    Script Date: 30.04.2017 14:29:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AddNewStudy]
	(
		@Type nvarchar (50),
		@Cost numeric(6,2),
		@UsedOrNot bit
	)
AS

BEGIN

	SET NOCOUNT OFF;
	
	INSERT INTO [Study] ([Study].[Type],[Study].[Cost],[Study].[UsedOrNot]) VALUES (@Type,@Cost,@UsedOrNot)

END