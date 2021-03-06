USE [Hospital]
GO
/****** Object:  StoredProcedure [dbo].[UpdateSelectedStudy]    Script Date: 30.04.2017 14:31:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[UpdateSelectedStudy]
(
	@studyId int,
	@studyType nvarchar(50),
	@studyCost numeric(6,2),
	@studyUsedOrNot bit
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	Update [Study] set Type=@studyType, Cost=@studyCost, UsedOrNot=@studyUsedOrNot  Where Id=@studyId
END
