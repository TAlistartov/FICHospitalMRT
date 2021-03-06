USE [Hospital]
GO
/****** Object:  StoredProcedure [dbo].[UsingOrUnusingLaborant]    Script Date: 27.01.2018 18:14:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[UsingOrUnusingLaborant]
	(
		@laborantId int,
		@laborantUsedOrUnused bit=null
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT off;

    Update [Laborant] SET UsedOrNot=@laborantUsedOrUnused WHERE [Laborant].[Id]=@laborantId
END
